namespace Box
{
    partial class PrintWindow
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.OuterBoxPrintButton = new System.Windows.Forms.Button();
            this.OuterBoxIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InnerBoxPrintButton = new System.Windows.Forms.Button();
            this.InnerBoxIdTextBox = new System.Windows.Forms.TextBox();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProgressBar,
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 330);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(519, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ProgressBar
            // 
            this.ProgressBar.MarqueeAnimationSpeed = 0;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(100, 16);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(402, 17);
            this.StatusLabel.Spring = true;
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OuterBoxPrintButton
            // 
            this.OuterBoxPrintButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.OuterBoxPrintButton.Location = new System.Drawing.Point(403, 192);
            this.OuterBoxPrintButton.Name = "OuterBoxPrintButton";
            this.OuterBoxPrintButton.Size = new System.Drawing.Size(113, 23);
            this.OuterBoxPrintButton.TabIndex = 7;
            this.OuterBoxPrintButton.Text = "打印";
            this.OuterBoxPrintButton.UseVisualStyleBackColor = true;
            // 
            // OuterBoxIdTextBox
            // 
            this.OuterBoxIdTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.OuterBoxIdTextBox.Location = new System.Drawing.Point(3, 192);
            this.OuterBoxIdTextBox.Name = "OuterBoxIdTextBox";
            this.OuterBoxIdTextBox.Size = new System.Drawing.Size(394, 19);
            this.OuterBoxIdTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(513, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "外箱箱号";
            // 
            // InnerBoxPrintButton
            // 
            this.InnerBoxPrintButton.AutoSize = true;
            this.InnerBoxPrintButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.InnerBoxPrintButton.Location = new System.Drawing.Point(403, 143);
            this.InnerBoxPrintButton.Name = "InnerBoxPrintButton";
            this.InnerBoxPrintButton.Size = new System.Drawing.Size(113, 23);
            this.InnerBoxPrintButton.TabIndex = 4;
            this.InnerBoxPrintButton.Text = "打印";
            this.InnerBoxPrintButton.UseVisualStyleBackColor = true;
            // 
            // InnerBoxIdTextBox
            // 
            this.InnerBoxIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerBoxIdTextBox.Location = new System.Drawing.Point(3, 143);
            this.InnerBoxIdTextBox.Name = "InnerBoxIdTextBox";
            this.InnerBoxIdTextBox.Size = new System.Drawing.Size(394, 19);
            this.InnerBoxIdTextBox.TabIndex = 3;
            // 
            // SearchTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.SearchTextBox, 2);
            this.SearchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchTextBox.Location = new System.Drawing.Point(3, 63);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(513, 19);
            this.SearchTextBox.TabIndex = 2;
            this.SearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(513, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "内箱箱号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(513, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入模块查找箱号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.SearchTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.InnerBoxIdTextBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.InnerBoxPrintButton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.OuterBoxIdTextBox, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.OuterBoxPrintButton, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(519, 352);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // PrintWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 352);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "标签打印";
            this.Load += new System.EventHandler(this.PrintWindow_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.Button OuterBoxPrintButton;
        private System.Windows.Forms.TextBox OuterBoxIdTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.TextBox InnerBoxIdTextBox;
        private System.Windows.Forms.Button InnerBoxPrintButton;
    }
}