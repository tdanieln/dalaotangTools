namespace InitializationWriteCard
{
    partial class Custom_Button
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        protected System.Windows.Forms.ImageList imageList1;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Custom_Button));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "l_button2_up.png");
            this.imageList1.Images.SetKeyName(1, "l_button2_down.png");
            this.imageList1.Images.SetKeyName(2, "l_button2_visited.png");
            // 
            // Custom_Button
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(239)))));
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ForeColor = System.Drawing.Color.Black;
            this.ImageIndex = 0;
            this.ImageList = this.imageList1;
            this.Size = new System.Drawing.Size(92, 32);
            this.UseVisualStyleBackColor = false;
            this.MouseLeave += new System.EventHandler(this.Custom_Button_MouseLeave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Custom_Button_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Custom_Button_MouseUp);
            this.MouseEnter += new System.EventHandler(this.Custom_Button_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
