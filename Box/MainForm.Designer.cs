namespace Box
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入箱号";
            // 
            // BoxSNSearchTextBox
            // 
            this.BoxSNSearchTextBox.Location = new System.Drawing.Point(93, 45);
            this.BoxSNSearchTextBox.Name = "BoxSNSearchTextBox";
            this.BoxSNSearchTextBox.Size = new System.Drawing.Size(289, 19);
            this.BoxSNSearchTextBox.TabIndex = 1;
            this.BoxSNSearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoxSNSearchTextBox_KeyDown);
            // 
            // SearchBoxButton
            // 
            this.SearchBoxButton.Location = new System.Drawing.Point(398, 43);
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
            this.BoxTypeTextBox.Size = new System.Drawing.Size(289, 19);
            this.BoxTypeTextBox.TabIndex = 9;
            // 
            // BoxSNTextBox
            // 
            this.BoxSNTextBox.Location = new System.Drawing.Point(93, 144);
            this.BoxSNTextBox.Name = "BoxSNTextBox";
            this.BoxSNTextBox.ReadOnly = true;
            this.BoxSNTextBox.Size = new System.Drawing.Size(289, 19);
            this.BoxSNTextBox.TabIndex = 10;
            // 
            // BoxCapacityTextBox
            // 
            this.BoxCapacityTextBox.Location = new System.Drawing.Point(93, 171);
            this.BoxCapacityTextBox.Name = "BoxCapacityTextBox";
            this.BoxCapacityTextBox.ReadOnly = true;
            this.BoxCapacityTextBox.Size = new System.Drawing.Size(289, 19);
            this.BoxCapacityTextBox.TabIndex = 11;
            // 
            // BoxRealCountTextBox
            // 
            this.BoxRealCountTextBox.Location = new System.Drawing.Point(93, 198);
            this.BoxRealCountTextBox.Name = "BoxRealCountTextBox";
            this.BoxRealCountTextBox.ReadOnly = true;
            this.BoxRealCountTextBox.Size = new System.Drawing.Size(289, 19);
            this.BoxRealCountTextBox.TabIndex = 12;
            // 
            // BoxCreateTimeTextBox
            // 
            this.BoxCreateTimeTextBox.Location = new System.Drawing.Point(93, 225);
            this.BoxCreateTimeTextBox.Name = "BoxCreateTimeTextBox";
            this.BoxCreateTimeTextBox.ReadOnly = true;
            this.BoxCreateTimeTextBox.Size = new System.Drawing.Size(289, 19);
            this.BoxCreateTimeTextBox.TabIndex = 13;
            // 
            // BoxStatusTextBox
            // 
            this.BoxStatusTextBox.Location = new System.Drawing.Point(93, 252);
            this.BoxStatusTextBox.Name = "BoxStatusTextBox";
            this.BoxStatusTextBox.ReadOnly = true;
            this.BoxStatusTextBox.Size = new System.Drawing.Size(289, 19);
            this.BoxStatusTextBox.TabIndex = 14;
            // 
            // PrintLabelButton
            // 
            this.PrintLabelButton.Location = new System.Drawing.Point(476, 418);
            this.PrintLabelButton.Name = "PrintLabelButton";
            this.PrintLabelButton.Size = new System.Drawing.Size(75, 23);
            this.PrintLabelButton.TabIndex = 15;
            this.PrintLabelButton.Text = "打印标签";
            this.PrintLabelButton.UseVisualStyleBackColor = true;
            this.PrintLabelButton.Click += new System.EventHandler(this.PrintLabelButton_Click);
            // 
            // PreviewLabel
            // 
            this.PreviewLabel.Location = new System.Drawing.Point(411, 119);
            this.PreviewLabel.Name = "PreviewLabel";
            this.PreviewLabel.Size = new System.Drawing.Size(400, 280);
            this.PreviewLabel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviewLabel.TabIndex = 16;
            this.PreviewLabel.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(629, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OrderIdTextBox
            // 
            this.OrderIdTextBox.Location = new System.Drawing.Point(93, 278);
            this.OrderIdTextBox.Name = "OrderIdTextBox";
            this.OrderIdTextBox.ReadOnly = true;
            this.OrderIdTextBox.Size = new System.Drawing.Size(289, 19);
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
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 304);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(289, 19);
            this.textBox2.TabIndex = 21;
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
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(93, 382);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(289, 19);
            this.textBox3.TabIndex = 27;
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
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(289, 19);
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
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(289, 19);
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
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(93, 460);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(289, 19);
            this.textBox6.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 463);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 32;
            this.label13.Text = "供应商";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(93, 434);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(289, 19);
            this.textBox7.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 437);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 30;
            this.label14.Text = "装箱数量";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(93, 408);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(289, 19);
            this.textBox8.TabIndex = 29;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 561);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBox2);
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label15;
    }
}