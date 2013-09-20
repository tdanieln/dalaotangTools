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
    public partial class FormXML : Form
    {
        public FormXML()
        {
            InitializeComponent();
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\Users\\tn\\Desktop\\mytools";
            ofd.Filter = "(*.xml)|*.xml";
            ofd.ShowDialog();
            string fileName = ofd.FileName;
            listViewOfXmlFiles.Items.Add(fileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listViewOfXmlFiles.SelectedItems.Count != 1)
            {
                MessageBox.Show("请选择一条记录");
                return;
            }

        }
    }
}
