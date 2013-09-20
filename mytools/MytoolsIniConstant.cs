//****  该文件为全局状态变量，熟悉xml的使用后，可以用读取xml文件的方法来替换
//****
//****

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace mytools
{
    class MytoolsIniConstant
    {

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string MytoolsConfigPath = "";

        /// <summary>
        /// 配置文件文件名
        /// </summary>
        public static string MytoolsConfigFileName = "";

        #region 读卡器设备配置文件变量

        /// <summary>
        /// 读卡器设备厂商
        /// </summary>
        public static int DeviceCompany = -1;

        /// <summary>
        /// 读卡器连接类型
        /// </summary>
        public static string ConnectType = "";

        /// <summary>
        /// 读卡器连接端口
        /// </summary>
        public static string ConnectPort = "";

        #endregion

        #region 卡片信息

        /// <summary>
        /// 卡号长度
        /// </summary>
        public static int CardNoLength = -1;

        /// <summary>
        /// 发行方标识
        /// </summary>
        public static string IssuerNetNo = "";

        /// <summary>
        /// 卡批次号
        /// </summary>
        public static string StockNo = "";

        /// <summary>
        /// 卡片默认
        /// </summary>
        public static int CardDefulType = -1;

        /// <summary>
        /// 合作银行
        /// </summary>
        public static string CooperateEnterprise = "";

        /// <summary>
        /// 剩余7位编号
        /// </summary>
        public static string RestCardNo = "";

        /// <summary>
        /// 16位卡号,默认全0
        /// </summary>
        public static string CardNo = "0000000000000000";

        #endregion


        /// <summary>
        /// 加密机类型
        /// </summary>
        public static int EncryptionType = -1;

        #region 终端相应信息

        /// <summary>
        /// 营业厅编号
        /// </summary>
        public static string AgentNo = "";

        /// <summary>
        /// 终端机编号
        /// </summary>
        public static string TerminalNo = "";

        /// <summary>
        /// 操作员号
        /// </summary>
        public static string OperatorNo = "";

        /// <summary>
        /// PSAM卡号
        /// </summary>
        public static string PsamNo = "";

        #endregion


        #region 参与方信息
        /// <summary>
        /// 卡发行方标识
        /// </summary>
        public static string IssuerNo = "";
        /// <summary>
        /// 清分方标识编号
        /// </summary>
        public static string ClearNo = "";
        /// <summary>
        /// 服务方标识编号
        /// </summary>
        public static string ServerNo = "";



        /// <summary>
        /// 服务部署的IP
        /// </summary>
        public static string ServerIP = "";

        /// <summary>
        /// 服务部署的端口
        /// </summary>
        public static string ServerPort = "";

        #endregion


        public static string Test = "";


        /// <summary>
        /// 构造函数
        /// </summary>
        static MytoolsIniConstant()
        {
            try
            {
                //获取应用程序的当前工作目录
                MytoolsConfigPath = Directory.GetCurrentDirectory();

                if (MytoolsConfigPath == null || MytoolsConfigPath == "")
                {
                    Console.WriteLine("获取mytools工具安装地址失败！请重新启动！");
                    return;
                }

                //配置文件的绝对地址+文件名
                MytoolsConfigFileName = MytoolsConfigPath + "\\mytoolsConfig.xml";

                //如果该文件名不存在
                if (!File.Exists(MytoolsConfigFileName))
                {
                    //按默认方式初始化配置文件
                    if (!configFileInitial(MytoolsConfigFileName))
                    {
                        Console.WriteLine("初始化配置文件失败！");
                        return;
                    }
                }


                string db2_connection = System.Configuration.ConfigurationManager.AppSettings.ToString();
            }
            catch
            {
            }

        }

        /// <summary>
        /// 配置文件初始化
        /// </summary>
        /// <param name="configFilePath">配置文件绝对路径</param>
        /// <returns></returns>
        public static bool configFileInitial(string configFilePath)
        {
            try
            {
                //打开文件，如果没有则创建文件
                FileStream fs = File.OpenWrite(MytoolsConfigFileName);

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
                        xmlWriter.WriteStartElement("MytoolsConfig");
                        {
                            //读卡器信息
                            xmlWriter.WriteStartElement("ReadCardDeviceConfig");
                            {
                                //默认中兴设备
                                xmlWriter.WriteElementString("DeviceCompany", ((int)ReaderVender.ZTE).ToString());
                                //默认连接类型为USB类型
                                xmlWriter.WriteElementString("ConnectType", "USB");
                                //默认连接端口为1号端口
                                xmlWriter.WriteElementString("ConnectPort", "1");
                            }
                            xmlWriter.WriteEndElement();

                            //卡片信息
                            xmlWriter.WriteStartElement("CardInfo");
                            {
                                //默认卡号长度为20位，包含发行方标识
                                xmlWriter.WriteElementString("CardNoLength", "16");
                                //默认发行方编号为1101
                                xmlWriter.WriteElementString("IssuerNetNo", "1108");
                                //默认入库批次为0813
                                xmlWriter.WriteElementString("StockNo", "0813");
                                //默认卡片类型为储值卡
                                xmlWriter.WriteElementString("CardType", ((int)CardType.PURSECARD).ToString());
                                //默认合作企业为自主发卡
                                xmlWriter.WriteElementString("CooperateEnterprise", "000");
                                //默认卡号为全0
                                xmlWriter.WriteElementString("RestCardNo", "0000000");
                                //默认卡号
                                xmlWriter.WriteElementString("CardNo", "0813220000000000");
                            }
                            xmlWriter.WriteEndElement();

                            //加密机信息
                            xmlWriter.WriteStartElement("EncryptionInfo");
                            {
                                //默认软加密机
                                xmlWriter.WriteElementString("EncryptionType", ((short)EncryptionDllType.SoftEncryption).ToString());
                            }
                            xmlWriter.WriteEndElement();

                            //终端信息
                            xmlWriter.WriteStartElement("TerminalInfo");
                            {
                                //默认卡号操作员编号
                                xmlWriter.WriteElementString("OperateNo", "000000");
                                //默认0113000003E7
                                xmlWriter.WriteElementString("TerminalNo", "0113000003E7");
                                //默认Psam卡号000000
                                xmlWriter.WriteElementString("PsamNo", "01110000005A");
                                //默认营业厅编号110800010016
                                xmlWriter.WriteElementString("AgentNo", "110800010016");
                            }
                            xmlWriter.WriteEndElement();

                            //参与方信息
                            xmlWriter.WriteStartElement("OperatorInfo");
                            {
                                //默认卡号发行方编号
                                xmlWriter.WriteElementString("IssuerNo", "9999999911010001");
                                //默认清分方编号
                                xmlWriter.WriteElementString("ClearNo", "9999999911020001");
                                //默认服务方编号
                                xmlWriter.WriteElementString("ServerNo", "9999999911030001");
                            }
                            xmlWriter.WriteEndElement();

                            //服务端口IP
                            xmlWriter.WriteStartElement("ServerInfo");
                            {
                                //默认卡号发行方编号
                                xmlWriter.WriteElementString("ServerIP", "192.168.1.1");
                                //默认清分方编号
                                xmlWriter.WriteElementString("ServerPort", "53880");
                            }
                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    //将内存缓冲区中写入文件流
                    fs.Write(FounctionResources.StreamToBytes(ms), 0, (int)ms.Length);
                    //写入文件系统
                    fs.Flush();
                    xmlWriter.Close();
                    ms.Close();
                    fs.Close();
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("创建配置文件失败！ " + err.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取系统配置文件信息
        /// </summary>
        /// <returns></returns>
        public bool getConfigInf()
        {
            XmlTextReader xtr = new XmlTextReader(MytoolsConfigFileName);
            try
            {

                while (xtr.Read())
                {

                    #region 读卡器设备配置信息

                    //获取读卡器厂家信息
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "DeviceCompany")
                    {
                        DeviceCompany = Convert.ToInt32(xtr.ReadString());
                        continue;
                    }

                    //获取读卡器连接类型
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ConnectType")
                    {
                        ConnectType = xtr.ReadString();
                        continue;
                    }

                    //获取读卡器端口
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ConnectPort")
                    {
                        ConnectPort = xtr.ReadString();
                        continue;
                    }
                    #endregion

                    #region 获取加密机型号

                    //获取加密机动态库类型
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "EncryptionType")
                    {
                        EncryptionType = Convert.ToInt32(xtr.ReadString());
                        continue;
                    }
                    #endregion


                    #region 卡片信息

                    //获取卡片长度信息
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "CardNoLength")
                    {
                        CardNoLength = Convert.ToInt32(xtr.ReadString());
                        continue;
                    }

                    //获取卡网络编号
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "IssuerNetNo")
                    {
                        IssuerNetNo = xtr.ReadString();
                        continue;
                    }

                    //获取批次信息
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "StockNo")
                    {
                        StockNo = xtr.ReadString();
                        continue;
                    }

                    //获取卡默认类型
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "CardType")
                    {
                        CardDefulType = Convert.ToInt32(xtr.ReadString());
                        continue;
                    }

                    //获取合作企业信息
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "CooperateEnterprise")
                    {
                        CooperateEnterprise = xtr.ReadString();
                        continue;
                    }

                    //获取剩余7位卡号
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "RestCardNo")
                    {
                        RestCardNo = xtr.ReadString();
                        continue;
                    }

                    //获取卡号
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "CardNo")
                    {
                        CardNo = xtr.ReadString();
                        continue;
                    }

                    #endregion


                    #region 终端信息

                    //获取操作员信息
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "OperateNo")
                    {
                        OperatorNo = xtr.ReadString();
                        continue;
                    }

                    //获取终端机编号
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "TerminalNo")
                    {
                        TerminalNo = xtr.ReadString();
                        continue;
                    }

                    //获取PSAM卡号
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "PsamNo")
                    {
                        PsamNo = xtr.ReadString();
                        continue;
                    }

                    //获取营业厅编号
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "AgentNo")
                    {
                        AgentNo = xtr.ReadString();
                        continue;
                    }

                    #endregion 获取发行方信息

                    //获取发行方标识
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "IssuerNo")
                    {
                        IssuerNo = xtr.ReadString();
                        continue;
                    }

                    //获取清分方标识
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ClearNo")
                    {
                        ClearNo = xtr.ReadString();
                        continue;
                    }

                    //获取服务方标识
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ServerNo")
                    {
                        ServerNo = xtr.ReadString();
                        continue;
                    }

                    //所连接的服务地址
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ServerIP")
                    {
                        ServerIP = xtr.ReadString();
                        continue;
                    }

                    //所连接的服务端口
                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "ServerPort")
                    {
                        ServerPort = xtr.ReadString();
                        continue;
                    }

                    if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "Test")
                    {
                        Test = xtr.ReadString();
                        continue;
                    }
                }
            }
            catch (Exception err)
            {
                xtr.Close();
                Console.WriteLine("读取配置文件失败!" + err.Message);
                return false;
            }
            xtr.Close();


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(MytoolsConfigFileName);

            XmlNode root = xmlDoc.SelectSingleNode("MytoolsConfig");

            XmlElement xe1 = xmlDoc.CreateElement("Test");

            XmlElement xe2 = xmlDoc.CreateElement("Test");
            xe2.InnerText = "192.168.1.1";
            xe2.AppendChild(xe1);

            xmlDoc.Save(MytoolsConfigFileName);
            return true;
        }
    }
}

