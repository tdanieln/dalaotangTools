namespace mytools
{
    partial class Form0018
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnSequence = new System.Windows.Forms.ColumnHeader();
            this.columnKind = new System.Windows.Forms.ColumnHeader();
            this.columnTransProft = new System.Windows.Forms.ColumnHeader();
            this.columnTerminalNo = new System.Windows.Forms.ColumnHeader();
            this.columnTransDate = new System.Windows.Forms.ColumnHeader();
            this.columnTransTime = new System.Windows.Forms.ColumnHeader();
            this.columnAmount = new System.Windows.Forms.ColumnHeader();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Location = new System.Drawing.Point(41, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(791, 418);
            this.panel1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSequence,
            this.columnKind,
            this.columnTransProft,
            this.columnTerminalNo,
            this.columnTransDate,
            this.columnTransTime,
            this.columnAmount});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(791, 415);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnSequence
            // 
            this.columnSequence.Text = "序号";
            this.columnSequence.Width = 59;
            // 
            // columnKind
            // 
            this.columnKind.Text = "交易类型";
            this.columnKind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnKind.Width = 76;
            // 
            // columnTransProft
            // 
            this.columnTransProft.Text = "联机/脱机交易序号";
            this.columnTransProft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnTransProft.Width = 121;
            // 
            // columnTerminalNo
            // 
            this.columnTerminalNo.Text = "终端编号";
            this.columnTerminalNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnTerminalNo.Width = 118;
            // 
            // columnTransDate
            // 
            this.columnTransDate.Text = "交易日期";
            this.columnTransDate.Width = 142;
            // 
            // columnTransTime
            // 
            this.columnTransTime.Text = "交易时间";
            this.columnTransTime.Width = 145;
            // 
            // columnAmount
            // 
            this.columnAmount.Text = "交易金额（元)";
            this.columnAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnAmount.Width = 103;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.progressBar1.Location = new System.Drawing.Point(41, 479);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(791, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonClose.Location = new System.Drawing.Point(351, 535);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(117, 44);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Form0018
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 609);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Name = "Form0018";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnSequence;
        private System.Windows.Forms.ColumnHeader columnTransProft;
        private System.Windows.Forms.ColumnHeader columnAmount;
        private System.Windows.Forms.ColumnHeader columnKind;
        private System.Windows.Forms.ColumnHeader columnTerminalNo;
        private System.Windows.Forms.ColumnHeader columnTransDate;
        private System.Windows.Forms.ColumnHeader columnTransTime;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonClose;

    }
}