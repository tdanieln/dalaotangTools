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
    public partial class FormReadCard : Custom_Form
    {
       // private File0015Content file0015Content;

        public FormReadCard()
        {
            InitializeComponent();
        }

        private void buttonRead0015_Click(object sender, EventArgs e)
        {
            groupBox0015.Visible = true;
            groupBox0015.Enabled = true;
            OperateCard opearteCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            File0015Content file0015content = new File0015Content();
            //初始化设备与卡片
            if (!opearteCard.ResetDeviceAndCard())
                return;
            byte[] file0015 = new byte[43];
            if (!opearteCard.Read0015(ref file0015))
            {
                opearteCard.Close();
                return;
            }
            opearteCard.Close();

            opearteCard.ResetDeviceAndCard();

            if (!opearteCard.Read0015(ref file0015content))
            {
                opearteCard.Close();
                return;
            }
            opearteCard.Close();
            string strFile0015 = FounctionResources.GetStringFromByte(file0015);
            textShow.Text = strFile0015;
            string issueNo = strFile0015.Substring(0,16);

            //if (issueNo == "B1B1BEA900010001")
            //{
            //    textIssueNo.Text = "北京快通";
            //}
            //else
            //{
            //    MessageBox.Show("发行方不为快通公司");
            //    return;
            //}

            int cardType = Convert.ToInt32(file0015content.CardType);

            //int cardType = Convert.ToInt32(FounctionResources.GetCardIDFrom0015(strCardType),16);
            if (cardType == 22)
            {
                textBoxCardType.Text = "储值卡";
            }
            else if (cardType == 23)
            {
                textBoxCardType.Text = "记账卡";
            }

            string cardNetno = FounctionResources.GetStringFromByte(file0015content.CardNet);
            textBoxCardNetNo.Text = cardNetno;

            string cardNo = FounctionResources.GetStringFromByte(file0015content.CardSerialNo);
            textCardNo.Text = cardNo;

            string cardSignedDate = (FounctionResources.GetStringFromByte(file0015content.OpenTime));
            textBoxSignedDate.Text = cardSignedDate.Substring(0,4) + "年" + cardSignedDate.Substring(4,2)
                                   + "月" + cardSignedDate.Substring(6,2) + "日";

            string cardEndDate = FounctionResources.GetStringFromByte(file0015content.EndTime);
            textBoxEndDate.Text = cardEndDate.Substring(0, 4) + "年" + cardEndDate.Substring(4, 2)
                                   + "月" + cardEndDate.Substring(6, 2) + "日";
            string plateNo  = Encoding.GetEncoding("gb2312").GetString(file0015content.NumOfPlate).ToUpper();
            textBoxPlateNo.Text = plateNo;
            //textCardNo.Text = FounctionResources.GetCardIDFrom0015(cardNo);

            listViewOfCard.Items.Add(cardNo);

        }

        private void buttonRead0016_Click(object sender, EventArgs e)
        {
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            readCard.ResetDeviceAndCard();
            File0016Content file0016content = new File0016Content();
            string strFile0016Content = null;
            readCard.Read0016(ref strFile0016Content,ref file0016content);

            readCard.Close();

            textShow.Text = strFile0016Content;

            string userName = Encoding.GetEncoding("GB2312").GetString(file0016content.NameofCardholder);
            textBoxUserName.Text = userName;

            int userIdentfyCardType = Convert.ToInt32(file0016content.TypeofCardholderCertificate);
            switch (userIdentfyCardType)
            {
                case 0: textBox5.Text = "身份证";break;
                case 1: textBox5.Text = "军官证"; break;
                case 2: textBox5.Text = "护 照"; break;
                case 3: textBox5.Text = "入境证"; break;
                case 4: textBox5.Text = "临时身份证"; break;
            }

            string userIdentfyCardNo = Encoding.GetEncoding("GB2312").GetString(file0016content.CardholderCertificateNum);
            textBoxUserIdentfyNo.Text = userIdentfyCardNo;
        }

        private void buttonReadRandom_Click(object sender, EventArgs e)
        {
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            readCard.ResetDeviceAndCard();
            byte[] randomNum = new byte[4];
            readCard.GetRandom(ref randomNum);
            readCard.Close();
            textShow.Text = "本次随机数为:" + FounctionResources.GetStringFromByte(randomNum);
        }

        private void button_Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReadBalance_Click(object sender, EventArgs e)
        {
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            readCard.ResetDeviceAndCard();
            string strBalanceInCard = null;
            readCard.GetBalance(ref strBalanceInCard);
            readCard.Close();
          //  string balanceInCard = (FounctionResources.GetStringFromByte(byteBalanceInCard)).ToString();
            int balanceInCard = Convert.ToInt32(strBalanceInCard, 16);
            textShow.Text = "卡内余额为:" + (balanceInCard/100m).ToString("0.00") ;
        }

        private void buttonGetTransProf_Click(object sender, EventArgs e)
        {
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            InitializeForPurchaseTimecosResponse initializeForPurchaseTimecosResponse = new InitializeForPurchaseTimecosResponse();
            //初始化设备
            readCard.ResetDeviceAndCard();
            //获取卡片的脱机交易序号
            readCard.InitializeForOfflineSNZero(ref initializeForPurchaseTimecosResponse);
            int balanceInCard = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForPurchaseTimecosResponse.oldBalance), 16);
            int overdraftlimit = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForPurchaseTimecosResponse.OverdraftLimit), 16);
            int offlineSN = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForPurchaseTimecosResponse.ICCardTransID), 16);
            string randomNo = FounctionResources.GetStringFromByte(initializeForPurchaseTimecosResponse.RandomNum);
            textBoxBalance.Text = (balanceInCard/100m).ToString("0.00");
            textBoxTouzhi.Text = (overdraftlimit / 100m).ToString("0.00");
            textBoxRandom.Text = randomNo;
            textOfflineSN.Text = (offlineSN).ToString();
            readCard.Close();
        }

        private void buttonGetOnlineSN_Click(object sender, EventArgs e)
        {
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            InitializeForLoadTimecosResponse initializeForLoadTimecosResponse = new InitializeForLoadTimecosResponse();
            readCard.ResetDeviceAndCard();
            readCard.InitializeForOnlineSZero(ref initializeForLoadTimecosResponse);
            readCard.Close();
            int balanceInCard = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForLoadTimecosResponse.oldBalance), 16);
            int offlineSN = Convert.ToInt32(FounctionResources.GetStringFromByte(initializeForLoadTimecosResponse.ICCardTransID),16);
            string randomNo = FounctionResources.GetStringFromByte(initializeForLoadTimecosResponse.RandomNum);
            string MAC1 = FounctionResources.GetStringFromByte(initializeForLoadTimecosResponse.MAC1);
            textBoxBalance.Text = (balanceInCard / 100m).ToString("0.00");
            textBoxRandom.Text = randomNo;
            textBoxOnlineSN.Text = offlineSN.ToString();
            textBoxMAC1.Text = MAC1;
        }

        private void buttonPurseTAC_Click(object sender, EventArgs e)
        {
            byte[]TAC = new byte[4];
            byte[] MAC2 = new byte[4];
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            readCard.ResetDeviceAndCard();
            readCard.GetPurchaseTransactionProof(ref TAC , ref  MAC2);
            textBoxPurseTAC.Text = FounctionResources.GetStringFromByte(TAC);
            textBoxPurseMAC2.Text = FounctionResources.GetStringFromByte(MAC2);

            readCard.Close();
        }

        private void buttonGetLoadTAC_Click(object sender, EventArgs e)
        {
            byte[] TAC = new byte[4];
            byte[] MAC2 = new byte[4];
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            readCard.ResetDeviceAndCard();
            readCard.GetLoadTransactionProof(ref TAC, ref  MAC2);
            textBoxLoadTAC.Text = FounctionResources.GetStringFromByte(TAC);
            textBoxLoadMAC.Text = FounctionResources.GetStringFromByte(MAC2);
            readCard.Close();
        }

        private void buttonRead0018_Click(object sender, EventArgs e)
        {
            OperateCard readCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            Form0018 form0018 = new Form0018();
            readCard.ResetDeviceAndCard();
            File0018Content file0018Content = new File0018Content();
            string strFile0018 = null;
            string operationKind = null;
            int i = 1;
            while (i<=50)
            {
            bool ret =  readCard.Read0018(i,ref file0018Content , ref strFile0018);
            if (!ret)
            {
                if (i == 1)
                {
                    MessageBox.Show("卡片无圈存、消费记录");
                }
                break;
            }

            switch (Convert.ToInt32(file0018Content.operationKind.ToString()))
            {
                case 2: operationKind = "充值"; break;
                case 6: operationKind = "传统消费"; break;
                case 9: operationKind = "复合消费"; break;
            }

            int balanceInCard = Convert.ToInt32(FounctionResources.GetStringFromByte(file0018Content.amountForTransaction),16);

            textShow.Text = strFile0018;
            ListViewItem item;
            string[] subitem = new string[7];
            subitem[0] = i.ToString();
            subitem[1] = operationKind.ToString();
            subitem[2] = (Convert.ToInt32(FounctionResources.GetStringFromByte(file0018Content.ICCardTransID), 16)).ToString();
            subitem[3] = FounctionResources.GetStringFromByte(file0018Content.posID);
            subitem[4] = FounctionResources.GetStringFromByte(file0018Content.transactionDate).Substring(0, 4)
                       + "年" + FounctionResources.GetStringFromByte(file0018Content.transactionDate).Substring(4, 2)
                       + "月" + FounctionResources.GetStringFromByte(file0018Content.transactionDate).Substring(6, 2)
                       + "日";
            subitem[5] = FounctionResources.GetStringFromByte(file0018Content.transactionTime).Substring(0, 2) + "时"
                       + FounctionResources.GetStringFromByte(file0018Content.transactionTime).Substring(2, 2) + "分"
                       + FounctionResources.GetStringFromByte(file0018Content.transactionTime).Substring(4, 2) + "秒";
            subitem[6] = (balanceInCard / 100m).ToString("0.00");
            item = new ListViewItem(subitem); 
            form0018.listView1.Items.Add(item);
            i++;
            form0018.Show();
            
            }
            readCard.Close();
            object[] args = new object[7];
            //object[0] = 
        }

        private void buttonRead0012_Click(object sender, EventArgs e)
        {
            OperateCard opearteCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            //初始化设备与卡片
            opearteCard.ResetDeviceAndCard();
            byte[] file0012 = new byte[36];
            if (!opearteCard.Read0012(ref file0012))
            {
                opearteCard.Close();
                return;
            }
            opearteCard.Close();
            textShow.Text = FounctionResources.GetStringFromByte(file0012);
        }

        private void buttonRead0019_Click(object sender, EventArgs e)
        {
            OperateCard opearteCard = new OperateCard(MytoolsIniConstant.DeviceCompany);
            File0019Content file0019content = new File0019Content();
            //初始化设备与卡片
            if (!opearteCard.ResetDeviceAndCard())
                return;
            byte[] file0019 = new byte[39];
            if (!opearteCard.Read0015(ref file0019))
            {
                opearteCard.Close();
                return;
            }
            opearteCard.Close();
        }


    }
}
