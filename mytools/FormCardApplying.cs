using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mytools
{
    public partial class FormCardApplying : Custom_Form
    {
        public FormCardApplying()
        {
            InitializeComponent();
        }

        private void buttonInCardQuery_Click(object sender, EventArgs e)
        {
            OperateCard operateCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            //初始化设备和卡片
            if (!operateCard.ResetDeviceAndCard())
                return;

            string strBalanceInCard = null;
            bool ret = operateCard.GetBalance(ref strBalanceInCard);
            if (!ret)
            {
                MessageBox.Show("读取卡片余额失败!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operateCard.Close();
                return;
            }
            operateCard.Close(); //关闭设备
            int balanceInCard = Convert.ToInt32(strBalanceInCard, 16);//将金额从16进制string转化为10进制int
            decimal balanceForShow = balanceInCard / 100m;
            textBoxBalanceBeforOperate.Text = balanceForShow.ToString("0.00");
        }

        private void textBoxLoadAmount_TextChanged(object sender, EventArgs e)
        {
            decimal result;
            if (!decimal.TryParse(textBoxLoadAmount.Text.Trim(),out result) && textBoxLoadAmount.Text != "")
            {
                MessageBox.Show("请输入正确的数字", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLoadAmount.Text = "";
                return;
            }
        }

        private void textBoxForPurchase_TextChanged(object sender, EventArgs e)
        {
            decimal result1;
            if (!decimal.TryParse(textBoxForPurchase.Text.Trim(),out result1) && textBoxForPurchase.Text != "")
            {
                MessageBox.Show("请输入正确的数字", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxForPurchase.Text = "";
                return;
            }
        }

        private void buttonCardPurchase_Click(object sender, EventArgs e)
        {
            if (textBoxForPurchase.Text == "")
            {
                MessageBox.Show("请填入正确的消费金额", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OperateCard operatecard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            File0015Content file0015Content = new File0015Content();
            int purchaseAmount = (int)(decimal.Parse(textBoxForPurchase.Text.Trim())*100);
            byte[] MAC1 = new byte[4];
            byte[] MAC2 = new byte[4];
            byte[] TAC = new byte[4];
            DateTime operationTime = DateTime.Now;
            string balanceInCard_String = null;
            InitializeForPurchaseTimecosResponse initializeForPurchaseTimecosResponse = new InitializeForPurchaseTimecosResponse();
            //打开设备
            operatecard.ResetDeviceAndCard();

            bool ret = operatecard.Read0015(ref file0015Content);
            if (!ret)
            {
                MessageBox.Show("读卡片0015文件信息失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operatecard.Close();
                return;
            }
            string cardNo = FounctionResources.GetStringFromByte(file0015Content.CardSerialNo);


            //初始化卡片
            ret = operatecard.InitializeForOfflineSN(purchaseAmount, ref initializeForPurchaseTimecosResponse);
            if (!ret)
            {
                MessageBox.Show("卡片初始化失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operatecard.Close();
                return;
            }

            int offlineSN = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForPurchaseTimecosResponse.ICCardTransID),16);

            ret = operatecard.GeneralPurseMAC1(cardNo
                                              , MytoolsIniConstant.TerminalNo
                                              ,operationTime
                                              ,initializeForPurchaseTimecosResponse.RandomNum
                                              ,offlineSN
                                              ,purchaseAmount
                                              ,ref MAC1);
            if (!ret)
            {
                MessageBox.Show("消费MAC生成失败");
                operatecard.Close();
                return;
            }

            ret = operatecard.CardPurse(MytoolsIniConstant.TerminalNo, operationTime, MAC1, ref MAC2, ref TAC);
            if (!ret)
            {
                MessageBox.Show("卡片消费失败");
                operatecard.Close();
                return;
            }


            ret = operatecard.GetBalance(ref balanceInCard_String);
            if (!ret)
            {
                MessageBox.Show("读卡内金额失败");
                operatecard.Close();
                return;
            }

            operatecard.Close();

            //还没有验TAC，有时间来写
            
            int balanceInCard_Int = Convert.ToInt32(balanceInCard_String,16);
            textBoxAfterCardOperate.Text =  (balanceInCard_Int/100m).ToString("0.00");


        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCardLoad_Click(object sender, EventArgs e)
        {
            if (textBoxLoadAmount.Text == "")
            {
                MessageBox.Show("请填入正确的消费金额", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OperateCard operateCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            File0015Content file0015Content = new File0015Content();
            string terminalNo = MytoolsIniConstant.TerminalNo;
            byte[] MAC2 = new byte[4];
            byte[] TAC = new byte[4];
            uint loadAmount = (uint)(decimal.Parse(textBoxLoadAmount.Text.Trim()) * 100);
            string balanceInCard_String = null;
            DateTime operationTime = DateTime.Now;
            InitializeForLoadTimecosResponse initializeForLoadTimecosResponse = new InitializeForLoadTimecosResponse();
            //打开设备
            operateCard.ResetDeviceAndCard();
            operateCard.Read0015(ref file0015Content);
            string cardNo = FounctionResources.GetStringFromByte(file0015Content.CardSerialNo);
            operateCard.GetBalance(ref balanceInCard_String);
            uint balanceInCard_Int = Convert.ToUInt32((balanceInCard_String),16);

            //圈存初始化
            bool ret = operateCard.InitializeForOnlineSN(loadAmount, ref initializeForLoadTimecosResponse);
            if (!ret)
            {
                MessageBox.Show("卡片圈存初始化失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operateCard.Close();
                return;
            }
            int onlineSN = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForLoadTimecosResponse.ICCardTransID), 16);


            ret = operateCard.VerifyMAC1ForLoadAndGeneralMAC2(cardNo
                                                             , balanceInCard_Int
                                                             , loadAmount
                                                             , terminalNo
                                                             , operationTime
                                                             , initializeForLoadTimecosResponse.RandomNum
                                                             , onlineSN
                                                             , initializeForLoadTimecosResponse.MAC1
                                                             , ref MAC2);
            if (!ret)
            {
                MessageBox.Show("MAC1验证MAC2生成失败！");
                operateCard.Close();
                return;
            }

            ret = operateCard.CardLoad(operationTime, MAC2, ref TAC) ;
            if (!ret)
            {
                MessageBox.Show("圈存失败");
                operateCard.Close();
                return;
            }

            ret = operateCard.GetBalance(ref balanceInCard_String);
            if (!ret)
            {
                MessageBox.Show("读卡内金额失败");
                operateCard.Close();
                return;
            }

            balanceInCard_Int = Convert.ToUInt32(balanceInCard_String, 16);
            textBoxAfterCardOperate.Text = (balanceInCard_Int / 100m).ToString("0.00");

            operateCard.Close();
        }

        private void buttonFFFFFFF_Click(object sender, EventArgs e)
        {
            OperateCard operateCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            File0015Content file0015Content = new File0015Content();
            string terminalNo = MytoolsIniConstant.TerminalNo;
            byte[] MAC2 = new byte[4];
            byte[] TAC = new byte[4];
            uint loadAmount = 0x00888888;
            string balanceInCard_String = null;
            DateTime operationTime = DateTime.Now;
            InitializeForLoadTimecosResponse initializeForLoadTimecosResponse = new InitializeForLoadTimecosResponse();
            //打开设备
            operateCard.ResetDeviceAndCard();
            operateCard.Read0015(ref file0015Content);
            string cardNo = FounctionResources.GetStringFromByte(file0015Content.CardSerialNo);
            operateCard.GetBalance(ref balanceInCard_String);
            uint balanceInCard_Int = Convert.ToUInt32((balanceInCard_String), 16);
            loadAmount -= balanceInCard_Int;
            //圈存初始化
            bool ret = operateCard.InitializeForOnlineSN(loadAmount, ref initializeForLoadTimecosResponse);
            if (!ret)
            {
                MessageBox.Show("卡片圈存初始化失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operateCard.Close();
                return;
            }
            int onlineSN = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForLoadTimecosResponse.ICCardTransID), 16);


            ret = operateCard.VerifyMAC1ForLoadAndGeneralMAC2(cardNo
                                                             , balanceInCard_Int
                                                             , loadAmount
                                                             , terminalNo
                                                             , operationTime
                                                             , initializeForLoadTimecosResponse.RandomNum
                                                             , onlineSN
                                                             , initializeForLoadTimecosResponse.MAC1
                                                             , ref MAC2);
            if (!ret)
            {
                MessageBox.Show("MAC1验证MAC2生成失败！");
                operateCard.Close();
                return;
            }

            ret = operateCard.CardLoad(operationTime, MAC2, ref TAC);
            if (!ret)
            {
                MessageBox.Show("圈存失败");
                operateCard.Close();
                return;
            }

            ret = operateCard.GetBalance(ref balanceInCard_String);
            if (!ret)
            {
                MessageBox.Show("读卡内金额失败");
                operateCard.Close();
                return;
            }

            balanceInCard_Int = Convert.ToUInt32(balanceInCard_String, 16);
            textBoxAfterCardOperate.Text = (balanceInCard_Int / 100m).ToString("0.00");

            operateCard.Close();

        }

        private void buttonCappPurchase_Click(object sender, EventArgs e)
        {
            if (textBoxForPurchase.Text == "")
            {
                MessageBox.Show("请填入正确的消费金额", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OperateCard operatecard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            File0015Content file0015Content = new File0015Content();
            int purchaseAmount = (int)(decimal.Parse(textBoxForPurchase.Text.Trim())*100);
            byte[] MAC1 = new byte[4];
            byte[] MAC2 = new byte[4];
            byte[] TAC = new byte[4];
            DateTime operationTime = DateTime.Now;
            InitializeForPurchaseTimecosResponse initializeForPurchaseTimecosResponse = new InitializeForPurchaseTimecosResponse();
            //打开设备
            operatecard.ResetDeviceAndCard();

            bool ret = operatecard.Read0015(ref file0015Content);
            if (!ret)
            {
                MessageBox.Show("读卡片0015文件信息失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operatecard.Close();
                return;
            }
            string cardNo = FounctionResources.GetStringFromByte(file0015Content.CardSerialNo);

            //复合消费初始化
            ret = operatecard.InitializeForCappPurchase(purchaseAmount, ref initializeForPurchaseTimecosResponse);
            if (!ret)
            {
                MessageBox.Show("卡片初始化失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operatecard.Close();
                return;
            }

            int offlineSN = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForPurchaseTimecosResponse.ICCardTransID),16);

            ret = operatecard.GeneralPurseCappMAC1(cardNo
                                              , MytoolsIniConstant.TerminalNo
                                              , operationTime
                                              , initializeForPurchaseTimecosResponse.RandomNum
                                              , offlineSN
                                              , purchaseAmount
                                              , ref MAC1);
            if (!ret)
            {
                MessageBox.Show("消费MAC生成失败");
                operatecard.Close();
                return;
            }

            ret = operatecard.UpdateCappDataCache();
            if (!ret)
            {
                MessageBox.Show("读卡片0015文件信息失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                operatecard.Close();
                return;
            }

            ret = operatecard.CardCappPurse(MytoolsIniConstant.TerminalNo, operationTime, MAC1, ref MAC2, ref TAC);
            if (!ret)
            {
                MessageBox.Show("卡片复合消费失败");
                operatecard.Close();
                return;
            }
        }
    }
}
