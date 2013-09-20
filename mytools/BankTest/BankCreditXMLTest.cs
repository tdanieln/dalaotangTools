using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.IO;

namespace mytools
{
    class BankCreditXMLTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="accountType"></param>
        /// <param name="userName"></param>
        /// <param name="userShortName"></param>
        /// <param name="identifyType"></param>
        /// <param name="identifyNo"></param>
        /// <param name="organizationNo"></param>
        /// <param name="userAddress"></param>
        /// <param name="postCode"></param>
        /// <param name="email"></param>
        /// <param name="contact"></param>
        /// <param name="telephoneNo"></param>
        /// <param name="faxNo"></param>
        /// <param name="mobileNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="orderNo"></param>
        /// <param name="IPAddress"></param>
        /// <param name="Port"></param>
        /// <returns></returns>
        public RetCodeBankXml ContractCard(string userType
                                , string accountType
                                , string userName
                                , string userShortName
                                , string identifyType
                                , string identifyNo
                                , string organizationNo
                                , string userAddress
                                , string postCode
                                , string email
                                , string contact
                                , string telephoneNo
                                , string faxNo
                                , string mobileNo
                                , string cardNo
                                , string orderNo
                                , string IPAddress
                                , string Port
                                )
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //建立内存流
            MemoryStream memStreamSend = new MemoryStream(1000);

            //向内存流写入xml内容
            XmlTextWriter xmlWriter = new XmlTextWriter(memStreamSend, Encoding.GetEncoding("gbk"));
            //确定xml文档缩进格式
            xmlWriter.Formatting = Formatting.Indented;

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ktetcapp");
            xmlWriter.WriteStartElement("issueOperation");
            xmlWriter.WriteElementString("opCode", ((int)BankOperationKind.Contract).ToString());
            //客户类别
            xmlWriter.WriteElementString("userType", userType);

            //账户类别
            xmlWriter.WriteElementString("accountType", accountType);

            //速通卡编号
            xmlWriter.WriteElementString("cardNo", cardNo);

            //客户名称
            xmlWriter.WriteElementString("userName", userName);
            //客户简称
            xmlWriter.WriteElementString("userShortName", userShortName);

            //证件类型
            xmlWriter.WriteElementString("identifyType", identifyType);

            //证件号码
            xmlWriter.WriteElementString("identifyNo", identifyNo);

            //组织机构代码
            xmlWriter.WriteElementString("organizationNo", organizationNo);

            //客户地址
            xmlWriter.WriteElementString("userAddress", userAddress);

            //邮政编码
            xmlWriter.WriteElementString("postCode", postCode);

            //邮政编码
            xmlWriter.WriteElementString("email", email);

            //联系人
            xmlWriter.WriteElementString("contact", contact);

            //电话号码
            xmlWriter.WriteElementString("telephoneNo", telephoneNo);

            //传真
            xmlWriter.WriteElementString("faxNo", faxNo);

            //手机号
            xmlWriter.WriteElementString("mobileNo", mobileNo);

            //注册日期
            xmlWriter.WriteElementString("strRegisterTime", time);

            //终端机编号
            xmlWriter.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);

            //订单号或流水号
            xmlWriter.WriteElementString("orderNo", orderNo);//"2010060511024916");

            //操作员
            xmlWriter.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);

            //营业厅
            xmlWriter.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            //实例化返回值结构体
            RetCodeBankXml bxrc = new RetCodeBankXml();

            Encoding encoding = Encoding.GetEncoding("gb18030");

            Console.WriteLine(encoding.GetString(memStreamSend.GetBuffer()));

            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend.Length);

            if (memStreamSend.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
                Console.WriteLine("数据长度异常");
                bxrc.errCode = -1;
                return bxrc;
            }


            // byte[] buffer = (memStreamSend.GetBuffer());


            //往网络流中写入XML文件大小
            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend);


            TcpClient client = new TcpClient();

            try
            {
                //建立网络连接
                client.Connect(IPAddress, Convert.ToInt32(Port));
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            //建立网络流
            NetworkStream netWorkStream = client.GetStream();

            //第一次写入网络流数据，内容为数据长度
            netWorkStream.Write(buffer, 0, buffer.Length);
            netWorkStream.Flush();

            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend.WriteTo(netWorkStream);
            memStreamSend.Close();      //实时关闭内存流
            netWorkStream.Flush();

            xmlWriter.Close();      //关闭写XML文件流


            //********************************************     接收MXL文档部分     *******************************************



            //复用netWorkStream流用于接收数据
            ////延时接收数据2s
            //netWorkStream.ReadTimeout = 2000;
            //接收前8个字节的长度信息
            int sum = FounctionResources.SizeOfXMLStream(netWorkStream);
            if (sum > 0x4000)
            {
                Console.Write("{0}", sum);
                Console.WriteLine("长度错误，退出");
                bxrc.errCode = -1;

                //返回结构体
                return bxrc;
            }
            Console.WriteLine("计算将要接收到的数据长度：{0}", sum);

            MemoryStream memStreamRecv = new MemoryStream(100);
            byte[] RecvByteArrary = new byte[sum];

            netWorkStream.ReadTimeout = 20000;
            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary, 0, sum);
            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count = 0;
            while (count < RecvByteArrary.Length)
            {
                memStreamRecv.WriteByte(RecvByteArrary[count++]);
            }

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs = new XmlReaderSettings();
            xmlrs.ConformanceLevel = ConformanceLevel.Fragment;

            xmlrs.IgnoreComments = true;
            xmlrs.IgnoreWhitespace = true;
            //netWorkStream.ReadTimeout = 10000;
            XmlReader xmlReader = XmlReader.Create(memStreamRecv, xmlrs);

            xmlReader.Read();
            //xmlReader.ReadStartElement("ktetcapp");
            xmlReader.ReadToFollowing("ktetcapp");
            if (xmlReader.ReadToFollowing("errorCode"))
            {
                string errorcode = Convert.ToString(xmlReader.ReadString());
                bxrc.errCode = Convert.ToInt32(errorcode);
                Console.WriteLine("errorCode {0}", errorcode);
            }


            if (xmlReader.ReadToFollowing("errorMsg"))
            {
                string errorMsg = xmlReader.ReadString();
                bxrc.errMessage = errorMsg;
                Console.WriteLine("errorMsg {0}", errorMsg);
            }
            if (xmlReader.ReadToFollowing("accountNo"))
            {
                string accountNo = xmlReader.ReadString();
                bxrc.accountNo = accountNo;
                Console.WriteLine("accountNo {0}", accountNo);
            }
            //serialno = ByteCopy.ReadSerialnoAndAdd();
            //读取数据完毕实时关闭流
            xmlReader.Close();
            memStreamRecv.Close();
            client.Close();
            return bxrc;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="userType"></param>
        /// <param name="userName"></param>
        /// <param name="userShortName"></param>
        /// <param name="identifyType"></param>
        /// <param name="identifyNo"></param>
        /// <param name="organizationNo"></param>
        /// <param name="userAddress"></param>
        /// <param name="postCode"></param>
        /// <param name="email"></param>
        /// <param name="contact"></param>
        /// <param name="telephoneNo"></param>
        /// <param name="faxNo"></param>
        /// <param name="mobileNo"></param>
        /// <param name="orderNo"></param>
        /// <param name="IPAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public RetCodeBankXml ContractInfoChange(string accountNo
                                                 , string userType
                                                 , string userName
                                                 , string userShortName
                                                 , string identifyType
                                                 , string identifyNo
                                                 , string organizationNo
                                                 , string userAddress
                                                 , string postCode
                                                 , string email
                                                 , string contact
                                                 , string telephoneNo
                                                 , string faxNo
                                                 , string mobileNo
                                                 , string orderNo
                                                 , string IPAddress
                                                 , string port)
        {

            string contractInfoChangeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            MemoryStream memStreamSend = new MemoryStream(1000);       //生成内存流，用于写入XML文件

            //往内存流内写XML文件
            XmlTextWriter xmlWriter = new XmlTextWriter(memStreamSend, Encoding.GetEncoding("gb18030"));
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("ktetcapp");
            xmlWriter.WriteStartElement("issueOperation");
            xmlWriter.WriteElementString("opCode", ((int)BankOperationKind.ContractInfoChange).ToString());
            //客户类别
            xmlWriter.WriteElementString("accountNo", accountNo);

            //客户地址
            xmlWriter.WriteElementString("userAddress", userAddress);

            ///邮政编码
            xmlWriter.WriteElementString("postCode", postCode);

            //邮政编码
            xmlWriter.WriteElementString("email", email);

            //联系人
            xmlWriter.WriteElementString("contact", contact);

            //电话号码
            xmlWriter.WriteElementString("telephoneNo", telephoneNo);

            //传真
            xmlWriter.WriteElementString("faxNo", faxNo);

            //手机号
            xmlWriter.WriteElementString("mobileNo", mobileNo);

            //注册日期
            xmlWriter.WriteElementString("strModifyTime", contractInfoChangeTime);

            //客户类别
            xmlWriter.WriteElementString("userType", userType);

            //客户名称
            xmlWriter.WriteElementString("userName", userName);

            //客户简称
            xmlWriter.WriteElementString("userShortName", userShortName);

            //证件类型
            xmlWriter.WriteElementString("identifyType", identifyType);

            //证件号码
            xmlWriter.WriteElementString("identifyNo", identifyNo);

            //组织机构代码
            xmlWriter.WriteElementString("organizationNo", organizationNo);

            //终端机编号
            xmlWriter.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);

            //订单号或流水号
            xmlWriter.WriteElementString("orderNo", orderNo);

            //操作员
            xmlWriter.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);

            //营业厅
            xmlWriter.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            RetCodeBankXml rcbx = new RetCodeBankXml();

            //显示输出将要发送的XML文件
            Encoding encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamSend.GetBuffer()));

            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
            //发送数据如果大于256K字节，自动退出
            if (memStreamSend.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
                Console.WriteLine("数据长度异常");
                rcbx.errCode = -1;
                return rcbx;
            }

            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend);

            TcpClient client = new TcpClient();
            client.Connect(IPAddress, Convert.ToInt32(port));
            NetworkStream netWorkStream = client.GetStream();

            //将长度写入网络流并刷新
            netWorkStream.Write(buffer, 0, 8);
            netWorkStream.Flush();
            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend.WriteTo(netWorkStream);
            memStreamSend.Close();      //实时关闭内存流
            netWorkStream.Flush();
            xmlWriter.Close();      //关闭写XML文件流

            Console.WriteLine("发送数据完毕\n");


            netWorkStream.ReadTimeout = 5000;
            //接收前8个字节的长度信息
            int sum = FounctionResources.SizeOfXMLStream(netWorkStream);
            if (sum > 0x4000)
            {
                Console.Write("{0}", sum);
                Console.WriteLine("长度错误，退出");
                rcbx.errCode = -1;
                return rcbx;
            }
            Console.WriteLine("计算将要接收到的数据长度：{0}", sum);

            MemoryStream memStreamRecv = new MemoryStream(100);
            byte[] RecvByteArrary = new byte[sum];

            netWorkStream.ReadTimeout = 20000;
            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary, 0, sum);
            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count = 0;
            while (count < RecvByteArrary.Length)
            {
                memStreamRecv.WriteByte(RecvByteArrary[count++]);
            }

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs = new XmlReaderSettings();
            xmlrs.ConformanceLevel = ConformanceLevel.Fragment;

            xmlrs.IgnoreComments = true;
            xmlrs.IgnoreWhitespace = true;
            //netWorkStream.ReadTimeout = 10000;
            XmlReader xmlReader = XmlReader.Create(memStreamRecv, xmlrs);

            xmlReader.Read();
            xmlReader.ReadToFollowing("ktetcapp");
            if (xmlReader.ReadToFollowing("errorCode"))
            {
                string errorcode = Convert.ToString(xmlReader.ReadString());
                Console.WriteLine("errorCode {0}", errorcode);
                rcbx.errCode = Convert.ToInt32(errorcode);
            }


            if (xmlReader.ReadToFollowing("errorMsg"))
            {
                string errorMsg = xmlReader.ReadString();
                Console.WriteLine("errorMsg {0}", errorMsg);
                rcbx.errMessage = errorMsg;
            }
            if (xmlReader.ReadToFollowing("accountNo"))
            {
                string retAccountNo = xmlReader.ReadString();
                Console.WriteLine("accountNo {0}", retAccountNo);
                rcbx.accountNo = retAccountNo;
            }

            //读取数据完毕实时关闭流
            xmlReader.Close();
            memStreamRecv.Close();
            client.Close();

            return rcbx;
        }


        public RetCodeBankXml AddAndRemoveCard(string accountNo
                                               , string cardNoAdded
                                               , string lastCardNoAdded
                                               , string cardNoDropped
                                               , string lastCardNoDropped
                                               , string orderNo
                                               , string ServerIP
                                               , string ServerPort)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //*********************************************     发送XML文档部分     *********************************************8

            MemoryStream memStreamSend = new MemoryStream(1000);       //生成内存流，用于写入XML文件

            //往内存流内写XML文件（记帐卡查询）
            XmlTextWriter xmlWriter = new XmlTextWriter(memStreamSend, Encoding.GetEncoding("gb18030"));
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();


            xmlWriter.WriteStartElement("ktetcapp");
            xmlWriter.WriteStartElement("issueOperation");
            //操作类别
            xmlWriter.WriteElementString("opCode", ((int)BankOperationKind.AddAndRemoveCard).ToString());
            //账号
            xmlWriter.WriteElementString("accountNo", accountNo);

            //增加的卡，号段中第一张卡地卡号
            xmlWriter.WriteElementString("cardNoAdded", cardNoAdded);
            //增加的卡，号段中的最后一张卡卡号
            xmlWriter.WriteElementString("lastCardNoAdded", lastCardNoAdded);
            //移除的卡，号段中第一张卡地卡号
            xmlWriter.WriteElementString("cardNoDropped", cardNoDropped);
            //移除的卡，号段中的最后一张卡卡号
            xmlWriter.WriteElementString("lastCardNoDropped", lastCardNoDropped);
            //操作时间
            xmlWriter.WriteElementString("strTime", time);
            //终端机编号
            xmlWriter.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);

            //订单号或流水号
            xmlWriter.WriteElementString("orderNo", orderNo);//"2010060513055216");//);//);

            //操作员
            xmlWriter.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);

            //营业厅
            xmlWriter.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            RetCodeBankXml rcbx = new RetCodeBankXml();

            //显示输出将要发送的XML文件
            Encoding encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamSend.GetBuffer()));

            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
            //发送数据如果大于256K字节，自动退出
            if (memStreamSend.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
                Console.WriteLine("数据长度异常");
                rcbx.errCode = -1;
                return rcbx;
            }

            //往网络流中写入XML文件大小
            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend);

            TcpClient client = new TcpClient();
            client.Connect(ServerIP, Convert.ToInt32(ServerPort));
            NetworkStream netWorkStream = client.GetStream();

            //将长度写入网络流并刷新
            netWorkStream.Write(buffer, 0, 8);
            netWorkStream.Flush();

            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend.WriteTo(netWorkStream);
            memStreamSend.Close();      //实时关闭内存流
            netWorkStream.Flush();
            xmlWriter.Close();      //关闭写XML文件流
            Console.WriteLine("发送数据完毕\n");

            //********************************************     接收MXL文档部分     *******************************************

            //复用netWorkStream流用于接收数据
            //延时接收数据2s
            netWorkStream.ReadTimeout = 5000;
            //接收前8个字节的长度信息
            int sum = FounctionResources.SizeOfXMLStream(netWorkStream);
            if (sum > 0x4000)
            {
                Console.Write("{0}", sum);
                Console.WriteLine("长度错误，退出");
                rcbx.errCode = -1;
                return rcbx;
            }
            Console.WriteLine("计算将要接收到的数据长度：{0}", sum);

            MemoryStream memStreamRecv = new MemoryStream(100);
            byte[] RecvByteArrary = new byte[sum];

            netWorkStream.ReadTimeout = 200000;
            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary, 0, sum);
            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count = 0;
            while (count < RecvByteArrary.Length)
            {
                memStreamRecv.WriteByte(RecvByteArrary[count++]);
            }

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs = new XmlReaderSettings();
            xmlrs.ConformanceLevel = ConformanceLevel.Fragment;

            xmlrs.IgnoreComments = true;
            xmlrs.IgnoreWhitespace = true;
            //netWorkStream.ReadTimeout = 10000;
            XmlReader xmlReader = XmlReader.Create(memStreamRecv, xmlrs);
            string numCardAdded = "";
            string numCardDropped = "";
            xmlReader.Read();
            xmlReader.ReadToFollowing("ktetcapp");
            if (xmlReader.ReadToFollowing("errorCode"))
            {
                string errorcode = Convert.ToString(xmlReader.ReadString());
                rcbx.errCode = Convert.ToInt32(errorcode);
                Console.WriteLine("errorCode {0}", errorcode);
            }


            if (xmlReader.ReadToFollowing("errorMsg"))
            {
                string errorMsg = xmlReader.ReadString();
                rcbx.errMessage = errorMsg;
                Console.WriteLine("errorMsg {0}", errorMsg);
            }
            if (xmlReader.ReadToFollowing("numCardAdded"))
            {
                numCardAdded = xmlReader.ReadString();
                rcbx.numCardAdded = Convert.ToInt32(numCardAdded);
                Console.WriteLine("numCardAdded {0}", numCardAdded);
            }

            if (xmlReader.ReadToFollowing("numCardDropped"))
            {
                numCardDropped = xmlReader.ReadString();
                rcbx.numCardDroped = Convert.ToInt32(numCardDropped);
                Console.WriteLine("numCardDropped {0}", numCardDropped);
            }
            //读取数据完毕实时关闭流
            xmlReader.Close();
            memStreamRecv.Close();
            client.Close();
            return rcbx;
        }



        public RetCodeBankXml SetCardForbidden(string cardNo
                                            , string accountNo
                                            , string orderNo
                                            , string serverIP
                                            , string serverPort)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            RetCodeBankXml rcbx = new RetCodeBankXml();

            //*********************************************     发送XML文档部分     *********************************************8

            MemoryStream memStreamSend = new MemoryStream(1000);       //生成内存流，用于写入XML文件

            //往内存流内写XML文件（记帐卡查询）
            XmlTextWriter xmlWriter = new XmlTextWriter(memStreamSend, Encoding.GetEncoding("gb18030"));
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();


            xmlWriter.WriteStartElement("ktetcapp");
            xmlWriter.WriteStartElement("issueOperation");
            xmlWriter.WriteElementString("opCode", ((int)BankOperationKind.ForbiddenCard).ToString());
            //客户类别
            xmlWriter.WriteElementString("accountNo", accountNo);

            xmlWriter.WriteElementString("cardNo", cardNo);


            //客户名称
            xmlWriter.WriteElementString("strOperationTime", time);

            //终端机编号
            xmlWriter.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);

            //订单号或流水号
            xmlWriter.WriteElementString("orderNo", orderNo);

            //操作员
            xmlWriter.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);

            //营业厅
            xmlWriter.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            //显示输出将要发送的XML文件
            Encoding encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamSend.GetBuffer()));

            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
            //发送数据如果大于256K字节，自动退出
            if (memStreamSend.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
                Console.WriteLine("数据长度异常");
                rcbx.errCode = -1;
                return rcbx;
            }

            TcpClient client = new TcpClient();
            client.Connect(serverIP, Convert.ToInt32(serverPort));
            NetworkStream netWorkStream = client.GetStream();

            //往网络流中写入XML文件大小
            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend);

            //将长度写入网络流并刷新
            netWorkStream.Write(buffer, 0, 8);
            netWorkStream.Flush();
            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend.WriteTo(netWorkStream);
            memStreamSend.Close();      //实时关闭内存流
            netWorkStream.Flush();
            xmlWriter.Close();      //关闭写XML文件流

            Console.WriteLine("发送数据完毕\n");



            //********************************************     接收MXL文档部分     *******************************************

            //复用netWorkStream流用于接收数据

            //延时接收数据2s
            netWorkStream.ReadTimeout = 5000;

            //接收前8个字节的长度信息
            int sum = FounctionResources.SizeOfXMLStream(netWorkStream);

            if (sum > 0x4000)
            {
                Console.Write("{0}", sum);
                Console.WriteLine("长度错误，退出");
                rcbx.errCode = -1;
                return rcbx;
            }

            Console.WriteLine("计算将要接收到的数据长度：{0}", sum);
            MemoryStream memStreamRecv = new MemoryStream(100);
            byte[] RecvByteArrary = new byte[sum];

            netWorkStream.ReadTimeout = 20000;

            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary, 0, sum);

            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count = 0;
            while (count < RecvByteArrary.Length)
            {
                memStreamRecv.WriteByte(RecvByteArrary[count++]);
            }

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs = new XmlReaderSettings();
            xmlrs.ConformanceLevel = ConformanceLevel.Fragment;

            xmlrs.IgnoreComments = true;
            xmlrs.IgnoreWhitespace = true;

            XmlReader xmlReader = XmlReader.Create(memStreamRecv, xmlrs);

            xmlReader.Read();
            xmlReader.ReadToFollowing("ktetcapp");
            if (xmlReader.ReadToFollowing("errorCode"))
            {
                string errorcode = Convert.ToString(xmlReader.ReadString());
                rcbx.errCode = Convert.ToInt32(errorcode);
                Console.WriteLine("errorCode {0}", errorcode);
            }


            if (xmlReader.ReadToFollowing("errorMsg"))
            {
                string errorMsg = xmlReader.ReadString();
                rcbx.errMessage = errorMsg;
                Console.WriteLine("errorMsg {0}", errorMsg);
            }
            if (xmlReader.ReadToFollowing("logID"))
            {
                string logID = xmlReader.ReadString();
                rcbx.logID = Convert.ToInt32(logID);
                Console.WriteLine("logID {0}", logID);
            }

            Console.WriteLine("卡禁用成功");
            //读取数据完毕实时关闭流
            xmlReader.Close();
            memStreamRecv.Close();
            client.Close();
            return rcbx;
        }


        /// <summary>
        /// 卡片解禁
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="accountNo"></param>
        /// <param name="orderNo"></param>
        /// <param name="serverIP"></param>
        /// <param name="serverPort"></param>
        /// <returns></returns>
        public RetCodeBankXml CardCancelForibidden(string cardNo
                                               , string accountNo
                                               , string orderNo
                                               , string serverIP
                                               , string serverPort)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            RetCodeBankXml rcbx = new RetCodeBankXml();

            MemoryStream memStreamSend = new MemoryStream(1000);       //生成内存流，用于写入XML文件

            //往内存流内写XML文件（记帐卡查询）
            XmlTextWriter xmlWriter = new XmlTextWriter(memStreamSend, Encoding.GetEncoding("gb18030"));
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();


            xmlWriter.WriteStartElement("ktetcapp");
            xmlWriter.WriteStartElement("issueOperation");
            xmlWriter.WriteElementString("opCode", "400001");
            //客户类别
            xmlWriter.WriteElementString("accountNo", accountNo);

            xmlWriter.WriteElementString("cardNo", cardNo);


            //客户名称
            xmlWriter.WriteElementString("strOperationTime", time);

            //终端机编号
            xmlWriter.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);

            //订单号或流水号
            xmlWriter.WriteElementString("orderNo", orderNo);//"2010060513055216");//);//);

            //操作员
            xmlWriter.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);

            //营业厅
            xmlWriter.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();



            //显示输出将要发送的XML文件
            Encoding encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamSend.GetBuffer()));

            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
            //发送数据如果大于256K字节，自动退出
            if (memStreamSend.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
                Console.WriteLine("数据长度异常");
                rcbx.errCode = -1;
                return rcbx;
            }

            //往网络流中写入XML文件大小
            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend);

            TcpClient client = new TcpClient();
            client.Connect(serverIP, Convert.ToInt32(serverPort));
            NetworkStream netWorkStream = client.GetStream();

            //将长度写入网络流并刷新
            netWorkStream.Write(buffer, 0, 8);
            netWorkStream.Flush();
            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend.WriteTo(netWorkStream);
            memStreamSend.Close();      //实时关闭内存流
            netWorkStream.Flush();
            xmlWriter.Close();      //关闭写XML文件流

            Console.WriteLine("发送数据完毕\n");

            //********************************************     接收MXL文档部分     *******************************************

            //复用netWorkStream流用于接收数据
            //延时接收数据2s
            netWorkStream.ReadTimeout = 5000;
            //接收前8个字节的长度信息
            int sum = FounctionResources.SizeOfXMLStream(netWorkStream);
            if (sum > 0x4000)
            {
                Console.Write("{0}", sum);
                Console.WriteLine("长度错误，退出");
                rcbx.errCode = -1;
                return rcbx;
            }
            Console.WriteLine("计算将要接收到的数据长度：{0}", sum);
            ;
            MemoryStream memStreamRecv = new MemoryStream(100);
            byte[] RecvByteArrary = new byte[sum];

            netWorkStream.ReadTimeout = 20000;
            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary, 0, sum);
            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count = 0;
            while (count < RecvByteArrary.Length)
            {
                memStreamRecv.WriteByte(RecvByteArrary[count++]);
            }

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs = new XmlReaderSettings();
            xmlrs.ConformanceLevel = ConformanceLevel.Fragment;
            xmlrs.IgnoreComments = true;
            xmlrs.IgnoreWhitespace = true;
            //netWorkStream.ReadTimeout = 10000;
            XmlReader xmlReader = XmlReader.Create(memStreamRecv, xmlrs);

            xmlReader.Read();
            xmlReader.ReadToFollowing("ktetcapp");
            if (xmlReader.ReadToFollowing("errorCode"))
            {
                string errorcode = xmlReader.ReadString();
                rcbx.errCode = Convert.ToInt32(errorcode);
                Console.WriteLine("errorCode {0}", errorcode);
            }


            if (xmlReader.ReadToFollowing("errorMsg"))
            {
                string errorMsg = xmlReader.ReadString();
                rcbx.errMessage = errorMsg;
                Console.WriteLine("errorMsg {0}", errorMsg);
            }
            if (xmlReader.ReadToFollowing("logID"))
            {
                string logID = xmlReader.ReadString();
                rcbx.logID = Convert.ToInt32(logID);
                Console.WriteLine("logID {0}", logID);
            }
            Console.WriteLine("卡解禁成功");
            //读取数据完毕实时关闭流
            xmlReader.Close();
            memStreamRecv.Close();
            client.Close();
            return rcbx;

        }




        public RetCodeBankXml CardReplaceStart(string accountNo
                                 , string oldCardNo
                                 , string newCardNo
                                 , byte[] randomNo
                                 , string orderNo
                                 , string serverIP
                                 , string serverPort)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            RetCodeBankXml rcbx = new RetCodeBankXml();

            //*********************************************     发送储值卡XML文档部分     *********************************************8

            MemoryStream memStreamSend2 = new MemoryStream(1000);       //生成内存流，用于写入XML文件

            XmlTextWriter xmlWriter2 = new XmlTextWriter(memStreamSend2, Encoding.GetEncoding("gb18030"));
            xmlWriter2.Formatting = Formatting.Indented;

            Console.WriteLine();

            xmlWriter2.WriteStartDocument();
            xmlWriter2.WriteStartElement("ktetcapp");
            xmlWriter2.WriteStartElement("issueOperation");
            xmlWriter2.WriteElementString("opCode", ((int)BankOperationKind.CardReplaceStart).ToString());
            xmlWriter2.WriteElementString("accountNo", accountNo);
            xmlWriter2.WriteElementString("oldCardNo", oldCardNo);
            xmlWriter2.WriteElementString("newCardNo", newCardNo);
            xmlWriter2.WriteElementString("randomNum", Convert.ToBase64String(randomNo, Base64FormattingOptions.None));
            xmlWriter2.WriteElementString("strReplaceTime", time);
            xmlWriter2.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);
            xmlWriter2.WriteElementString("orderNo", orderNo);
            xmlWriter2.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);
            xmlWriter2.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter2.WriteEndElement();
            xmlWriter2.WriteEndElement();
            xmlWriter2.WriteEndDocument();
            xmlWriter2.Flush();

            //显示输出将要发送的XML文件
            Encoding encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamSend2.GetBuffer()));

            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend2.Length);
            //发送数据如果大于256K字节，自动退出
            if (memStreamSend2.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend2.Length);
                Console.WriteLine("数据长度异常");
                rcbx.errCode = -1;
                return rcbx;
            }

            //往网络流中写入XML文件大小
            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend2);

            TcpClient client = new TcpClient();
            client.Connect(serverIP, Convert.ToInt32(serverPort));
            NetworkStream netWorkStream = client.GetStream();

            //将长度写入网络流并刷新
            netWorkStream.Write(buffer, 0, 8);
            netWorkStream.Flush();
            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend2.WriteTo(netWorkStream);
            memStreamSend2.Close();      //实时关闭内存流
            netWorkStream.Flush();
            xmlWriter2.Close();      //关闭写XML文件流

            Console.WriteLine("发送数据完毕\n");

            //********************************************     接收储值卡查询MXL文档部分     *******************************************
            //复用netWorkStream流用于接收数据
            //延时接收数据2s
            netWorkStream.ReadTimeout = 5000;
            int sum1 = 0;
            byte[] length1 = new byte[8];
            string temp1 = ASCIIEncoding.ASCII.GetString(length1);

            sum1 = FounctionResources.SizeOfXMLStream(netWorkStream);
            //接收前8个字节的长度信息  
            //sum = ByteCopy.SizeOfXMLStream(netWorkStream);
            if (sum1 > 0x4000)
            {
                Console.Write("{0}", sum1);
                Console.WriteLine("长度错误，退出");
                rcbx.errCode = -1;
                return rcbx;
            }
            Console.WriteLine("计算将要接收到的数据长度：{0}", sum1);

            MemoryStream memStreamRecv1 = new MemoryStream(100);
            byte[] RecvByteArrary1 = new byte[sum1];

            netWorkStream.ReadTimeout = 20000;
            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary1, 0, sum1);

            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count1 = 0;
            while (count1 < RecvByteArrary1.Length)
            {
                memStreamRecv1.WriteByte(RecvByteArrary1[count1++]);
            }

            memStreamRecv1.Seek(0, SeekOrigin.Begin);

            //显示输出将要发送的XML文件
            encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamRecv1.GetBuffer()));

            memStreamRecv1.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs1 = new XmlReaderSettings();
            xmlrs1.ConformanceLevel = ConformanceLevel.Fragment;

            xmlrs1.IgnoreComments = true;
            xmlrs1.IgnoreWhitespace = true;
            XmlReader xmlReader1 = XmlReader.Create(memStreamRecv1, xmlrs1);

            xmlReader1.Read();
            xmlReader1.ReadToFollowing("ktetcapp");

            if (xmlReader1.ReadToFollowing("errorCode"))
            {
                string errorcode = Convert.ToString(xmlReader1.ReadString());
                rcbx.errCode = Convert.ToInt32(errorcode);
                Console.WriteLine("errorCode {0}", errorcode);
            }

            if (xmlReader1.ReadToFollowing("errorMsg"))
            {

                string errorMsg = xmlReader1.ReadString();
                rcbx.errMessage = errorMsg;
                Console.WriteLine("errorMsg {0}", errorMsg);
            }

            if (xmlReader1.ReadToFollowing("dataInfoLength"))
            {

                int dataInfoLength = Convert.ToInt32(xmlReader1.ReadString());
                rcbx.dataInfoLength = dataInfoLength;
                Console.WriteLine("dataInfoLength {0}", dataInfoLength);
            }
            if (xmlReader1.ReadToFollowing("dataInfo"))
            {
                rcbx.dataInfo = xmlReader1.ReadString();
                Console.WriteLine("dataInfo {0}", rcbx.dataInfo);
                Console.WriteLine("转换之后为");
                Convert.FromBase64String(rcbx.dataInfo);
            }
            if (xmlReader1.ReadToFollowing("MAC"))
            {
                rcbx.MAC1 = Convert.FromBase64String(xmlReader1.ReadString().Trim());
                Console.WriteLine("MAC1 {0}", rcbx.MAC1);
            }
            byte[] yu = new byte[55];


            client.Close();
            return rcbx;
        }




        public RetCodeBankXml CardReplaceEnsure(string accountNo
                                 , string oldCardNo
                                 , string newCardNo
                                 , byte[] randomNo
                                 , string orderNo
                                 , string serverIP
                                 , string serverPort)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            RetCodeBankXml rcbx = new RetCodeBankXml();

            MemoryStream memStreamSend = new MemoryStream(1000);       //生成内存流，用于写入XML文件

            XmlTextWriter xmlWriter = new XmlTextWriter(memStreamSend, Encoding.GetEncoding("gb18030"));
            xmlWriter.Formatting = Formatting.Indented;

            Console.WriteLine();

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ktetcapp");
            xmlWriter.WriteStartElement("issueOperation");
            xmlWriter.WriteElementString("opCode", "100006");
            xmlWriter.WriteElementString("accountNo", accountNo);
            xmlWriter.WriteElementString("oldCardNo", oldCardNo);
            xmlWriter.WriteElementString("newCardNo", newCardNo);

            xmlWriter.WriteElementString("randomNum", Convert.ToBase64String(randomNo, Base64FormattingOptions.None));
            xmlWriter.WriteElementString("strReplaceTime", time);
            xmlWriter.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);
            xmlWriter.WriteElementString("orderNo", orderNo);
            xmlWriter.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);
            xmlWriter.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();

            //显示输出将要发送的XML文件
            Encoding encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamSend.GetBuffer()));

            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
            //发送数据如果大于256K字节，自动退出
            if (memStreamSend.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
                Console.WriteLine("数据长度异常");
                rcbx.errCode = -1;
                return rcbx;
            }

            //往网络流中写入XML文件大小
            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend);

            TcpClient client = new TcpClient();
            client.Connect(serverIP, Convert.ToInt32(serverPort));
            NetworkStream netWorkStream = client.GetStream();

            //将长度写入网络流并刷新
            netWorkStream.Write(buffer, 0, 8);
            netWorkStream.Flush();
            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend.WriteTo(netWorkStream);
            memStreamSend.Close();      //实时关闭内存流
            netWorkStream.Flush();
            xmlWriter.Close();      //关闭写XML文件流

            Console.WriteLine("发送数据完毕\n");

            //********************************************     接收储值卡查询MXL文档部分     *******************************************
            //复用netWorkStream流用于接收数据
            //延时接收数据2s
            netWorkStream.ReadTimeout = 5000;
            int sum1 = 0;
            byte[] length1 = new byte[8];

            sum1 = FounctionResources.SizeOfXMLStream(netWorkStream);

            if (sum1 > 0x4000)
            {
                Console.Write("{0}", sum1);
                Console.WriteLine("长度错误，退出");
                rcbx.errCode = -1;
                return rcbx;
            }
            Console.WriteLine("计算将要接收到的数据长度：{0}", sum1);

            MemoryStream memStreamRecv = new MemoryStream(100);
            byte[] RecvByteArrary = new byte[sum1];

            netWorkStream.ReadTimeout = 20000;
            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary, 0, sum1);

            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count1 = 0;
            encoding = Encoding.GetEncoding("gb18030");
            while (count1 < RecvByteArrary.Length)
            {
                memStreamRecv.WriteByte(RecvByteArrary[count1++]);
            }

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //显示输出将要发送的XML文件
            encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamRecv.GetBuffer()));

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs1 = new XmlReaderSettings();
            xmlrs1.ConformanceLevel = ConformanceLevel.Fragment;

            xmlrs1.IgnoreComments = true;
            xmlrs1.IgnoreWhitespace = true;
            XmlReader xmlReader = XmlReader.Create(memStreamRecv, xmlrs1);

            xmlReader.Read();
            xmlReader.ReadToFollowing("ktetcapp");
            //if (xmlReader1.IsStartElement("answer"))
            //{
            if (xmlReader.ReadToFollowing("errorCode"))
            {
                rcbx.errCode = Convert.ToInt32(xmlReader.ReadString());
                Console.WriteLine("errorCode {0}", rcbx.errCode);
            }


            if (xmlReader.ReadToFollowing("errorMsg"))
            {
                rcbx.errMessage = xmlReader.ReadString();
                Console.WriteLine("errorMsg {0}", rcbx.errMessage);
            }

            if (xmlReader.ReadToFollowing("dataInfoLength"))
            {
                rcbx.dataInfoLength = Convert.ToInt32(xmlReader.ReadString());
                Console.WriteLine("dataInfoLength {0}", rcbx.dataInfoLength);
            }
            if (xmlReader.ReadToFollowing("dataInfo"))
            {
                rcbx.dataInfo = xmlReader.ReadString();
                Console.WriteLine("dataInfo {0}", rcbx.dataInfo);
            }
            if (xmlReader.ReadToFollowing("MAC"))
            {
                rcbx.MAC1 = Convert.FromBase64String(xmlReader.ReadString().Trim());
                Console.WriteLine("MAC1 {0}", FounctionResources.GetStringFromByte(rcbx.MAC1));
            }

            return rcbx;
        }



        public RetCodeBankXml PrepareClearAccount(string accountNo
                                                  , string orderNo
                                                  , string serverIP
                                                  , string serverPort)
        {

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            RetCodeBankXml rcbx = new RetCodeBankXml();

            //*********************************************     发送XML文档部分     *********************************************8

            MemoryStream memStreamSend = new MemoryStream(1000);       //生成内存流，用于写入XML文件

            XmlTextWriter xmlWriter = new XmlTextWriter(memStreamSend, Encoding.GetEncoding("gb18030"));
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();


            xmlWriter.WriteStartElement("ktetcapp");
            xmlWriter.WriteStartElement("issueOperation");
            xmlWriter.WriteElementString("opCode", ((int)BankOperationKind.PrepareClearAccount).ToString());
            //客户类别
            xmlWriter.WriteElementString("accountNo", accountNo);

            //客户名称
            xmlWriter.WriteElementString("strWriteOffTime", time);

            //终端机编号
            xmlWriter.WriteElementString("terminalNo", MytoolsIniConstant.TerminalNo);

            //订单号或流水号
            xmlWriter.WriteElementString("orderNo", orderNo);//DateTime.Now.ToString("yyyyMMddHHmmss") + "16");//"2010060513055216");

            //操作员
            xmlWriter.WriteElementString("operatorNo", MytoolsIniConstant.OperatorNo);

            //营业厅
            xmlWriter.WriteElementString("agentNo", MytoolsIniConstant.AgentNo);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();


            //显示输出将要发送的XML文件
            Encoding encoding = Encoding.GetEncoding("gb18030");
            Console.WriteLine(encoding.GetString(memStreamSend.GetBuffer()));


            //计算XML文件的大小
            Console.WriteLine("\n发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
            //发送数据如果大于256K字节，自动退出
            if (memStreamSend.Length > Math.Pow(10, 6))
            {
                Console.WriteLine("发送前内存流中XML文件的大小：{0}", memStreamSend.Length);
                Console.WriteLine("数据长度异常");
                rcbx.errCode = -1;
                return rcbx;
            }

            //往网络流中写入XML文件大小
            byte[] buffer = FounctionResources.SizeOfXMLMemorystream(memStreamSend);


            TcpClient client = new TcpClient();
            client.Connect(serverIP, Convert.ToInt32(serverPort));
            NetworkStream netWorkStream = client.GetStream();

            //将长度写入网络流并刷新
            netWorkStream.Write(buffer, 0, 8);
            netWorkStream.Flush();
            //将内存流内的XML文件数据写入网络流，并刷新
            memStreamSend.WriteTo(netWorkStream);
            memStreamSend.Close();      //实时关闭内存流
            netWorkStream.Flush();
            xmlWriter.Close();      //关闭写XML文件流

            Console.WriteLine("发送数据完毕\n");

            //********************************************     接收MXL文档部分     *******************************************

            //复用netWorkStream流用于接收数据
            //延时接收数据2s
            netWorkStream.ReadTimeout = 5000;
            //接收前8个字节的长度信息
            int sum = FounctionResources.SizeOfXMLStream(netWorkStream);
            if (sum > 0x4000)
            {
                Console.Write("{0}", sum);
                Console.WriteLine("长度错误，退出");
                rcbx.errCode = -1;
                return rcbx;
            }
            Console.WriteLine("计算将要接收到的数据长度：{0}", sum);

            MemoryStream memStreamRecv = new MemoryStream(100);
            byte[] RecvByteArrary = new byte[sum];

            netWorkStream.ReadTimeout = 20000;
            //读取剩余的万络数据，大小由sum控制
            netWorkStream.Read(RecvByteArrary, 0, sum);
            //将网络流接收后实时关闭网络流
            netWorkStream.Close();
            int count = 0;
            while (count < RecvByteArrary.Length)
            {
                memStreamRecv.WriteByte(RecvByteArrary[count++]);
            }

            memStreamRecv.Seek(0, SeekOrigin.Begin);

            //创建验证
            XmlReaderSettings xmlrs = new XmlReaderSettings();
            xmlrs.ConformanceLevel = ConformanceLevel.Fragment;

            xmlrs.IgnoreComments = true;
            xmlrs.IgnoreWhitespace = true;
            //netWorkStream.ReadTimeout = 10000;
            XmlReader xmlReader = XmlReader.Create(memStreamRecv, xmlrs);

            xmlReader.Read();
            //xmlReader.ReadStartElement("ktetcapp");
            xmlReader.ReadToFollowing("ktetcapp");
            if (xmlReader.ReadToFollowing("errorCode"))
            {
                rcbx.errCode = Convert.ToInt32(xmlReader.ReadString());
                Console.WriteLine("errorCode {0}", rcbx.errCode);
            }

            if (xmlReader.ReadToFollowing("errorMsg"))
            {
                rcbx.errMessage = xmlReader.ReadString();
                Console.WriteLine("errorMsg {0}", rcbx.errMessage);
            }

            //读取数据完毕实时关闭流
            xmlReader.Close();
            memStreamRecv.Close();
            client.Close();
            return rcbx;
        }
    }
}
