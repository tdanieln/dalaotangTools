namespace mytools
{
    partial class FormConfig
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
            this.groupBoxServerInfo = new System.Windows.Forms.GroupBox();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.textBoxServerIp = new System.Windows.Forms.TextBox();
            this.labelServerIp = new System.Windows.Forms.Label();
            this.groupBoxOperatorInfo = new System.Windows.Forms.GroupBox();
            this.textBoxServerNo = new System.Windows.Forms.TextBox();
            this.labelServerNo = new System.Windows.Forms.Label();
            this.textBoxClearNo = new System.Windows.Forms.TextBox();
            this.labelClearNo = new System.Windows.Forms.Label();
            this.labelIssuerNo = new System.Windows.Forms.Label();
            this.textBoxIssuerNo = new System.Windows.Forms.TextBox();
            this.groupBoxTerminal = new System.Windows.Forms.GroupBox();
            this.labelAgentNo = new System.Windows.Forms.Label();
            this.textBoxAgentNo = new System.Windows.Forms.TextBox();
            this.textBoxPSAMNo = new System.Windows.Forms.TextBox();
            this.labelPSAM = new System.Windows.Forms.Label();
            this.textBoxTerminalNo = new System.Windows.Forms.TextBox();
            this.labelTerminalNo = new System.Windows.Forms.Label();
            this.labelOperatorNo = new System.Windows.Forms.Label();
            this.textBoxOperatorNo = new System.Windows.Forms.TextBox();
            this.groupBoxCardInfo = new System.Windows.Forms.GroupBox();
            this.buttonReView = new System.Windows.Forms.Button();
            this.textBoxCardNo = new System.Windows.Forms.TextBox();
            this.labelCardNo = new System.Windows.Forms.Label();
            this.textBoxRestCardNo = new System.Windows.Forms.TextBox();
            this.labelRestCardNo = new System.Windows.Forms.Label();
            this.labelCooperation = new System.Windows.Forms.Label();
            this.textBoxCooperate = new System.Windows.Forms.TextBox();
            this.textBoxCardType = new System.Windows.Forms.TextBox();
            this.labelCardTpye = new System.Windows.Forms.Label();
            this.textBoxStockNo = new System.Windows.Forms.TextBox();
            this.labelStockNo = new System.Windows.Forms.Label();
            this.labelCardNetNo = new System.Windows.Forms.Label();
            this.textBoxIssueNetNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonEnsure = new System.Windows.Forms.Button();
            this.groupBoxEncryption = new System.Windows.Forms.GroupBox();
            this.radioButtonBJEncryption = new System.Windows.Forms.RadioButton();
            this.radioButtonLNEncryption = new System.Windows.Forms.RadioButton();
            this.radioButtonSoftEncryption = new System.Windows.Forms.RadioButton();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.groupBoxReaderDevice = new System.Windows.Forms.GroupBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.radioButtonWatch = new System.Windows.Forms.RadioButton();
            this.radioButtonGenvict = new System.Windows.Forms.RadioButton();
            this.radioButtonZTE = new System.Windows.Forms.RadioButton();
            this.radioButtonJL = new System.Windows.Forms.RadioButton();
            this.groupBoxServerInfo.SuspendLayout();
            this.groupBoxOperatorInfo.SuspendLayout();
            this.groupBoxTerminal.SuspendLayout();
            this.groupBoxCardInfo.SuspendLayout();
            this.groupBoxEncryption.SuspendLayout();
            this.groupBoxReaderDevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxServerInfo
            // 
            this.groupBoxServerInfo.Controls.Add(this.textBoxServerPort);
            this.groupBoxServerInfo.Controls.Add(this.labelServerPort);
            this.groupBoxServerInfo.Controls.Add(this.textBoxServerIp);
            this.groupBoxServerInfo.Controls.Add(this.labelServerIp);
            this.groupBoxServerInfo.Location = new System.Drawing.Point(25, 161);
            this.groupBoxServerInfo.Name = "groupBoxServerInfo";
            this.groupBoxServerInfo.Size = new System.Drawing.Size(618, 54);
            this.groupBoxServerInfo.TabIndex = 12;
            this.groupBoxServerInfo.TabStop = false;
            this.groupBoxServerInfo.Text = "服务信息";
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.Location = new System.Drawing.Point(320, 16);
            this.textBoxServerPort.MaxLength = 16;
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(36, 21);
            this.textBoxServerPort.TabIndex = 21;
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(261, 21);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(53, 12);
            this.labelServerPort.TabIndex = 20;
            this.labelServerPort.Text = "服务端口";
            // 
            // textBoxServerIp
            // 
            this.textBoxServerIp.Location = new System.Drawing.Point(90, 16);
            this.textBoxServerIp.MaxLength = 16;
            this.textBoxServerIp.Name = "textBoxServerIp";
            this.textBoxServerIp.Size = new System.Drawing.Size(105, 21);
            this.textBoxServerIp.TabIndex = 19;
            // 
            // labelServerIp
            // 
            this.labelServerIp.AutoSize = true;
            this.labelServerIp.Location = new System.Drawing.Point(10, 22);
            this.labelServerIp.Name = "labelServerIp";
            this.labelServerIp.Size = new System.Drawing.Size(53, 12);
            this.labelServerIp.TabIndex = 0;
            this.labelServerIp.Text = "服务地址";
            // 
            // groupBoxOperatorInfo
            // 
            this.groupBoxOperatorInfo.Controls.Add(this.textBoxServerNo);
            this.groupBoxOperatorInfo.Controls.Add(this.labelServerNo);
            this.groupBoxOperatorInfo.Controls.Add(this.textBoxClearNo);
            this.groupBoxOperatorInfo.Controls.Add(this.labelClearNo);
            this.groupBoxOperatorInfo.Controls.Add(this.labelIssuerNo);
            this.groupBoxOperatorInfo.Controls.Add(this.textBoxIssuerNo);
            this.groupBoxOperatorInfo.Location = new System.Drawing.Point(25, 221);
            this.groupBoxOperatorInfo.Name = "groupBoxOperatorInfo";
            this.groupBoxOperatorInfo.Size = new System.Drawing.Size(618, 58);
            this.groupBoxOperatorInfo.TabIndex = 11;
            this.groupBoxOperatorInfo.TabStop = false;
            this.groupBoxOperatorInfo.Text = "虚拟参与方信息";
            // 
            // textBoxServerNo
            // 
            this.textBoxServerNo.Location = new System.Drawing.Point(493, 21);
            this.textBoxServerNo.MaxLength = 16;
            this.textBoxServerNo.Name = "textBoxServerNo";
            this.textBoxServerNo.Size = new System.Drawing.Size(90, 21);
            this.textBoxServerNo.TabIndex = 18;
            // 
            // labelServerNo
            // 
            this.labelServerNo.AutoSize = true;
            this.labelServerNo.Location = new System.Drawing.Point(419, 26);
            this.labelServerNo.Name = "labelServerNo";
            this.labelServerNo.Size = new System.Drawing.Size(65, 12);
            this.labelServerNo.TabIndex = 17;
            this.labelServerNo.Text = "服务方编号";
            // 
            // textBoxClearNo
            // 
            this.textBoxClearNo.Location = new System.Drawing.Point(291, 22);
            this.textBoxClearNo.MaxLength = 16;
            this.textBoxClearNo.Name = "textBoxClearNo";
            this.textBoxClearNo.Size = new System.Drawing.Size(101, 21);
            this.textBoxClearNo.TabIndex = 16;
            // 
            // labelClearNo
            // 
            this.labelClearNo.AutoSize = true;
            this.labelClearNo.Location = new System.Drawing.Point(220, 26);
            this.labelClearNo.Name = "labelClearNo";
            this.labelClearNo.Size = new System.Drawing.Size(65, 12);
            this.labelClearNo.TabIndex = 15;
            this.labelClearNo.Text = "清分方编号";
            // 
            // labelIssuerNo
            // 
            this.labelIssuerNo.AutoSize = true;
            this.labelIssuerNo.Location = new System.Drawing.Point(10, 26);
            this.labelIssuerNo.Name = "labelIssuerNo";
            this.labelIssuerNo.Size = new System.Drawing.Size(65, 12);
            this.labelIssuerNo.TabIndex = 14;
            this.labelIssuerNo.Text = "发行方编号";
            // 
            // textBoxIssuerNo
            // 
            this.textBoxIssuerNo.Location = new System.Drawing.Point(90, 22);
            this.textBoxIssuerNo.MaxLength = 16;
            this.textBoxIssuerNo.Name = "textBoxIssuerNo";
            this.textBoxIssuerNo.Size = new System.Drawing.Size(105, 21);
            this.textBoxIssuerNo.TabIndex = 13;
            // 
            // groupBoxTerminal
            // 
            this.groupBoxTerminal.Controls.Add(this.labelAgentNo);
            this.groupBoxTerminal.Controls.Add(this.textBoxAgentNo);
            this.groupBoxTerminal.Controls.Add(this.textBoxPSAMNo);
            this.groupBoxTerminal.Controls.Add(this.labelPSAM);
            this.groupBoxTerminal.Controls.Add(this.textBoxTerminalNo);
            this.groupBoxTerminal.Controls.Add(this.labelTerminalNo);
            this.groupBoxTerminal.Controls.Add(this.labelOperatorNo);
            this.groupBoxTerminal.Controls.Add(this.textBoxOperatorNo);
            this.groupBoxTerminal.Location = new System.Drawing.Point(25, 285);
            this.groupBoxTerminal.Name = "groupBoxTerminal";
            this.groupBoxTerminal.Size = new System.Drawing.Size(618, 54);
            this.groupBoxTerminal.TabIndex = 10;
            this.groupBoxTerminal.TabStop = false;
            this.groupBoxTerminal.Text = "虚拟终端信息";
            // 
            // labelAgentNo
            // 
            this.labelAgentNo.AutoSize = true;
            this.labelAgentNo.Location = new System.Drawing.Point(445, 21);
            this.labelAgentNo.Name = "labelAgentNo";
            this.labelAgentNo.Size = new System.Drawing.Size(53, 12);
            this.labelAgentNo.TabIndex = 12;
            this.labelAgentNo.Text = "营业厅号";
            // 
            // textBoxAgentNo
            // 
            this.textBoxAgentNo.Location = new System.Drawing.Point(500, 16);
            this.textBoxAgentNo.MaxLength = 12;
            this.textBoxAgentNo.Name = "textBoxAgentNo";
            this.textBoxAgentNo.Size = new System.Drawing.Size(112, 21);
            this.textBoxAgentNo.TabIndex = 11;
            // 
            // textBoxPSAMNo
            // 
            this.textBoxPSAMNo.Location = new System.Drawing.Point(344, 17);
            this.textBoxPSAMNo.MaxLength = 12;
            this.textBoxPSAMNo.Name = "textBoxPSAMNo";
            this.textBoxPSAMNo.Size = new System.Drawing.Size(95, 21);
            this.textBoxPSAMNo.TabIndex = 5;
            // 
            // labelPSAM
            // 
            this.labelPSAM.AutoSize = true;
            this.labelPSAM.Location = new System.Drawing.Point(301, 21);
            this.labelPSAM.Name = "labelPSAM";
            this.labelPSAM.Size = new System.Drawing.Size(41, 12);
            this.labelPSAM.TabIndex = 4;
            this.labelPSAM.Text = "PSAM号";
            // 
            // textBoxTerminalNo
            // 
            this.textBoxTerminalNo.Location = new System.Drawing.Point(190, 17);
            this.textBoxTerminalNo.MaxLength = 12;
            this.textBoxTerminalNo.Name = "textBoxTerminalNo";
            this.textBoxTerminalNo.Size = new System.Drawing.Size(100, 21);
            this.textBoxTerminalNo.TabIndex = 3;
            // 
            // labelTerminalNo
            // 
            this.labelTerminalNo.AutoSize = true;
            this.labelTerminalNo.Location = new System.Drawing.Point(137, 21);
            this.labelTerminalNo.Name = "labelTerminalNo";
            this.labelTerminalNo.Size = new System.Drawing.Size(53, 12);
            this.labelTerminalNo.TabIndex = 2;
            this.labelTerminalNo.Text = "终端机号";
            // 
            // labelOperatorNo
            // 
            this.labelOperatorNo.AutoSize = true;
            this.labelOperatorNo.Location = new System.Drawing.Point(8, 21);
            this.labelOperatorNo.Name = "labelOperatorNo";
            this.labelOperatorNo.Size = new System.Drawing.Size(53, 12);
            this.labelOperatorNo.TabIndex = 1;
            this.labelOperatorNo.Text = "操作员号";
            // 
            // textBoxOperatorNo
            // 
            this.textBoxOperatorNo.Location = new System.Drawing.Point(65, 17);
            this.textBoxOperatorNo.MaxLength = 6;
            this.textBoxOperatorNo.Name = "textBoxOperatorNo";
            this.textBoxOperatorNo.Size = new System.Drawing.Size(67, 21);
            this.textBoxOperatorNo.TabIndex = 0;
            // 
            // groupBoxCardInfo
            // 
            this.groupBoxCardInfo.Controls.Add(this.buttonReView);
            this.groupBoxCardInfo.Controls.Add(this.textBoxCardNo);
            this.groupBoxCardInfo.Controls.Add(this.labelCardNo);
            this.groupBoxCardInfo.Controls.Add(this.textBoxRestCardNo);
            this.groupBoxCardInfo.Controls.Add(this.labelRestCardNo);
            this.groupBoxCardInfo.Controls.Add(this.labelCooperation);
            this.groupBoxCardInfo.Controls.Add(this.textBoxCooperate);
            this.groupBoxCardInfo.Controls.Add(this.textBoxCardType);
            this.groupBoxCardInfo.Controls.Add(this.labelCardTpye);
            this.groupBoxCardInfo.Controls.Add(this.textBoxStockNo);
            this.groupBoxCardInfo.Controls.Add(this.labelStockNo);
            this.groupBoxCardInfo.Controls.Add(this.labelCardNetNo);
            this.groupBoxCardInfo.Controls.Add(this.textBoxIssueNetNo);
            this.groupBoxCardInfo.Location = new System.Drawing.Point(25, 343);
            this.groupBoxCardInfo.Name = "groupBoxCardInfo";
            this.groupBoxCardInfo.Size = new System.Drawing.Size(618, 93);
            this.groupBoxCardInfo.TabIndex = 9;
            this.groupBoxCardInfo.TabStop = false;
            this.groupBoxCardInfo.Text = "虚拟卡片配置";
            // 
            // buttonReView
            // 
            this.buttonReView.Location = new System.Drawing.Point(497, 49);
            this.buttonReView.Name = "buttonReView";
            this.buttonReView.Size = new System.Drawing.Size(71, 23);
            this.buttonReView.TabIndex = 25;
            this.buttonReView.Text = "预览卡号";
            this.buttonReView.UseVisualStyleBackColor = true;
            this.buttonReView.Click += new System.EventHandler(this.buttonReView_Click);
            // 
            // textBoxCardNo
            // 
            this.textBoxCardNo.Location = new System.Drawing.Point(150, 50);
            this.textBoxCardNo.MaxLength = 16;
            this.textBoxCardNo.Name = "textBoxCardNo";
            this.textBoxCardNo.ReadOnly = true;
            this.textBoxCardNo.Size = new System.Drawing.Size(334, 21);
            this.textBoxCardNo.TabIndex = 24;
            // 
            // labelCardNo
            // 
            this.labelCardNo.AutoSize = true;
            this.labelCardNo.Location = new System.Drawing.Point(88, 54);
            this.labelCardNo.Name = "labelCardNo";
            this.labelCardNo.Size = new System.Drawing.Size(53, 12);
            this.labelCardNo.TabIndex = 23;
            this.labelCardNo.Text = "虚拟卡号";
            // 
            // textBoxRestCardNo
            // 
            this.textBoxRestCardNo.Location = new System.Drawing.Point(502, 16);
            this.textBoxRestCardNo.MaxLength = 7;
            this.textBoxRestCardNo.Name = "textBoxRestCardNo";
            this.textBoxRestCardNo.Size = new System.Drawing.Size(48, 21);
            this.textBoxRestCardNo.TabIndex = 22;
            this.textBoxRestCardNo.Text = "0000000";
            // 
            // labelRestCardNo
            // 
            this.labelRestCardNo.AutoSize = true;
            this.labelRestCardNo.Location = new System.Drawing.Point(426, 20);
            this.labelRestCardNo.Name = "labelRestCardNo";
            this.labelRestCardNo.Size = new System.Drawing.Size(71, 12);
            this.labelRestCardNo.TabIndex = 21;
            this.labelRestCardNo.Text = "剩余7位卡号";
            // 
            // labelCooperation
            // 
            this.labelCooperation.AutoSize = true;
            this.labelCooperation.Location = new System.Drawing.Point(305, 20);
            this.labelCooperation.Name = "labelCooperation";
            this.labelCooperation.Size = new System.Drawing.Size(77, 12);
            this.labelCooperation.TabIndex = 20;
            this.labelCooperation.Text = "合作企业编号";
            // 
            // textBoxCooperate
            // 
            this.textBoxCooperate.Location = new System.Drawing.Point(384, 16);
            this.textBoxCooperate.MaxLength = 3;
            this.textBoxCooperate.Name = "textBoxCooperate";
            this.textBoxCooperate.Size = new System.Drawing.Size(36, 21);
            this.textBoxCooperate.TabIndex = 19;
            this.textBoxCooperate.Text = "000";
            // 
            // textBoxCardType
            // 
            this.textBoxCardType.Location = new System.Drawing.Point(263, 16);
            this.textBoxCardType.MaxLength = 2;
            this.textBoxCardType.Name = "textBoxCardType";
            this.textBoxCardType.Size = new System.Drawing.Size(28, 21);
            this.textBoxCardType.TabIndex = 18;
            this.textBoxCardType.Text = "22";
            // 
            // labelCardTpye
            // 
            this.labelCardTpye.AutoSize = true;
            this.labelCardTpye.Location = new System.Drawing.Point(207, 20);
            this.labelCardTpye.Name = "labelCardTpye";
            this.labelCardTpye.Size = new System.Drawing.Size(53, 12);
            this.labelCardTpye.TabIndex = 17;
            this.labelCardTpye.Text = "卡片类型";
            // 
            // textBoxStockNo
            // 
            this.textBoxStockNo.Location = new System.Drawing.Point(156, 16);
            this.textBoxStockNo.MaxLength = 4;
            this.textBoxStockNo.Name = "textBoxStockNo";
            this.textBoxStockNo.Size = new System.Drawing.Size(39, 21);
            this.textBoxStockNo.TabIndex = 16;
            this.textBoxStockNo.Text = "0716";
            // 
            // labelStockNo
            // 
            this.labelStockNo.AutoSize = true;
            this.labelStockNo.Location = new System.Drawing.Point(111, 20);
            this.labelStockNo.Name = "labelStockNo";
            this.labelStockNo.Size = new System.Drawing.Size(41, 12);
            this.labelStockNo.TabIndex = 15;
            this.labelStockNo.Text = "批次号";
            // 
            // labelCardNetNo
            // 
            this.labelCardNetNo.AutoSize = true;
            this.labelCardNetNo.Location = new System.Drawing.Point(12, 21);
            this.labelCardNetNo.Name = "labelCardNetNo";
            this.labelCardNetNo.Size = new System.Drawing.Size(53, 12);
            this.labelCardNetNo.TabIndex = 14;
            this.labelCardNetNo.Text = "网络编号";
            // 
            // textBoxIssueNetNo
            // 
            this.textBoxIssueNetNo.Location = new System.Drawing.Point(68, 16);
            this.textBoxIssueNetNo.MaxLength = 4;
            this.textBoxIssueNetNo.Name = "textBoxIssueNetNo";
            this.textBoxIssueNetNo.Size = new System.Drawing.Size(38, 21);
            this.textBoxIssueNetNo.TabIndex = 13;
            this.textBoxIssueNetNo.Text = "1108";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体-方正超大字符集", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "配置系统文件";
            // 
            // buttonEnsure
            // 
            this.buttonEnsure.Location = new System.Drawing.Point(30, 526);
            this.buttonEnsure.Name = "buttonEnsure";
            this.buttonEnsure.Size = new System.Drawing.Size(618, 42);
            this.buttonEnsure.TabIndex = 7;
            this.buttonEnsure.Text = "确认提交信息";
            this.buttonEnsure.UseVisualStyleBackColor = true;
            this.buttonEnsure.Click += new System.EventHandler(this.buttonEnsure_Click);
            // 
            // groupBoxEncryption
            // 
            this.groupBoxEncryption.Controls.Add(this.radioButtonBJEncryption);
            this.groupBoxEncryption.Controls.Add(this.radioButtonLNEncryption);
            this.groupBoxEncryption.Controls.Add(this.radioButtonSoftEncryption);
            this.groupBoxEncryption.Location = new System.Drawing.Point(25, 100);
            this.groupBoxEncryption.Name = "groupBoxEncryption";
            this.groupBoxEncryption.Size = new System.Drawing.Size(618, 54);
            this.groupBoxEncryption.TabIndex = 5;
            this.groupBoxEncryption.TabStop = false;
            this.groupBoxEncryption.Text = "加密机配置";
            // 
            // radioButtonBJEncryption
            // 
            this.radioButtonBJEncryption.AutoSize = true;
            this.radioButtonBJEncryption.Location = new System.Drawing.Point(255, 25);
            this.radioButtonBJEncryption.Name = "radioButtonBJEncryption";
            this.radioButtonBJEncryption.Size = new System.Drawing.Size(101, 16);
            this.radioButtonBJEncryption.TabIndex = 2;
            this.radioButtonBJEncryption.TabStop = true;
            this.radioButtonBJEncryption.Text = "北京加密机88K";
            this.radioButtonBJEncryption.UseVisualStyleBackColor = true;
            // 
            // radioButtonLNEncryption
            // 
            this.radioButtonLNEncryption.AutoSize = true;
            this.radioButtonLNEncryption.Location = new System.Drawing.Point(449, 25);
            this.radioButtonLNEncryption.Name = "radioButtonLNEncryption";
            this.radioButtonLNEncryption.Size = new System.Drawing.Size(101, 16);
            this.radioButtonLNEncryption.TabIndex = 1;
            this.radioButtonLNEncryption.TabStop = true;
            this.radioButtonLNEncryption.Text = "辽宁加密机88K";
            this.radioButtonLNEncryption.UseVisualStyleBackColor = true;
            // 
            // radioButtonSoftEncryption
            // 
            this.radioButtonSoftEncryption.AutoSize = true;
            this.radioButtonSoftEncryption.Location = new System.Drawing.Point(15, 25);
            this.radioButtonSoftEncryption.Name = "radioButtonSoftEncryption";
            this.radioButtonSoftEncryption.Size = new System.Drawing.Size(161, 16);
            this.radioButtonSoftEncryption.TabIndex = 0;
            this.radioButtonSoftEncryption.TabStop = true;
            this.radioButtonSoftEncryption.Text = "软加密机_北京实验室测试";
            this.radioButtonSoftEncryption.UseVisualStyleBackColor = true;
            // 
            // buttonQuit
            // 
            this.buttonQuit.Location = new System.Drawing.Point(30, 574);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(618, 39);
            this.buttonQuit.TabIndex = 4;
            this.buttonQuit.Text = "退   出";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // groupBoxReaderDevice
            // 
            this.groupBoxReaderDevice.Controls.Add(this.labelPort);
            this.groupBoxReaderDevice.Controls.Add(this.comboBoxPort);
            this.groupBoxReaderDevice.Controls.Add(this.radioButtonWatch);
            this.groupBoxReaderDevice.Controls.Add(this.radioButtonGenvict);
            this.groupBoxReaderDevice.Controls.Add(this.radioButtonZTE);
            this.groupBoxReaderDevice.Controls.Add(this.radioButtonJL);
            this.groupBoxReaderDevice.Location = new System.Drawing.Point(25, 43);
            this.groupBoxReaderDevice.Name = "groupBoxReaderDevice";
            this.groupBoxReaderDevice.Size = new System.Drawing.Size(618, 51);
            this.groupBoxReaderDevice.TabIndex = 3;
            this.groupBoxReaderDevice.TabStop = false;
            this.groupBoxReaderDevice.Text = "读卡器配置";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(447, 22);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(23, 12);
            this.labelPort.TabIndex = 6;
            this.labelPort.Text = "COM";
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(476, 16);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(121, 20);
            this.comboBoxPort.TabIndex = 5;
            // 
            // radioButtonWatch
            // 
            this.radioButtonWatch.AutoSize = true;
            this.radioButtonWatch.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.radioButtonWatch.Location = new System.Drawing.Point(275, 20);
            this.radioButtonWatch.Name = "radioButtonWatch";
            this.radioButtonWatch.Size = new System.Drawing.Size(47, 16);
            this.radioButtonWatch.TabIndex = 4;
            this.radioButtonWatch.TabStop = true;
            this.radioButtonWatch.Text = "握奇";
            this.radioButtonWatch.UseVisualStyleBackColor = false;
            this.radioButtonWatch.Click += new System.EventHandler(this.radioButtonWatch_Click);
            // 
            // radioButtonGenvict
            // 
            this.radioButtonGenvict.AutoSize = true;
            this.radioButtonGenvict.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.radioButtonGenvict.Location = new System.Drawing.Point(15, 20);
            this.radioButtonGenvict.Name = "radioButtonGenvict";
            this.radioButtonGenvict.Size = new System.Drawing.Size(47, 16);
            this.radioButtonGenvict.TabIndex = 1;
            this.radioButtonGenvict.TabStop = true;
            this.radioButtonGenvict.Text = "金溢";
            this.radioButtonGenvict.UseVisualStyleBackColor = false;
            this.radioButtonGenvict.Click += new System.EventHandler(this.radioButtonGenvict_Click);
            // 
            // radioButtonZTE
            // 
            this.radioButtonZTE.AutoSize = true;
            this.radioButtonZTE.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.radioButtonZTE.Location = new System.Drawing.Point(189, 20);
            this.radioButtonZTE.Name = "radioButtonZTE";
            this.radioButtonZTE.Size = new System.Drawing.Size(47, 16);
            this.radioButtonZTE.TabIndex = 3;
            this.radioButtonZTE.TabStop = true;
            this.radioButtonZTE.Text = "中兴";
            this.radioButtonZTE.UseVisualStyleBackColor = false;
            this.radioButtonZTE.Click += new System.EventHandler(this.radioButtonZTE_Click);
            // 
            // radioButtonJL
            // 
            this.radioButtonJL.AutoSize = true;
            this.radioButtonJL.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.radioButtonJL.Location = new System.Drawing.Point(105, 20);
            this.radioButtonJL.Name = "radioButtonJL";
            this.radioButtonJL.Size = new System.Drawing.Size(47, 16);
            this.radioButtonJL.TabIndex = 2;
            this.radioButtonJL.TabStop = true;
            this.radioButtonJL.Text = "聚利";
            this.radioButtonJL.UseVisualStyleBackColor = false;
            this.radioButtonJL.Click += new System.EventHandler(this.radioButtonJL_Click);
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(672, 643);
            this.Controls.Add(this.groupBoxServerInfo);
            this.Controls.Add(this.groupBoxOperatorInfo);
            this.Controls.Add(this.groupBoxTerminal);
            this.Controls.Add(this.groupBoxCardInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEnsure);
            this.Controls.Add(this.groupBoxEncryption);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.groupBoxReaderDevice);
            this.Name = "FormConfig";
            this.Text = "配置系统文件";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBoxServerInfo.ResumeLayout(false);
            this.groupBoxServerInfo.PerformLayout();
            this.groupBoxOperatorInfo.ResumeLayout(false);
            this.groupBoxOperatorInfo.PerformLayout();
            this.groupBoxTerminal.ResumeLayout(false);
            this.groupBoxTerminal.PerformLayout();
            this.groupBoxCardInfo.ResumeLayout(false);
            this.groupBoxCardInfo.PerformLayout();
            this.groupBoxEncryption.ResumeLayout(false);
            this.groupBoxEncryption.PerformLayout();
            this.groupBoxReaderDevice.ResumeLayout(false);
            this.groupBoxReaderDevice.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonGenvict;
        private System.Windows.Forms.RadioButton radioButtonWatch;
        private System.Windows.Forms.RadioButton radioButtonZTE;
        private System.Windows.Forms.RadioButton radioButtonJL;
        private System.Windows.Forms.GroupBox groupBoxReaderDevice;
        private System.Windows.Forms.Button buttonEnsure;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.GroupBox groupBoxEncryption;
        private System.Windows.Forms.RadioButton radioButtonLNEncryption;
        private System.Windows.Forms.RadioButton radioButtonSoftEncryption;
        private System.Windows.Forms.RadioButton radioButtonBJEncryption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxCardInfo;
        private System.Windows.Forms.GroupBox groupBoxTerminal;
        private System.Windows.Forms.TextBox textBoxTerminalNo;
        private System.Windows.Forms.Label labelTerminalNo;
        private System.Windows.Forms.Label labelOperatorNo;
        private System.Windows.Forms.TextBox textBoxOperatorNo;
        private System.Windows.Forms.TextBox textBoxPSAMNo;
        private System.Windows.Forms.Label labelPSAM;
        private System.Windows.Forms.Label labelAgentNo;
        private System.Windows.Forms.TextBox textBoxAgentNo;
        private System.Windows.Forms.Label labelCooperation;
        private System.Windows.Forms.TextBox textBoxCardType;
        private System.Windows.Forms.Label labelCardTpye;
        private System.Windows.Forms.TextBox textBoxStockNo;
        private System.Windows.Forms.Label labelStockNo;
        private System.Windows.Forms.Label labelCardNetNo;
        private System.Windows.Forms.TextBox textBoxIssueNetNo;
        private System.Windows.Forms.Button buttonReView;
        private System.Windows.Forms.TextBox textBoxCardNo;
        private System.Windows.Forms.Label labelCardNo;
        private System.Windows.Forms.TextBox textBoxRestCardNo;
        private System.Windows.Forms.Label labelRestCardNo;
        private System.Windows.Forms.GroupBox groupBoxOperatorInfo;
        private System.Windows.Forms.TextBox textBoxServerNo;
        private System.Windows.Forms.Label labelServerNo;
        private System.Windows.Forms.TextBox textBoxClearNo;
        private System.Windows.Forms.Label labelClearNo;
        private System.Windows.Forms.Label labelIssuerNo;
        private System.Windows.Forms.TextBox textBoxIssuerNo;
        private System.Windows.Forms.TextBox textBoxCooperate;
        private System.Windows.Forms.GroupBox groupBoxServerInfo;
        private System.Windows.Forms.Label labelServerIp;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.Label labelServerPort;
        private System.Windows.Forms.TextBox textBoxServerIp;
    }
}