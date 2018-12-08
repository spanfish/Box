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
using System.Data.SQLite;
using System.IO;

namespace Box
{
    public partial class MainForm : Form
    {
        private static readonly string FilePath = @".\Default.db";

        private readonly SynchronizationContext synchronizationContext;

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

        private OrderInfo OrderInfo
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
        private int TopMargin
        {
            get;
            set;
        }
        private int LeftMargin
        {
            get;
            set;
        }
        private int LineSpace
        {
            get;
            set;
        }

        
        private void InitializePrintPreview()
        {
            WidthInPixel = 799;// 100 * 200 * 10 / 254;
            HeightInPixel = 559;// 70 * 200 * 10 / 254;
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

            if (!File.Exists(FilePath))
            {
                using (FileStream stream = File.Create(FilePath))
                {
                    if (null != stream)
                    {
                        stream.Close();
                    }

                    using (var conn = new SQLiteConnection("Data Source=" + FilePath))
                    {
                        try
                        {
                            conn.Open();
                            using (SQLiteCommand command = conn.CreateCommand())
                            {
                                command.CommandText = "create table Setting(Id TEXT, Name Text, DefValue TEXT, PRIMARY KEY (Id, Name))";
                                command.ExecuteNonQuery();

                                command.CommandText = "create table Print(Id TEXT, Name Text, DispSeq INTEGER, DispText Text, BarCode TEXT, PRIMARY KEY (Id, Name))";
                                command.ExecuteNonQuery();
                            }

                            conn.Close();
                        }
                        finally
                        {
                            if (conn != null)
                            {
                                try
                                {
                                    conn.Close();
                                }
                                finally
                                {

                                }
                            }
                        }
                    }
                }
            }


           

            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            string def = Properties.Settings.Default["HeadFontName"] as string;
            if(String.IsNullOrEmpty(def))
            {
                def = "楷体";
            }
            foreach (FontFamily ff in fonts.Families)
            {
                HeadFontList.Items.Add(ff.Name);
                TitleFontList.Items.Add(ff.Name);
                FieldFontList.Items.Add(ff.Name);

                if(ff.Name == def)
                {
                    HeadFontList.SelectedItem = def;
                    TitleFontList.SelectedItem = def;
                    FieldFontList.SelectedItem = def;
                }
            }
            def = Properties.Settings.Default["HeadFontSize"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "24";
            }
            HeadFontSizeList.SelectedItem = def;

            def = Properties.Settings.Default["TitleFontSize"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "20";
            }
            TitleFontSizeList.SelectedItem = def;

            def = Properties.Settings.Default["FieldFontSize"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "20";
            }
            FieldFontSizeList.SelectedItem = def;

            def = Properties.Settings.Default["HeadFontStyle"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "粗体";
            }
            HeadFontTypeList.SelectedItem = def;

            def = Properties.Settings.Default["TitleFontStyle"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "粗体";
            }
            TitleFontTypeList.SelectedItem = def;

            def = Properties.Settings.Default["FieldFontStyle"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "粗体";
            }
            FieldFontTypeList.SelectedItem = def;

            def = Properties.Settings.Default["LineSpace"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "0";
            }
            LineSpaceTB.Text = def;
            
            def = Properties.Settings.Default["LeftMargin"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "10";
            }
            LeftMarginTB.Text = def;

            def = Properties.Settings.Default["TopMargin"] as string;
            if (String.IsNullOrEmpty(def))
            {
                def = "15";
            }
            TopMarginTB.Text = def;

            TopMargin = Int32.Parse(TopMarginTB.Text);
            LeftMargin = Int32.Parse(LeftMarginTB.Text);
            LineSpace = Int32.Parse(LineSpaceTB.Text);
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
            ImageToConvert = new Bitmap(WidthInPixel, HeightInPixel);
            PreviewLabel.Image = ImageToConvert;
            if (BoxSNSearchTextBox.Text == null || BoxSNSearchTextBox.Text.Trim().Length == 0)
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
                //var request = new RestRequest("dlicense/v2/manu/order/info", Method.POST);
                var request = new RestRequest("dlicense/v2/manu/order/query", Method.POST);
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
                        if (String.IsNullOrEmpty(res.OrderInfo.Workform))
                        {
                            res.OrderInfo.Workform = GetDef(res.OrderInfo.OrderId, "Workform");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.MaterialCode))
                        {
                            res.OrderInfo.MaterialCode = GetDef(res.OrderInfo.OrderId, "GuBeiNo");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.KehuCode))
                        {
                            res.OrderInfo.KehuCode = GetDef(res.OrderInfo.OrderId, "KehuNo");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.ProductModel))
                        {
                            res.OrderInfo.ProductModel = GetDef(res.OrderInfo.OrderId, "ProdModel");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.ProductDesc))
                        {
                            res.OrderInfo.ProductDesc = GetDef(res.OrderInfo.OrderId, "ProdDesc");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.VendorName))
                        {
                            res.OrderInfo.VendorName = GetDef(res.OrderInfo.OrderId, "Supplier");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.VendorName))
                        {
                            res.OrderInfo.VendorName = "杭州古北电子科技有限公司";
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.BatchNo))
                        {
                            res.OrderInfo.BatchNo = GetDef(res.OrderInfo.OrderId, "BatchNo");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.ProductVerTag))
                        {
                            res.OrderInfo.ProductVerTag = GetDef(res.OrderInfo.OrderId, "Version");
                        }

                        if (String.IsNullOrEmpty(res.OrderInfo.Firmware))
                        {
                            res.OrderInfo.Firmware = GetDef(res.OrderInfo.OrderId, "Firmware");
                        }
                        OrderInfo = res.OrderInfo;

                        ShowOrderInfo(res.OrderInfo);
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

        private void ShowOrderInfo(OrderInfo orderInfo)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                OrderInfo oi = o as OrderInfo;

                OrderIdTB.Text = oi.AgreementId;
                WorkformTB.Text = oi.Workform;
                //古北产品编码
                GubeiNoTB.Text = oi.MaterialCode;
                //客户产品编码
                KehuNoTB.Text = oi.KehuCode;
                //产品型号
                ProdModelTB.Text = oi.ProductModel;
                //产品描述
                ProductDescTB.Text = oi.ProductDesc;
                //装箱数量
                //供应商
                SupplierTB.Text = orderInfo.VendorName;
                //批次
                BatchTB.Text = orderInfo.BatchNo;
                //Version
                VersionTB.Text = orderInfo.ProductVerTag;
                //固件
                FirmwareTB.Text = orderInfo.Firmware;
            }), orderInfo);
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

        private List<string> PrintItems
        {
            get;
            set;
        }

        private void GetPrintItem()
        {
            PrintItems = new List<string>();
            if (File.Exists(FilePath))
            {
                using (var conn = new SQLiteConnection("Data Source=" + FilePath))
                {
                    try
                    {
                        conn.Open();
                        //create table Print(Id TEXT, Name Text, BarCode TEXT, PRIMARY KEY (Id, Name))
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            command.CommandText = "SELECT  Name, DispSeq, DispText, BarCode from Print where Id=@Id and DispSeq > 0 order by DispSeq Asc";

                            command.Parameters.Add("@Id", DbType.String);

                            command.Parameters["@Id"].Value = OrderInfo.OrderId;

                            using (SQLiteDataReader dr = command.ExecuteReader())
                            {
                                while (dr != null && dr.Read())
                                {
                                    string Name = dr.GetString(0);
                                    string DispText = dr.GetString(2);
                                    string BarCode = dr.GetString(3);

                                    string tmp = string.Format("{0}\t{1}\t{2}", Name, DispText == null ? "" : DispText, BarCode == null ? "" : BarCode);

                                    PrintItems.Add(tmp);
                                }

                            }

                        }

                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("无法取得打印项目" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (conn != null)
                        {
                            try
                            {
                                conn.Close();
                            }
                            finally
                            {

                            }
                        }
                    }

                }
            }

        }

        private void ZebraPreview(BoxInfo box, OrderInfo oi)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
#if DEBUG
                //OrderInfo = new OrderInfo();
                //OrderInfo.OrderId = "00000000";
                //OrderInfo.AgreementId = "B-MK-18082201-2";
                //OrderInfo.ProductModel = "BL5015-HBSG011002";
                //OrderInfo.ProductName = "ProductName";
                //OrderInfo.ProductDesc = "Opple_Control_WiFi_MTK7698_3.3V_U. FL接口外置天线_stamp_PC BA_V1.1_new模块(欧普WIFI控制盒去谐波205100010281)";
                //OrderInfo.Workform = "20181105162108-221-1";
                //OrderInfo.FactoryName = "12345678901234560";
                //OrderInfo.MaterialCode = "12345678901234560";
                //OrderInfo.KehuCode = "12345678901234560";
                //OrderInfo.VendorName = "杭州古北电子科技有限公司";
                //OrderInfo.ProductVerTag = "12345678901234560";
                //OrderInfo.BatchNo = "12345678901234560";
                //OrderInfo.FactoryName = "12345678901234560";
                
                //BoxInfo = new BoxInfo();
                //BoxInfo.OemFactoryId = "12345678901234560";
                //BoxInfo.RealCount = 1000;
                //BoxInfo.OrderId = "12345678901234560";
                //BoxInfo.BoxSN = "IBC20181208000013";
#endif
                GetPrintItem();
                if (PrintItems == null || PrintItems.Count == 0)
                {
                    MessageBox.Show("无打印项目", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region
                if (!String.IsNullOrEmpty(LineSpaceTB.Text))
                {
                    LineSpace = Int32.Parse(LineSpaceTB.Text);
                }

                string headFontName = HeadFontList.SelectedItem as string;
                if (headFontName == null)
                {
                    headFontName = "楷体";
                }

                string titleFontName = TitleFontList.SelectedItem as string;
                if (titleFontName == null)
                {
                    titleFontName = "楷体";
                }

                string fieldFontName = FieldFontList.SelectedItem as string;
                if (fieldFontName == null)
                {
                    fieldFontName = "楷体";
                }

                int headFontSize = 20;
                string tmp = HeadFontSizeList.SelectedItem as string;
                if (tmp != null)
                {
                    headFontSize = Int32.Parse(tmp);
                }

                int titleFontSize = 16;
                tmp = TitleFontSizeList.SelectedItem as string;
                if (tmp != null)
                {
                    titleFontSize = Int32.Parse(tmp);
                }

                int fieldFontSize = 16;
                tmp = FieldFontSizeList.SelectedItem as string;
                if (tmp != null)
                {
                    fieldFontSize = Int32.Parse(tmp);
                }

                FontStyle headFs = FontStyle.Bold;
                string fontStyle = HeadFontTypeList.SelectedItem as string;
                if (fontStyle == "粗体")
                {
                    headFs = FontStyle.Bold;
                }
                else if (fontStyle == "斜体")
                {
                    headFs = FontStyle.Italic;
                }
                else if (fontStyle == "普通")
                {
                    headFs = FontStyle.Regular;
                }

                FontStyle titleFs = FontStyle.Bold;
                fontStyle = TitleFontTypeList.SelectedItem as string;
                if (fontStyle == "粗体")
                {
                    titleFs = FontStyle.Bold;
                }
                else if (fontStyle == "斜体")
                {
                    titleFs = FontStyle.Italic;
                }
                else if (fontStyle == "普通")
                {
                    titleFs = FontStyle.Regular;
                }

                FontStyle fieldFs = FontStyle.Bold;
                fontStyle = FieldFontTypeList.SelectedItem as string;
                if (fontStyle == "粗体")
                {
                    fieldFs = FontStyle.Bold;
                }
                else if (fontStyle == "斜体")
                {
                    fieldFs = FontStyle.Italic;
                }
                else if (fontStyle == "普通")
                {
                    fieldFs = FontStyle.Regular;
                }
                if(!String.IsNullOrEmpty(TopMarginTB.Text))
                {
                    TopMargin = Int32.Parse(TopMarginTB.Text);
                }
                if (!String.IsNullOrEmpty(LeftMarginTB.Text))
                {
                    LeftMargin = Int32.Parse(LeftMarginTB.Text);
                }

                Font headFont = new Font(headFontName, headFontSize, headFs, GraphicsUnit.Point);
                Font titleFont = new Font(titleFontName, titleFontSize, titleFs, GraphicsUnit.Point);
                Font fieldFont = new Font(fieldFontName, fieldFontSize, fieldFs, GraphicsUnit.Point);

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
                #endregion

                Graphics g = Graphics.FromImage(ImageToConvert);
                //background
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, WidthInPixel, HeightInPixel);

                int top = TopMargin;
                int left = LeftMargin;

                //打印标题
                string title = "产品装箱单";
                g.DrawString(title, headFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), headFormat);
                Size tmpSize = TextRenderer.MeasureText(g, title, headFont);
                top += tmpSize.Height;
                top += LineSpace;

                foreach (string pr in PrintItems)
                {
                    string[] tmpArr = pr.Split('\t');
                    string Name = tmpArr[0];
                    string Disp = tmpArr[1];
                    string IsBar = tmpArr[2];
                    //取得打印文字长度
                    string Text = "";
                    if (Name == "OrderCode")
                    {
                        //订单编码
                        Text = OrderInfo.AgreementId;
                    }
                    else if (Name == "WorkFlow")
                    {
                        //工单
                        Text = OrderInfo.Workform;
                    }
                    else if (Name == "Batch")
                    {
                        //批次号
                        Text = OrderInfo.BatchNo;
                    }
                    else if (Name == "GubeiNo")
                    {
                        //古北产品编码
                        Text = OrderInfo.MaterialCode;
                    }
                    else if (Name == "CustomerNo")
                    {
                        //客户产品编码
                        Text = OrderInfo.KehuCode;
                    }
                    else if (Name == "ProdModel")
                    {
                        //产品型号
                        Text = OrderInfo.ProductModel;
                    }
                    else if (Name == "ProdDesc")
                    {
                        //产品描述
                        Text = OrderInfo.ProductDesc;
                    }
                    else if (Name == "Ver")
                    {
                        //Ver
                        Text = OrderInfo.ProductVerTag;
                    }
                    else if (Name == "RealCount")
                    {
                        //装箱数量
                        Text = string.Format("{0} PCS", BoxInfo.RealCount);
                    }
                    else if (Name == "BoxNo")
                    {
                        //箱号
                        Text = BoxInfo.BoxSN;
                    }
                    else if (Name == "Supplier")
                    {
                        //供应商
                        Text = OrderInfo.VendorName;
                    }
                    else if (Name == "QC")
                    {
                        //QC
                        Text = "";
                    }
                    else if (Name == "ManufactureDate")
                    {
                        //制造日期
                        Text = DateTime.Now.ToString("yyyy.MM.dd");
                    }
                    else if (Name == "Firmware")
                    {
                        //固件
                        Text = OrderInfo.Firmware;
                    }
                    g.DrawString(Disp, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                    tmpSize = TextRenderer.MeasureText(g, Disp, titleFont);
                    int titleWidth = tmpSize.Width;
                    int titleHeight = tmpSize.Height;
                    //可用空间
                    int w = WidthInPixel - left * 2 - titleWidth * 2 - 2;
                    if(String.IsNullOrEmpty(Text))
                    {
                        top += tmpSize.Height;
                    }
                    else
                    {
                        if ("1" == IsBar)
                        {
                            var barcodeWriter = new BarcodeWriter();
                            barcodeWriter.Format = BarcodeFormat.CODE_128;
                            barcodeWriter.Options.Height = 80;
                            barcodeWriter.Options.Width = w;
                            Bitmap barcode = barcodeWriter.Write(Text);

                            g.DrawImage(barcode, left + tmpSize.Width, top);
                            top += barcodeWriter.Options.Height;
                        }
                        else
                        {
                            tmpSize = TextRenderer.MeasureText(g, Text, fieldFont);
                            if (tmpSize.Width <= w)
                            {
                                //居中
                                g.DrawString(Text, fieldFont, textBrush, new RectangleF(0, top, WidthInPixel, HeightInPixel), fieldFormat);
                                top += tmpSize.Height > titleHeight ? tmpSize.Height : titleHeight;
                            }
                            else
                            {
                                //打不下
                                SizeF tmpSizeF = g.MeasureString(Text, fieldFont, w + titleWidth);
                                g.DrawString(Text, fieldFont, textBrush, new RectangleF(left + titleWidth, top, w + titleWidth, HeightInPixel), fieldFormat);
                                int tmpInt = (int)Math.Ceiling(tmpSizeF.Height);
                                top += tmpInt > titleHeight ? tmpInt : titleHeight;
                            }
                            
                        }
                    }

                    top += LineSpace;
                }

                #region
                //int titleWidth = TextRenderer.MeasureText(g, "古北产品编码：", titleFont).Width;
                //int fieldWidth = WidthInPixel - titleWidth - marginLeft * 2;
                //int fieldLeft = titleWidth + marginLeft;

                //title = "订单号码：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //int rowHeight = strSize.Height;
                //g.DrawString(box.OrderId == null ? "" : box.OrderId, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;
                //int oneRow = strSize.Height;

                //title = "工单号码：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //g.DrawString(oi.Workform == null ? "" : oi.Workform, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;

                //title = "古北产品编码：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //g.DrawString(oi.MaterialCode == null ? "" : oi.MaterialCode, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;

                //title = "客户产品编码：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //g.DrawString(oi.KehuCode == null ? "" : oi.KehuCode, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;


                //title = "产品型号：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //g.DrawString(oi.ProductModel == null ? "" : oi.ProductModel, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;


                //title = "产品描述：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);


                //int descFontSize = fieldFontSize;
                //Font descFont = new Font(fieldFontName, descFontSize, fieldFs, GraphicsUnit.Point);
                //string prodDesc = oi.ProductDesc == null ? "" : oi.ProductDesc;
                //Size prodDescSize = TextRenderer.MeasureText(g, title, descFont);
                //while (prodDescSize.Height > rowHeight * 2)
                //{
                //    descFontSize--;
                //    descFont = new Font(fieldFontName, descFontSize, fieldFs, GraphicsUnit.Point);
                //    prodDescSize = TextRenderer.MeasureText(g, title, descFont);
                //}

                ////strSize = TextRenderer.MeasureText(g, title, titleFont);
                //g.DrawString(prodDesc,
                //    descFont, textBrush, new RectangleF(marginLeft + strSize.Width, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += rowHeight * 2;
                //top += lineMargin;

                //title = "装箱数量：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //g.DrawString(string.Format("{0} PCS", box.RealCount), fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;

                //title = "箱号：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);

                //var barcodeWriter = new BarcodeWriter();
                //barcodeWriter.Format = BarcodeFormat.CODE_128;
                //barcodeWriter.Options.Height = 80;
                //barcodeWriter.Options.Width = WidthInPixel - titleWidth * 2 - marginLeft * 2;//fieldWidth;
                //Bitmap barcode = barcodeWriter.Write(box.BoxSN == null ? "" : box.BoxSN);

                //g.DrawImage(barcode, fieldLeft, top);
                //top += barcodeWriter.Options.Height;
                //top += lineMargin;

                //title = "供应商：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //g.DrawString(oi.VendorName == null ? "" : oi.VendorName, fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;

                //title = "Q   C：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);
                //top += strSize.Height;
                //top += lineMargin;

                //title = "制造日期：";
                //g.DrawString(title, titleFont, textBrush, new RectangleF(left, top, WidthInPixel, HeightInPixel), titleFormat);
                //strSize = TextRenderer.MeasureText(g, title, titleFont);

                //g.DrawString(DateTime.Now.ToString("yyyy.MM.dd"), fieldFont, textBrush, new RectangleF(fieldLeft, top, fieldWidth, HeightInPixel), fieldFormat);
                //top += strSize.Height;
                //top += lineMargin;

                #endregion

                SearchBoxButton.Enabled = true;
                BoxSNSearchTextBox.Enabled = true;
                PrintLabelButton.Enabled = true;

                PreviewLabel.Image = ImageToConvert;
            }), null);
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            BoxInfo box = new BoxInfo();
            box.BoxSN = "OB420181122000008";
            
            OrderInfo oi = new OrderInfo();
            oi.ProductDesc = "Opple_Control box_WiFi_BL_7682_17P_stamp_PCBA_antenna_V1.2模块(欧普WIFI控制盒208000001281)";
            ZebraPreview(box, oi);
        }

        //打印
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

        //读取打印默认值，已订单为单位
        private string GetDef(string Id, string Name)
        {
            string def = "";
            if (File.Exists(FilePath))
            {
                using (var conn = new SQLiteConnection("Data Source=" + FilePath))
                {
                    try
                    {
                        conn.Open();
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            command.CommandText = "SELECT DefValue FROM Setting WHERE Id = @Id AND Name=@Name";

                            command.Parameters.Add("@Id", DbType.String);
                            command.Parameters.Add("@Name", DbType.String);

                            command.Parameters["@Id"].Value = Id;
                            command.Parameters["@Name"].Value = Name;

                            string v = command.ExecuteScalar() as string;
                            if(v != null)
                            {
                                def = v;
                            }
                        }

                        conn.Close();
                    }
                    finally
                    {
                        if (conn != null)
                        {
                            try
                            {
                                conn.Close();
                            }
                            finally
                            {

                            }
                        }
                    }

                }
            }
            return def;
        }
        
        //预览标签
        private void PreviewBtn_Click(object sender, EventArgs e)
        {
            if(OrderInfo == null)
            {
                return;
            }

            OrderInfo.OrderId = OrderIdTB.Text;
            OrderInfo.Workform = WorkformTB.Text;
            OrderInfo.MaterialCode = GubeiNoTB.Text;
            OrderInfo.KehuCode = KehuNoTB.Text;
            OrderInfo.ProductModel = ProdModelTB.Text;
            OrderInfo.ProductDesc = ProductDescTB.Text;
            OrderInfo.VendorName = SupplierTB.Text;
            OrderInfo.ProductVerTag = VersionTB.Text;
            OrderInfo.BatchNo = BatchTB.Text;
            OrderInfo.Firmware = FirmwareTB.Text;
            //保存打印默认值
            if (File.Exists(FilePath))
            {
                using (var conn = new SQLiteConnection("Data Source=" + FilePath))
                {
                    try
                    {
                        conn.Open();
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            command.CommandText = "UPDATE Setting SET DefValue = @DefValue WHERE Id = @Id AND Name=@Name";

                            command.Parameters.Add("@DefValue", DbType.String);
                            command.Parameters.Add("@ID", DbType.String);
                            command.Parameters.Add("@Name", DbType.String);

                            #region 工单
                            command.Parameters["@ID"].Value = OrderInfo.OrderId;
                            command.Parameters["@Name"].Value = "Workform";
                            command.Parameters["@DefValue"].Value = WorkformTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "Workform";
                                    insCmd.Parameters["@DefValue"].Value = WorkformTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }
                            #endregion

                            #region 古北产品编码
                            command.Parameters["@Name"].Value = "GuBeiNo";
                            command.Parameters["@DefValue"].Value = GubeiNoTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "GuBeiNo";
                                    insCmd.Parameters["@DefValue"].Value = GubeiNoTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }
                            #endregion

                            #region 客户产品编码
                            command.Parameters["@Name"].Value = "KehuNo";
                            command.Parameters["@DefValue"].Value = KehuNoTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "KehuNo";
                                    insCmd.Parameters["@DefValue"].Value = KehuNoTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }
                            #endregion

                            #region 产品型号
                            command.Parameters["@Name"].Value = "ProdModel";
                            command.Parameters["@DefValue"].Value = ProdModelTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "ProdModel";
                                    insCmd.Parameters["@DefValue"].Value = ProdModelTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }
                            #endregion

                            command.Parameters["@Name"].Value = "ProdDesc";
                            command.Parameters["@DefValue"].Value = ProductDescTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "ProdDesc";
                                    insCmd.Parameters["@DefValue"].Value = ProductDescTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }

                            command.Parameters["@Name"].Value = "Supplier";
                            command.Parameters["@DefValue"].Value = SupplierTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "Supplier";
                                    insCmd.Parameters["@DefValue"].Value = SupplierTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }

                            command.Parameters["@Name"].Value = "BatchNo";
                            command.Parameters["@DefValue"].Value = BatchTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "BatchNo";
                                    insCmd.Parameters["@DefValue"].Value = BatchTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }

                            command.Parameters["@Name"].Value = "Version";
                            command.Parameters["@DefValue"].Value = VersionTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "Version";
                                    insCmd.Parameters["@DefValue"].Value = VersionTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }

                            command.Parameters["@Name"].Value = "Firmware";
                            command.Parameters["@DefValue"].Value = FirmwareTB.Text;
                            if (command.ExecuteNonQuery() == 0)
                            {
                                using (SQLiteCommand insCmd = conn.CreateCommand())
                                {
                                    insCmd.CommandText = "INSERT INTO Setting(Id, Name, DefValue) VALUES(@Id, @Name, @DefValue)";

                                    insCmd.Parameters.Add("@DefValue", DbType.String);
                                    insCmd.Parameters.Add("@Name", DbType.String);
                                    insCmd.Parameters.Add("@ID", DbType.String);

                                    insCmd.Parameters["@ID"].Value = OrderInfo.OrderId;
                                    insCmd.Parameters["@Name"].Value = "Firmware";
                                    insCmd.Parameters["@DefValue"].Value = FirmwareTB.Text;
                                    insCmd.ExecuteNonQuery();
                                }
                            }
                        }

                        conn.Close();
                    }
                    finally
                    {
                        if (conn != null)
                        {
                            try
                            {
                                conn.Close();
                            }
                            finally
                            {

                            }
                        }
                    }

                }
            }
            
            //生成预览
            ZebraPreview(BoxInfo, OrderInfo);
        }

        private void SelectPrintItemButton_Click(object sender, EventArgs e)
        {
            if (OrderInfo == null)
            {
                MessageBox.Show("无订单，请先检索箱子", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PrintItemSelectFrm frm = new PrintItemSelectFrm();
            frm.OrderID = OrderInfo.OrderId;
            frm.ShowDialog();
        }

        private void HeadFontList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HeadFontName = (sender as ComboBox).SelectedItem as string;
            if(!String.IsNullOrEmpty(HeadFontName))
            {
                Properties.Settings.Default["HeadFontName"] = HeadFontName;
                Properties.Settings.Default.Save();
            }
        }

        private void HeadFontSizeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HeadFontSize = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(HeadFontSize))
            {
                Properties.Settings.Default["HeadFontSize"] = HeadFontSize;
                Properties.Settings.Default.Save();
            }
        }

        private void HeadFontTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HeadFonStyle = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(HeadFonStyle))
            {
                Properties.Settings.Default["HeadFontStyle"] = HeadFonStyle;
                Properties.Settings.Default.Save();
            }

        }

        private void LineSpaceTB_TextChanged(object sender, EventArgs e)
        {
            string v = (sender as TextBox).Text;
            int Result;
            if(Int32.TryParse(v, out Result))
            {
                Properties.Settings.Default["LineSpace"] = v;
                Properties.Settings.Default.Save();
            }
        }

        private void TitleFontList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TitleFontName = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(TitleFontName))
            {
                Properties.Settings.Default["TitleFontName"] = TitleFontName;
                Properties.Settings.Default.Save();
            }
        }

        private void TitleFontSizeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TitleFontSize = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(TitleFontSize))
            {
                Properties.Settings.Default["TitleFontSize"] = TitleFontSize;
                Properties.Settings.Default.Save();
            }
        }

        private void TitleFontTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TitleFontStyle = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(TitleFontStyle))
            {
                Properties.Settings.Default["TitleFontStyle"] = TitleFontStyle;
                Properties.Settings.Default.Save();
            }

        }

        private void FieldFontList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FieldFontName = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(FieldFontName))
            {
                Properties.Settings.Default["FieldFontName"] = FieldFontName;
                Properties.Settings.Default.Save();
            }
        }

        private void FieldFontSizeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TitleFontSize = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(TitleFontSize))
            {
                Properties.Settings.Default["TitleFontSize"] = TitleFontSize;
                Properties.Settings.Default.Save();
            }

        }

        private void FieldFontTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TitleFontStyle = (sender as ComboBox).SelectedItem as string;
            if (!String.IsNullOrEmpty(TitleFontStyle))
            {
                Properties.Settings.Default["TitleFontStyle"] = TitleFontStyle;
                Properties.Settings.Default.Save();
            }

        }

        private void LeftMarginTB_TextChanged(object sender, EventArgs e)
        {
            string v = (sender as TextBox).Text;
            int Result;
            if (Int32.TryParse(v, out Result))
            {
                Properties.Settings.Default["LeftMargin"] = v;
                Properties.Settings.Default.Save();
            }
        }

        private void TopMarginTB_TextChanged(object sender, EventArgs e)
        {
            string v = (sender as TextBox).Text;
            int Result;
            if (Int32.TryParse(v, out Result))
            {
                Properties.Settings.Default["TopMargin"] = v;
                Properties.Settings.Default.Save();
            }
        }
    }
}
