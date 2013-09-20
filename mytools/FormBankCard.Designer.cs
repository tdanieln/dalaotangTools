namespace mytools
{
    partial class FormBankCard
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
            this.BankCardSign = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxQuantityOfSignedCard = new System.Windows.Forms.TextBox();
            this.button_Sign = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonReplaceCard = new System.Windows.Forms.Button();
            this.buttonReadNewCard = new System.Windows.Forms.Button();
            this.textBoxNewCardNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonReadOldCard = new System.Windows.Forms.Button();
            this.textBoxOldCardNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.BankCardSign.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BankCardSign
            // 
            this.BankCardSign.BackColor = System.Drawing.Color.Gainsboro;
            this.BankCardSign.Controls.Add(this.label2);
            this.BankCardSign.Controls.Add(this.textBoxQuantityOfSignedCard);
            this.BankCardSign.Controls.Add(this.button_Sign);
            this.BankCardSign.Controls.Add(this.textBox2);
            this.BankCardSign.Controls.Add(this.label1);
            this.BankCardSign.Controls.Add(this.textBox1);
            this.BankCardSign.Location = new System.Drawing.Point(12, 38);
            this.BankCardSign.Name = "BankCardSign";
            this.BankCardSign.Size = new System.Drawing.Size(270, 190);
            this.BankCardSign.TabIndex = 0;
            this.BankCardSign.TabStop = false;
            this.BankCardSign.Text = "银行卡批量签约";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "签约数量";
            // 
            // textBoxQuantityOfSignedCard
            // 
            this.textBoxQuantityOfSignedCard.Location = new System.Drawing.Point(126, 82);
            this.textBoxQuantityOfSignedCard.Name = "textBoxQuantityOfSignedCard";
            this.textBoxQuantityOfSignedCard.Size = new System.Drawing.Size(100, 21);
            this.textBoxQuantityOfSignedCard.TabIndex = 4;
            this.textBoxQuantityOfSignedCard.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // button_Sign
            // 
            this.button_Sign.Location = new System.Drawing.Point(92, 148);
            this.button_Sign.Name = "button_Sign";
            this.button_Sign.Size = new System.Drawing.Size(75, 23);
            this.button_Sign.TabIndex = 3;
            this.button_Sign.Text = "签约";
            this.button_Sign.UseVisualStyleBackColor = true;
            this.button_Sign.Click += new System.EventHandler(this.button_Sign_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(92, 24);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(96, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "09092300200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "卡号";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(194, 24);
            this.textBox1.MaxLength = 5;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 21);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightGray;
            this.groupBox2.Controls.Add(this.buttonReplaceCard);
            this.groupBox2.Controls.Add(this.buttonReadNewCard);
            this.groupBox2.Controls.Add(this.textBoxNewCardNo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.buttonReadOldCard);
            this.groupBox2.Controls.Add(this.textBoxOldCardNo);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(301, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 190);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // buttonReplaceCard
            // 
            this.buttonReplaceCard.Location = new System.Drawing.Point(99, 150);
            this.buttonReplaceCard.Name = "buttonReplaceCard";
            this.buttonReplaceCard.Size = new System.Drawing.Size(75, 23);
            this.buttonReplaceCard.TabIndex = 8;
            this.buttonReplaceCard.Text = "更 换";
            this.buttonReplaceCard.UseVisualStyleBackColor = true;
            this.buttonReplaceCard.Click += new System.EventHandler(this.buttonReplaceCard_Click);
            // 
            // buttonReadNewCard
            // 
            this.buttonReadNewCard.Location = new System.Drawing.Point(200, 63);
            this.buttonReadNewCard.Name = "buttonReadNewCard";
            this.buttonReadNewCard.Size = new System.Drawing.Size(61, 23);
            this.buttonReadNewCard.TabIndex = 7;
            this.buttonReadNewCard.Text = "读新卡";
            this.buttonReadNewCard.UseVisualStyleBackColor = true;
            this.buttonReadNewCard.Click += new System.EventHandler(this.buttonReadNewCard_Click);
            // 
            // textBoxNewCardNo
            // 
            this.textBoxNewCardNo.Location = new System.Drawing.Point(55, 64);
            this.textBoxNewCardNo.MaxLength = 16;
            this.textBoxNewCardNo.Name = "textBoxNewCardNo";
            this.textBoxNewCardNo.Size = new System.Drawing.Size(129, 21);
            this.textBoxNewCardNo.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "新卡号";
            // 
            // buttonReadOldCard
            // 
            this.buttonReadOldCard.Location = new System.Drawing.Point(200, 22);
            this.buttonReadOldCard.Name = "buttonReadOldCard";
            this.buttonReadOldCard.Size = new System.Drawing.Size(61, 23);
            this.buttonReadOldCard.TabIndex = 4;
            this.buttonReadOldCard.Text = "读旧卡";
            this.buttonReadOldCard.UseVisualStyleBackColor = true;
            this.buttonReadOldCard.Click += new System.EventHandler(this.buttonReadOldCard_Click);
            // 
            // textBoxOldCardNo
            // 
            this.textBoxOldCardNo.Location = new System.Drawing.Point(55, 24);
            this.textBoxOldCardNo.MaxLength = 16;
            this.textBoxOldCardNo.Name = "textBoxOldCardNo";
            this.textBoxOldCardNo.Size = new System.Drawing.Size(129, 21);
            this.textBoxOldCardNo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "旧卡号";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightGray;
            this.groupBox3.Location = new System.Drawing.Point(592, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(288, 190);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // button_Close
            // 
            this.button_Close.Location = new System.Drawing.Point(400, 424);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 3;
            this.button_Close.Text = "关闭";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // FormBankCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(903, 504);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BankCardSign);
            this.Name = "FormBankCard";
            this.Text = "数据库操作";
            this.BankCardSign.ResumeLayout(false);
            this.BankCardSign.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox BankCardSign;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Sign;
        private System.Windows.Forms.TextBox textBoxQuantityOfSignedCard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.Button buttonReadOldCard;
        private System.Windows.Forms.TextBox textBoxOldCardNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonReplaceCard;
        private System.Windows.Forms.Button buttonReadNewCard;
        private System.Windows.Forms.TextBox textBoxNewCardNo;
    }
}