using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Box
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ListOrder()
        {
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
    }
}
