﻿namespace Box
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BoxSNSearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchBoxButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BoxTypeTextBox = new System.Windows.Forms.TextBox();
            this.BoxSNTextBox = new System.Windows.Forms.TextBox();
            this.BoxCapacityTextBox = new System.Windows.Forms.TextBox();
            this.BoxRealCountTextBox = new System.Windows.Forms.TextBox();
            this.BoxCreateTimeTextBox = new System.Windows.Forms.TextBox();
            this.BoxStatusTextBox = new System.Windows.Forms.TextBox();
            this.PrintLabelButton = new System.Windows.Forms.Button();
            this.PreviewLabel = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.OrderIdTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.workflowID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ProdModelTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.VendorTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.ProductDescTextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.HeadFontList = new System.Windows.Forms.ComboBox();
            this.HeadFontSizeList = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TitleFontSizeList = new System.Windows.Forms.ComboBox();
            this.TitleFontList = new System.Windows.Forms.ComboBox();
            this.FieldFontSizeList = new System.Windows.Forms.ComboBox();
            this.FieldFontList = new System.Windows.Forms.ComboBox();
            this.HeadFontTypeList = new System.Windows.Forms.ComboBox();
            this.TitleFontTypeList = new System.Windows.Forms.ComboBox();
            this.FieldFontTypeList = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.LineSpaceTB = new System.Windows.Forms.TextBox();
            this.SelectPrintItemButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.LeftMarginTB = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.TopMarginTB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入箱号";
            // 
            // BoxSNSearchTextBox
            // 
            this.BoxSNSearchTextBox.Location = new System.Drawing.Point(93, 26);
            this.BoxSNSearchTextBox.Name = "BoxSNSearchTextBox";
            this.BoxSNSearchTextBox.Size = new System.Drawing.Size(214, 19);
            this.BoxSNSearchTextBox.TabIndex = 1;
            this.BoxSNSearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoxSNSearchTextBox_KeyDown);
            // 
            // SearchBoxButton
            // 
            this.SearchBoxButton.Location = new System.Drawing.Point(93, 51);
            this.SearchBoxButton.Name = "SearchBoxButton";
            this.SearchBoxButton.Size = new System.Drawing.Size(75, 23);
            this.SearchBoxButton.TabIndex = 2;
            this.SearchBoxButton.Text = "查询箱子";
            this.SearchBoxButton.UseVisualStyleBackColor = true;
            this.SearchBoxButton.Click += new System.EventHandler(this.SearchBoxButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "箱体类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "箱号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "箱体容量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "装箱状态";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "创建时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "实际数量";
            // 
            // BoxTypeTextBox
            // 
            this.BoxTypeTextBox.Location = new System.Drawing.Point(93, 119);
            this.BoxTypeTextBox.Name = "BoxTypeTextBox";
            this.BoxTypeTextBox.ReadOnly = true;
            this.BoxTypeTextBox.Size = new System.Drawing.Size(245, 19);
            this.BoxTypeTextBox.TabIndex = 9;
            // 
            // BoxSNTextBox
            // 
            this.BoxSNTextBox.Location = new System.Drawing.Point(93, 144);
            this.BoxSNTextBox.Name = "BoxSNTextBox";
            this.BoxSNTextBox.ReadOnly = true;
            this.BoxSNTextBox.Size = new System.Drawing.Size(245, 19);
            this.BoxSNTextBox.TabIndex = 10;
            // 
            // BoxCapacityTextBox
            // 
            this.BoxCapacityTextBox.Location = new System.Drawing.Point(93, 171);
            this.BoxCapacityTextBox.Name = "BoxCapacityTextBox";
            this.BoxCapacityTextBox.ReadOnly = true;
            this.BoxCapacityTextBox.Size = new System.Drawing.Size(128, 19);
            this.BoxCapacityTextBox.TabIndex = 11;
            // 
            // BoxRealCountTextBox
            // 
            this.BoxRealCountTextBox.Location = new System.Drawing.Point(93, 198);
            this.BoxRealCountTextBox.Name = "BoxRealCountTextBox";
            this.BoxRealCountTextBox.ReadOnly = true;
            this.BoxRealCountTextBox.Size = new System.Drawing.Size(128, 19);
            this.BoxRealCountTextBox.TabIndex = 12;
            // 
            // BoxCreateTimeTextBox
            // 
            this.BoxCreateTimeTextBox.Location = new System.Drawing.Point(93, 225);
            this.BoxCreateTimeTextBox.Name = "BoxCreateTimeTextBox";
            this.BoxCreateTimeTextBox.ReadOnly = true;
            this.BoxCreateTimeTextBox.Size = new System.Drawing.Size(128, 19);
            this.BoxCreateTimeTextBox.TabIndex = 13;
            // 
            // BoxStatusTextBox
            // 
            this.BoxStatusTextBox.Location = new System.Drawing.Point(93, 252);
            this.BoxStatusTextBox.Name = "BoxStatusTextBox";
            this.BoxStatusTextBox.ReadOnly = true;
            this.BoxStatusTextBox.Size = new System.Drawing.Size(128, 19);
            this.BoxStatusTextBox.TabIndex = 14;
            // 
            // PrintLabelButton
            // 
            this.PrintLabelButton.Location = new System.Drawing.Point(657, 147);
            this.PrintLabelButton.Name = "PrintLabelButton";
            this.PrintLabelButton.Size = new System.Drawing.Size(75, 23);
            this.PrintLabelButton.TabIndex = 15;
            this.PrintLabelButton.Text = "打印标签";
            this.PrintLabelButton.UseVisualStyleBackColor = true;
            this.PrintLabelButton.Click += new System.EventHandler(this.PrintLabelButton_Click);
            // 
            // PreviewLabel
            // 
            this.PreviewLabel.Location = new System.Drawing.Point(474, 171);
            this.PreviewLabel.Name = "PreviewLabel";
            this.PreviewLabel.Size = new System.Drawing.Size(503, 352);
            this.PreviewLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewLabel.TabIndex = 16;
            this.PreviewLabel.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(738, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OrderIdTextBox
            // 
            this.OrderIdTextBox.Location = new System.Drawing.Point(93, 278);
            this.OrderIdTextBox.Name = "OrderIdTextBox";
            this.OrderIdTextBox.Size = new System.Drawing.Size(375, 19);
            this.OrderIdTextBox.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 281);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "订单号码";
            // 
            // workflowID
            // 
            this.workflowID.Location = new System.Drawing.Point(93, 304);
            this.workflowID.Name = "workflowID";
            this.workflowID.Size = new System.Drawing.Size(375, 19);
            this.workflowID.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 307);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "工单号码";
            // 
            // ProdModelTextBox
            // 
            this.ProdModelTextBox.Location = new System.Drawing.Point(93, 382);
            this.ProdModelTextBox.Name = "ProdModelTextBox";
            this.ProdModelTextBox.Size = new System.Drawing.Size(375, 19);
            this.ProdModelTextBox.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 385);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 26;
            this.label10.Text = "产品型号";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(93, 356);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(375, 19);
            this.textBox4.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 359);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "客户产品编码";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(93, 330);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(375, 19);
            this.textBox5.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 333);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 22;
            this.label12.Text = "古北产品编码";
            // 
            // VendorTextBox
            // 
            this.VendorTextBox.Location = new System.Drawing.Point(93, 436);
            this.VendorTextBox.Name = "VendorTextBox";
            this.VendorTextBox.Size = new System.Drawing.Size(375, 19);
            this.VendorTextBox.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 439);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 32;
            this.label13.Text = "供应商";
            // 
            // ProductDescTextBox
            // 
            this.ProductDescTextBox.Location = new System.Drawing.Point(93, 408);
            this.ProductDescTextBox.Name = "ProductDescTextBox";
            this.ProductDescTextBox.Size = new System.Drawing.Size(375, 19);
            this.ProductDescTextBox.TabIndex = 29;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 411);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 28;
            this.label15.Text = "产品描述";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(576, 147);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 34;
            this.button2.Text = "刷新预览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // HeadFontList
            // 
            this.HeadFontList.FormattingEnabled = true;
            this.HeadFontList.Location = new System.Drawing.Point(474, 24);
            this.HeadFontList.Name = "HeadFontList";
            this.HeadFontList.Size = new System.Drawing.Size(121, 20);
            this.HeadFontList.TabIndex = 35;
            // 
            // HeadFontSizeList
            // 
            this.HeadFontSizeList.FormattingEnabled = true;
            this.HeadFontSizeList.Items.AddRange(new object[] {
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
            this.HeadFontSizeList.Location = new System.Drawing.Point(474, 53);
            this.HeadFontSizeList.Name = "HeadFontSizeList";
            this.HeadFontSizeList.Size = new System.Drawing.Size(121, 20);
            this.HeadFontSizeList.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(440, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 37;
            this.label14.Text = "字体";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(440, 53);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 38;
            this.label16.Text = "大小";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TitleFontSizeList
            // 
            this.TitleFontSizeList.FormattingEnabled = true;
            this.TitleFontSizeList.Items.AddRange(new object[] {
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
            this.TitleFontSizeList.Location = new System.Drawing.Point(611, 53);
            this.TitleFontSizeList.Name = "TitleFontSizeList";
            this.TitleFontSizeList.Size = new System.Drawing.Size(121, 20);
            this.TitleFontSizeList.TabIndex = 40;
            // 
            // TitleFontList
            // 
            this.TitleFontList.FormattingEnabled = true;
            this.TitleFontList.Location = new System.Drawing.Point(611, 24);
            this.TitleFontList.Name = "TitleFontList";
            this.TitleFontList.Size = new System.Drawing.Size(121, 20);
            this.TitleFontList.TabIndex = 39;
            // 
            // FieldFontSizeList
            // 
            this.FieldFontSizeList.FormattingEnabled = true;
            this.FieldFontSizeList.Items.AddRange(new object[] {
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
            this.FieldFontSizeList.Location = new System.Drawing.Point(747, 53);
            this.FieldFontSizeList.Name = "FieldFontSizeList";
            this.FieldFontSizeList.Size = new System.Drawing.Size(121, 20);
            this.FieldFontSizeList.TabIndex = 44;
            // 
            // FieldFontList
            // 
            this.FieldFontList.FormattingEnabled = true;
            this.FieldFontList.Location = new System.Drawing.Point(747, 24);
            this.FieldFontList.Name = "FieldFontList";
            this.FieldFontList.Size = new System.Drawing.Size(121, 20);
            this.FieldFontList.TabIndex = 43;
            // 
            // HeadFontTypeList
            // 
            this.HeadFontTypeList.FormattingEnabled = true;
            this.HeadFontTypeList.Items.AddRange(new object[] {
            "普通",
            "粗体",
            "斜体"});
            this.HeadFontTypeList.Location = new System.Drawing.Point(474, 82);
            this.HeadFontTypeList.Name = "HeadFontTypeList";
            this.HeadFontTypeList.Size = new System.Drawing.Size(121, 20);
            this.HeadFontTypeList.TabIndex = 47;
            // 
            // TitleFontTypeList
            // 
            this.TitleFontTypeList.FormattingEnabled = true;
            this.TitleFontTypeList.Items.AddRange(new object[] {
            "普通",
            "粗体",
            "斜体"});
            this.TitleFontTypeList.Location = new System.Drawing.Point(611, 82);
            this.TitleFontTypeList.Name = "TitleFontTypeList";
            this.TitleFontTypeList.Size = new System.Drawing.Size(121, 20);
            this.TitleFontTypeList.TabIndex = 48;
            // 
            // FieldFontTypeList
            // 
            this.FieldFontTypeList.FormattingEnabled = true;
            this.FieldFontTypeList.Items.AddRange(new object[] {
            "普通",
            "粗体",
            "斜体"});
            this.FieldFontTypeList.Location = new System.Drawing.Point(747, 82);
            this.FieldFontTypeList.Name = "FieldFontTypeList";
            this.FieldFontTypeList.Size = new System.Drawing.Size(121, 20);
            this.FieldFontTypeList.TabIndex = 49;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(439, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 50;
            this.label21.Text = "类型";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(428, 121);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 12);
            this.label24.TabIndex = 53;
            this.label24.Text = "行间距";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LineSpaceTB
            // 
            this.LineSpaceTB.Location = new System.Drawing.Point(474, 118);
            this.LineSpaceTB.Name = "LineSpaceTB";
            this.LineSpaceTB.Size = new System.Drawing.Size(67, 19);
            this.LineSpaceTB.TabIndex = 54;
            // 
            // SelectPrintItemButton
            // 
            this.SelectPrintItemButton.Location = new System.Drawing.Point(474, 147);
            this.SelectPrintItemButton.Name = "SelectPrintItemButton";
            this.SelectPrintItemButton.Size = new System.Drawing.Size(96, 23);
            this.SelectPrintItemButton.TabIndex = 55;
            this.SelectPrintItemButton.Text = "设定打印项目";
            this.SelectPrintItemButton.UseVisualStyleBackColor = true;
            this.SelectPrintItemButton.Click += new System.EventHandler(this.SelectPrintItemButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(93, 463);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(375, 19);
            this.textBox1.TabIndex = 57;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(12, 466);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(41, 12);
            this.label25.TabIndex = 56;
            this.label25.Text = "批次号";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 488);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(375, 19);
            this.textBox2.TabIndex = 59;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 491);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(44, 12);
            this.label26.TabIndex = 58;
            this.label26.Text = "Version";
            // 
            // LeftMarginTB
            // 
            this.LeftMarginTB.Location = new System.Drawing.Point(601, 115);
            this.LeftMarginTB.Name = "LeftMarginTB";
            this.LeftMarginTB.Size = new System.Drawing.Size(39, 19);
            this.LeftMarginTB.TabIndex = 60;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(554, 121);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(41, 12);
            this.label27.TabIndex = 61;
            this.label27.Text = "左边距";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label28.Location = new System.Drawing.Point(472, 9);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(31, 12);
            this.label28.TabIndex = 62;
            this.label28.Text = "台头";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label29.Location = new System.Drawing.Point(609, 9);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(31, 12);
            this.label29.TabIndex = 63;
            this.label29.Text = "标题";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label30.Location = new System.Drawing.Point(745, 9);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(31, 12);
            this.label30.TabIndex = 64;
            this.label30.Text = "项目";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(646, 119);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 65;
            this.label17.Text = "上边距";
            // 
            // TopMarginTB
            // 
            this.TopMarginTB.Location = new System.Drawing.Point(693, 115);
            this.TopMarginTB.Name = "TopMarginTB";
            this.TopMarginTB.Size = new System.Drawing.Size(49, 19);
            this.TopMarginTB.TabIndex = 66;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 561);
            this.Controls.Add(this.TopMarginTB);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.LeftMarginTB);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.SelectPrintItemButton);
            this.Controls.Add(this.LineSpaceTB);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.FieldFontTypeList);
            this.Controls.Add(this.TitleFontTypeList);
            this.Controls.Add(this.HeadFontTypeList);
            this.Controls.Add(this.FieldFontSizeList);
            this.Controls.Add(this.FieldFontList);
            this.Controls.Add(this.TitleFontSizeList);
            this.Controls.Add(this.TitleFontList);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.HeadFontSizeList);
            this.Controls.Add(this.HeadFontList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.VendorTextBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ProductDescTextBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.ProdModelTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.workflowID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.OrderIdTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PreviewLabel);
            this.Controls.Add(this.PrintLabelButton);
            this.Controls.Add(this.BoxStatusTextBox);
            this.Controls.Add(this.BoxCreateTimeTextBox);
            this.Controls.Add(this.BoxRealCountTextBox);
            this.Controls.Add(this.BoxCapacityTextBox);
            this.Controls.Add(this.BoxSNTextBox);
            this.Controls.Add(this.BoxTypeTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SearchBoxButton);
            this.Controls.Add(this.BoxSNSearchTextBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewLabel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BoxSNSearchTextBox;
        private System.Windows.Forms.Button SearchBoxButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox BoxTypeTextBox;
        private System.Windows.Forms.TextBox BoxSNTextBox;
        private System.Windows.Forms.TextBox BoxCapacityTextBox;
        private System.Windows.Forms.TextBox BoxRealCountTextBox;
        private System.Windows.Forms.TextBox BoxCreateTimeTextBox;
        private System.Windows.Forms.TextBox BoxStatusTextBox;
        private System.Windows.Forms.Button PrintLabelButton;
        private System.Windows.Forms.PictureBox PreviewLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox OrderIdTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox workflowID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ProdModelTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox VendorTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox ProductDescTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox HeadFontList;
        private System.Windows.Forms.ComboBox HeadFontSizeList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox TitleFontSizeList;
        private System.Windows.Forms.ComboBox TitleFontList;
        private System.Windows.Forms.ComboBox FieldFontSizeList;
        private System.Windows.Forms.ComboBox FieldFontList;
        private System.Windows.Forms.ComboBox HeadFontTypeList;
        private System.Windows.Forms.ComboBox TitleFontTypeList;
        private System.Windows.Forms.ComboBox FieldFontTypeList;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox LineSpaceTB;
        private System.Windows.Forms.Button SelectPrintItemButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox LeftMarginTB;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TopMarginTB;
    }
}