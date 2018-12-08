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
        public string OrderCodeBar
        {
            get;
            set;
        }
        //批次号
        public string BatchSeq
        {
            get;
            set;
        }
        //批次号名称
        public string Batch
        {
            get;
            set;
        }
        public string BatchBar
        {
            get;
            set;
        }
        //古北产品编码
        public string GubeiNoSeq
        {
            get;
            set;
        }
        //古北产品编码
        public string GubeiNo
        {
            get;
            set;
        }
        public string GubeiNoBar
        {
            get;
            set;
        }

        //客户产品编码
        public string CustomerNoSeq
        {
            get;
            set;
        }
        //客户产品编码
        public string CustomerNo
        {
            get;
            set;
        }
        public string CustomerNoBar
        {
            get;
            set;
        }

        //产品型号
        public string ProdModelSeq
        {
            get;
            set;
        }

        //产品型号
        public string ProdModel
        {
            get;
            set;
        }
        public string ProdModelBar
        {
            get;
            set;
        }
        //产品描述
        public string ProdDescSeq
        {
            get;
            set;
        }
        //产品描述
        public string ProdDesc
        {
            get;
            set;
        }
        public string ProdDescBar
        {
            get;
            set;
        }
        //Ver
        public string VerSeq
        {
            get;
            set;
        }
        //Ver
        public string Ver
        {
            get;
            set;
        }
        public string VerBar
        {
            get;
            set;
        }
        //装箱数量
        public string RealCountSeq
        {
            get;
            set;
        }
        //装箱数量
        public string RealCount
        {
            get;
            set;
        }
        public string RealCountBar
        {
            get;
            set;
        }
        //箱号
        public string BoxNoSeq
        {
            get;
            set;
        }
        //箱号
        public string BoxNo
        {
            get;
            set;
        }
        public string BoxNoBar
        {
            get;
            set;
        }
        //供应商
        public string SupplierSeq
        {
            get;
            set;
        }
        //供应商
        public string Supplier
        {
            get;
            set;
        }
        public string SupplierBar
        {
            get;
            set;
        }
        //QC
        public string QCSeq
        {
            get;
            set;
        }
        //QC
        public string QC
        {
            get;
            set;
        }
        public string QCBar
        {
            get;
            set;
        }
        //制造日期
        public string ManufactureDateSeq
        {
            get;
            set;
        }
        //制造日期
        public string ManufactureDate
        {
            get;
            set;
        }
        public string ManufactureDateBar
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
            OrderCodeSeq = OrderCodeSeqTB.Text;
            OrderCode = OrderCodeTB.Text;
            OrderCodeBar = OrderCodeCK.Checked ? "1" : "0";

            BatchSeq = BatchSeqTB.Text;
            Batch = BatchTB.Text;
            BatchBar = BatchCK.Checked ? "1" : "0";

            GubeiNoSeq = GubeiNoSeqTB.Text;
            GubeiNo = GubeiNoTB.Text;
            GubeiNoBar = GubeiNoCK.Checked ? "1" : "0";

            CustomerNoSeq = CustomerNoSeqTB.Text;
            CustomerNo = CustomerNoTB.Text;
            CustomerNoBar = CustomerNoCK.Checked ? "1" : "0";

            ProdModelSeq = ProdModelSeqTB.Text;
            ProdModel = ProdModelTB.Text;
            ProdModelBar = ProdModelCK.Checked ? "1" : "0";

            ProdDescSeq = ProdDescSeqTB.Text;
            ProdDesc = ProdDescTB.Text;
            ProdDescBar = ProdDescCK.Checked ? "1" : "0";

            VerSeq = VerSeqTB.Text;
            Ver = VerTB.Text;
            VerBar = VerCK.Checked ? "1" : "0";

            RealCountSeq = RealCountSeqTB.Text;
            RealCount = RealCountTB.Text;
            RealCountBar = RealCountCK.Checked ? "1" : "0";

            BoxNoSeq = BoxNoSeqTB.Text;
            BoxNo = BoxNoTB.Text;
            BoxNoBar = BoxNoCK.Checked ? "1" : "0";

            SupplierSeq = SupplierSeqTB.Text;
            Supplier = SupplierTB.Text;
            SupplierBar = SupplierCK.Checked ? "1" : "0";

            QCSeq = QCSeqTB.Text;
            QC = QCTB.Text;
            QCBar = QCCK.Checked ? "1" : "0";

            ManufactureDateSeq = ManufactureDateSeqTB.Text;
            ManufactureDate = ManufactureDateTB.Text;
            ManufactureDateBar = ManufactureDateCK.Checked ? "1" : "0";

            SavePrintItem();
        }

        //create table Print(Id TEXT, Name Text, BarCode TEXT, PRIMARY KEY (Id, Name))

        private void SavePrintItem()
        {
            if (File.Exists(FilePath))
            {
                using (var conn = new SQLiteConnection("Data Source=" + FilePath))
                {
                    SQLiteTransaction trans = null;
                    try
                    {
                        conn.Open();
                        trans = conn.BeginTransaction();
                        using (SQLiteCommand command = conn.CreateCommand())
                        {
                            //command.CommandText = "UPDATE Setting SET DefValue = @DefValue WHERE Id = @Id AND Name=@Name";

                            //command.Parameters.Add("@DefValue", DbType.String);
                            //command.Parameters.Add("@ID", DbType.String);
                            //command.Parameters.Add("@Name", DbType.String);

                            //command.Parameters["@ID"].Value = workflowID.Text;
                            //command.Parameters["@Name"].Value = "GuBeiNo";
                            //command.Parameters["@DefValue"].Value = textBox5.Text;

                        }
                        trans.Commit();
                        conn.Close();
                    }
                    catch
                    {
                        if(trans != null)
                        {
                            trans.Rollback();
                        }
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

        private void PrintItemSelectFrm_Load(object sender, EventArgs e)
        {
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
            GetPrintItem();
        }

        private void GetPrintItem()
        {
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
                            command.CommandText = "SELECT  Name, DispSeq, DispText, BarCode from Print where Id=@Id";

                            command.Parameters.Add("@ID", DbType.String);
                            command.Parameters["@ID"].Value = OrderID;

                            using (SQLiteDataReader dr = command.ExecuteReader())
                            {
                                while(dr != null && dr.Read())
                                {
                                    string Name = dr.GetString(0);
                                    int DispSeq = dr.GetInt32(1);
                                    string DispText = dr.GetString(1);
                                    string BarCode = dr.GetString(2);
                                    if (!String.IsNullOrEmpty(Name))
                                    {
                                        var property = this.GetType().GetProperty(Name);
                                        property.SetValue(this, "newName");
                                    }
                                }
                                
                            }
                            
                        }

                        conn.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
