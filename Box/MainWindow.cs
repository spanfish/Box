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

namespace Box
{
    public partial class MainWindow : Form, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region 全局变量，存放登陆信息
        public AppConfig AppConfig
        {
            get;
            set;
        }
        #endregion

        private int status;

        private int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                //
            }
        }

        private bool executing;

        private bool Executing
        {
            get
            {
                return executing;
            }
            set
            {
                executing = value;
                this.progressBar.Visible = executing;
            }
        }

        private string msg;

        private string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
                this.msgStatusLabel.Text = msg;
            }
        }

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

        private BindingList<Order> OrderList
        {
            get;
            set;
        }

        private RestClient client;

        private readonly SynchronizationContext synchronizationContext;

        private LPPrint Printer
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();

            synchronizationContext = SynchronizationContext.Current;

            InitializeApp();

            InitializeStatus();

            InitializeListView();

            
            string host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            client = new RestClient(host);

            InitializePrint();

            InitializeOrderDetailBinding();
        }

        private void InitializeApp()
        {
            this.AppConfig = new AppConfig(synchronizationContext);
        }

        private void InitializeOrderDetailBinding()
        {
            this.ActiveOrder = new Order();
            //this.orderIdLabel.DataBindings.Add(new Binding("Text", this, "ActiveOrder", false, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void InitializeStatus()
        {
            this.Executing = false;
            this.Msg = "Ready";

            this.nextPageButton.DataBindings.Add(new Binding("Enabled", this.AppConfig, "IsLoginSuccess", false, DataSourceUpdateMode.OnPropertyChanged));
            this.prevPageButton.DataBindings.Add(new Binding("Enabled", this.AppConfig, "IsLoginSuccess", false, DataSourceUpdateMode.OnPropertyChanged));
            this.orderRefreshButton.DataBindings.Add(new Binding("Enabled", this.AppConfig, "IsLoginSuccess", false, DataSourceUpdateMode.OnPropertyChanged));
        }

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

        private void InitializeListView()
        {
            this.dataGridView1.AutoGenerateColumns = false;

            OrderList = new BindingList<Order>();
            var source = new BindingSource(OrderList, null);
            this.dataGridView1.DataSource = source;            
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
                    OrderResponse orderRes = response.Data;
                    if(orderRes != null && orderRes.Orderlist != null && orderRes.Orderlist.Count > 0)
                    {
                        synchronizationContext.Post(new SendOrPostCallback(o =>
                        {
                            List<Order> list = o as List<Order>;
                            if(list != null)
                            {
                                foreach(Order order in list)
                                {
                                    OrderList.Add(order);
                                }
                                //var source = new BindingSource(OrderList, null);
                                //this.dataGridView1.DataSource = source;
                                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                            }
                            
                        }), orderRes.Orderlist);

                    }
                    
                });
                Console.WriteLine("");
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
            this.Printer.Write("^XA^FO20,10^FDZEBRA^FS^XZ");
        }

        private void orderRefreshButton_Click(object sender, EventArgs e)
        {
            ListOrder(0);
        }

        private void nextPageButton_Click(object sender, EventArgs e)
        {
            int i = OrderList.Count;
            ListOrder(i);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Order activeOrder = dataGridView1.CurrentRow.DataBoundItem as Order;
            this.ActiveOrder = activeOrder;
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            order.OrderId = "1234";
            this.ActiveOrder = order;
        }
    }
}
