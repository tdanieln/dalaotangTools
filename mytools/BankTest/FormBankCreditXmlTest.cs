using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace mytools
{
    public partial class FormBankCreditXmlTest : Custom_Form
    {





        public FormBankCreditXmlTest()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 界面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormBankCreditXmlTest_Load(object sender, EventArgs e)
        {
            string BankCreditTestConfigFilePath = Directory.GetCurrentDirectory() + "\\BankTest\\";

            if (!Directory.Exists(BankCreditTestConfigFilePath))
                Directory.CreateDirectory(BankCreditTestConfigFilePath);

            string BankCreditTestConfigFileName = BankCreditTestConfigFilePath + "Config.xml";
            //如果没有找到配置文件
            if (!File.Exists(BankCreditTestConfigFileName))
            {
                FileStream fs = File.OpenWrite(BankCreditTestConfigFileName);

                //使用内存流创建数据
                using (MemoryStream ms = new MemoryStream())
                {
                    //用内存流，gb18030编码方式创建xml实例
                    XmlTextWriter xmlWriter = new XmlTextWriter(ms, Encoding.GetEncoding("gb18030"));
                    //设置
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteStartDocument();
                    {
                        //开始标记
                        xmlWriter.WriteStartElement("BankCreditXMLConfig");
                        {
                            //通信前置服务配置
                            xmlWriter.WriteStartElement("CommunicationServerConfig");
                            {
                                //通信前置服务器IP
                                xmlWriter.WriteElementString("ServerIP", "192.168.1.1");
                                //通信前置服务端口
                                xmlWriter.WriteElementString("ServerPort", "51888");
                            }
                            xmlWriter.WriteEndElement();
                            //用户信息
                            xmlWriter.WriteStartElement("UserInfo");
                            {
                                //默认为个人用户
                                xmlWriter.WriteElementString("UserType", ((int)UserTpye.Personal).ToString());
                                //默认账户类型记账
                                xmlWriter.WriteElementString("AccountType",((int) AccountType.CreditCard).ToString());
                                //用户名
                                xmlWriter.WriteElementString("UserName", "速通科技");
                                //简称
                                xmlWriter.WriteElementString("UserShortName", "速通");
                                //证件类型
                                xmlWriter.WriteElementString("IdentifyType", ((int)IdentifyType.ID).ToString());
                                //证件号
                                xmlWriter.WriteElementString("IdentifyNo", "110101190000000000");
                                //组织机构代码
                                xmlWriter.WriteElementString("OrganizationNo", "663804337");
                                //用户地址
                                xmlWriter.WriteElementString("UserAddress", "首发大厦");
                                //邮政编码
                                xmlWriter.WriteElementString("ZipCode", "100161");
                                //默认电子邮件地址
                                xmlWriter.WriteElementString("Email", "123@ktetc.com");
                                //联系人
                                xmlWriter.WriteElementString("Contact", "坑爹货");
                                //电话号码
                                xmlWriter.WriteElementString("TelephoneNo", "010-67617799");
                                //传真号
                                xmlWriter.WriteElementString("FaxNo", "67617799");
                                //移动电话号码
                                xmlWriter.WriteElementString("MobilePhone", "13500000000");
                                //该卡片对应的账户信息
                                xmlWriter.WriteElementString("AccountNo", "00000");
                                //该卡片对应的账户信息
                                xmlWriter.WriteElementString("ContractCardNo", "0000000000");
                            }
                            xmlWriter.WriteEndElement();
                            xmlWriter.WriteStartElement("CardNo");
                            {
                                //要使用的第一张卡号
                                xmlWriter.WriteElementString("FirstCardNo", "0716220000000000");
                                //要使用的第二张卡号
                                xmlWriter.WriteElementString("SecondCardNo", "0716220000000000");
                                //要使用的第三张卡号
                                xmlWriter.WriteElementString("ThirdCardNo", "0716220000000000");
                                //要使用的第四张卡号
                                xmlWriter.WriteElementString("FourthCardNo", "0716220000000000");
                                //要使用的第五张卡号
                                xmlWriter.WriteElementString("FifthCardNo", "0716220000000000");
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    fs.Write(FounctionResources.StreamToBytes(ms), 0, (int)ms.Length);
                    fs.Flush();
                    xmlWriter.Close();
                    ms.Close();
                    fs.Close();
                }
            }

            try
            {
                //读取配置文件
                XmlTextReader xtr = new XmlTextReader(BankCreditTestConfigFileName);

                while (xtr.Read())
                {
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ServerIP")
                    {
                        textBoxServerIp.Text = xtr.ReadString();
                        continue;
                    }
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ServerPort")
                    {
                        textBoxServerPort.Text = xtr.ReadString();
                        continue;
                    }
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "UserName")
                    {
                        textBoxUserName.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "UserType")
                    {
                        if (Convert.ToInt32(xtr.ReadString()) == (int)UserTpye.Personal)
                        {
                            radioPersonal.Checked = true;
                            radioCompany.Checked = false;
                        }
                        else
                        {
                            radioCompany.Checked = true;
                            radioPersonal.Checked = false;
                        }
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "AccountType")
                    {
                        if (Convert.ToInt32(xtr.ReadString()) == (int)AccountType.PurchaseCard)
                        {
                            radioButtonCredit.Checked = false;
                            radioButtonPurchase.Checked = true;
                        }
                        else
                        {
                            radioButtonPurchase.Checked = false;
                            radioButtonCredit.Checked = true;
                        }
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "UserName")
                    {
                        textBoxUserName.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "UserShortName")
                    {
                        textBoxUserShortName.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "IdentifyType")
                    {
                        DataTable dt = new DataTable();
                        DataRow[] dr = new DataRow[6];

                        dr[0] = dt.NewRow();
                        dr[1] = dt.NewRow();
                        dr[2] = dt.NewRow();
                        dr[3] = dt.NewRow();
                        dr[4] = dt.NewRow();
                        dr[5] = dt.NewRow();

                        dt.Columns.Add("证件类型", typeof(string));
                        dt.Columns.Add("数值", typeof(int));

                        dr[0]["证件类型"] = "身份证";
                        dr[0]["数值"] = (int)IdentifyType.ID;
                        dt.Rows.Add(dr[0]);

                        dr[1]["证件类型"] = "军官证";
                        dr[1]["数值"] = (int)IdentifyType.OFFICER;
                        dt.Rows.Add(dr[1]);

                        dr[2]["证件类型"] = "护照";
                        dr[2]["数值"] = (int)IdentifyType.PASSPORT;
                        dt.Rows.Add(dr[2]);

                        dr[3]["证件类型"] = "入境证";
                        dr[3]["数值"] = (int)IdentifyType.IMMIGRATION;
                        dt.Rows.Add(dr[3]);

                        dr[4]["证件类型"] = "临时身份证";
                        dr[4]["数值"] = (int)IdentifyType.TEMP_ID;
                        dt.Rows.Add(dr[4]);

                        dr[5]["证件类型"] = "驾驶证";
                        dr[5]["数值"] = (int)IdentifyType.DRIVER_LICENSE;
                        dt.Rows.Add(dr[5]);

                        comboBoxIdentifyType.DataSource = dt;
                        comboBoxIdentifyType.DisplayMember = "证件类型";
                        comboBoxIdentifyType.ValueMember = "数值";

                        if (Convert.ToInt32(xtr.ReadString()) == (int)IdentifyType.ID)
                        {
                            comboBoxIdentifyType.SelectedText = "身份证";
                            comboBoxIdentifyType.SelectedValue = (int)IdentifyType.ID;
                        }
                        else if (Convert.ToInt32(xtr.ReadString()) == (int)IdentifyType.OFFICER)
                        {
                            comboBoxIdentifyType.SelectedText = "军官证";
                            comboBoxIdentifyType.SelectedValue = (int)IdentifyType.OFFICER;
                        }
                        else if (Convert.ToInt32(xtr.ReadString()) == (int)IdentifyType.PASSPORT)
                        {
                            comboBoxIdentifyType.SelectedText = "护照";
                            comboBoxIdentifyType.SelectedValue = (int)IdentifyType.PASSPORT;
                        }
                        else if (Convert.ToInt32(xtr.ReadString()) == (int)IdentifyType.IMMIGRATION)
                        {
                            comboBoxIdentifyType.SelectedText = "入境证";
                            comboBoxIdentifyType.SelectedValue = (int)IdentifyType.IMMIGRATION;
                        }
                        else if (Convert.ToInt32(xtr.ReadString()) == (int)IdentifyType.TEMP_ID)
                        {
                            comboBoxIdentifyType.SelectedText = "临时身份证";
                            comboBoxIdentifyType.SelectedValue = (int)IdentifyType.PASSPORT;
                        }
                        else if (Convert.ToInt32(xtr.ReadString()) == (int)IdentifyType.DRIVER_LICENSE)
                        {
                            comboBoxIdentifyType.SelectedText = "驾照";
                            comboBoxIdentifyType.SelectedValue = (int)IdentifyType.DRIVER_LICENSE;
                        }
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "IdentifyNo")
                    {
                        textBoxIdentifyNo.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "OrganizationNo")
                    {
                        textBoxOrganization.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "UserAddress")
                    {
                        textBoxUserAddress.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ZipCode")
                    {
                        textBoxZipCode.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Email")
                    {
                        textBoxEmail.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Contact")
                    {
                        textBoxContact.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "TelephoneNo")
                    {
                        textBoxTel.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "FaxNo")
                    {
                        textBoxFaxNo.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "MobilePhone")
                    {
                        textBoxMobilePhone.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ContractCardNo")
                    {
                        textBoxContractCard.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "AccountNo")
                    {
                        textBoxAccountNo.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "FirstCardNo")
                    {
                        textBoxFirstCard.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "SecondCardNo")
                    {
                        textBoxLastAddCard.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ThirdCardNo")
                    {
                        textBoxFirstRemoveCard.Text = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "FourthCardNo")
                    {
                        textBoxLastRemoveCard.Text = xtr.ReadString();
                        continue;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("" + err.Message);
                return;
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void buttonSaveUserInfo_Click(object sender, EventArgs e)
        {
            SaveUserInfoConfig();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        private bool SaveUserInfoConfig()
        {
            try
            {
                string BankCreditTestConfigFilePath = Directory.GetCurrentDirectory() + "\\BankTest\\";

                string BankCreditTestConfigFileName = BankCreditTestConfigFilePath + "Config.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(BankCreditTestConfigFileName);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("BankCreditXMLConfig").ChildNodes; //获取Config节点的所有子节点
                foreach (XmlNode rootChildxn in nodeList)
                {
                    if (rootChildxn.Name == "UserInfo")
                    {
                        XmlNodeList readCardDeviceConfigChildNode = rootChildxn.ChildNodes;

                        foreach (XmlNode xn in readCardDeviceConfigChildNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "UserType")
                            {
                                xe.InnerText = radioPersonal.Checked == true ? ((int)UserTpye.Personal).ToString() : ((int)UserTpye.EnterPrise).ToString();
                            }
                            else if (xe.Name == "AccountType")
                            {
                                xe.InnerText = radioButtonPurchase.Checked == true ? ((int)AccountType.PurchaseCard).ToString() : ((int)AccountType.CreditCard).ToString();
                            }
                            else if (xe.Name == "UserName")
                            {
                                xe.InnerText = textBoxUserName.Text;
                            }
                            else if (xe.Name == "UserShortName")
                            {
                                xe.InnerText = textBoxUserShortName.Text;
                            }
                            else if (xe.Name == "IdentifyType")
                            {
                                xe.InnerText = comboBoxIdentifyType.SelectedValue.ToString();
                            }
                            else if (xe.Name == "IdentifyNo")
                            {
                                xe.InnerText = textBoxIdentifyNo.Text;
                            }
                            else if (xe.Name == "OrganizationNo")
                            {
                                xe.InnerText = textBoxOrganization.Text;
                            }
                            else if (xe.Name == "ZipCode")
                            {
                                xe.InnerText = textBoxZipCode.Text;
                            }
                            else if (xe.Name == "Email")
                            {
                                xe.InnerText = textBoxEmail.Text;
                            }
                            else if (xe.Name == "Contact")
                            {
                                xe.InnerText = textBoxContact.Text;
                            }
                            else if (xe.Name == "TelephoneNo")
                            {
                                xe.InnerText = textBoxTel.Text;
                            }
                            else if (xe.Name == "FaxNo")
                            {
                                xe.InnerText = textBoxFaxNo.Text;
                            }
                            else if (xe.Name == "MobilePhone")
                            {
                                xe.InnerText = textBoxMobilePhone.Text;
                            }
                            else if (xe.Name == "ContractCardNo")
                            {
                                xe.InnerText = textBoxContractCard.Text;
                            }
                            else if (xe.Name == "AccountNo")
                            {
                                xe.InnerText = textBoxAccountNo.Text;
                            }
                        }
                    }
                }

                xmlDoc.Save(BankCreditTestConfigFileName);//保存。

                MessageBox.Show("配置文件保存成功");
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
            return true;
        }



        private bool SaveServerInfoConfig()
        {
            try
            {
                string BankCreditTestConfigFilePath = Directory.GetCurrentDirectory() + "\\BankTest\\";

                string BankCreditTestConfigFileName = BankCreditTestConfigFilePath + "Config.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(BankCreditTestConfigFileName);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("BankCreditXMLConfig").ChildNodes; //获取Config节点的所有子节点
                foreach (XmlNode rootChildxn in nodeList)
                {
                    if (rootChildxn.Name == "CommunicationServerConfig")
                    {
                        XmlNodeList readCardDeviceConfigChildNode = rootChildxn.ChildNodes;

                        foreach (XmlNode xn in readCardDeviceConfigChildNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "ServerIP")
                            {
                                xe.InnerText = textBoxServerIp.Text;
                            }
                            else if (xe.Name == "ServerPoft")
                            {
                                xe.InnerText = textBoxServerPort.Text;
                            }
                        }
                    }
                }

                xmlDoc.Save(BankCreditTestConfigFileName);//保存。

                MessageBox.Show("配置文件保存成功");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
            return true;
        }


        private bool SaveCardInfoConfig()
        {
            try
            {
                string BankCreditTestConfigFilePath = Directory.GetCurrentDirectory() + "\\BankTest\\";

                string BankCreditTestConfigFileName = BankCreditTestConfigFilePath + "Config.xml";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(BankCreditTestConfigFileName);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("BankCreditXMLConfig").ChildNodes; //获取Config节点的所有子节点
                foreach (XmlNode rootChildxn in nodeList)
                {
                    if (rootChildxn.Name == "CardNo")
                    {
                        XmlNodeList readCardDeviceConfigChildNode = rootChildxn.ChildNodes;

                        foreach (XmlNode xn in readCardDeviceConfigChildNode)//遍历所有子节点
                        {
                            XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                            if (xe.Name == "FirstCardNo")
                            {
                                xe.InnerText = textBoxFirstCard.Text;
                            }
                            else if (xe.Name == "SecondCardNo")
                            {
                                xe.InnerText = textBoxLastAddCard.Text;
                            }
                            else if (xe.Name == "ThirdCardNo")
                            {
                                xe.InnerText = textBoxFirstRemoveCard.Text;
                            }
                            else if (xe.Name == "FourthCardNo")
                            {
                                xe.InnerText = textBoxLastRemoveCard.Text;
                            }
                        }
                    }
                }

                xmlDoc.Save(BankCreditTestConfigFileName);//保存。

                MessageBox.Show("配置文件保存成功");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
            return true;
        }






        private void buttonContract_Click(object sender, EventArgs e)
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "16";

            BankCreditXMLTest bcxt = new BankCreditXMLTest();

            RetCodeBankXml rcbx;
            //
            rcbx = bcxt.ContractCard(radioPersonal.Checked == true ? ((int)UserTpye.Personal).ToString() : ((int)UserTpye.EnterPrise).ToString()
                , radioButtonPurchase.Checked == true ? ((int)AccountType.PurchaseCard).ToString() : ((int)AccountType.CreditCard).ToString()
                , textBoxUserName.Text
                , textBoxUserShortName.Text
                , comboBoxIdentifyType.SelectedValue.ToString()
                , textBoxIdentifyNo.Text
                , textBoxOrganization.Text
                , textBoxUserAddress.Text
                , textBoxZipCode.Text
                , textBoxEmail.Text
                , textBoxContact.Text
                , textBoxTel.Text
                , textBoxFaxNo.Text
                , textBoxMobilePhone.Text
                , textBoxContractCard.Text
                , orderNo
                , textBoxServerIp.Text
                , textBoxServerPort.Text);

            if (rcbx.errCode == 0)
            {
                textBoxAccountNo.Text = rcbx.accountNo;
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n" + "账号为：{2} \r\n", rcbx.errCode, rcbx.errMessage, rcbx.accountNo);
            }
            else
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);

        }


        private void buttonChangeInfo_Click(object sender, EventArgs e)
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "16";

            BankCreditXMLTest bcxt = new BankCreditXMLTest();

            RetCodeBankXml rcbx = bcxt.ContractInfoChange(textBoxAccountNo.Text
                                                         , radioPersonal.Checked == true ? ((int)UserTpye.Personal).ToString() : ((int)UserTpye.EnterPrise).ToString()
                                                         , textBoxUserName.Text
                                                         , textBoxUserShortName.Text
                                                         , comboBoxIdentifyType.SelectedValue.ToString()
                                                         , textBoxIdentifyNo.Text
                                                         , textBoxOrganization.Text
                                                         , textBoxUserAddress.Text
                                                         , textBoxZipCode.Text
                                                         , textBoxEmail.Text
                                                         , textBoxContact.Text
                                                         , textBoxTel.Text
                                                         , textBoxFaxNo.Text
                                                         , textBoxMobilePhone.Text
                                                         , orderNo
                                                         , textBoxServerIp.Text
                                                         , textBoxServerPort.Text);

            if (rcbx.errCode == 0)
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n" + "账号为：{2} \r\n", rcbx.errCode, rcbx.errMessage, rcbx.accountNo);
            }
            else
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);
        }

        private void buttonAddAndRemove_Click(object sender, EventArgs e)
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "16";

            BankCreditXMLTest bcxt = new BankCreditXMLTest();

            RetCodeBankXml rcbx = bcxt.AddAndRemoveCard(textBoxAccountNo.Text
                                                       , textBoxFirstCard.Text
                                                       , textBoxLastAddCard.Text
                                                       , textBoxFirstRemoveCard.Text
                                                       , textBoxLastRemoveCard.Text
                                                       , orderNo
                                                       , textBoxServerIp.Text
                                                       , textBoxServerPort.Text);
            if (rcbx.errCode == 0)
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n" + "成功加卡数量为{2},成功减卡数量为{3}\r\n", rcbx.errCode, rcbx.errMessage, rcbx.numCardAdded
                ,rcbx.numCardDroped);
            }
            else
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);
        }

        private void buttonForbidden_Click(object sender, EventArgs e)
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "16";

            BankCreditXMLTest bcxt = new BankCreditXMLTest();

            RetCodeBankXml rcbx = bcxt.SetCardForbidden(textBoxContractCard.Text
                                                       , textBoxAccountNo.Text
                                                       , orderNo
                                                       , textBoxServerIp.Text
                                                       , textBoxServerPort.Text);
            if (rcbx.errCode == 0)
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}\r\n返回消息为：{1}\r\n日志号为{2}\r\n", rcbx.errCode, rcbx.errMessage, rcbx.logID);
            }
            else
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);
        }

        private void buttonCancelForbidden_Click(object sender, EventArgs e)
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "16";

            BankCreditXMLTest bcxt = new BankCreditXMLTest();

            RetCodeBankXml rcbx = bcxt.CardCancelForibidden(textBoxContractCard.Text
                                                           , textBoxAccountNo.Text
                                                           , orderNo
                                                           , textBoxServerIp.Text
                                                           , textBoxServerPort.Text);
            if (rcbx.errCode == 0)
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}\r\n返回消息为：{1}\r\n日志号为{2}\r\n", rcbx.errCode, rcbx.errMessage, rcbx.logID);
            }
            else
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);
 
        }

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "16";

            BankCreditXMLTest bcxt = new BankCreditXMLTest();

            byte[] random = new byte[4]{0,0,0,0};

            RetCodeBankXml rcbx = bcxt.CardReplaceStart(textBoxAccountNo.Text
                                                      , textBoxContractCard.Text
                                                      , textBoxFirstCard.Text
                                                      , random
                                                      , orderNo
                                                      , textBoxServerIp.Text
                                                      , textBoxServerPort.Text);
            if (rcbx.errCode == 0)
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}\r\n返回消息为：{1}\r\n，数据长度为：{2}\r\n，数据内容为：{3}\r\n，MAC码为{4}\r\n"
                                    , rcbx.errCode, rcbx.errMessage, rcbx.dataInfoLength, rcbx.dataInfo, rcbx.MAC1.ToString());
            }
            else
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);
                textBoxShow.Text = textBoxShow.Text + "记账卡更换start方法失败！";
                return;
            }

            rcbx = bcxt.CardReplaceEnsure(textBoxAccountNo.Text
                                        , textBoxContractCard.Text
                                        , textBoxFirstCard.Text
                                        , random
                                        , orderNo
                                        , textBoxServerIp.Text
                                        , textBoxServerPort.Text);
            if (rcbx.errCode == 0)
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}\r\n返回消息为：{1}\r\n，数据长度为：{2}\r\n，数据内容为：{3}\r\n，MAC码为{4}\r\n"
                                    , rcbx.errCode, rcbx.errMessage, rcbx.dataInfoLength, rcbx.dataInfo, rcbx.MAC1.ToString());
            }
            else
            {
                textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);
                textBoxShow.Text = textBoxShow.Text + "记账卡更换start方法失败！";
                return;
            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "16";

            BankCreditXMLTest bcxt = new BankCreditXMLTest();

            RetCodeBankXml rcbx = bcxt.PrepareClearAccount(textBoxAccountNo.Text
                                                          , orderNo
                                                          , textBoxServerIp.Text
                                                          , textBoxServerPort.Text);

            textBoxShow.Text = textBoxShow.Text + string.Format("返回码为：{0}" + "\r\n" + "返回消息为：{1}" + "\r\n", rcbx.errCode, rcbx.errMessage);


        }

        private void buttonSaveServerInfo_Click(object sender, EventArgs e)
        {
            SaveServerInfoConfig();
        }

        private void buttonSaveCardInfo_Click(object sender, EventArgs e)
        {
            SaveCardInfoConfig();
        }




    }
}
