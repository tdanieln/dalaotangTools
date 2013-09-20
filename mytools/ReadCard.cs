using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mytools
{
    public class ReadCard
    {
        CallWatchDataDevice device = new CallWatchDataDevice();

        //复位设备与卡片
        public bool ResetDeviceAndCard()
        {
            bool ret = false;
            ret = device.InitDevice("1");
            if (!ret)
            {
                MessageBox.Show("打开设备设备！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                device.CloseDevice();
                return false;
            }
            ret = device.InitCPUcard();
            if (!ret)
            {
                MessageBox.Show("卡片初始化失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                device.CloseDevice();
                return false;
            }
            return true;
        }

        //关闭设备
        public bool Close()
        {
            device.CloseDevice();
            return true;
        }

        public bool Read0015(ref byte[] file0015Content)
        {
            bool ret = false;
            int respLenght = 43;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            ret = device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }

            ret = device.CPUTimecosCmd("00B095002B", ref file0015Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }
            device.CloseDevice();
            return true;
        }

        public bool Read0015(ref File0015Content file0015Content)
        {
            bool ret = false;
            int respLenght = 43;
            byte[] resp = new byte[respLenght];
            byte[] byteFile0015Content = new byte[respLenght];
            int sw12 = -1;
       

            ret = device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }

            ret = device.CPUTimecosCmd("00B095002B", ref byteFile0015Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }
            device.CloseDevice();
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




        public bool Read0016(ref string strFile0016Content ,ref File0016Content file0016Content)
        {
            bool ret = false;
            int sw12 = -1;
            int respLenght = 55;
            byte[] resp = new byte[respLenght];
            byte[] byteFile0016Content = new byte[respLenght]; 

            ret = device.CPUTimecosCmd("00A40000023F00", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }

            ret = device.CPUTimecosCmd("00B0960037", ref byteFile0016Content, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }
            strFile0016Content = FounctionResources.GetStringFromByte(byteFile0016Content);
            file0016Content.CardholderID = byteFile0016Content[0];
            file0016Content.WorkerID = byteFile0016Content[1];
            file0016Content.NameofCardholder = FounctionResources.GetByteFromSpecialPosition(byteFile0016Content, 2, 20);
            file0016Content.CardholderCertificateNum = FounctionResources.GetByteFromSpecialPosition(byteFile0016Content
                                                    , 22, 32);
            file0016Content.TypeofCardholderCertificate = byteFile0016Content[54];
            device.CloseDevice();
            return true;
        }

        public bool Read0018()
        {

            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;
            byte[] file0018 = new byte[23];


            bool ret = this.VerifyPIN();
            if (!ret)
            {
                this.device.CloseDevice();
                return false;
            }

            ret = device.CPUTimecosCmd("00A40000021001", ref resp, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }
            ret = device.CPUTimecosCmd("00B2980017", ref file0018, respLenght, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }

            return true;
        }

        public bool GetRandom(ref byte[] randomNum)
        {
            bool ret = false;
            int sw12 = -1;
            int respLength = 4;
            byte[] resp = new byte[respLength];
            ret = device.CPUTimecosCmd("0084000004", ref resp, respLength, ref  sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.Close();
                return false;
            }
            randomNum = resp;
            return true;
        }

        public bool GetBalance(ref int balanceInCard)
        {
            bool ret = false;
            int sw12 = -1;
            int respLength = 4;
            byte[] resp = new byte[respLength];

            ret = this.device.CPUTimecosCmd("00A40000021001", ref resp, respLength, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("选择1001文件失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            ret = device.CPUTimecosCmd("805C000204", ref resp, respLength, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                device.CloseDevice();
                return false;
            }
            balanceInCard = Convert.ToInt32(FounctionResources.GetStringFromByte(resp),16);
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
                return false;
            }
            return true;
        }

        //电子钱包消费交易初始化
        public bool InitializeForOfflineSN(ref InitializeForPurchaseTimecosResponse initializeForPurchaseTimecosResponse)
        {
            bool ret = false;
            int respLenght = 300;
            byte[] resp = new byte[respLenght];
            int sw12 = -1;

            ret = this.VerifyPIN();
            if (!ret)
            {
                this.device.CloseDevice();
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

            //ByteCopy.Memcpy(ref initializeForPurchaseTimecosResponse.oldBalance, 0, resp, 0, 4);
            //ByteCopy.Memcpy(ref initializeForPurchaseTimecosResponse.ICCardTransID, 0, resp, 4, 2);
            //ByteCopy.Memcpy(ref initializeForPurchaseTimecosResponse.OverdraftLimit, 0, resp, 6, 3);
            //initializeForPurchaseTimecosResponse.keyVersionNo = resp[9];
            //initializeForPurchaseTimecosResponse.AlgorithmID = resp[10];
            //ByteCopy.Memcpy(ref initializeForPurchaseTimecosResponse.RandomNum, 0, resp, 11, 4);

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


        public bool InitializeForOnlineSN(ref InitializeForLoadTimecosResponse initializeForLoadTimecosResponse)
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

            cmd = "805000020B010000000000000000002E10";

            ret = this.device.CPUTimecosCmd(cmd, ref resp, respLenght, ref sw12);
            if (!ret && sw12 != 0x9000)
            {
                this.device.CloseDevice();
                //MessageBox.Show("初始化失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //ByteCopy.Memcpy(ref initializeForLoadTimecosResponse.oldBalance, 0, resp, 0, 4);
            //ByteCopy.Memcpy(ref initializeForLoadTimecosResponse.ICCardTransID, 0, resp, 4, 2);
            //initializeForLoadTimecosResponse.keyVersionNo = resp[6];
            //initializeForLoadTimecosResponse.AlgorithmID = resp[7];
            //ByteCopy.Memcpy(ref initializeForLoadTimecosResponse.RandomNum, 0, resp, 8, 4);
            //ByteCopy.Memcpy(ref initializeForLoadTimecosResponse.MAC1, 0, resp, 12, 4);
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


        public bool GetPurchaseTransactionProof(ref byte[] TAC, ref byte[] MAC2)
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
                //ByteCopy.Memcpy(ref MAC2, 0, resp, 0, 4);
                //ByteCopy.Memcpy(ref TAC, 0, resp, 4, 4);
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
                //MessageBox.Show("电子钱包消费交易初始化失败！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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








    }
}
