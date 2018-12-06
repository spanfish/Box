using RestSharp;
using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Box
{
    public partial class PrintWindow : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string InnerBoxId { get; set; }
        public string OuterBoxId { get; set; }

        private bool _executing;
        public bool Executing
        {
            get
            {
                return _executing;
            }
            set
            {
                _executing = value;
                if (_executing)
                {
                    this.ProgressBar.MarqueeAnimationSpeed = 150;
                }
                else
                {
                    this.ProgressBar.MarqueeAnimationSpeed = 0;
                    this.ProgressBar.Value = 0;
                }
                OnPropertyChanged("Executing");
            }
        }

        private string _msg;
        public string Msg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
                if (_msg != null)
                {
                    this.StatusLabel.Text = _msg;
                }
                else
                {
                    this.StatusLabel.Text = "";
                }
                OnPropertyChanged("Msg");
            }
        }

        public string OrderId
        {
            get;
            set;
        }

        public AppConfig Config
        {
            get;
            set;
        }

        private RestClient client;

        private readonly SynchronizationContext synchronizationContext;

        public PrintWindow(string orderId, AppConfig config)
        {
            InitializeComponent();

            synchronizationContext = SynchronizationContext.Current;
            string host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            client = new RestClient(host);

            this.OrderId = orderId;
            this.Config = config;

            this.InnerBoxIdTextBox.DataBindings.Add(new Binding("Text", this, "InnerBoxId", false, DataSourceUpdateMode.OnPropertyChanged));
            this.OuterBoxIdTextBox.DataBindings.Add(new Binding("Text", this, "OuterBoxId", false, DataSourceUpdateMode.OnPropertyChanged));

#if DEBUG
            this.OrderId = "GB-RMK-18112201";
#endif
        }

        private void PrintWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    var request = new RestRequest("dlicense/v2/manu/boxing/queryboxbydev", Method.POST);

                    request.RequestFormat = DataFormat.Json;

                    request.AddHeader("reqUserId", Config.Login.Userid);
                    request.AddHeader("reqUserSession", Config.Login.Loginsession);
                    request.AddHeader("grouptype", Config.Account.Grouptype);
                    request.AddHeader("OemfactoryId", Config.Account.OemfactoryId);

                    StringBuilder sb = new StringBuilder();
                    sb.Append("{");
                    sb.Append("\"orderid\": \"");
                    sb.Append(this.OrderId);
                    sb.Append("\", ");
                    
                    sb.Append("\"did\": ");
                    sb.Append("\"");
                    sb.Append(SearchTextBox.Text);
                    sb.Append("\"");
                    sb.Append("}");

                    string body = sb.ToString();
                    request.AddParameter("application/json", body, ParameterType.RequestBody);

                    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

                    this.Executing = true;
                    var asyncHandle = client.ExecuteAsync<BoxSearchResponse>(request, response =>
                    {
                        synchronizationContext.Post(new SendOrPostCallback(o =>
                        {
                            try
                            {
                                if (response.IsSuccessful)
                                {
                                    BoxSearchResponse boxRes = response.Data;
                                    if (boxRes != null)
                                    {
                                        //if (boxRes.Status == 0 && boxRes.RetData != null && boxRes.RetData.BoxInfo != null)
                                        //{
                                        //    //foreach(BoxInfo box in boxRes.RetData.BoxInfo)
                                        //    //{
                                        //        //if(box.BoxType == "inner")
                                        //        //{
                                        //        //    this.InnerBoxId = box.BoxSN;
                                        //        //    OnPropertyChanged("InnerBoxId");
                                        //        //}
                                        //        //else if(box.BoxType == "outerforbox")
                                        //        //{
                                        //        //    this.OuterBoxId = box.BoxSN;
                                        //        //    OnPropertyChanged("OuterBoxId");
                                        //        //}
                                        //    //}
                                        //}
                                        //else
                                        //{
                                        //    this.Msg = boxRes.Msg == null ? "错误" : boxRes.Msg;
                                        //}
                                    }
                                    else
                                    {
                                        this.Msg = response.ErrorMessage == null ? "错误" : response.ErrorMessage;
                                    }
                                }
                                else
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                this.Msg = ex.Message;
                            }
                            finally
                            {
                                this.Executing = false;
                            }

                        }), response);
                        
                    });
                }
                catch(Exception ex)
                {
                    this.Msg = ex.Message;
                    this.Executing = false;
                }
            }
        }
    }
}
