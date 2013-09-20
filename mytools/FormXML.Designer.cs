namespace mytools
{
    partial class FormXML
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listViewOfXmlFiles = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(397, 457);
            this.textBox1.TabIndex = 0;
            // 
            // listViewOfXmlFiles
            // 
            this.listViewOfXmlFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1});
            this.listViewOfXmlFiles.GridLines = true;
            this.listViewOfXmlFiles.Location = new System.Drawing.Point(415, 13);
            this.listViewOfXmlFiles.Name = "listViewOfXmlFiles";
            this.listViewOfXmlFiles.Size = new System.Drawing.Size(356, 97);
            this.listViewOfXmlFiles.TabIndex = 1;
            this.listViewOfXmlFiles.UseCompatibleStateImageBehavior = false;
            this.listViewOfXmlFiles.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "文件名";
            this.ColumnHeader1.Width = 279;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(427, 149);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(124, 53);
            this.buttonOpenFile.TabIndex = 2;
            this.buttonOpenFile.Text = "打开文件";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(603, 149);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 53);
            this.button2.TabIndex = 3;
            this.button2.Text = "文件分析";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 481);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.listViewOfXmlFiles);
            this.Controls.Add(this.textBox1);
            this.Name = "FormXML";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listViewOfXmlFiles;
        private System.Windows.Forms.ColumnHeader ColumnHeader1;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button button2;
    }
}