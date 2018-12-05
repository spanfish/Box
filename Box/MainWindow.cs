using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using RestSharp;
using System.Reflection;
using ZXing;

namespace Box
{
    public partial class MainWindow : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region 全局变量，存放登陆信息
        public AppConfig AppConfig;
        #endregion

        #region 当前订单
        private Order _ActiveOrder;
        private Order ActiveOrder
        {
            get
            {
                return _ActiveOrder;
            }
            set
            {
                _ActiveOrder = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ActiveOrder"));
                }
            }
        }
        #endregion

        #region 查询中
        private bool executing;

        public bool Executing
        {
            get
            {
                return executing;
            }
            set
            {
                executing = value;
                this.progressBar.Visible = executing;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Executing"));
                }
            }
        }
        #endregion

        #region 状态栏消息
        private string msg;

        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
                this.msgStatusLabel.Text = msg;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Msg"));
                }
            }
        }
        #endregion
        
        #region 打印机
        private LPPrint Printer
        {
            get;
            set;
        }
        #endregion

        #region 本地变量

        private RestClient client;

        //当前订单显示页
        private int PageIndex;

        private readonly SynchronizationContext synchronizationContext;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //线程同步
            synchronizationContext = SynchronizationContext.Current;
            //全局变量初始化
            InitializeAppConfig();
            //状态栏初始化
            InitializeStatus();
            //列表初始化
            InitializeListView();

            //RestFull API初始化
            InitializeRest();
            
            //打印机初始化
            InitializePrint();

            InitializeDataBinding();

            InitializePrintPreview();

            PageIndex = 0;
        }

        private int WidthInPixel
        {
            get;
            set;
        }

        private int HeightInPixel
        {
            get;
            set;
        }

        private Bitmap ImageToConvert
        {
            get;
            set;
        }
        private void InitializePrintPreview()
        {
            WidthInPixel = 100 * 200 * 10 / 254;
            HeightInPixel = 70 * 200 * 10 / 254;
            ImageToConvert = new Bitmap(WidthInPixel, HeightInPixel);
        }

        private void InitializeRest()
        {
            string host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            client = new RestClient(host);
        }

        #region 全局变量初始化
        private void InitializeAppConfig()
        {
            this.AppConfig = new AppConfig(synchronizationContext);
        }
        #endregion

        private void InitializeDataBinding()
        {
            this.ActiveOrder = new Order();
            this.nextPageButton.DataBindings.Add(new Binding("Enabled", this.AppConfig, "IsLoginSuccess", false, DataSourceUpdateMode.OnPropertyChanged));
            this.prevPageButton.DataBindings.Add(new Binding("Enabled", this.AppConfig, "IsLoginSuccess", false, DataSourceUpdateMode.OnPropertyChanged));
            this.orderRefreshButton.DataBindings.Add(new Binding("Enabled", this.AppConfig, "IsLoginSuccess", false, DataSourceUpdateMode.OnPropertyChanged));
            //orderIdLabel
            this.orderIdLabel.DataBindings.Add(new Binding("Text", this.ActiveOrder, "OrderId", false, DataSourceUpdateMode.OnPropertyChanged));
            //prodNameLabel
            this.prodNameLabel.DataBindings.Add(new Binding("Text", this.ActiveOrder, "ProductName", false, DataSourceUpdateMode.OnPropertyChanged));
        }

        #region 状态栏初始化
        private void InitializeStatus()
        {
            this.Executing = false;
            this.Msg = "Ready";
        }
        #endregion

        #region 打印机初始化
        private void InitializePrint()
        {
            this.Printer = new LPPrint();
            this.Printer.OpenAsync(
                (bool opened, string printName)=> 
                {
                    if(opened)
                    {
                        this.printStatusLabel.Text = string.Format("打印机已连接:{0}", printName);
                    }
                    else
                    {
                        this.printStatusLabel.Text = string.Format("未检测到打印机:{0}", printName);
                    }
                }
            );
            Console.WriteLine("");
        }
        #endregion

        private void InitializeListView()
        {
            this.dataGridView1.AutoGenerateColumns = false;
          
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //Login();
        }

        private void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            LoginWindow loginWindow = new LoginWindow(this.AppConfig);
            loginWindow.ShowDialog();
        }
        
        private void ListOrder(int index)
        {
            try
            {
                var request = new RestRequest("dlicense/v2/manu/query", Method.POST);

                request.RequestFormat = DataFormat.Json;
                
                request.AddHeader("reqUserId", AppConfig.Login.Userid);
                request.AddHeader("reqUserSession", AppConfig.Login.Loginsession);
                request.AddHeader("grouptype", AppConfig.Account.Grouptype);
                request.AddHeader("OemfactoryId", AppConfig.Account.OemfactoryId);


                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"index\":");
                sb.Append(string.Format("{0},", index));
                //sb.Append("0,");

                sb.Append("\"pagesize\":");
                sb.Append("30,");

                sb.Append("\"sortby\":");
                sb.Append("\"createtime\",");

                sb.Append("\"sortorder\":");
                sb.Append("\"desc\",");

                sb.Append("\"checkstatus\":");
                sb.Append("-1,");

                sb.Append("\"producestatus\":");
                sb.Append("-1");

                sb.Append("}");

                string body = sb.ToString();
                Console.WriteLine(body);

                request.AddParameter("application/json", body, ParameterType.RequestBody);

                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var asyncHandle = client.ExecuteAsync<OrderResponse>(request, response => {
                    string ErrorMessage = response.ErrorMessage;
                    if(response.IsSuccessful)
                    {
                        OrderResponse orderRes = response.Data;
                        if(orderRes != null)
                        {
                            synchronizationContext.Post(new SendOrPostCallback(o =>
                            {
                                this.PageIndex = index;
                                List<Order> list = o as List<Order>;
                                if(list == null)
                                {
                                    list = new List<Order>();
                                }
                                var source = new BindingSource(list, null);
                                this.dataGridView1.DataSource = source;
                                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                            }), orderRes.Orderlist);
                        }
                        else
                        {
                            ErrorMessage = "无法转换为JSON";
                        }
                    }
                    
                });
            }
            catch
            {
                
            }
        }

        /**
         * 刷新订单
         **/
        private void OrderListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListOrder(0);
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Printer.Close();
        }

        //打印测试
        private void printerTestMenu_Click(object sender, EventArgs e)
        {
            if (!this.Printer.IsOpened)
            {
                this.Printer.OpenAsync((bool opened, string printName) =>
                    {
                        if (opened)
                        {
                            this.PrintTestLabel();
                        }
                        else
                        {
                            MessageBox.Show("未检测到打印机", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                );
            }
            else
            {
                this.PrintTestLabel();
            }
            
        }

        private void PrintTestLabel()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR2,2~SD10^JUS^LRN^CI0^XZ");
            sb.Append("^XA");
            sb.Append("^MMT");
            sb.Append("^PW799");
            sb.Append("^LL0559");
            sb.Append("^LS0");
            sb.Append("^FT308,58^A0N,39,38^FH\\^FD产品装箱单^FS");

            sb.Append("^FT47,94^A0N,31,31^FH\\^FD订单编号：^FS");
            sb.Append("^FT279,98^A0N,31,31^FH\\^FDGB-MK-18101204^FS");//订单编号

            sb.Append("^FT47,135^A0N,31,31^FH\\^FD工单编号：^FS");
            sb.Append("^FT272,135^A0N,31,31^FH\\^FD20181113182229-242^FS");//工单编号

            sb.Append("^FT47,175^A0N,31,31^FH\\^FD古北产品编码：^FS");
            sb.Append("^FT282,179^A0N,31,31^FH\\^FDGB.M0.051.002^FS");//古北产品编码

            sb.Append("^FT45,218^A0N,31,31^FH\\^FD客户产品编码：^FS");
            sb.Append("^FT312,220^A0N,31,31^FH\\^FD208000001281^FS");//客户产品编码

            sb.Append("^FT45,255^A0N,31,31^FH\\^FD产品型号：^FS");
            sb.Append("^FT287,259^A0N,31,31^FH\\^FDBL3328-1051002^FS");//产品型号

            sb.Append("^FT46,295^A0N,31,31^FH\\^FD产品描述：^FS");
            sb.Append("^FT199,299^A0N,25,24^FH\\^FDOpple_Controlbox_WiFi_BL_7682_17P_stamp_PCBA_Antenna_V1.2模块^FS");//产品描述

            sb.Append("^FT43,369^A0N,31,31^FH\\^FD装箱数量：^FS");
            sb.Append("^FT347,372^A0N,31,31^FH\\^FD3600 PCS^FS");//装箱数量

            sb.Append("^FT43,414^A0N,31,31^FH\\^FD箱号：^FS");
            sb.Append("^BY1,3,23^FT246,401^BCN,,Y,N");
            sb.Append("^FD>:OB420181122000000^FS");//箱号

            sb.Append("^FT44,463^A0N,31,31^FH\\^FD供应商：^FS");
            sb.Append("^FT240,466^A0N,31,31^FH\\^FD杭州古北电子科技有限公司^FS");

            sb.Append("^FT46,505^A0N,31,36^FH\\^FDQ  C:^FS");

            sb.Append("^FT44,543^A0N,31,31^FH\\^FD制造日期：^FS");
            sb.Append("^FT321,544^A0N,31,31^FH\\^FD2018.12.05^FS");
            
            sb.Append("^PQ1,0,1,Y^XZ");
            this.Printer.Write(sb.ToString());
        }

        private void orderRefreshButton_Click(object sender, EventArgs e)
        {
            ListOrder(0);
        }

        private void nextPageButton_Click(object sender, EventArgs e)
        {
            ListOrder(PageIndex + 1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Order activeOrder = dataGridView1.CurrentRow.DataBoundItem as Order;
            ActiveOrder.CopyProperties(activeOrder);
            LookupBoxForOrder(ActiveOrder.OrderId);
        }

        private void LookupBoxForOrder(string orderId)
        {
            try
            {
                var request = new RestRequest("dlicense/v2/manu/boxing/listboxbyworkform", Method.POST);

                request.RequestFormat = DataFormat.Json;

                request.AddHeader("reqUserId", AppConfig.Login.Userid);
                request.AddHeader("reqUserSession", AppConfig.Login.Loginsession);
                request.AddHeader("grouptype", AppConfig.Account.Grouptype);
                request.AddHeader("OemfactoryId", AppConfig.Account.OemfactoryId);


                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"orderid\":");
                sb.Append("\"").Append(orderId).Append("\"");
                sb.Append("}");

                string body = sb.ToString();
                
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var asyncHandle = client.ExecuteAsync<ListboxByWorkFormResponse>(request, response => {
                    string ErrorMessage = response.ErrorMessage;
                    if (response.IsSuccessful)
                    {
                        ListboxByWorkFormResponse res = response.Data;
                        if (res != null)
                        {
                            
                        }
                        else
                        {
                            ErrorMessage = "无法转换为JSON";
                        }
                    }

                });
            }
            catch
            {

            }
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.OrderId = "1234";
            this.ActiveOrder = order;
        }

        private void InnerBoxPrintMenuItem_Click(object sender, EventArgs e)
        {
            PrintWindow pw = new PrintWindow(this.ActiveOrder == null ? "" : this.ActiveOrder.OrderId, this.AppConfig);
            pw.ShowDialog();
        }

        private void prevPageButton_Click(object sender, EventArgs e)
        {
            ListOrder(PageIndex - 1);
        }

        private void OuterBoxPrintButton_Click(object sender, EventArgs e)
        {
            BoxInfo box = new BoxInfo();
            box.BoxSN = "OB420181122000008";
            ZebraPreview(box);
        }

        private void InnerBoxPrintButton_Click(object sender, EventArgs e)
        {
            BoxInfo box = new BoxInfo();
            box.BoxSN = "OB420181122000009";
            ZebraPreview(box);
        }

        private void ZebraPreview(BoxInfo box)
        { 
            int marginTop = 15;
            int marginLeft = 15;
            int lineMargin = 15;

            //pbPicture.Width = WidthInPixel;
            //pbPicture.Height = HeightInPixel;

            Font titleFont = new Font("SimSun", 12, FontStyle.Bold, GraphicsUnit.Point);
            Font headFont = new Font("SimSun", 16, FontStyle.Bold, GraphicsUnit.Point);
            Font fieldFont = new Font("SimSun", 16, FontStyle.Regular, GraphicsUnit.Point);

            StringFormat headFormat = new StringFormat();
            headFormat.Alignment = StringAlignment.Center;
            headFormat.LineAlignment = StringAlignment.Near;

            StringFormat titleFormat = new StringFormat();
            titleFormat.Alignment = StringAlignment.Near;
            titleFormat.LineAlignment = StringAlignment.Near;

            StringFormat fieldFormat = new StringFormat();
            fieldFormat.Alignment = StringAlignment.Center;
            fieldFormat.LineAlignment = StringAlignment.Near;

            SolidBrush textBrush = new SolidBrush(Color.Black);

            
           
            Graphics g = Graphics.FromImage(ImageToConvert);
            //background
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, WidthInPixel, HeightInPixel);

            int top = marginTop;
            int left = marginLeft;

            int titleWidth = TextRenderer.MeasureText(g, "古北产品编码：", titleFont).Width;
            int fieldWidth = WidthInPixel - titleWidth - marginLeft * 2;
            int fieldLeft = titleWidth + marginLeft;

            string title = "产品装箱单";
            g.DrawString(title, headFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), headFormat);
            Size strSize = TextRenderer.MeasureText(g, title, headFont);
            top += strSize.Height;
            top += lineMargin;


            title = "订单号码：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("GB-MK-18101204", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "工单号码：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("20181113182229-242", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "古北产品编码：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("GB.M.073.051.002", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "客户产品编码：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("208000001281", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "产品型号：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("BL3328-1051002", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "产品描述：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("Opple_Controlbox_WiFi_BL_7682_17P_stamp_PCBA_antenna_v1.2模块（欧普WIFI控制和208000001281）",
                headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height * 2;
            top += lineMargin;

            title = "装箱数量：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("36000 PCS", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "箱号：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.CODE_39;
            barcodeWriter.Options.Height = 50;
            barcodeWriter.Options.Width = fieldWidth;
            Bitmap barcode = barcodeWriter.Write(box.BoxSN);

            g.DrawImage(barcode, fieldLeft, top);
            top += strSize.Height * 2 + lineMargin;
            top += lineMargin;

            title = "供应商：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("杭州古北电子科技有限公司", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "Q   C：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            top += strSize.Height;
            top += lineMargin;

            title = "制造日期：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("2018.11.22", headFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            //pbPicture.Image = ImageToConvert;
                
        }
    }
}
