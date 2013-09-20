namespace mytools
{
    partial class FormCardApplying
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
            this.groupBoxLoad = new System.Windows.Forms.GroupBox();
            this.buttonFFFFFFF = new System.Windows.Forms.Button();
            this.buttonCardPurchase = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxForPurchase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLoadAmount = new System.Windows.Forms.TextBox();
            this.buttonCardLoad = new System.Windows.Forms.Button();
            this.buttonInCardQuery = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxAfterCardOperate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxBalanceBeforOperate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonCappPurchase = new System.Windows.Forms.Button();
            this.groupBoxLoad.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLoad
            // 
            this.groupBoxLoad.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBoxLoad.Controls.Add(this.buttonCappPurchase);
            this.groupBoxLoad.Controls.Add(this.buttonFFFFFFF);
            this.groupBoxLoad.Controls.Add(this.buttonCardPurchase);
            this.groupBoxLoad.Controls.Add(this.label4);
            this.groupBoxLoad.Controls.Add(this.textBoxForPurchase);
            this.groupBoxLoad.Controls.Add(this.label3);
            this.groupBoxLoad.Controls.Add(this.label2);
            this.groupBoxLoad.Controls.Add(this.label1);
            this.groupBoxLoad.Controls.Add(this.textBoxLoadAmount);
            this.groupBoxLoad.Controls.Add(this.buttonCardLoad);
            this.groupBoxLoad.Controls.Add(this.buttonInCardQuery);
            this.groupBoxLoad.Location = new System.Drawing.Point(52, 67);
            this.groupBoxLoad.Name = "groupBoxLoad";
            this.groupBoxLoad.Size = new System.Drawing.Size(347, 237);
            this.groupBoxLoad.TabIndex = 0;
            this.groupBoxLoad.TabStop = false;
            this.groupBoxLoad.Text = "功能选择";
            // 
            // buttonFFFFFFF
            // 
            this.buttonFFFFFFF.Location = new System.Drawing.Point(231, 145);
            this.buttonFFFFFFF.Name = "buttonFFFFFFF";
            this.buttonFFFFFFF.Size = new System.Drawing.Size(85, 29);
            this.buttonFFFFFFF.TabIndex = 9;
            this.buttonFFFFFFF.Text = "FFFFFFFF";
            this.buttonFFFFFFF.UseVisualStyleBackColor = true;
            this.buttonFFFFFFF.Click += new System.EventHandler(this.buttonFFFFFFF_Click);
            // 
            // buttonCardPurchase
            // 
            this.buttonCardPurchase.Location = new System.Drawing.Point(231, 105);
            this.buttonCardPurchase.Name = "buttonCardPurchase";
            this.buttonCardPurchase.Size = new System.Drawing.Size(85, 29);
            this.buttonCardPurchase.TabIndex = 8;
            this.buttonCardPurchase.Text = "传统消费";
            this.buttonCardPurchase.UseVisualStyleBackColor = true;
            this.buttonCardPurchase.Click += new System.EventHandler(this.buttonCardPurchase_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "元";
            // 
            // textBoxForPurchase
            // 
            this.textBoxForPurchase.Location = new System.Drawing.Point(114, 111);
            this.textBoxForPurchase.Name = "textBoxForPurchase";
            this.textBoxForPurchase.Size = new System.Drawing.Size(88, 21);
            this.textBoxForPurchase.TabIndex = 6;
            this.textBoxForPurchase.TextChanged += new System.EventHandler(this.textBoxForPurchase_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "欲消费金额为：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "欲圈存金额为";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "元";
            // 
            // textBoxLoadAmount
            // 
            this.textBoxLoadAmount.Location = new System.Drawing.Point(114, 75);
            this.textBoxLoadAmount.Name = "textBoxLoadAmount";
            this.textBoxLoadAmount.Size = new System.Drawing.Size(88, 21);
            this.textBoxLoadAmount.TabIndex = 2;
            this.textBoxLoadAmount.TextChanged += new System.EventHandler(this.textBoxLoadAmount_TextChanged);
            // 
            // buttonCardLoad
            // 
            this.buttonCardLoad.Location = new System.Drawing.Point(231, 70);
            this.buttonCardLoad.Name = "buttonCardLoad";
            this.buttonCardLoad.Size = new System.Drawing.Size(85, 29);
            this.buttonCardLoad.TabIndex = 1;
            this.buttonCardLoad.Text = "圈存";
            this.buttonCardLoad.UseVisualStyleBackColor = true;
            this.buttonCardLoad.Click += new System.EventHandler(this.buttonCardLoad_Click);
            // 
            // buttonInCardQuery
            // 
            this.buttonInCardQuery.Location = new System.Drawing.Point(21, 20);
            this.buttonInCardQuery.Name = "buttonInCardQuery";
            this.buttonInCardQuery.Size = new System.Drawing.Size(108, 34);
            this.buttonInCardQuery.TabIndex = 0;
            this.buttonInCardQuery.Text = "查询卡内信息";
            this.buttonInCardQuery.UseVisualStyleBackColor = true;
            this.buttonInCardQuery.Click += new System.EventHandler(this.buttonInCardQuery_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.groupBox1.Controls.Add(this.textBoxAfterCardOperate);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBoxBalanceBeforOperate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(461, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 419);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "初始化信息";
            // 
            // textBoxAfterCardOperate
            // 
            this.textBoxAfterCardOperate.Location = new System.Drawing.Point(286, 23);
            this.textBoxAfterCardOperate.Name = "textBoxAfterCardOperate";
            this.textBoxAfterCardOperate.Size = new System.Drawing.Size(77, 21);
            this.textBoxAfterCardOperate.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(367, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "元";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(201, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "操作后金额";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(167, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "元";
            // 
            // textBoxBalanceBeforOperate
            // 
            this.textBoxBalanceBeforOperate.Location = new System.Drawing.Point(91, 23);
            this.textBoxBalanceBeforOperate.Name = "textBoxBalanceBeforOperate";
            this.textBoxBalanceBeforOperate.Size = new System.Drawing.Size(70, 21);
            this.textBoxBalanceBeforOperate.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "操作前金额";
            // 
            // buttonQuit
            // 
            this.buttonQuit.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonQuit.Location = new System.Drawing.Point(12, 492);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(928, 95);
            this.buttonQuit.TabIndex = 17;
            this.buttonQuit.Text = "退   出";
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // buttonCappPurchase
            // 
            this.buttonCappPurchase.Location = new System.Drawing.Point(231, 189);
            this.buttonCappPurchase.Name = "buttonCappPurchase";
            this.buttonCappPurchase.Size = new System.Drawing.Size(85, 29);
            this.buttonCappPurchase.TabIndex = 10;
            this.buttonCappPurchase.Text = "复合消费";
            this.buttonCappPurchase.UseVisualStyleBackColor = true;
            this.buttonCappPurchase.Click += new System.EventHandler(this.buttonCappPurchase_Click);
            // 
            // FormCardApplying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 613);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxLoad);
            this.Name = "FormCardApplying";
            this.Text = "FormCardApplying";
            this.groupBoxLoad.ResumeLayout(false);
            this.groupBoxLoad.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLoad;
        private System.Windows.Forms.Button buttonInCardQuery;
        private System.Windows.Forms.Button buttonCardLoad;
        private System.Windows.Forms.TextBox textBoxLoadAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxAfterCardOperate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxBalanceBeforOperate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonCardPurchase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxForPurchase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Button buttonFFFFFFF;
        private System.Windows.Forms.Button buttonCappPurchase;
    }
}