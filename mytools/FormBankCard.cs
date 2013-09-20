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
    public partial class FormBankCard : Custom_Form
    {
        

        public FormBankCard()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!FounctionResources.CheckIsNum(((TextBox)sender).Text.Trim()))
            {
                MessageBox.Show("请正确输入卡片数量", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxQuantityOfSignedCard.Clear();
            }
            return;            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!FounctionResources.CheckIsNum(((TextBox)sender).Text.Trim()))
            {
                MessageBox.Show("请正确输入卡号", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Clear();
            }
            return; 
        }

        private void button_Sign_Click(object sender, EventArgs e)
        {
            string cardNo = textBox2.Text.Trim() + textBox1.Text.Trim();
            BackStage backGround = new BackStage();
            bool ret = backGround.SignBankCard(cardNo);
            if (ret)
            {
                MessageBox.Show("成功！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; 
            }
            return;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReadOldCard_Click(object sender, EventArgs e)
        {
            OperateCard operateCard = new OperateCard();
            File0015Content file0015Content = new File0015Content();

            operateCard.ResetDeviceAndCard();
            bool ret = operateCard.Read0015(ref file0015Content);
            if (!ret)
            {
                MessageBox.Show("读卡内信息失败");
                operateCard.Close();
                return;
            }
            textBoxOldCardNo.Text = FounctionResources.GetStringFromByte(file0015Content.CardSerialNo);
            operateCard.Close();
            
        }

        private void buttonReadNewCard_Click(object sender, EventArgs e)
        {
            OperateCard operateCard = new OperateCard();
            File0015Content file0015Content = new File0015Content();

            operateCard.ResetDeviceAndCard();
            bool ret = operateCard.Read0015(ref file0015Content);
            if (!ret)
            {
                MessageBox.Show("读卡内信息失败");
                operateCard.Close();
                return;
            }
            textBoxNewCardNo.Text = FounctionResources.GetStringFromByte(file0015Content.CardSerialNo);
            operateCard.Close();
            if (textBoxOldCardNo.Text == textBoxNewCardNo.Text)
            {
                MessageBox.Show("新旧卡号不允许相同");
                textBoxNewCardNo.Text = "";
            }


        }

        private void buttonReplaceCard_Click(object sender, EventArgs e)
        {
            BackStage backGround = new BackStage();
            try
            {
                backGround.ReplaceBankCard(textBoxOldCardNo.Text, textBoxNewCardNo.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show("更换失败");
            }

        }


    }
}
