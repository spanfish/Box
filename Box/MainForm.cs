using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Box
{
    public partial class MainForm : Form, INotifyPropertyChanged
    {
        private readonly SynchronizationContext synchronizationContext;

        public event PropertyChangedEventHandler PropertyChanged;

        private RestClient client;

        private AppConfig AppConfig
        {
            get;
            set;
        }
        
        private BoxInfo BoxInfo
        {
            get;
            set;
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
            PreviewLabel.Image = ImageToConvert;
        }

        public MainForm()
        {
            InitializeComponent();

            //线程同步
            synchronizationContext = SynchronizationContext.Current;
            AppConfig = new AppConfig(synchronizationContext);
            string host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            client = new RestClient(host);

            PrintLabelButton.Enabled = false;
            InitializePrintPreview();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Login();
        }

        private void Login()
        {
            LoginWindow LW = new LoginWindow(AppConfig);
            LW.ShowDialog(this);
        }

        private void ClearBoxInfo()
        {
            BoxTypeTextBox.Text = "";
            BoxSNTextBox.Text = "";
            BoxCapacityTextBox.Text = "";
            BoxRealCountTextBox.Text = "";
            BoxCreateTimeTextBox.Text = "";
            BoxStatusTextBox.Text = "";
        }

        private void SearchBox()
        {
            if(BoxSNSearchTextBox.Text == null || BoxSNSearchTextBox.Text.Trim().Length == 0)
            {
                ShowError("请输入箱号");
                return;
            }

            SearchBoxButton.Enabled = false;
            BoxSNSearchTextBox.Enabled = false;
            PrintLabelButton.Enabled = false;
            try
            {
                var request = new RestRequest("dlicense/v2/manu/boxing/queryboxinfo", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("reqUserId", AppConfig.Login.Userid);
                request.AddHeader("reqUserSession", AppConfig.Login.Loginsession);
                request.AddHeader("grouptype", AppConfig.Account.Grouptype);
                request.AddHeader("OemfactoryId", AppConfig.Account.OemfactoryId);


                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"boxsn\":");
                sb.Append("\"").Append(BoxSNSearchTextBox.Text).Append("\"");
                sb.Append("}");

                string body = sb.ToString();

                request.AddParameter("application/json", body, ParameterType.RequestBody);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var asyncHandle = client.ExecuteAsync<BoxSearchResponse>(request, response =>
                {
                    //InitializeControls();

                    if (!response.IsSuccessful)
                    {
                        ShowError(response.ErrorMessage);
                        return;
                    }
                    BoxSearchResponse res = response.Data;

                    if (res != null && res.Status == 0)
                    {
                        if(res.RetData == null)
                        {
                            ShowError(null);
                            return;
                        }
                        if(res.RetData.BoxInfo == null)
                        {
                            ShowError("未找到箱子，请确认箱号是否正确");
                            return;
                        }

                        BoxInfo = res.RetData.BoxInfo;
                        ShowBox(res.RetData.BoxInfo);
                        SearchOrderInfo(BoxInfo.OrderId);
                    }
                    else
                    {
                        ShowError(res == null ? null : res.Msg);
                    }
                });
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void SearchOrderInfo(string orderId)
        {
            try
            {
                var request = new RestRequest("dlicense/v2/manu/order/info", Method.POST);
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
                var asyncHandle = client.ExecuteAsync<OrderResponse>(request, response =>
                {
                    //InitializeControls();

                    if (!response.IsSuccessful)
                    {
                        ShowError(response.ErrorMessage);
                        return;
                    }
                    OrderResponse res = response.Data;
                    if (res != null && res.Status == 0)
                    {
                        ZebraPreview(BoxInfo, res.OrderInfo);
                    }
                    else
                    {
                        ShowError(res == null ? null : res.Msg);
                    }
                });
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ShowBox(BoxInfo box)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                BoxInfo b = o as BoxInfo;

                BoxTypeTextBox.Text = b.BoxType;
                BoxSNTextBox.Text = b.BoxSN;
                BoxCapacityTextBox.Text = string.Format("{0}", b.Capacity);
                BoxRealCountTextBox.Text = string.Format("{0}", b.RealCount);
                BoxCreateTimeTextBox.Text = b.CreateTime;
                BoxStatusTextBox.Text = b.Status;

            }), box);
        }

        private void SearchBoxButton_Click(object sender, EventArgs e)
        {
            SearchBox();
        }

        private void ShowError(string errMsg)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                InitializeControls();

                string msg = o as string;
                if (msg == null)
                {
                    msg = "未知错误，请重试";
                }

                MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }), errMsg);
        }
        private void InitializeControls()
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                SearchBoxButton.Enabled = true;
                BoxSNSearchTextBox.Enabled = true;
                PrintLabelButton.Enabled = false;
            }), null);
        }

        private void BoxSNSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SearchBox();
            }
        }

        private void ZebraPreview(BoxInfo box, OrderInfo oi)
        {
            int marginTop = 15;
            int marginLeft = 10;
            int lineMargin = 16;

            Font titleFont = new Font("楷体", 16, FontStyle.Bold, GraphicsUnit.Point);
            Font headFont = new Font("楷体", 20, FontStyle.Bold, GraphicsUnit.Point);
            Font fieldFont = new Font("楷体", 16, FontStyle.Regular, GraphicsUnit.Point);

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
            g.DrawString(oi.AgreementId == null ? "" : box.OrderId, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;
            int oneRow = strSize.Height;

            title = "工单号码：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("20181113182229-242", fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "古北产品编码：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString(oi.MaterialCode == null ? "" : oi.MaterialCode, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "客户产品编码：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("208000001281", fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;


            title = "产品型号：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString(oi.ProductModel == null ? "" : oi.ProductModel, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            
            title = "产品描述：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);


            //while (strSize.Height > oneRow * 2)
            //{
            //    float fontSize = fieldFont.Size - 1;
            //    if(fontSize < 8)
            //    {
            //        break;
            //    }
            //}

            g.DrawString(oi.ProductDesc == null ? "" : oi.ProductDesc,
                fieldFont, textBrush, new RectangleF(marginLeft + strSize.Width, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height * 2;
            top += lineMargin;

            title = "装箱数量：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString(string.Format("{0} PCS", box.RealCount), fieldFont, textBrush, new RectangleF(fieldLeft, top + 10, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;

            title = "箱号：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.CODE_128;
            barcodeWriter.Options.Height = 80;
            barcodeWriter.Options.Width = fieldWidth;
            Bitmap barcode = barcodeWriter.Write(box.BoxSN == null ? "" : box.BoxSN);

            g.DrawImage(barcode, fieldLeft, top);
            top += strSize.Height * 2 + lineMargin + lineMargin;
            top += lineMargin;

            title = "供应商：";
            g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
            strSize = TextRenderer.MeasureText(g, title, titleFont);
            g.DrawString("杭州古北电子科技有限公司", fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
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

            g.DrawString(DateTime.Now.ToString("yyyy.MM.dd"), fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
            top += strSize.Height;
            top += lineMargin;


            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                SearchBoxButton.Enabled = true;
                BoxSNSearchTextBox.Enabled = true;
                PrintLabelButton.Enabled = true;

                PreviewLabel.Image = ImageToConvert;
            }), null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BoxInfo box = new BoxInfo();
            box.BoxSN = "OB420181122000008";
            
            OrderInfo oi = new OrderInfo();
            oi.ProductDesc = "Opple_Control box_WiFi_BL_7682_17P_stamp_PCBA_antenna_V1.2模块(欧普WIFI控制盒208000001281)";
            ZebraPreview(box, oi);
        }

        private void PrintLabelButton_Click(object sender, EventArgs e)
        {
            LPPrint printer = null;
            try
            {
                string zplCmd = Convert.BitmapToZPLII(ImageToConvert, 0, 0);

                StringBuilder sb = new StringBuilder();
                sb.Append("^XA^LH0,0~SD20^PR3,3,3^XZ");
                sb.Append("^XA");
                sb.Append("^PW799");
                sb.Append("^LL0559");
                sb.Append(zplCmd);
                sb.Append("^PQ1,0,1,Y^PS^XZ");

                printer = new LPPrint();

                bool o = printer.Open();
                
                if (o)
                {
                    bool ret = printer.Write(sb.ToString());
                    if(!ret)
                    {
                        MessageBox.Show("无法向打印机发生命令", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("无法连接打印机", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                if(printer != null && printer.IsOpened)
                {
                    printer.Close();
                }
            }
            
        }
    }
}
