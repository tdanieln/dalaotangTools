using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO.Ports;

namespace mytools
{
    public partial class FormConfig : Custom_Form
    {

        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            if (MytoolsIniConstant.DeviceCompany == -1 || MytoolsIniConstant.ConnectType == "" || 
                MytoolsIniConstant.ConnectPort == ""|| MytoolsIniConstant.EncryptionType == -1)
            {
                MessageBox.Show("获取配置文件信息失败！");
                return;
            }

            getIniConfig();

        }


        private void radioButtonJL_Click(object sender, EventArgs e)
        {
            labelPort.Text = "COM";
            GetComNo();
        }

        private void radioButtonGenvict_Click(object sender, EventArgs e)
        {
            labelPort.Text = "COM";
            GetComNo();
        }

        private void radioButtonZTE_Click(object sender, EventArgs e)
        {
            labelPort.Text = "USB";
            GetUsbNo();
        }

        private void radioButtonWatch_Click(object sender, EventArgs e)
        {
            labelPort.Text = "USB";
            GetUsbNo();
        }

        /// <summary>
        /// 获取COM端口号
        /// </summary>
        private void GetComNo()
        {
            //获取端口名称
            string[] port = SerialPort.GetPortNames();
            //获取端口数量
            int comQuantity = port.Length;

            string[] portName = new string[comQuantity];

            comboBoxPort.Items.Clear();

            for (int i = 0; i < comQuantity; i++)
            {
                portName[i] = port[i].Substring(3, port[i].Length-3);
                //初始化端口号combobox
                comboBoxPort.Items.Add(portName[i]);
            }
        }

        private void GetUsbNo()
        {
            //USB信息获取暂未解决
            comboBoxPort.Items.Clear();
            for (int i = 1; i < 8; i++)
            {
                comboBoxPort.Items.Add(i);
            }
        }

        private void buttonEnsure_Click(object sender, EventArgs e)
        {
            #region 保存加密机信息

            if (radioButtonGenvict.Checked)
            {
                MytoolsIniConstant.DeviceCompany = (int)ReaderVender.GENVICT;
                MytoolsIniConstant.ConnectType = DevConnMode.COM.ToString();
                MytoolsIniConstant.ConnectPort = comboBoxPort.Text;
            }
            else if (radioButtonJL.Checked)
            {
                MytoolsIniConstant.DeviceCompany = (int)ReaderVender.JULI;
                MytoolsIniConstant.ConnectType = DevConnMode.COM.ToString();
                MytoolsIniConstant.ConnectPort = comboBoxPort.Text;
            }
            else if (radioButtonWatch.Checked)
            {
                MytoolsIniConstant.DeviceCompany = (int)ReaderVender.WATCH;
                MytoolsIniConstant.ConnectType = DevConnMode.USB.ToString();
                MytoolsIniConstant.ConnectPort = comboBoxPort.Text;
            }
            else if (radioButtonZTE.Checked)
            {
                MytoolsIniConstant.DeviceCompany = (int)ReaderVender.ZTE;
                MytoolsIniConstant.ConnectType = DevConnMode.USB.ToString();
                MytoolsIniConstant.ConnectPort = comboBoxPort.Text;
            }
            #endregion 

            if (radioButtonSoftEncryption.Checked)
            {
                MytoolsIniConstant.EncryptionType = (int)EncryptionDllType.SoftEncryption;
            }
            else if (radioButtonLNEncryption.Checked)
            {
                MytoolsIniConstant.EncryptionType = (int)EncryptionDllType.EncryptionMachineLN;
            }
            else if (radioButtonBJEncryption.Checked)
            {
                MytoolsIniConstant.EncryptionType = (int)EncryptionDllType.EncryptionMachineBJ;
            }

            SaveReaderCardConfigFile();
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        private bool SaveReaderCardConfigFile()
        {
            string newCardNo = textBoxStockNo.Text + textBoxCardType.Text + textBoxCooperate.Text + textBoxRestCardNo.Text;

            if (textBoxCardNo.Text != newCardNo)
            {
                DialogResult dr = MessageBox.Show(string.Format("即将保存的卡号{0}与显示的卡号{1}不一致,\r\n确认将卡号保存为{0}?",newCardNo,textBoxCardNo.Text)
                ,"信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.No)
                    return false;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(MytoolsIniConstant.MytoolsConfigFileName);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("MytoolsConfig").ChildNodes; //获取Config节点的所有子节点

                foreach (XmlNode rootChildxn in nodeList)
                {
                    if (rootChildxn.Name == "ReadCardDeviceConfig")
                    {
                        XmlNodeList readCardDeviceConfigChildNode = rootChildxn.ChildNodes;

                        foreach (XmlNode xn in readCardDeviceConfigChildNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "DeviceCompany")
                            {
                                xe.InnerText = MytoolsIniConstant.DeviceCompany.ToString();
                            }
                            else if (xe.Name == "ConnectType")
                            {
                                xe.InnerText = MytoolsIniConstant.ConnectType.ToString();
                            }
                            else if (xe.Name == "ConnectPort")
                            {
                                xe.InnerText = MytoolsIniConstant.ConnectPort.ToString();
                            }
                        }
                    }
                    else if (rootChildxn.Name == "EncryptionInfo")
                    {
                        XmlNodeList readEncryptionInfoNode = rootChildxn.ChildNodes;

                        foreach (XmlNode xn in readEncryptionInfoNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "EncryptionType")
                            {
                                xe.InnerText = MytoolsIniConstant.EncryptionType.ToString();
                            }
                        }
                    }
                    else if (rootChildxn.Name == "TerminalInfo")
                    {
                        XmlNodeList readTerminalInfoNode = rootChildxn.ChildNodes;
                        foreach (XmlNode xn in readTerminalInfoNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "OperateNo")
                            {
                                if (textBoxOperatorNo.Text.Length == textBoxOperatorNo.MaxLength && FounctionResources.CheckIsNum(textBoxOperatorNo.Text))
                                {
                                    MytoolsIniConstant.OperatorNo = textBoxOperatorNo.Text;
                                    xe.InnerText = MytoolsIniConstant.OperatorNo;
                                    
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "TerminalNo")
                            {
                                if (textBoxTerminalNo.Text.Length == textBoxTerminalNo.MaxLength)
                                {
                                    MytoolsIniConstant.TerminalNo = textBoxTerminalNo.Text;
                                    xe.InnerText = MytoolsIniConstant.TerminalNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "PsamNo")
                            {
                                if (textBoxPSAMNo.Text.Length == textBoxPSAMNo.MaxLength)
                                {
                                    MytoolsIniConstant.PsamNo = textBoxPSAMNo.Text;
                                    xe.InnerText = MytoolsIniConstant.PsamNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "AgentNo")
                            {
                                if (textBoxAgentNo.Text.Length == textBoxTerminalNo.MaxLength)
                                {
                                    MytoolsIniConstant.AgentNo = textBoxAgentNo.Text;
                                    xe.InnerText = MytoolsIniConstant.AgentNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                        }
                    }
                    else if (rootChildxn.Name == "CardInfo")
                    {
                        XmlNodeList readCardInfoNode = rootChildxn.ChildNodes;
                        foreach (XmlNode xn in readCardInfoNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "IssueNetNo")
                            {
                                if (textBoxIssueNetNo.Text.Length == textBoxIssueNetNo.MaxLength && FounctionResources.CheckIsNum(textBoxIssueNetNo.Text))
                                {
                                    MytoolsIniConstant.IssuerNetNo = textBoxIssueNetNo.Text;
                                    xe.InnerText = MytoolsIniConstant.IssuerNetNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "StockNo")
                            {

                                if (textBoxStockNo.Text.Length == textBoxStockNo.MaxLength && FounctionResources.CheckIsNum(textBoxStockNo.Text))
                                {
                                    MytoolsIniConstant.StockNo = textBoxStockNo.Text;
                                    xe.InnerText = MytoolsIniConstant.StockNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "CardTpye")
                            {
                                if (textBoxCardType.Text.Length == textBoxCardType.MaxLength && FounctionResources.CheckIsNum(textBoxCardType.Text))
                                {
                                    MytoolsIniConstant.CardDefulType = Convert.ToInt32(textBoxCardType.Text);
                                    xe.InnerText = textBoxCardType.Text;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "CooperateEnterprise")
                            {
                                if (textBoxCooperate.Text.Length == textBoxCooperate.MaxLength && FounctionResources.CheckIsNum(textBoxCooperate.Text))
                                {
                                    MytoolsIniConstant.CooperateEnterprise = textBoxCooperate.Text;
                                    xe.InnerText = MytoolsIniConstant.CooperateEnterprise;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "RestCardNo")
                            {
                                if (textBoxRestCardNo.Text.Length == textBoxRestCardNo.MaxLength && FounctionResources.CheckIsNum(textBoxRestCardNo.Text))
                                {
                                    MytoolsIniConstant.RestCardNo = textBoxRestCardNo.Text;
                                    xe.InnerText = MytoolsIniConstant.RestCardNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "CardNo")
                            {
                                if (newCardNo.Length == textBoxCardNo.MaxLength && FounctionResources.CheckIsNum(newCardNo))
                                {
                                    MytoolsIniConstant.CardNo = newCardNo;
                                    xe.InnerText = MytoolsIniConstant.CardNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                        }

                    }
                    else if (rootChildxn.Name == "OperatorInfo")
                    {
                        XmlNodeList readOperatorInfoNode = rootChildxn.ChildNodes;
                        foreach (XmlNode xn in readOperatorInfoNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "IssuerNo")
                            {
                                if (textBoxIssuerNo.Text.Length == textBoxIssuerNo.MaxLength && FounctionResources.CheckIsNum(textBoxIssuerNo.Text))
                                {
                                    MytoolsIniConstant.IssuerNo = textBoxIssuerNo.Text;
                                    xe.InnerText = MytoolsIniConstant.IssuerNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "ClearNo")
                            {
                                if (textBoxClearNo.Text.Length == textBoxClearNo.MaxLength && FounctionResources.CheckIsNum(textBoxClearNo.Text))
                                {
                                    MytoolsIniConstant.ClearNo = textBoxClearNo.Text;
                                    xe.InnerText = MytoolsIniConstant.ClearNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                            else if (xe.Name == "ServerNo")
                            {
                                if (textBoxServerNo.Text.Length == textBoxServerNo.MaxLength && FounctionResources.CheckIsNum(textBoxServerNo.Text))
                                {
                                    MytoolsIniConstant.ServerNo = textBoxServerNo.Text;
                                    xe.InnerText = MytoolsIniConstant.ServerNo;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                        }
                    }
                    else if (rootChildxn.Name == "ServerInfo")
                    {
                        XmlNodeList readServerInfoNode = rootChildxn.ChildNodes;
                        foreach (XmlNode xn in readServerInfoNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "ServerIP")
                            {
                                if (textBoxServerIp.Text.Trim().Length == 0)
                                    throw new Exception(xe.Name + "长度或内容不正确");
                                MytoolsIniConstant.ServerIP = textBoxServerIp.Text;
                            }
                            else if (xe.Name == "ServerPort")
                            {
                                if (textBoxServerPort.Text.Trim().Length == 0 || FounctionResources.CheckIsNum(textBoxServerPort.Text))
                                {
                                    MytoolsIniConstant.ServerPort = textBoxServerPort.Text;
                                    xe.InnerText = MytoolsIniConstant.ServerPort;
                                }
                                else
                                    throw new Exception(xe.Name + "长度或内容不正确");
                            }
                        }
                    }
                }

                xmlDoc.Save(MytoolsIniConstant.MytoolsConfigFileName);//保存。

                //按之前保存的信息重新显示
                getIniConfig();

                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message +"\r\n设置保存失败,请检查设备配置文件是否损坏", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReView_Click(object sender, EventArgs e)
        {
            textBoxCardNo.Text = textBoxStockNo.Text + textBoxCardType.Text + textBoxCooperate.Text + textBoxRestCardNo.Text;
        }


        private void getIniConfig()
        {
            #region 读设备配置文件数据

            if (MytoolsIniConstant.DeviceCompany == (int)ReaderVender.GENVICT)
            {
                radioButtonGenvict.Checked = true;
                labelPort.Text = "COM";
            }
            else if (MytoolsIniConstant.DeviceCompany == (int)ReaderVender.JULI)
            {
                radioButtonJL.Checked = true;
                labelPort.Text = "COM";
            }
            else if (MytoolsIniConstant.DeviceCompany == (int)ReaderVender.WATCH)
            {
                radioButtonWatch.Checked = true;
                labelPort.Text = "USB";
            }
            else if (MytoolsIniConstant.DeviceCompany == (int)ReaderVender.ZTE)
            {
                radioButtonZTE.Checked = true;
                labelPort.Text = "USB";
            }
            #endregion

            #region 读加密机动态库资料
            if (MytoolsIniConstant.EncryptionType == (int)EncryptionDllType.SoftEncryption)
            {
                radioButtonSoftEncryption.Checked = true;
            }
            else if (MytoolsIniConstant.EncryptionType == (int)EncryptionDllType.EncryptionMachineLN)
            {
                radioButtonLNEncryption.Checked = true;
            }
            else if (MytoolsIniConstant.EncryptionType == (int)EncryptionDllType.EncryptionMachineBJ)
            {
                radioButtonBJEncryption.Checked = true;
            }
            #endregion

            //获取配置文件中的端口号
            comboBoxPort.Text = MytoolsIniConstant.ConnectPort;

            //卡网络编号
            textBoxIssueNetNo.Text = MytoolsIniConstant.IssuerNetNo;

            //卡批次号
            textBoxStockNo.Text = MytoolsIniConstant.StockNo;

            //卡类型
            textBoxCardType.Text = MytoolsIniConstant.CardDefulType.ToString();

            //合作企业号
            textBoxCooperate.Text = MytoolsIniConstant.CooperateEnterprise;

            //卡片剩余位数
            textBoxRestCardNo.Text = MytoolsIniConstant.RestCardNo;

            //虚拟操作员号
            textBoxOperatorNo.Text = MytoolsIniConstant.OperatorNo;

            //虚拟终端机编号
            textBoxTerminalNo.Text = MytoolsIniConstant.TerminalNo;

            //虚拟pSAM卡卡号
            textBoxPSAMNo.Text = MytoolsIniConstant.PsamNo;

            //虚拟营业厅编号
            textBoxAgentNo.Text = MytoolsIniConstant.AgentNo;

            //虚拟发行方编号
            textBoxIssuerNo.Text = MytoolsIniConstant.IssuerNo;

            //虚拟清分方编号
            textBoxClearNo.Text = MytoolsIniConstant.ClearNo;

            //虚拟服务方编号
            textBoxServerNo.Text = MytoolsIniConstant.ServerNo;

            //虚拟卡号
            textBoxCardNo.Text = MytoolsIniConstant.CardNo;

            //服务地址
            textBoxServerIp.Text = MytoolsIniConstant.ServerIP;

            //服务端口
            textBoxServerPort.Text = MytoolsIniConstant.ServerPort;
        }
    }
}
