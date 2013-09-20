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
    public partial class Custom_Form : Form
    {
        private Point mouseOffset;
        private bool isMouseDown = false;


        public Custom_Form()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void Custom_Form_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                if (e.Y > 36 / 608f * Size.Height)
                {

                }
                else
                {
                    xOffset = -e.X;// - SystemInformation.FrameBorderSize.Width;
                    yOffset = -e.Y;// - SystemInformation.CaptionHeight;// -SystemInformation.FrameBorderSize.Height;
                    mouseOffset = new Point(xOffset, yOffset);
                    isMouseDown = true;
                }
                
            }
        }

        private void Custom_Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }

        }

        private void Custom_Form_MouseUp(object sender, MouseEventArgs e)
        {
            // Changes the isMouseDown field so that the form does
            // not move unless the user is pressing the left mouse button.
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }

        }


    }
}
