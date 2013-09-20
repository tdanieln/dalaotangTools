namespace mytools
{
    partial class FormMain
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
            this.buttonReadConfig = new System.Windows.Forms.Button();
            this.buttonDealwithCard = new System.Windows.Forms.Button();
            this.buttonQueryCard = new System.Windows.Forms.Button();
            this.button_close = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.CardOperate = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadCardinfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_CardApplying = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemXML = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemReadXML = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBankTest = new System.Windows.Forms.Button();
            this.buttonPrintTest = new System.Windows.Forms.Button();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.ReadOBU = new System.Windows.Forms.Button();
            this.buttonNetTest = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonComPrint = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReadConfig
            // 
            this.buttonReadConfig.Location = new System.Drawing.Point(324, 86);
            this.buttonReadConfig.Name = "buttonReadConfig";
            this.buttonReadConfig.Size = new System.Drawing.Size(98, 42);
            this.buttonReadConfig.TabIndex = 4;
            this.buttonReadConfig.Text = "系统配置";
            this.buttonReadConfig.UseVisualStyleBackColor = true;
            this.buttonReadConfig.Click += new System.EventHandler(this.buttonReadConfig_Click);
            // 
            // buttonDealwithCard
            // 
            this.buttonDealwithCard.Location = new System.Drawing.Point(43, 156);
            this.buttonDealwithCard.Name = "buttonDealwithCard";
            this.buttonDealwithCard.Size = new System.Drawing.Size(98, 42);
            this.buttonDealwithCard.TabIndex = 3;
            this.buttonDealwithCard.Text = "操作卡片";
            this.buttonDealwithCard.UseVisualStyleBackColor = true;
            this.buttonDealwithCard.Click += new System.EventHandler(this.buttonDealwithCard_Click);
            // 
            // buttonQueryCard
            // 
            this.buttonQueryCard.Location = new System.Drawing.Point(43, 86);
            this.buttonQueryCard.Name = "buttonQueryCard";
            this.buttonQueryCard.Size = new System.Drawing.Size(98, 42);
            this.buttonQueryCard.TabIndex = 2;
            this.buttonQueryCard.Text = "查询卡内信息";
            this.buttonQueryCard.UseVisualStyleBackColor = true;
            this.buttonQueryCard.Click += new System.EventHandler(this.buttonQueryCard_Click);
            // 
            // button_close
            // 
            this.button_close.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button_close.Location = new System.Drawing.Point(22, 384);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(592, 59);
            this.button_close.TabIndex = 1;
            this.button_close.Text = "关  闭";
            this.button_close.UseVisualStyleBackColor = false;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(2, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 23);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CardOperate,
            this.MenuItemXML});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(916, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // CardOperate
            // 
            this.CardOperate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReadCardinfoToolStripMenuItem,
            this.MenuItem_CardApplying});
            this.CardOperate.Name = "CardOperate";
            this.CardOperate.Size = new System.Drawing.Size(68, 21);
            this.CardOperate.Text = "卡片操作";
            // 
            // ReadCardinfoToolStripMenuItem
            // 
            this.ReadCardinfoToolStripMenuItem.Name = "ReadCardinfoToolStripMenuItem";
            this.ReadCardinfoToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.ReadCardinfoToolStripMenuItem.Text = "读卡内信息";
            this.ReadCardinfoToolStripMenuItem.Click += new System.EventHandler(this.ReadCardinfoToolStripMenuItem_Click);
            // 
            // MenuItem_CardApplying
            // 
            this.MenuItem_CardApplying.Name = "MenuItem_CardApplying";
            this.MenuItem_CardApplying.Size = new System.Drawing.Size(136, 22);
            this.MenuItem_CardApplying.Text = "对卡片操作";
            this.MenuItem_CardApplying.Click += new System.EventHandler(this.MenuItem_CardApplying_Click);
            // 
            // MenuItemXML
            // 
            this.MenuItemXML.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemReadXML});
            this.MenuItemXML.Name = "MenuItemXML";
            this.MenuItemXML.Size = new System.Drawing.Size(106, 21);
            this.MenuItemXML.Text = "XML文件的读写";
            // 
            // MenuItemReadXML
            // 
            this.MenuItemReadXML.Name = "MenuItemReadXML";
            this.MenuItemReadXML.Size = new System.Drawing.Size(114, 22);
            this.MenuItemReadXML.Text = "读XML";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "测试程序";
            // 
            // buttonBankTest
            // 
            this.buttonBankTest.Location = new System.Drawing.Point(189, 86);
            this.buttonBankTest.Name = "buttonBankTest";
            this.buttonBankTest.Size = new System.Drawing.Size(98, 42);
            this.buttonBankTest.TabIndex = 6;
            this.buttonBankTest.Text = "银行测试";
            this.buttonBankTest.UseVisualStyleBackColor = true;
            this.buttonBankTest.Click += new System.EventHandler(this.buttonBankTest_Click);
            // 
            // buttonPrintTest
            // 
            this.buttonPrintTest.Location = new System.Drawing.Point(189, 156);
            this.buttonPrintTest.Name = "buttonPrintTest";
            this.buttonPrintTest.Size = new System.Drawing.Size(98, 42);
            this.buttonPrintTest.TabIndex = 7;
            this.buttonPrintTest.Text = "打印测试";
            this.buttonPrintTest.UseVisualStyleBackColor = true;
            this.buttonPrintTest.Click += new System.EventHandler(this.buttonPrintTest_Click);
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // ReadOBU
            // 
            this.ReadOBU.Location = new System.Drawing.Point(43, 234);
            this.ReadOBU.Name = "ReadOBU";
            this.ReadOBU.Size = new System.Drawing.Size(98, 42);
            this.ReadOBU.TabIndex = 8;
            this.ReadOBU.Text = "读系统信息";
            this.ReadOBU.UseVisualStyleBackColor = true;
            this.ReadOBU.Click += new System.EventHandler(this.buttonOBU_Click);
            // 
            // buttonNetTest
            // 
            this.buttonNetTest.Location = new System.Drawing.Point(324, 156);
            this.buttonNetTest.Name = "buttonNetTest";
            this.buttonNetTest.Size = new System.Drawing.Size(98, 42);
            this.buttonNetTest.TabIndex = 9;
            this.buttonNetTest.Text = "网络测试";
            this.buttonNetTest.UseVisualStyleBackColor = true;
            this.buttonNetTest.Click += new System.EventHandler(this.buttonNetTest_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(189, 234);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(98, 42);
            this.buttonTest.TabIndex = 10;
            this.buttonTest.Text = "随改随用的测试";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonComPrint
            // 
            this.buttonComPrint.Location = new System.Drawing.Point(324, 234);
            this.buttonComPrint.Name = "buttonComPrint";
            this.buttonComPrint.Size = new System.Drawing.Size(98, 42);
            this.buttonComPrint.TabIndex = 11;
            this.buttonComPrint.Text = "串口打印机测试";
            this.buttonComPrint.UseVisualStyleBackColor = true;
            this.buttonComPrint.Click += new System.EventHandler(this.buttonComPrint_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 467);
            this.Controls.Add(this.buttonComPrint);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonNetTest);
            this.Controls.Add(this.ReadOBU);
            this.Controls.Add(this.buttonPrintTest);
            this.Controls.Add(this.buttonBankTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonReadConfig);
            this.Controls.Add(this.buttonDealwithCard);
            this.Controls.Add(this.buttonQueryCard);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "主界面";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CardOperate;
        private System.Windows.Forms.ToolStripMenuItem ReadCardinfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_CardApplying;
        private System.Windows.Forms.ToolStripMenuItem MenuItemXML;
        private System.Windows.Forms.ToolStripMenuItem MenuItemReadXML;
        private System.Windows.Forms.Button buttonQueryCard;
        private System.Windows.Forms.Button buttonDealwithCard;
        private System.Windows.Forms.Button buttonReadConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBankTest;
        private System.Windows.Forms.Button buttonPrintTest;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.Button ReadOBU;
        private System.Windows.Forms.Button buttonNetTest;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonComPrint;
    }
}