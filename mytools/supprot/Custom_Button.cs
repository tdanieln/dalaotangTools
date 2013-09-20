using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InitializationWriteCard
{
    public partial class Custom_Button : Button
    {
        public enum ButtonStatus : int
        {
            image_Up = 0, //抬起
            image_Down = 1,  //按下
            image_Focus = 2,  //焦点
        }

        public Custom_Button()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void Custom_Button_MouseDown(object sender, MouseEventArgs e)
        {
            ((Custom_Button)sender).ImageIndex = (int)ButtonStatus.image_Down;
        }

        private void Custom_Button_MouseUp(object sender, MouseEventArgs e)
        {
            ((Custom_Button)sender).ImageIndex = (int)ButtonStatus.image_Up;
        }

        private void Custom_Button_MouseEnter(object sender, EventArgs e)
        {
            ((Custom_Button)sender).ImageIndex = (int)ButtonStatus.image_Focus;
        }

        private void Custom_Button_MouseLeave(object sender, EventArgs e)
        {
            ((Custom_Button)sender).ImageIndex = (int)ButtonStatus.image_Up;
        }
    }
}
