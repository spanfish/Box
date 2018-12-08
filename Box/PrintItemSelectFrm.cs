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

        //工单编码顺序
        public string WorkFlowSeq
        {
            get;
            set;
        }

        public string WorkFlow
        {
            get;
            set;
        }
        public string WorkFlowBar
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


        public string FirmwareSeq
        {
            get;
            set;
        }

        public string Firmware
        {
            get;
            set;
        }
        public string FirmwareBar
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

            WorkFlowSeq = WorkFlowSeqTB.Text;
            WorkFlow = WorkFlowTB.Text;
            WorkFlowBar = WorkFlowCK.Checked ? "1" : "0";

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

            FirmwareSeq = FirmwareSeqTB.Text;
            Firmware = FirmwareTB.Text;
            FirmwareBar = FirmwareCK.Checked ? "1" : "0";

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
                            command.CommandText = "DELETE from Print where Id=@Id";
                            command.Parameters.Add("@Id", DbType.String);
                            command.Parameters["@Id"].Value = OrderID;
                            command.ExecuteNonQuery();

                            command.CommandText = "INSERT INTO Print(Id, Name, DispSeq, DispText, BarCode) VALUES(@Id, @Name, @DispSeq, @DispText, @BarCode)";
                            //command.Parameters.Add("@Id", DbType.String);
                            command.Parameters.Add("@Name", DbType.String);
                            command.Parameters.Add("@DispSeq", DbType.Int32);
                            command.Parameters.Add("@DispText", DbType.String);
                            command.Parameters.Add("@BarCode", DbType.String);

                            command.Parameters["@Name"].Value = "OrderCode";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(OrderCodeSeq) ? 0 : Int32.Parse(OrderCodeSeq);
                            command.Parameters["@DispText"].Value = OrderCode;
                            command.Parameters["@BarCode"].Value = OrderCodeBar;
                            command.ExecuteNonQuery();
                            
                            command.Parameters["@Name"].Value = "WorkFlow";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(WorkFlowSeq) ? 0 : Int32.Parse(WorkFlowSeq);
                            command.Parameters["@DispText"].Value = WorkFlow;
                            command.Parameters["@BarCode"].Value = WorkFlowBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "Batch";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(BatchSeq) ? 0 : Int32.Parse(BatchSeq);
                            command.Parameters["@DispText"].Value = Batch;
                            command.Parameters["@BarCode"].Value = BatchBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "GubeiNo";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(GubeiNoSeq) ? 0 : Int32.Parse(GubeiNoSeq);
                            command.Parameters["@DispText"].Value = GubeiNo;
                            command.Parameters["@BarCode"].Value = GubeiNoBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "CustomerNo";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(CustomerNoSeq) ? 0 : Int32.Parse(CustomerNoSeq);
                            command.Parameters["@DispText"].Value = CustomerNo;
                            command.Parameters["@BarCode"].Value = CustomerNoBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "ProdModel";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(ProdModelSeq) ? 0 : Int32.Parse(ProdModelSeq);
                            command.Parameters["@DispText"].Value = ProdModel;
                            command.Parameters["@BarCode"].Value = ProdModelBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "ProdDesc";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(ProdDescSeq) ? 0 : Int32.Parse(ProdDescSeq);
                            command.Parameters["@DispText"].Value = ProdDesc;
                            command.Parameters["@BarCode"].Value = ProdDescBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "Ver";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(VerSeq) ? 0 : Int32.Parse(VerSeq);
                            command.Parameters["@DispText"].Value = Ver;
                            command.Parameters["@BarCode"].Value = VerBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "RealCount";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(RealCountSeq) ? 0 : Int32.Parse(RealCountSeq);
                            command.Parameters["@DispText"].Value = RealCount;
                            command.Parameters["@BarCode"].Value = RealCountBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "BoxNo";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(BoxNoSeq) ? 0 : Int32.Parse(BoxNoSeq);
                            command.Parameters["@DispText"].Value = BoxNo;
                            command.Parameters["@BarCode"].Value = BoxNoBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Id"].Value = OrderID;
                            command.Parameters["@Name"].Value = "Supplier";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(SupplierSeq) ? 0 : Int32.Parse(SupplierSeq);
                            command.Parameters["@DispText"].Value = Supplier;
                            command.Parameters["@BarCode"].Value = SupplierBar;
                            command.ExecuteNonQuery();
                            
                            command.Parameters["@Name"].Value = "QC";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(QCSeq) ? 0 : Int32.Parse(QCSeq);
                            command.Parameters["@DispText"].Value = QC;
                            command.Parameters["@BarCode"].Value = QCBar;
                            command.ExecuteNonQuery();
                            
                            command.Parameters["@Name"].Value = "ManufactureDate";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(ManufactureDateSeq) ? 0 : Int32.Parse(ManufactureDateSeq);
                            command.Parameters["@DispText"].Value = ManufactureDate;
                            command.Parameters["@BarCode"].Value = ManufactureDateBar;
                            command.ExecuteNonQuery();

                            command.Parameters["@Name"].Value = "Firmware";
                            command.Parameters["@DispSeq"].Value = String.IsNullOrEmpty(FirmwareSeq) ? 0 : Int32.Parse(FirmwareSeq);
                            command.Parameters["@DispText"].Value = Firmware;
                            command.Parameters["@BarCode"].Value = FirmwareBar;
                            command.ExecuteNonQuery();
                        }
                        trans.Commit();
                        conn.Close();

                        MessageBox.Show("成功保存", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        if(trans != null)
                        {
                            trans.Rollback();
                        }
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

        private void PrintItemSelectFrm_Load(object sender, EventArgs e)
        {
#if DEBUG
            //OrderID = "00000000";
#endif
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

            OrderIdTB.Text = OrderID;
            GetPrintItem();

            OrderCodeSeqTB.Text = OrderCodeSeq;
            if(!String.IsNullOrEmpty(OrderCode))
            { 
                OrderCodeTB.Text = OrderCode;
            }
            OrderCodeCK.Checked = OrderCodeBar == "1";

            WorkFlowSeqTB.Text = WorkFlowSeq;
            if (!String.IsNullOrEmpty(WorkFlow))
            { 
                WorkFlowTB.Text = WorkFlow;
            }
            WorkFlowCK.Checked = WorkFlowBar == "1";

            BatchSeqTB.Text = BatchSeq;
            if (!String.IsNullOrEmpty(Batch))
            {
                BatchTB.Text = Batch;
            }
            BatchCK.Checked = BatchBar == "1";


            GubeiNoSeqTB.Text = GubeiNoSeq;
            if (!String.IsNullOrEmpty(GubeiNo))
            {
                GubeiNoTB.Text = GubeiNo;
            }
            
            GubeiNoCK.Checked = GubeiNoBar == "1";

            CustomerNoSeqTB.Text = CustomerNoSeq;
            
            if (!String.IsNullOrEmpty(CustomerNo))
            {
                CustomerNoTB.Text = CustomerNo;
            }
            CustomerNoCK.Checked = CustomerNoBar == "1";

            ProdModelSeqTB.Text = ProdModelSeq;
            
            if (!String.IsNullOrEmpty(ProdModel))
            {
                ProdModelTB.Text = ProdModel;
            }
            ProdModelCK.Checked = ProdModelBar == "1";

            ProdDescSeqTB.Text = ProdDescSeq;
           
            if (!String.IsNullOrEmpty(ProdDesc))
            {
                ProdDescTB.Text = ProdDesc;
            }
            ProdDescCK.Checked = ProdDescBar == "1";

            VerSeqTB.Text = VerSeq;
           
            if (!String.IsNullOrEmpty(Ver))
            {
                VerTB.Text = Ver;
            }
            VerCK.Checked = VerBar == "1";

            RealCountSeqTB.Text = RealCountSeq;
            if (!String.IsNullOrEmpty(RealCount))
            {
                RealCountTB.Text = RealCount;
            }
            RealCountCK.Checked = RealCountBar == "1";

            BoxNoSeqTB.Text = BoxNoSeq;
            if (!String.IsNullOrEmpty(BoxNo))
            {
                BoxNoTB.Text = BoxNo;
            }
            BoxNoCK.Checked = BoxNoBar == "1";

            SupplierSeqTB.Text = SupplierSeq;
            if (!String.IsNullOrEmpty(Supplier))
            {
                SupplierTB.Text = Supplier;
            }
            
            SupplierCK.Checked = SupplierBar == "1";

            QCSeqTB.Text = QCSeq;
            
            if (!String.IsNullOrEmpty(QC))
            {
                QCTB.Text = QC;
            }
            QCCK.Checked = QCBar == "1";

            ManufactureDateSeqTB.Text = ManufactureDateSeq;
            
            if (!String.IsNullOrEmpty(ManufactureDate))
            {
                ManufactureDateTB.Text = ManufactureDate;
            }
            ManufactureDateCK.Checked = ManufactureDateBar == "1";

            FirmwareSeqTB.Text = FirmwareSeq;
            
            if (!String.IsNullOrEmpty(ManufactureDate))
            {
                FirmwareTB.Text = Firmware;
            }
            FirmwareCK.Checked = FirmwareBar == "1";
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

                            command.Parameters.Add("@Id", DbType.String);
                            command.Parameters["@Id"].Value = OrderID;

                            using (SQLiteDataReader dr = command.ExecuteReader())
                            {
                                while(dr != null && dr.Read())
                                {
                                    string Name = dr.GetString(0);
                                    int DispSeq = dr.GetInt32(1);
                                    string DispText = dr.GetString(2);
                                    string BarCode = dr.GetString(3);
                                    if (!String.IsNullOrEmpty(Name))
                                    {
                                        var property = this.GetType().GetProperty(Name + "Seq");
                                        property.SetValue(this, DispSeq > 0 ? string.Format("{0}", DispSeq) : "");

                                        property = this.GetType().GetProperty(Name + "Bar");
                                        property.SetValue(this, BarCode);

                                        property = this.GetType().GetProperty(Name);
                                        property.SetValue(this, String.IsNullOrEmpty(DispText) ? Name : DispText);
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
