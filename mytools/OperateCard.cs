using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mytools
{
    public class OperateCard
    {

        //编码格式
        private static Encoding getEncoding = Encoding.GetEncoding("GBK");

        private CallDevice device = null;

        int deviceCompany = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deviceCompany"></param>
        public OperateCard(int deviceCompany)
        {
            this.deviceCompany = deviceCompany;

            if (this.deviceCompany == (int)ReaderVender.WATCH)
                this.device = new CallWatchDataDevice();
            else if (this.deviceCompany == (int)ReaderVender.ZTE)
                this.device = new CallZTEDevice();
            else if (this.deviceCompany == (int)ReaderVender.JULI)
                this.device = new CallJuLiDevice();
            else if (this.deviceCompany == (int)ReaderVender.GENVICT)
                this.device = new CallGenvictDevice();
        }

        /// <summary>
        /// 复位设备与卡片
        /// </summary>
        /// <returns></returns>
        public bool ResetDeviceAndCard()
        {
            string methodName = "复位设备与复位卡片功能 \t";
            bool ret = false;

            ret = this.device.InitDevice(MytoolsIniConstant.ConnectPort);
            if (!ret)
            {
                Log.WriteLog(methodName + "打开读卡器失败！");
                MessageBox.Show("请打开设备！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.device.CloseDevice();
                return false;
            }
            ret = this.device.InitCPUcard();
            if (!ret)
            {
                Log.WriteLog(methodName + "初始化卡片失败");
                MessageBox.Show("卡片初始化失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.device.CloseDevice();
                return false;
            }

            Log.WriteLog(methodName + "执行成功！");
            return true;
        }

        //关闭设备
        public bool Close()
        {
            this.device.CloseDevice();
            return true;
        }

        /// <summary>
        /// 读0012文件
        /// </summary>
        /// <param name="file0012Content">0012文件内容</param>
        /// <returns></returns>
        public bool Read0012(ref byte[] file0012Content)
        {
            bool ret = false;
            int respLenght = 40;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            ret = this.device.CPUTimecosCmd("00B0920024", ref file0012Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }
            this.device.CloseDevice();
            return true;
        }

        /// <summary>
        /// 读0015文件
        /// </summary>
        /// <param name="file0015Content"></param>
        /// <returns></returns>
        public bool Read0015(ref byte[] file0015Content)
        {
            bool ret = false;
            int respLenght = 43;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            ret = this.device.CPUTimecosCmd("00B095002B", ref file0015Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }
            this.device.CloseDevice();
            return true;
        }

        /// <summary>
        /// 读0015文件
        /// </summary>
        /// <param name="file0015Content">0015文件结构体</param>
        /// <returns></returns>
        public bool Read0015(ref File0015Content file0015Content)
        {
            bool ret = false;
            int respLenght = 43;
            byte[] byteFile0015Content = new byte[respLenght];
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            //选择目录文件
            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            //读0015文件
            ret = this.device.CPUTimecosCmd("00B095002B", ref byteFile0015Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            file0015Content.CardIssuersNo = FounctionResources.GetByteFromSpecialPosition(byteFile0015Content
                                           , 0, 8);
            file0015Content.CardType = byteFile0015Content[8];
            file0015Content.CardVersionNo = byteFile0015Content[9];
            file0015Content.CardNet = FounctionResources.GetByteFromSpecialPosition(byteFile0015Content
                                           , 10, 2);
            file0015Content.CardSerialNo = FounctionResources.GetByteFromSpecialPosition(byteFile0015Content
                                         , 12, 8);
            file0015Content.OpenTime = FounctionResources.GetByteFromSpecialPosition(byteFile0015Content
                                     , 20, 4);
            file0015Content.EndTime = FounctionResources.GetByteFromSpecialPosition(byteFile0015Content
                                     , 24, 4);
            file0015Content.NumOfPlate = FounctionResources.GetByteFromSpecialPosition(byteFile0015Content
                                     , 28, 12);
            return true;
        }



        /// <summary>
        /// 读0016文件
        /// </summary>
        /// <param name="strFile0016Content"></param>
        /// <param name="file0016Content"></param>
        /// <returns></returns>
        public bool Read0016(ref string strFile0016Content ,ref File0016Content file0016Content)
        {
            bool ret = false;
            int sw12 = -1;
            int respLenght = 55;
            byte[] resp = new byte[respLenght];
            byte[] byteFile0016Content = new byte[respLenght]; 

            //选择0016文件位置
            ret = this.device.CPUTimecosCmd("00A40000023F00", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            //读0016文件
            ret = this.device.CPUTimecosCmd("00B0960037", ref byteFile0016Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }
            strFile0016Content = FounctionResources.GetStringFromByte(byteFile0016Content);
            file0016Content.CardholderID = byteFile0016Content[0];
            file0016Content.WorkerID = byteFile0016Content[1];
            file0016Content.NameofCardholder = FounctionResources.GetByteFromSpecialPosition(byteFile0016Content, 2, 20);
            file0016Content.CardholderCertificateNum = FounctionResources.GetByteFromSpecialPosition(byteFile0016Content
                                                    , 22, 32);
            file0016Content.TypeofCardholderCertificate = byteFile0016Content[54];
            this.device.CloseDevice();
            return true;
        }

        /// <summary>
        /// 读0018文件
        /// </summary>
        /// <param name="i">0018文件内记录的条数</param>
        /// <param name="file0018Content">0018文件结构体</param>
        /// <param name="strFile0018">0018文件字符串</param>
        /// <returns></returns>
        public bool Read0018(int i,ref File0018Content file0018Content,ref string strFile0018)
        {

            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;
            byte[] file0018 = new byte[23];


            bool ret = this.VerifyPIN();
            if (!ret)
            {
                MessageBox.Show("卡片验证失败，请不要再次读卡，将卡片交给专业人员处理！");
                this.device.CloseDevice();
                return false;
            }


            string cmd = "00B2" + i.ToString("X2") + "C417";

            ret = this.device.CPUTimecosCmd(cmd, ref file0018, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            strFile0018 = FounctionResources.GetStringFromByte(file0018);

            file0018Content.ICCardTransID = FounctionResources.GetByteFromSpecialPosition(file0018,0,2);
            file0018Content.OverdraftLimit = FounctionResources.GetByteFromSpecialPosition(file0018, 2, 3);
            file0018Content.amountForTransaction = FounctionResources.GetByteFromSpecialPosition(file0018, 5, 4);
            file0018Content.operationKind = file0018[9];
            file0018Content.posID = FounctionResources.GetByteFromSpecialPosition(file0018, 10, 6);
            file0018Content.transactionDate = FounctionResources.GetByteFromSpecialPosition(file0018, 16, 4);
            file0018Content.transactionTime = FounctionResources.GetByteFromSpecialPosition(file0018, 20, 3);

            if (Convert.ToInt32(FounctionResources.GetStringFromByte(file0018Content.ICCardTransID),16) == 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 写0015文件MAC生成
        /// </summary>
        /// <param name="appMACRandom"></param>
        /// <param name="cardNo"></param>
        /// <param name="cardNetNo"></param>
        /// <param name="cardVersion"></param>
        /// <param name="cardSignedDate"></param>
        /// <param name="cardExperiedDate"></param>
        /// <param name="bindingFlag"></param>
        /// <param name="plateNumber"></param>
        /// <param name="dataAndMAC"></param>
        /// <returns></returns>
        public bool GeneralAppMAC0015(byte[] appMACRandom
                                        , string cardNo
                                        , string cardNetNo
                                        , DateTime cardSignedDate
                                        , DateTime cardExperiedDate
                                        , bool bindingFlag
                                        , string plateNumber
                                        , ref byte[] dataAndMAC)
        {
            string methodName = "调用GeneralAppMAC0015 产生写0015文件MAC码\t";
            byte[] returnValue = new byte[1] { 0xFF };
            byte[] appMAC = new byte[4];
            byte[] MACData = new byte[56];


            //卡片类型默认为储值
            byte cardType_Byte = Convert.ToByte(int.Parse(cardNo.Substring(4, 2)).ToString());

            byte[] plateNumber_Byte = new byte[12]{0xFF,0xFF,0xFF,0xFF
                                                 ,0xFF,0xFF,0xFF,0xFF
                                                 ,0xFF,0xFF,0xFF,0xFF};

            if (bindingFlag)
            {
                plateNumber_Byte = getEncoding.GetBytes(plateNumber.ToUpper());
            }

            //待加密数据域赋值
            FounctionResources.MemoryCopy(ref MACData, 0, appMACRandom, 0, 4);
            //TIMECOS命令，写二进制文件
            MACData[8] = 0x04;
            MACData[9] = 0xD6;
            MACData[10] = 0x95;
            MACData[11] = 0x00;
            MACData[12] = 0x2F;

            MACData[13] = 0xB1;// 0xB1;
            MACData[14] = 0xB1;// 0xB1;
            MACData[15] = 0xBE;// 0xBE;
            MACData[16] = 0xA9;// 0xA9;
            MACData[17] = 0x00;// 0x00;
            MACData[18] = 0x01;// 0x01;
            MACData[19] = 0x00;// 0x00;
            MACData[20] = 0x01;// 0x01;

            //卡片类型
            MACData[21] = cardType_Byte;

            //复合消费(高4位为支持消费模式)，后四位表示卡片版本
            MACData[22] = 0x10;// 0x10;

            //网络号
            FounctionResources.MemoryCopy(ref MACData, 23, FounctionResources.StringToByteSequenceWithin16(cardNetNo), 0, 2);
            //卡号
            FounctionResources.MemoryCopy(ref MACData, 25, FounctionResources.StringToByteSequenceWithin16(cardNo), 0, 8);
            //启用日期
            FounctionResources.MemoryCopy(ref MACData, 33,
                                FounctionResources.DecStringToHexByteArray(cardSignedDate.ToString("yyyyMMdd"), 4), 0, 4);
            //到期日期
            FounctionResources.MemoryCopy(ref MACData, 37,
                                FounctionResources.DecStringToHexByteArray(cardExperiedDate.ToString("yyyyMMdd"), 4), 0, 4);

            if (plateNumber_Byte.Length > 12)
            {
                FounctionResources.MemoryCopy(ref MACData, 41, plateNumber_Byte, 0, 12);
            }
            else
            {
                FounctionResources.MemoryCopy(ref MACData, 41, plateNumber_Byte, 0, plateNumber_Byte.Length);
            }


            if (bindingFlag)
                //用户类型/绑定标识
                MACData[53] = (int)BindFlag.Bind;
            else
                MACData[53] = (int)BindFlag.NotBind;

            try
            {
                MACCompute macc = new MACCompute();
                macc.App_MAC_Generate(MACCompute.DAMK_DF01_KEY
                                    , FounctionResources.StringToByteSequenceWithin16(cardNo)
                                    , MACData.Length
                                    , MACData
                                    , ref appMAC
                                    , ref returnValue);

                    
                    //cardVersion, 10
                    //                   , Security.GetBytesFromString(cardNo)
                    //                   , MACData.Length
                    //                   , MACData
                    //                   , ref appMAC
                    //                   , ref returnValue);
            }
            catch (Exception err)
            {
                Log.WriteLog(methodName + "连接加密机异常" + err.Message);
                return false;
            }
            if (returnValue[0] != 0x00)
            {
                Log.WriteLog(methodName + "加密机计算MAC失败,返回值 0x" + returnValue[0].ToString("X2"));
                return false;
            }

            FounctionResources.MemoryCopy(ref dataAndMAC, 0, MACData, 8, 48);
            FounctionResources.MemoryCopy(ref dataAndMAC, 48, appMAC, 0, 4);

            Log.WriteLog(methodName + "执行成功！返回的写卡信息为 " + FounctionResources.GetStringFromByte(dataAndMAC));

            return true;
        }


        /// <summary>
        /// 产生0016文件MAC
        /// </summary>
        /// <param name="appMACRandom"></param>
        /// <param name="cardNo"></param>
        /// <param name="userName"></param>
        /// <param name="identifyType"></param>
        /// <param name="identifyNum"></param>
        /// <param name="dataAndMAC"></param>
        /// <returns></returns>
        public bool GeneralAppMAC0016(byte[] appMACRandom
                                        , string cardNo
                                        , string userName
                                        , int identifyType
                                        , string identifyNum
                                        , ref byte[] dataAndMAC)
        {
            byte[] returnValue = new byte[1] { 0xFF };
            byte[] appMAC = new byte[4];
            byte[] MACData = new byte[68];

            //随机数
            FounctionResources.MemoryCopy(ref MACData, 0, appMACRandom, 0, 4);

            //随机数后备四字节0
            MACData[8] = 0x04;
            MACData[9] = 0xD6;
            MACData[10] = 0x96;
            MACData[11] = 0x00;
            MACData[12] = 0x3B;

            //持卡人身份标识不启用//[13]
            //本系统职工表示不启用//[14]
            //客户姓名
            byte[] nameBytes = new byte[20];
            byte[] encodedName = getEncoding.GetBytes(userName);
            FounctionResources.MemoryCopy(ref nameBytes, 0, encodedName, 0
                , (encodedName.Length > nameBytes.Length ? nameBytes.Length : encodedName.Length));
            FounctionResources.MemoryCopy(ref MACData, 15, nameBytes, 0, nameBytes.Length);

            //证件号
            byte[] identifyNo = new byte[32];
            byte[] convertIdentifyNo = getEncoding.GetBytes(identifyNum);
            FounctionResources.MemoryCopy(ref identifyNo, 0, convertIdentifyNo, 0
                , (convertIdentifyNo.Length > identifyNo.Length ? identifyNo.Length : convertIdentifyNo.Length));
            FounctionResources.MemoryCopy(ref MACData, 35, identifyNo, 0, identifyNo.Length);

            //证件类型
            MACData[67] = (byte)identifyType;

            //加密机调用失败则中断事务
            try
            {
                MACCompute macc = new MACCompute();
                macc.App_MAC_Generate(MACCompute.DAMK_MF_KEY
                                            ,FounctionResources.StringToByteSequenceWithin16(cardNo)
                                            ,MACData.Length
                                            ,MACData
                                            ,ref appMAC
                                            ,ref returnValue);
            }
            catch (Exception err)
            {
                //异常由内部细化后打印在控制台上，不向终端返回加密机的具体错误信息
                Log.WriteLog("连接加密机异常" + err.Message);
                return false;
            }

            if (returnValue[0] != 0x00)
            {
                Log.WriteLog("加密机计算MAC失败,返回值 0x" + returnValue[0].ToString("X2"));
                return false;
            }

            //经加密机调用要返回的数据
            FounctionResources.MemoryCopy(ref dataAndMAC, 0, MACData, 8, 60);
            FounctionResources.MemoryCopy(ref dataAndMAC, 60, appMAC, 0, 4);

            Log.WriteLog("GeneralAppMAC0016 执行成功，返回的写卡信息为：" + FounctionResources.GetStringFromByte(dataAndMAC));
            return true;
        }


        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="randomNum"></param>
        /// <returns></returns>
        public bool GetRandom(ref byte[] randomNum)
        {
            string methodName = "方法:取卡片随机数\t";
            bool ret = false;
            int sw12 = -1;
            int respLength = 4;
            byte[] resp = new byte[respLength];
            ret = this.device.CPUTimecosCmd("0084000004", ref resp, respLength, ref  sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.Close();
                Log.WriteLog(methodName + "执行失败，执行命令内容为0084000004，返回值信息为：" + sw12.ToString("X"));
                return false;
            }
            randomNum = resp;
            Log.WriteLog(methodName + "执行成功");
            return true;
        }

        /// <summary>
        /// 取卡片余额
        /// </summary>
        /// <param name="balanceInCard"></param>
        /// <returns></returns>
        public bool GetBalance(ref string balanceInCard)
        {
            bool ret = false;
            int sw12 = -1;
            int respLength = 4;
            byte[] resp = new byte[respLength];

            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLength, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                //MessageBox.Show("选择1001文件失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            ret = this.device.CPUTimecosCmd("805C000204", ref resp, respLength, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                return false;
            }
            balanceInCard = (FounctionResources.GetStringFromByte(resp));
            return true; 
        }

        public bool VerifyPIN()
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            //选择1001文件
            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                //this.device.CloseDevice();
                //MessageBox.Show("选择1001文件失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }
            //验证PIN
            ret = this.device.CPUTimecosCmd("0020000006313233343536", ref resp, respLenght, ref sw12);
            if (ret && sw12 == 0x9403)
            {
                //this.device.CloseDevice();
                //MessageBox.Show("无PIN", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return true;
            }
            else if (!ret || sw12 != 0x9000)
            {
                //this.device.CloseDevice();
                //MessageBox.Show("验PIN失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }
            return true;
        }

        //电子钱包0消费交易初始化
        public bool InitializeForOfflineSNZero(ref InitializeForPurchaseTimecosResponse initializeForPurchaseTimecosResponse)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;


            //选择1001文件
            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                //this.device.CloseDevice();
                //MessageBox.Show("选择1001文件失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }

            string cmd = "805001020B010000000000000000002E0F";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("电子钱包消费交易初始化失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }

            //卡内余额
            initializeForPurchaseTimecosResponse.oldBalance = FounctionResources.GetByteFromSpecialPosition(resp, 0, 4);
            //脱机交易序号
            initializeForPurchaseTimecosResponse.ICCardTransID = FounctionResources.GetByteFromSpecialPosition(resp, 4, 2);
            //透支限额
            initializeForPurchaseTimecosResponse.OverdraftLimit = FounctionResources.GetByteFromSpecialPosition(resp, 6, 3);
            //密钥版本号
            initializeForPurchaseTimecosResponse.keyVersionNo = resp[9];
            //算法标识
            initializeForPurchaseTimecosResponse.AlgorithmID = resp[10];
            //随机数
            initializeForPurchaseTimecosResponse.RandomNum = FounctionResources.GetByteFromSpecialPosition(resp, 11, 4);
            return true;
        }

        /// <summary>
        /// 电子钱包0圈存初始化
        /// </summary>
        /// <param name="initializeForLoadTimecosResponse"></param>
        /// <returns></returns>
        public bool InitializeForOnlineSZero(ref InitializeForLoadTimecosResponse initializeForLoadTimecosResponse)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;
            string cmd = null;

            ret = this.VerifyPIN();
            if (!ret)
            {
                MessageBox.Show("验pin失败");
                this.device.CloseDevice();
                return false;
            }

            cmd = "805000020B010000000000000000002E10";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret && sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("初始化失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                Log.WriteLog("电子钱包0金额圈存初始化失败，发送指令内容为" + cmd + "返回信息sw为" + sw12.ToString("X"));
                return false;
            }
                        //卡内余额
            initializeForLoadTimecosResponse.oldBalance = FounctionResources.GetByteFromSpecialPosition(resp, 0, 4);
            //脱机交易序号
            initializeForLoadTimecosResponse.ICCardTransID = FounctionResources.GetByteFromSpecialPosition(resp, 4, 2);
            //密钥版本号
            initializeForLoadTimecosResponse.keyVersionNo = resp[6];
            //算法标识
            initializeForLoadTimecosResponse.AlgorithmID = resp[7];
            //随机数
            initializeForLoadTimecosResponse.RandomNum = FounctionResources.GetByteFromSpecialPosition(resp, 8, 4);

            initializeForLoadTimecosResponse.MAC1 = FounctionResources.GetByteFromSpecialPosition(resp, 12, 4);
            return true;
        }

        /// <summary>
        /// 取消费TAC、MAC2
        /// </summary>
        /// <param name="TAC"></param>
        /// <param name="MAC2"></param>
        /// <returns></returns>
        public bool GetPurchaseTransactionProof(ref byte[] TAC, ref byte[] MAC2)
        {
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            TAC = new byte[4];
            MAC2 = new byte[4];

            //bool ret = this.VerifyPIN();
            //if (!ret)
            //{
            //    this.device.CloseDevice();
            //    return false;
            //}

            //选择1001文件
            bool ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                //this.device.CloseDevice();
                //MessageBox.Show("选择1001文件失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }

            string cmd = "805001020B010000000000000000002E0F";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("电子钱包消费交易初始化失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //ByteCopy.Memcpy(ref ICCardTransID, 0, resp, 4, 2);
            //ICCardTransID = FounctionResources.GetByteFromSpecialPosition(resp, 4, 2);
            string offlineSN = FounctionResources.GetStringFromByte(FounctionResources.GetByteFromSpecialPosition(resp,4,2));

            //cmd = "805A000602" + ICCardTransID[0].ToString("X2") + ICCardTransID[1].ToString("X2") + "08";
              cmd = "805A000602" + offlineSN + "08";

            //提取TAC或MAC
            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret)
            {
                this.device.CloseDevice();
                //MessageBox.Show("提取TAC或MAC失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (sw12 == 0x9000 || sw12 == 0x9406)
            {
                MAC2 = FounctionResources.GetByteFromSpecialPosition(resp, 0, 4);
                TAC = FounctionResources.GetByteFromSpecialPosition(resp, 4, 4);
                return true;
            }
            else
            {
                this.device.CloseDevice();
                //MessageBox.Show("提取TAC或MAC失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }
        }

        /// <summary>
        /// 取圈存TAC
        /// </summary>
        /// <param name="TAC"></param>
        /// <param name="MAC2"></param>
        /// <returns></returns>
        public bool GetLoadTransactionProof(ref byte[] TAC, ref byte[] MAC2)
        {
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            TAC = new byte[4];
            MAC2 = new byte[4];

            bool ret = this.VerifyPIN();
            if (!ret)
            {
                this.device.CloseDevice();
                return false;
            }

            string cmd = "805000020B010000000000000000002E10";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }
            string onlineSN = FounctionResources.GetStringFromByte(FounctionResources.GetByteFromSpecialPosition(resp, 4, 2));

            cmd = "805A000202" + onlineSN + "08";

            //提取TAC或MAC
            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret)
            {
                this.device.CloseDevice();
                return false;
            }
            else if (sw12 == 0x9000 || sw12 == 0x9406)
            {
                MAC2 = FounctionResources.GetByteFromSpecialPosition(resp, 0, 4);
                TAC = FounctionResources.GetByteFromSpecialPosition(resp, 4, 4);
                return true;
            }
            else
            {
                this.device.CloseDevice();
                //MessageBox.Show("提取TAC或MAC失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }


        /// <summary>
        /// 消费初始化
        /// </summary>
        /// <param name="amount">消费金额</param>
        /// <param name="initializeForPurchaseTimecosResponse"></param>
        /// <returns></returns>
        public bool InitializeForOfflineSN(int purchaseAmount, ref InitializeForPurchaseTimecosResponse initializeForPurchaseTimecosResponse)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;


            //选择1001文件
            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                //this.device.CloseDevice();
                //MessageBox.Show("选择1001文件失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }

            byte[] amountByte = FounctionResources.IntToByteInSequence(purchaseAmount);
            string amountString = FounctionResources.GetStringFromByte(amountByte);


            string cmd = "805001020B"  //电子钱包消费初始化timecos命令
                       + "01" + amountString   //1字节密钥标识符，字节交易金额
                       + MytoolsIniConstant.TerminalNo //6字节终端机编号
                       +"0F";          

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }

            //卡内余额
            initializeForPurchaseTimecosResponse.oldBalance = FounctionResources.GetByteFromSpecialPosition(resp, 0, 4);
            //脱机交易序号
            initializeForPurchaseTimecosResponse.ICCardTransID = FounctionResources.GetByteFromSpecialPosition(resp, 4, 2);
            //透支限额
            initializeForPurchaseTimecosResponse.OverdraftLimit = FounctionResources.GetByteFromSpecialPosition(resp, 6, 3);
            //密钥版本号
            initializeForPurchaseTimecosResponse.keyVersionNo = resp[9];
            //算法标识
            initializeForPurchaseTimecosResponse.AlgorithmID = resp[10];
            //随机数
            initializeForPurchaseTimecosResponse.RandomNum = FounctionResources.GetByteFromSpecialPosition(resp, 11, 4);
            return true;

        }

        /// <summary>
        /// 复合消费消费初始化
        /// </summary>
        /// <param name="purchaseAmount"></param>
        /// <param name="initializeForPurchaseTimecosResponse"></param>
        /// <returns></returns>
        public bool InitializeForCappPurchase(int purchaseAmount, ref InitializeForPurchaseTimecosResponse initializeForPurchaseTimecosResponse)
        {
            bool operationOK = false;

            try
            {
                bool commandResult = false;

                int respLenght = 300;

                byte[] resp = new byte[respLenght];

                int sw12 = -1;

                //选择1001文件
                commandResult = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
                if (!commandResult || sw12 != 0x9000)
                {
                    MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                    operationOK = false;
                    throw new Exception("执行命令失败 选择1001文件失败。");
                }

                //消费金额用字符数组标识
                byte[] amountByte = FounctionResources.IntToByteInSequence(purchaseAmount);

                string amountString = FounctionResources.GetStringFromByte(amountByte);

                string cmd = "805003020B"  //电子钱包消费初始化timecos命令
                           + "01" + amountString   //1字节密钥标识符，字节交易金额
                           + MytoolsIniConstant.TerminalNo //6字节终端机编号
                           + "0F";

                commandResult = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
                if (!commandResult || sw12 != 0x9000)
                {
                    this.device.CloseDevice();
                    MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                    operationOK = false;
                    throw new Exception("执行复合消费初始化命令失败");
                }

                //卡内余额
                initializeForPurchaseTimecosResponse.oldBalance = FounctionResources.GetByteFromSpecialPosition(resp, 0, 4);
                //脱机交易序号
                initializeForPurchaseTimecosResponse.ICCardTransID = FounctionResources.GetByteFromSpecialPosition(resp, 4, 2);
                //透支限额
                initializeForPurchaseTimecosResponse.OverdraftLimit = FounctionResources.GetByteFromSpecialPosition(resp, 6, 3);
                //密钥版本号
                initializeForPurchaseTimecosResponse.keyVersionNo = resp[9];
                //算法标识
                initializeForPurchaseTimecosResponse.AlgorithmID = resp[10];
                //随机数
                initializeForPurchaseTimecosResponse.RandomNum = FounctionResources.GetByteFromSpecialPosition(resp, 11, 4);

                operationOK = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                operationOK = false;
            }
            return operationOK;
        }

        
        /// <summary>
        /// 更新复合消费缓存
        /// </summary>
        /// <returns></returns>
        public bool UpdateCappDataCache()
        {
            bool operationOK = false;
            try
            {
                bool commandResult = false;

                int respLenght = 300;

                byte[] resp = new byte[respLenght];

                int sw12 = -1;

                string passInfo = "AA";//文件标识符，1字节
                passInfo += "2B00";//记录长度43字节+应用锁定标识，00标识未锁定，2字节
                passInfo += "1101";//入口路网号，2字节
                passInfo += "0100";//入口收费站号，2字节
                passInfo += "01";//入口车道号，1字节
                passInfo += "0FFFFFFF";//入口时间，4字节
                passInfo += "01";//车型，1字节
                passInfo += "01";//车道口类型：封闭式ＭＴＣ入口，1字节
                passInfo += "000000000000000000";//标识站,9个字节
                passInfo += "000099";//收费员工号，3字节
                passInfo += "01";//入口班次，1字节
                passInfo += "000000000000000000000000";//车牌号码，12个字节
                passInfo += "00000000";//预留4字节

                Console.WriteLine(passInfo.Length.ToString());
                
                string cmd = "80DCAAC827"  //更新缓存
                           + passInfo;    //1字节密钥标识符，字节交易金额

                //复合消费更新通行记录
                commandResult = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
                if (!commandResult || sw12 != 0x9000)
                {
                    this.device.CloseDevice();
                    MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                    operationOK = false;
                    throw new Exception("执行复合消费初始化命令失败");
                }
                operationOK = true;
            }
            catch (Exception err)
            {

            }

            return operationOK;
        }


        /// <summary>
        /// 复合消费
        /// </summary>
        /// <param name="terminalNo"></param>
        /// <param name="operationTime"></param>
        /// <param name="MAC1"></param>
        /// <param name="MAC2"></param>
        /// <param name="TAC"></param>
        /// <returns></returns>
        public bool CardCappPurse(string terminalNo
                             , DateTime operationTime
                             , byte[] MAC1
                             , ref byte[] MAC2
                             , ref byte[] TAC)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;
            string cmd = null;
            byte[] operationTime_Byte = FounctionResources.DateTransToByte(operationTime);
            string operation_String = FounctionResources.GetStringFromByte(operationTime_Byte);
            string terminalNo_ForPurse = terminalNo.Substring(4, 8);
            string MAC1_String = FounctionResources.GetStringFromByte(MAC1);

            cmd = "805401000F"
                + terminalNo_ForPurse
                + operation_String
                + MAC1_String
                + "08";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            TAC[0] = resp[0];
            TAC[1] = resp[1];
            TAC[2] = resp[2];
            TAC[3] = resp[3];
            MAC2[0] = resp[4];
            MAC2[1] = resp[5];
            MAC2[2] = resp[6];
            MAC2[3] = resp[7];

            return true;
        }


        /// <summary>
        /// 圈存初始化
        /// </summary>
        /// <param name="loadAmount">圈存金额</param>
        /// <param name="initializeForLoadTimecosResponse">返回圈存结构体</param>
        /// <returns></returns>
        public bool InitializeForOnlineSN(uint loadAmount, ref InitializeForLoadTimecosResponse initializeForLoadTimecosResponse)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;
            string cmd = null;

            ret = this.VerifyPIN();
            if (!ret)
            {
                this.device.CloseDevice();
                return false;
            }

            byte[] amountByte = FounctionResources.IntToByteInSequence(loadAmount);
            string amountString = FounctionResources.GetStringFromByte(amountByte);

            cmd = "805000020B"      //圈存初始化
                + "01" + amountString   //1字节密钥标识符，4字节交易金额
                + MytoolsIniConstant.TerminalNo    //终端机编号
                + "10";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret && sw12 != 0x9000)
            {
                this.device.CloseDevice();
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }
            //卡内余额
            initializeForLoadTimecosResponse.oldBalance = FounctionResources.GetByteFromSpecialPosition(resp, 0, 4);
            //脱机交易序号
            initializeForLoadTimecosResponse.ICCardTransID = FounctionResources.GetByteFromSpecialPosition(resp, 4, 2);
            //密钥版本号
            initializeForLoadTimecosResponse.keyVersionNo = resp[6];
            //算法标识
            initializeForLoadTimecosResponse.AlgorithmID = resp[7];
            //随机数
            initializeForLoadTimecosResponse.RandomNum = FounctionResources.GetByteFromSpecialPosition(resp, 8, 4);

            initializeForLoadTimecosResponse.MAC1 = FounctionResources.GetByteFromSpecialPosition(resp, 12, 4);
            return true;
        }

        /// <summary>
        /// 卡片圈存
        /// </summary>
        /// <param name="operationTime"></param>
        /// <param name="MAC2"></param>
        /// <param name="TAC"></param>
        /// <returns></returns>
        public bool CardLoad(DateTime operationTime, byte[] MAC2 , ref byte[] TAC)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;
            string cmd = null;
            byte[] operationTime_Byte = FounctionResources.DateTransToByte(operationTime);
            string operation_String = FounctionResources.GetStringFromByte(operationTime_Byte);
            string MAC2_String = FounctionResources.GetStringFromByte(MAC2);
            cmd = "805200000B" + operation_String + MAC2_String + "04";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                return false;
            }
            TAC = resp;

            return true;
        }


        /// <summary>
        /// 验证卡片圈存初始化返回的MAC1并生成MAC2
        /// </summary>
        /// <param name="cardNo">卡号</param>
        /// <param name="balanceInCard">卡内余额</param>
        /// <param name="loadAmount">圈存金额</param>
        /// <param name="terminalNo">终端交易序号</param>
        /// <param name="operationTime">操作时间</param>
        /// <param name="random">初始化返回的伪随机数</param>
        /// <param name="onlineSN">联机交易序号</param>
        /// <param name="MAC1">初始化返回的MAC1</param>
        /// <param name="MAC2">准备送往卡内的MAC2</param>
        /// <returns></returns>
        public bool VerifyMAC1ForLoadAndGeneralMAC2(string cardNo
                                      , uint balanceInCard
                                      , uint loadAmount
                                      , string terminalNo
                                      , DateTime operationTime
                                      , byte []random
                                      , int onlineSN
                                      , byte []MAC1
                                      , ref byte []MAC2)
        {

            bool operationResult = false;
            try
            {
                byte[] div_factor = FounctionResources.StringToByteSequenceWithin16(cardNo);
                byte[] sessionKey = new byte[8];
                byte[] MAC1_Data = new byte[15];
                byte[] returnCode = new byte[1];
                byte[] MAC2_Data = new byte[18];

                byte[] operationTime_Byte = FounctionResources.DateTransToByte(operationTime);
                byte[] cardNo_Byte = FounctionResources.StringToByteSequenceWithin16(cardNo);
                byte[] terminalNo_Byte = FounctionResources.StringToByteSequenceWithin16(terminalNo);
                byte[] onlineSN_Byte = FounctionResources.IntToByteInSequence(onlineSN);
                byte[] balanceInCard_Byte = FounctionResources.IntToByteInSequence(balanceInCard);
                byte[] loadAmount_Byte = FounctionResources.IntToByteInSequence(loadAmount);

                //过程密钥因子
                sessionKey[0] = random[0];
                sessionKey[1] = random[1];
                sessionKey[2] = random[2];
                sessionKey[3] = random[3];
                sessionKey[4] = onlineSN_Byte[2];
                sessionKey[5] = onlineSN_Byte[3];
                sessionKey[6] = 0x80;
                sessionKey[7] = 0x00;

                //MAC1验证所需要的数据
                MAC1_Data[0] = balanceInCard_Byte[0];
                MAC1_Data[1] = balanceInCard_Byte[1];
                MAC1_Data[2] = balanceInCard_Byte[2];
                MAC1_Data[3] = balanceInCard_Byte[3];
                MAC1_Data[4] = loadAmount_Byte[0];
                MAC1_Data[5] = loadAmount_Byte[1];
                MAC1_Data[6] = loadAmount_Byte[2];
                MAC1_Data[7] = loadAmount_Byte[3];
                MAC1_Data[8] = 0x02;
                MAC1_Data[9] = terminalNo_Byte[0];
                MAC1_Data[10] = terminalNo_Byte[1];
                MAC1_Data[11] = terminalNo_Byte[2];
                MAC1_Data[12] = terminalNo_Byte[3];
                MAC1_Data[13] = terminalNo_Byte[4];
                MAC1_Data[14] = terminalNo_Byte[5];

                //MAC2生成所需要的数据
                MAC2_Data[0] = loadAmount_Byte[0];
                MAC2_Data[1] = loadAmount_Byte[1];
                MAC2_Data[2] = loadAmount_Byte[2];
                MAC2_Data[3] = loadAmount_Byte[3];
                MAC2_Data[4] = 0x02;
                MAC2_Data[5] = terminalNo_Byte[0];
                MAC2_Data[6] = terminalNo_Byte[1];
                MAC2_Data[7] = terminalNo_Byte[2];
                MAC2_Data[8] = terminalNo_Byte[3];
                MAC2_Data[9] = terminalNo_Byte[4];
                MAC2_Data[10] = terminalNo_Byte[5];
                MAC2_Data[11] = operationTime_Byte[0];
                MAC2_Data[12] = operationTime_Byte[1];
                MAC2_Data[13] = operationTime_Byte[2];
                MAC2_Data[14] = operationTime_Byte[3];
                MAC2_Data[15] = operationTime_Byte[4];
                MAC2_Data[16] = operationTime_Byte[5];
                MAC2_Data[17] = operationTime_Byte[6];

                MACCompute macc = new MACCompute();

                bool result = macc.MAC_VerifyAndGenerate(MACCompute.DLK1_KEY, 1, cardNo_Byte, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1,
                    MAC2_Data.Length, MAC2_Data, ref MAC2, ref returnCode);

                ////这里并没有采用系统内原先使用的验证与生成一起的加密机接口，而且分离了验证与生成
                //macc.MAC_Verify(MACCompute.DLK1_KEY, 1, cardNo_Byte, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, ref returnCode);

                //if (returnCode[0] != 0x00)
                //{
                //    throw new Exception("加密机计算MAC失败,返回值 0x" + returnCode[0].ToString("X2"));
                //}

                //macc.MAC_Generate(MACCompute.DLK1_KEY, 1, cardNo_Byte, sessionKey, MAC2_Data.Length, MAC2_Data, MAC2, ref returnCode);
                if (result == false || returnCode[0] != 0x00)
                {
                    throw new Exception("加密机计算MAC失败,返回值 0x" + returnCode[0].ToString("X2"));
                }
                operationResult = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                operationResult = false;
            }

            return operationResult;
        }


        /// <summary>
        /// 产生消费MAC1
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="terminalNo"></param>
        /// <param name="occurTime"></param>
        /// <param name="randomNum"></param>
        /// <param name="offlineSN"></param>
        /// <param name="purchaseAmount"></param>
        /// <param name="MAC1"></param>
        /// <returns></returns>
        public bool GeneralPurseMAC1(string cardNo
                                    , string terminalNo
                                    , DateTime occurTime
                                    , byte[] randomNum
                                    , int offlineSN
                                    , int purchaseAmount
                                    , ref byte[] MAC1)
        {
            bool operationResult = false;
            byte[] sessionKey = new byte[8];
            byte[] MAC1_Data = new byte[18];
            byte[] terminalNo_Byte = new byte[6];
            byte[] offlineSN_Byte = new byte[2];
            byte[] purchaseAmount_Byte = new byte[4];
            byte[] occurTime_Byte = new byte[7];
            byte[] returnCode = new byte[1];

            try
            {
                byte[] div_factor = FounctionResources.StringToByteSequenceWithin16(cardNo);
                offlineSN_Byte = FounctionResources.IntToByteInSequence(offlineSN);
                terminalNo_Byte = FounctionResources.StringToByteSequenceWithin16(terminalNo);
                purchaseAmount_Byte = FounctionResources.IntToByteInSequence(purchaseAmount);
                occurTime_Byte = FounctionResources.DateTransToByte(occurTime);

                sessionKey[0] = randomNum[0];
                sessionKey[1] = randomNum[1];
                sessionKey[2] = randomNum[2];
                sessionKey[3] = randomNum[3];
                sessionKey[4] = offlineSN_Byte[2];
                sessionKey[5] = offlineSN_Byte[3];
                sessionKey[6] = terminalNo_Byte[4];
                sessionKey[7] = terminalNo_Byte[5];

                MAC1_Data[0] = purchaseAmount_Byte[0];
                MAC1_Data[1] = purchaseAmount_Byte[1];
                MAC1_Data[2] = purchaseAmount_Byte[2];
                MAC1_Data[3] = purchaseAmount_Byte[3];
                MAC1_Data[4] = 0x06;
                MAC1_Data[5] = terminalNo_Byte[0];
                MAC1_Data[6] = terminalNo_Byte[1];
                MAC1_Data[7] = terminalNo_Byte[2];
                MAC1_Data[8] = terminalNo_Byte[3];
                MAC1_Data[9] = terminalNo_Byte[4];
                MAC1_Data[10] = terminalNo_Byte[5];
                MAC1_Data[11] = occurTime_Byte[0];
                MAC1_Data[12] = occurTime_Byte[1];
                MAC1_Data[13] = occurTime_Byte[2];
                MAC1_Data[14] = occurTime_Byte[3];
                MAC1_Data[15] = occurTime_Byte[4];
                MAC1_Data[16] = occurTime_Byte[5];
                MAC1_Data[17] = occurTime_Byte[6];

                MACCompute macc = new MACCompute();
                bool result = macc.MAC_Generate(MACCompute.DPK1_KEY, 1, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, ref returnCode);
                if (result == false || returnCode[0] != 0x00)
                {
                    throw new Exception("加密机计算MAC失败,返回值 0x" + returnCode[0].ToString("X2"));
                }
                operationResult = true;
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
                operationResult = false;
            }
            return operationResult;
        }

        public bool GeneralPurseCappMAC1(string cardNo
                            , string terminalNo
                            , DateTime occurTime
                            , byte[] randomNum
                            , int offlineSN
                            , int purchaseAmount
                            , ref byte[] MAC1)
        {
            bool operationResult = false;
            byte[] sessionKey = new byte[8];
            byte[] MAC1_Data = new byte[18];
            byte[] terminalNo_Byte = new byte[6];
            byte[] offlineSN_Byte = new byte[2];
            byte[] purchaseAmount_Byte = new byte[4];
            byte[] occurTime_Byte = new byte[7];
            byte[] returnCode = new byte[1];

            try
            {
                byte[] div_factor = FounctionResources.StringToByteSequenceWithin16(cardNo);
                offlineSN_Byte = FounctionResources.IntToByteInSequence(offlineSN);
                terminalNo_Byte = FounctionResources.StringToByteSequenceWithin16(terminalNo);
                purchaseAmount_Byte = FounctionResources.IntToByteInSequence(purchaseAmount);
                occurTime_Byte = FounctionResources.DateTransToByte(occurTime);

                sessionKey[0] = randomNum[0];
                sessionKey[1] = randomNum[1];
                sessionKey[2] = randomNum[2];
                sessionKey[3] = randomNum[3];
                sessionKey[4] = offlineSN_Byte[2];
                sessionKey[5] = offlineSN_Byte[3];
                sessionKey[6] = terminalNo_Byte[4];
                sessionKey[7] = terminalNo_Byte[5];

                MAC1_Data[0] = purchaseAmount_Byte[0];
                MAC1_Data[1] = purchaseAmount_Byte[1];
                MAC1_Data[2] = purchaseAmount_Byte[2];
                MAC1_Data[3] = purchaseAmount_Byte[3];
                MAC1_Data[4] = 0x09;
                MAC1_Data[5] = terminalNo_Byte[0];
                MAC1_Data[6] = terminalNo_Byte[1];
                MAC1_Data[7] = terminalNo_Byte[2];
                MAC1_Data[8] = terminalNo_Byte[3];
                MAC1_Data[9] = terminalNo_Byte[4];
                MAC1_Data[10] = terminalNo_Byte[5];
                MAC1_Data[11] = occurTime_Byte[0];
                MAC1_Data[12] = occurTime_Byte[1];
                MAC1_Data[13] = occurTime_Byte[2];
                MAC1_Data[14] = occurTime_Byte[3];
                MAC1_Data[15] = occurTime_Byte[4];
                MAC1_Data[16] = occurTime_Byte[5];
                MAC1_Data[17] = occurTime_Byte[6];

                MACCompute macc = new MACCompute();
                bool result = macc.MAC_Generate(MACCompute.DPK1_KEY, 1, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, ref returnCode);
                if (result == false || returnCode[0] != 0x00)
                {
                    throw new Exception("加密机计算MAC失败,返回值 0x" + returnCode[0].ToString("X2"));
                }
                operationResult = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                operationResult = false;
            }
            return operationResult;
        }


        /// <summary>
        /// 卡片消费
        /// </summary>
        /// <param name="terminalNo">终端机编号</param>
        /// <param name="operationTime">操作时间</param>
        /// <param name="MAC1">MAC1</param>
        /// <param name="MAC2">MAC2</param>
        /// <param name="TAC">TAC</param>
        /// <returns></returns>
        public bool CardPurse(string terminalNo
                             ,DateTime operationTime
                             ,byte[] MAC1
                             ,ref byte[] MAC2
                             ,ref byte[] TAC)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;
            string cmd = null;
            byte[] operationTime_Byte = FounctionResources.DateTransToByte(operationTime);
            string operation_String = FounctionResources.GetStringFromByte(operationTime_Byte);
            string terminalNo_ForPurse = terminalNo.Substring(4, 8);
            string MAC1_String = FounctionResources.GetStringFromByte(MAC1);

            cmd = "805401000F"
                + terminalNo_ForPurse
                + operation_String
                + MAC1_String
                +"08";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12),"卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            TAC[0] = resp[0];
            TAC[1] = resp[1];
            TAC[2] = resp[2];
            TAC[3] = resp[3];
            MAC2[0] = resp[4];
            MAC2[1] = resp[5];
            MAC2[2] = resp[6];
            MAC2[3] = resp[7];

            return true;

        }


        public bool Read0019(ref byte[] file0019Content)
        {
            bool ret = false;
            int respLenght = 39;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }

            ret = this.device.CPUTimecosCmd("00B2AA0027", ref file0019Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                MessageBox.Show(Enum.Card_ReturnSW(sw12), "卡片返回值信息");
                this.device.CloseDevice();
                return false;
            }
            this.device.CloseDevice();
            return true;
        }

        /// <summary>
        /// 写cos内文件
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool WriteFile(string command)
        {
            string methodName = "方法：写CPU卡文件\t";
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            //选择1001文件
            ret = this.device.CPUTimecosCmd(command, ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("写文件失败", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log.WriteLog(methodName + "执行失败，执行指令内容信息为："+ command + "返回错误码为："+ sw12.ToString("X"));
                return false;
            }
            Log.WriteLog(methodName + "执行成功，执行指令内容信息为：" + command);
            return true;
        }


        //选择1001文件
        public bool Select1001Directory()
        {
            string methodName = "方法：选择卡片1001文件功能\t";
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            //选择1001文件
            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("选择卡片目录失败", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Log.WriteLog(methodName + "失败,执行之类内容为：00A40000021001，返回值为：" + sw12.ToString("X"));
                return false;
            }
            Log.WriteLog(methodName + "执行成功");
            return true;
        }

        //选择文件根目录
        public bool SelectRootDirectory()
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            //选择根目录
            ret = this.device.CPUTimecosCmd("00A40000023F00", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("选择卡片目录失败", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


    }
}
