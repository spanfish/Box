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
    public partial class MainWindow : Form
    {
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

        private BindingList<Order> OrderList
        {
            get;
            set;
        }

        private RestClient client;

        private readonly SynchronizationContext synchronizationContext;

        public MainWindow()
        {
            InitializeComponent();

            InitializeStatus();

            InitializeListView();

            synchronizationContext = SynchronizationContext.Current;
            string host = System.Configuration.ConfigurationManager.AppSettings["Host"];
            client = new RestClient(host);
        }

        private void InitializeStatus()
        {
            this.Executing = false;
            this.Msg = "Ready";
        }

        private void InitializeListView()
        {
            OrderList = new BindingList<Order>();
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
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }
        
        private void ListOrder()
        {
            try
            {
                var request = new RestRequest("dlicense/v2/manu/query", Method.POST);

                request.RequestFormat = DataFormat.Json;
                
                request.AddHeader("reqUserId", App.LoginResponse.Userid);
                request.AddHeader("reqUserSession", App.LoginResponse.Loginsession);
                request.AddHeader("grouptype", App.Account.Grouptype);
                request.AddHeader("OemfactoryId", App.Account.OemfactoryId);


                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                sb.Append("\"index\":");
                sb.Append("0,");

                sb.Append("\"pagesize\":");
                sb.Append("50,");

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
                                var source = new BindingSource(list, null);
                                this.dataGridView1.DataSource = source;
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

            //            NorthwindDataSetTableAdapters.OrdersTableAdapter ta = new ListViewSample.NorthwindDataSetTableAdapters.OrdersTableAdapter();
            //            03
            //    NorthwindDataSet.OrdersDataTable dt = ta.GetData();
            //            04
            //    bindingSource1.DataSource = dt;
            //            05


            //06
            //    this.listView1.VirtualMode = true;
            //            07
            //    this.listView1.VirtualListSize = bindingSource1.Count;
            //            08
            //    this.listView1.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.OrderRowRetrieveVirtualItem);
            //            09


            //10
            //    // ヘッダー部の指定 ：これはデザイナーで指定しても良い
            //11
            //    // 最後に設定したほうが、体感速度は速くなると思う（あくまでも個人的感覚）
            //12
            //    listView1.Columns.Add("注文ID");
            //            13
            //    listView1.Columns.Add("注文日");
            //            14
            //    listView1.Columns[1].Width = 90;
            //            15
            //    listView1.Columns.Add("顧客ID");
            //            16
            //    listView1.Columns[2].Width = 80;
            //            17
            //    listView1.Columns.Add("送付先都市");
            //            18
            //    listView1.Columns[3].Width = 120;

        }

        /**
         * 刷新订单
         **/
        private void OrderListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListOrder();
        }
    }
}
