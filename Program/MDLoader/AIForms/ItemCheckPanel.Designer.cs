namespace MDLoader
{
    partial class ItemCheckPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pan_checkitem = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rtb_content = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chb_check = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pan_checkitem.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_checkitem
            // 
            this.pan_checkitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pan_checkitem.Controls.Add(this.panel3);
            this.pan_checkitem.Controls.Add(this.panel2);
            this.pan_checkitem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_checkitem.Location = new System.Drawing.Point(0, 0);
            this.pan_checkitem.Name = "pan_checkitem";
            this.pan_checkitem.Size = new System.Drawing.Size(1179, 50);
            this.pan_checkitem.TabIndex = 1;
            this.pan_checkitem.Paint += new System.Windows.Forms.PaintEventHandler(this.pan_checkitem_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rtb_content);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1167, 48);
            this.panel3.TabIndex = 1;
            // 
            // rtb_content
            // 
            this.rtb_content.BackColor = System.Drawing.SystemColors.Control;
            this.rtb_content.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_content.Font = new System.Drawing.Font("楷体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtb_content.Location = new System.Drawing.Point(0, 0);
            this.rtb_content.Name = "rtb_content";
            this.rtb_content.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtb_content.Size = new System.Drawing.Size(1092, 48);
            this.rtb_content.TabIndex = 1;
            this.rtb_content.Text = "";
            this.rtb_content.TextChanged += new System.EventHandler(this.rtb_content_TextChanged_1);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chb_check);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(1092, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(75, 48);
            this.panel4.TabIndex = 0;
            // 
            // chb_check
            // 
            this.chb_check.AutoSize = true;
            this.chb_check.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_check.Dock = System.Windows.Forms.DockStyle.Right;
            this.chb_check.Location = new System.Drawing.Point(5, 0);
            this.chb_check.Name = "chb_check";
            this.chb_check.Size = new System.Drawing.Size(70, 48);
            this.chb_check.TabIndex = 0;
            this.chb_check.Text = "选中";
            this.chb_check.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1167, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 48);
            this.panel2.TabIndex = 0;
            // 
            // ItemCheckPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pan_checkitem);
            this.Name = "ItemCheckPanel";
            this.Size = new System.Drawing.Size(1179, 50);
            this.SizeChanged += new System.EventHandler(this.ItemCheckPanel_SizeChanged);
            this.pan_checkitem.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_checkitem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.CheckBox chb_check;
        private System.Windows.Forms.RichTextBox rtb_content;
    }
}
