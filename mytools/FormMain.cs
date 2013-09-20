using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrintClass;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace mytools
{
    public partial class FormMain : Custom_Form
    {
        PrintCls p;

        CallZTEOBUReader obuReader = new CallZTEOBUReader();
        //CallOBUReader obuReader = new CallWanJiOBUReader();

        public FormMain()
        {
            InitializeComponent();
            this.Focus();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ReadCardinfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReadCard formReadCard = new FormReadCard();
            formReadCard.Show();
        }

        private void MenuItem_CardApplying_Click(object sender, EventArgs e)
        {
            FormCardApplying formCardApplying = new FormCardApplying();
            formCardApplying.Show();
        }


        private void buttonQueryCard_Click(object sender, EventArgs e)
        {
            FormReadCard formReadCard = new FormReadCard();
            formReadCard.Show();
        }

        private void buttonReadConfig_Click(object sender, EventArgs e)
        {
            FormConfig formConfig = new FormConfig();
            formConfig.Show();
        }

        private void buttonDealwithCard_Click(object sender, EventArgs e)
        {
            FormCardApplying formCardApplying = new FormCardApplying();
            formCardApplying.Show();
        }

        private void buttonBankTest_Click(object sender, EventArgs e)
        {
            FormBankCreditXmlTest formBankCreditXmlTest = new FormBankCreditXmlTest();
            formBankCreditXmlTest.Show();
        }

        private void buttonPrintTest_Click(object sender, EventArgs e)
        {

            PaperSize ps = new PaperSize();

            ps.PaperName = "A3";

            //设置打印纸张和打印方向
            System.Drawing.Printing.PaperSize pkCustomSize1 = new System.Drawing.Printing.PaperSize("First   custom   size", 828, 1170);
            //printDocument1.DefaultPageSettings.Landscape = true;
            printDocument.DefaultPageSettings.PaperSize = pkCustomSize1;

            //printDocument.

            p = new PrintCls();

            printDialog.Document = printDocument;

            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
                printDocument.Print();

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            p = new PrintCls();
            p.PrintInitial(e);
            p.LineWidth = 1;

            p.CurrentX = 290;
            p.CurrentY = 60;
            p.Size = 15;
            p.Bold = true;
            p.PrintString("辽宁省高速公路电子收费预收款收据");

            p.CurrentX = 125;
            p.CurrentY = 115;
            p.Size = 12;
            p.Bold = false;
            p.PrintString(DateTime.Now.ToString("yyyy年MM月dd日"));

            p.CurrentX = 550;
            p.CurrentY = 115;
            p.PrintString("000000000000");

            /*
            p.CurrentX = 90;
            p.CurrentY = 140;
            p.PrintOutBox("受理网点   测试营业厅", 600, "l", 30);
            */



            //横线1
            p.PrintLine(130, 130, 670, 130);
            //横线2
            p.PrintLine(130, 155, 630, 155);
            //最下横线
            p.PrintLine(130, 350, 670, 350);


            //左竖线1
            p.PrintLine(130, 130, 130, 350);
            //右竖线
            p.PrintLine(630, 130, 630, 350);
            //右外竖线
            p.PrintLine(670, 130, 670, 350);

            p.CurrentX = 132;
            p.CurrentY = 135;
            p.PrintString("受理网点");

            p.CurrentY = 165;
            p.PrintString("客户名称");

            p.CurrentY = 205;
            p.PrintString("业务流水号");

            p.CurrentY = 245;
            p.PrintString("款项名称");

            p.CurrentY = 285;
            p.PrintString("金额");

            p.CurrentY = 325;
            p.PrintString("人民币");

        }

        private void buttonOBU_Click(object sender, EventArgs e)
        {
            string errInfo = "";

            int sz = Marshal.SizeOf(typeof(SysInfoType));

            IntPtr ptr = Marshal.AllocCoTaskMem(sz);
            try
            {
                SysInfoType sysInfoType = new SysInfoType(ptr);

                byte[] ip = new byte[13];

                //设备号
                string devNo = "192.168.9.107";

                int portNo = 55890;

                long fd = 0;

                //if (!obuReader.OBUProg_DevInit((int)RSUConnectMode.USB, "USB1", 0,ref errInfo))
                //    return;

                ////if (!obuReader.OBUProg_Read_SysInfo(ref sysInfoType, ref errInfo))
                ////    return;

                //if (!obuReader.OBUProg_DevRelease(ref errInfo))
                //    return;

                if (!obuReader.RSU_Open((int)RSUConnectMode.USB, "USB1", 0,ref fd, ref errInfo))
                    return;

                if (!obuReader.RSU_Close(fd, ref errInfo))
                    return;

                if (!obuReader.RSU_Open((int)RSUConnectMode.USB, "USB1", 0, ref fd, ref errInfo))
                    return;

                if (!obuReader.RSU_Init_rq(fd, ref errInfo))
                    return;



            }
            catch (Exception err)
            {

            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            System.Threading.Thread.Sleep(10);



        }

        private void buttonNetTest_Click(object sender, EventArgs e)
        {

            OperateCard operatecard = new OperateCard(MytoolsIniConstant.DeviceCompany);

            operatecard.ResetDeviceAndCard();

            File0015Content file0015Content = new File0015Content();

            bool ret = operatecard.Read0015(ref file0015Content);
            if (!ret)
            {
                MessageBox.Show("读卡片0015文件信息失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operatecard.Close();
                return;
            }

            byte[] randomNo = new byte[4] {0x00,0x00,0x00,0x00};
            byte[] terminalNo = FounctionResources.StringToByteSequenceWithin16(MytoolsIniConstant.TerminalNo);
            byte[] offlineSN = FounctionResources.IntToByteInSequence(1);
            byte[] purchaseAmount = FounctionResources.IntToByteInSequence(1);

            byte[] mac1 = new byte[4];

            TcpClient tcpclient = new TcpClient();
            try
            {
                tcpclient.Connect("192.168.1.134", 13331);
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
                tcpclient.Close();
                return;
            }

            NetworkStream netWorkStream = null;

            byte[] sendInfo = new byte[50];

            FounctionResources.MemoryCopy(ref sendInfo, 0, file0015Content.CardSerialNo, 0, file0015Content.CardSerialNo.Length); 
                //randomNo + terminalNo + offlineSN + purchaseAmount;


            try
            {
                netWorkStream = tcpclient.GetStream();
                netWorkStream.Write(sendInfo, 0, sendInfo.Length);
                netWorkStream.Flush();

                netWorkStream.Read(mac1, 0, 4);
            }
            catch (InvalidOperationException err)
            {
                Console.WriteLine(err.Message + "没有打开连接");
                return;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                netWorkStream.Close();
            }

            netWorkStream.Close();

            tcpclient.Close();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            #region 复合消费测试
            //OperateCard operateCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            //File0015Content file0015Content = new File0015Content();
            //string terminalNo = MytoolsIniConstant.TerminalNo;
            //DateTime operationTime = DateTime.Now;
            //byte[] random = new byte[4];
            //byte[] mac= new byte[4];
            //byte[] dataAndMac0015 = new byte[52];
            //byte[] dataAndMac0016 = new byte[64];
            //InitializeForLoadTimecosResponse initializeForLoadTimecosResponse = new InitializeForLoadTimecosResponse();
            ////打开设备
            //if (!operateCard.ResetDeviceAndCard())
            //    return;

            //if (!operateCard.Read0015(ref file0015Content))
            //    return;

            //if (!operateCard.SelectRootDirectory())
            //    return;

            //if (!operateCard.GetRandom(ref random))
            //    return;

            //if (!operateCard.GeneralAppMAC0016(random
            //                                  ,FounctionResources.GetStringFromByte(file0015Content.CardSerialNo)
            //                                  ,"ASDFJKL"
            //                                  ,1
            //                                  ,"110101111111112232"
            //                                  , ref dataAndMac0016))
            //    return;

            //if (!operateCard.WriteFile(FounctionResources.GetStringFromByte(dataAndMac0016)))
            //    return;

            //if (!operateCard.Select1001Directory())
            //    return;

            //if (!operateCard.GetRandom(ref random))
            //    return;

            //if (!operateCard.GeneralAppMAC0015(random
            //                             , FounctionResources.GetStringFromByte(file0015Content.CardSerialNo)
            //                             , "2101"
            //                             , DateTime.Now
            //                             , DateTime.Now
            //                             , false
            //                             , ""
            //                             , ref dataAndMac0015))
            //    return;

            //if (!operateCard.WriteFile(FounctionResources.GetStringFromByte(dataAndMac0015)))
            //    return;

            #endregion

            #region 我擦的
            /*
            int itemDistanse = 35;
            int iniY = 300;
            int money = 1234500;
            string A = "壹贰叁肆伍陆柒捌玖十壹贰叁肆伍陆柒捌玖十壹贰叁肆伍陆柒捌玖十";
            CallBeiYangPrinter beiYangPrinter = new CallBeiYangPrinter();
            string errInfo = "";
            if (!beiYangPrinter.InitialDev((int)DevConnMode.USB, "", 0, ref errInfo))
                return;

            if (A.Length < 17)
            {
                if (!beiYangPrinter.SetPrintInfo("客户名称：" + A, 150, iniY, ref errInfo))
                    return;
            }
            else if (A.Length >= 17 && A.Length < 33)
            {
                if (!beiYangPrinter.SetPrintInfo("客户名称：" + A.Substring(0, 16), 150, iniY, ref errInfo))
                    return;
                if (!beiYangPrinter.SetPrintInfo("          " + A.Substring(16), 150, iniY -= itemDistanse , ref errInfo))
                    return;
            }
            else
            {
                return;
            }

            beiYangPrinter.SetPrintInfo("缴费金额：" + (money/100m).ToString("0.00") + "元", 150, iniY -= itemDistanse, ref errInfo);

            beiYangPrinter.SetPrintInfo("缴款方式：", 450, iniY, ref errInfo);

            beiYangPrinter.SetPrintInfo("金额大写：" + FounctionResources.RMBDecAmountToUpCase(Convert.ToDouble(money / 100)), 150, iniY -= itemDistanse, ref errInfo);

            beiYangPrinter.SetPrintInfo("网点名称：", 150, iniY -= itemDistanse, ref errInfo);

            beiYangPrinter.SetPrintInfo("开票人： ", 150, iniY -= itemDistanse, ref errInfo);

            if (!beiYangPrinter.SetPrintInfo("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd"), 450, iniY, ref errInfo))
                return;

            if (!beiYangPrinter.Print(ref errInfo))
                return;

            if (!beiYangPrinter.CheckStatus(ref errInfo))
                return;



           // beiYangPrinter.OpenBYPrinter((int)DevConnMode.USB, "", 0, ref errInfo);
            */
            #endregion

            printDialog.Document = printDocument;
            DialogResult dr = printDialog.ShowDialog();
            if (dr == DialogResult.OK)
                printDocument.Print();

        }

        private void buttonComPrint_Click(object sender, EventArgs e)
        {
            int itemDistanse = 35;
            int iniY = 300;
            int money = 1234500;
            string A = "肆伍陆柒捌玖十壹贰叁肆";
            CallBeiYangPrinter beiYangPrinter = new CallBeiYangPrinter();
            string errInfo = "";
            if (!beiYangPrinter.InitialDev((int)DevConnMode.COM, "", 0, ref errInfo))
                return;

            if (A.Length < 17)
            {
                if (!beiYangPrinter.SetPrintInfo("客户名称：" + A, 150, iniY, ref errInfo))
                    return;
            }
            else if (A.Length >= 17 && A.Length < 33)
            {
                if (!beiYangPrinter.SetPrintInfo("客户名称：" + A.Substring(0, 16), 150, iniY, ref errInfo))
                    return;
                if (!beiYangPrinter.SetPrintInfo("          " + A.Substring(16), 150, iniY -= itemDistanse, ref errInfo))
                    return;
            }
            else
            {
                return;
            }

            beiYangPrinter.SetPrintInfo("缴费金额：" + (money / 100m).ToString("0.00") + "元", 150, iniY -= itemDistanse, ref errInfo);

            beiYangPrinter.SetPrintInfo("缴款方式：", 450, iniY, ref errInfo);

            beiYangPrinter.SetPrintInfo("金额大写：" + FounctionResources.RMBDecAmountToUpCase(Convert.ToDouble(money / 100)), 150, iniY -= itemDistanse, ref errInfo);

            beiYangPrinter.SetPrintInfo("网点名称：", 150, iniY -= itemDistanse, ref errInfo);

            beiYangPrinter.SetPrintInfo("开票人： ", 150, iniY -= itemDistanse, ref errInfo);

            if (!beiYangPrinter.SetPrintInfo("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd"), 450, iniY, ref errInfo))
                return;

            if (!beiYangPrinter.Print(ref errInfo))
                return;

            if (!beiYangPrinter.CheckStatus(ref errInfo))
                return;
        }


        //private void buttonReadVehicleInfo_Click(object sender, EventArgs e)
        //{

        //    string errInfo = "";

        //    int sz = Marshal.SizeOf(typeof(ETCVehicleInfoType));

        //    IntPtr ptr = Marshal.AllocCoTaskMem(sz);
        //    try
        //    {
        //        ETCVehicleInfoType etcVehicleInfoType = new ETCVehicleInfoType(ptr);

        //        byte[] ip = new byte[13];

        //        //设备号
        //        string devNo = "192.168.7.135";

        //        int portNo = 55890;

        //        if (!obuReader.OBUProg_DevInit((int)RSUConnectMode.TCPIP, devNo, portNo, ref errInfo))
        //            return;

        //        if (!obuReader.OBUProg_Read_ETCVehicleInfo(ref etcVehicleInfoType, ref errInfo))
        //            return;

        //        if (!obuReader.OBUProg_DevRelease(ref errInfo))
        //            return;
        //    }
        //    catch (Exception err)
        //    {

        //    }
        //    finally
        //    {
        //        Marshal.FreeHGlobal(ptr);
        //    }

        //    System.Threading.Thread.Sleep(10);
        //}

        //private void buttonWriteSysInfo_Click(object sender, EventArgs e)
        //{
        //    string errInfo = "";

        //    int portNo = 55890;

        //    string devNo = "192.168.7.135";

        //    int sz = Marshal.SizeOf(typeof(SysInfoType));
        //    IntPtr ptr = Marshal.AllocCoTaskMem(sz);
        //    SysInfoType readyToWrite = new SysInfoType(ptr);

        //    readyToWrite.contractSignedDate[0] = Convert.ToByte(DateTime.Now.ToString("yyyyMMdd").Substring(0, 2), 16);
        //    readyToWrite.contractSignedDate[1] = Convert.ToByte(DateTime.Now.ToString("yyyyMMdd").Substring(2, 2), 16);
        //    readyToWrite.contractSignedDate[2] = Convert.ToByte(DateTime.Now.ToString("yyyyMMdd").Substring(4, 2), 16);
        //    readyToWrite.contractSignedDate[3] = Convert.ToByte(DateTime.Now.ToString("yyyyMMdd").Substring(6, 2), 16);

        //    readyToWrite.contractExpiredDate[0] = Convert.ToByte(DateTime.Now.AddYears(5).ToString("yyyyMMdd").Substring(0, 2), 16);
        //    readyToWrite.contractExpiredDate[1] = Convert.ToByte(DateTime.Now.AddYears(5).ToString("yyyyMMdd").Substring(2, 2), 16);
        //    readyToWrite.contractExpiredDate[2] = Convert.ToByte(DateTime.Now.AddYears(5).ToString("yyyyMMdd").Substring(4, 2), 16);
        //    readyToWrite.contractExpiredDate[3] = Convert.ToByte(DateTime.Now.AddYears(5).ToString("yyyyMMdd").Substring(6, 2), 16);

        //    string contractProvider = "B1B1BEA900010001";
        //    for (int i = 0; i < (contractProvider.Length / 2); i++)
        //    {
        //        readyToWrite.contractProvider[i] = Convert.ToByte(contractProvider.Substring(i * 2, 2), 16);
        //    }

        //    readyToWrite.contractVersion = 0x11;

        //    readyToWrite.contractType = 0x00;

        //    string contractSerialNumber = "1101090900001002";

        //    for (int i = 0; i < (contractSerialNumber.Length / 2); i++)
        //    {
        //        readyToWrite.contractSerialNumber[i] = Convert.ToByte(contractSerialNumber.Substring(i * 2, 2), 16);
        //    }

        //    if (!obuReader.OBUProg_DevInit((int)RSUConnectMode.TCPIP, devNo, portNo, ref errInfo))
        //        return;

        //    if (!obuReader.OBUProg_Write_SysInfo(readyToWrite, ref errInfo))
        //        return;

        //    if (!obuReader.OBUProg_DevRelease(ref errInfo))
        //        return;


        //}

        //private void buttonWriteVehicleInfo_Click(object sender, EventArgs e)
        //{
        //    string errInfo = "";

        //    int portNo = 55890;

        //    string devNo = "192.168.7.135";

        //    int sz = Marshal.SizeOf(typeof(ETCVehicleInfoType));
        //    IntPtr ptr = Marshal.AllocCoTaskMem(sz);
        //    ETCVehicleInfoType readyToWrite = new ETCVehicleInfoType(ptr);

        //    if (!obuReader.OBUProg_DevInit((int)RSUConnectMode.TCPIP, devNo, portNo, ref errInfo))
        //        return;
        //    if (!obuReader.OBUProg_Write_ETCVehicleInfo(readyToWrite,ref errInfo))
        //        return;
        //    if (!obuReader.OBUProg_DevRelease(ref errInfo))
        //        return;
        //}

        //private void buttonObuOnlineActive_Click(object sender, EventArgs e)
        //{

        //}

        //private void buttonActiveServer_Click(object sender, EventArgs e)
        //{

        //    OBUOnlineServer obuonlineServer = new OBUOnlineServer();
        //    obuonlineServer.Start(IPAddress.Parse("192.168.1.134"),38324);

        //}

    }
}
