using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Box
{
    public partial class PrintItemSelectFrm : Form
    {
        //订单号
        public string OrderID
        {
            get;
            set;
        }

        //订单编码顺序
        public string OrderCodeSeq
        {
            get;
            set;
        }
        //订单编码名称
        public string OrderCode
        {
            get;
            set;
        }
        //订单编码
        public string OrderCode
        {
            get;
            set;
        }

        private static readonly string FilePath = @".\Default.db";

        public PrintItemSelectFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void SavePrintItem()
        {
            if (File.Exists(FilePath))
            {
                using (var conn = new SQLiteConnection("Data Source=" + FilePath))
                {
                    try
                    {
                        conn.Open();
                        //using (SQLiteCommand command = conn.CreateCommand())
                        //{
                        //    command.CommandText = "UPDATE Setting SET DefValue = @DefValue WHERE Id = @Id AND Name=@Name";

                        //    command.Parameters.Add("@DefValue", DbType.String);
                        //    command.Parameters.Add("@ID", DbType.String);
                        //    command.Parameters.Add("@Name", DbType.String);

                        //    command.Parameters["@ID"].Value = workflowID.Text;
                        //    command.Parameters["@Name"].Value = "GuBeiNo";
                        //    command.Parameters["@DefValue"].Value = textBox5.Text;

                        //}

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

            ZebraPreview(BoxInfo, OrderInfo);
        }

        private void PrintItemSelectFrm_Load(object sender, EventArgs e)
        {

        }
    }
    }
}
