namespace MDLoader
{
    partial class AICheckListForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pan_bottom = new System.Windows.Forms.Panel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pan_top = new System.Windows.Forms.Panel();
            this.chb_checkAll = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lab_info = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.checkitem1 = new MDLoader.ItemCheckPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pan_bottom.SuspendLayout();
            this.pan_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.checkitem1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 50);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1319, 891);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pan_bottom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 941);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1319, 68);
            this.panel1.TabIndex = 1;
            // 
            // pan_bottom
            // 
            this.pan_bottom.Controls.Add(this.panel6);
            this.pan_bottom.Controls.Add(this.btn_Cancel);
            this.pan_bottom.Controls.Add(this.panel3);
            this.pan_bottom.Controls.Add(this.btn_Apply);
            this.pan_bottom.Controls.Add(this.panel5);
            this.pan_bottom.Controls.Add(this.panel4);
            this.pan_bottom.Controls.Add(this.panel2);
            this.pan_bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_bottom.Location = new System.Drawing.Point(0, 0);
            this.pan_bottom.Name = "pan_bottom";
            this.pan_bottom.Size = new System.Drawing.Size(1319, 68);
            this.pan_bottom.TabIndex = 3;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Cancel.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_Cancel.Location = new System.Drawing.Point(970, 12);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(141, 37);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Apply.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_Apply.Location = new System.Drawing.Point(1123, 12);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(141, 37);
            this.btn_Apply.TabIndex = 2;
            this.btn_Apply.Text = "应用";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(1264, 12);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(55, 37);
            this.panel5.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1319, 12);
            this.panel4.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1319, 19);
            this.panel2.TabIndex = 1;
            // 
            // pan_top
            // 
            this.pan_top.Controls.Add(this.lab_info);
            this.pan_top.Controls.Add(this.chb_checkAll);
            this.pan_top.Controls.Add(this.panel7);
            this.pan_top.Controls.Add(this.panel8);
            this.pan_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_top.Location = new System.Drawing.Point(0, 0);
            this.pan_top.Name = "pan_top";
            this.pan_top.Size = new System.Drawing.Size(1319, 50);
            this.pan_top.TabIndex = 2;
            this.pan_top.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // chb_checkAll
            // 
            this.chb_checkAll.AutoSize = true;
            this.chb_checkAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chb_checkAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.chb_checkAll.Location = new System.Drawing.Point(1207, 11);
            this.chb_checkAll.Name = "chb_checkAll";
            this.chb_checkAll.Size = new System.Drawing.Size(70, 39);
            this.chb_checkAll.TabIndex = 0;
            this.chb_checkAll.Text = "全选";
            this.chb_checkAll.UseVisualStyleBackColor = true;
            this.chb_checkAll.CheckedChanged += new System.EventHandler(this.chb_checkAll_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(1277, 11);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(42, 39);
            this.panel7.TabIndex = 1;
            // 
            // lab_info
            // 
            this.lab_info.AutoSize = true;
            this.lab_info.Font = new System.Drawing.Font("宋体", 12F);
            this.lab_info.Location = new System.Drawing.Point(12, 14);
            this.lab_info.Name = "lab_info";
            this.lab_info.Size = new System.Drawing.Size(202, 24);
            this.lab_info.TabIndex = 2;
            this.lab_info.Text = "连接到AI Agent..";
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1319, 11);
            this.panel8.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1111, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(12, 37);
            this.panel3.TabIndex = 5;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(958, 12);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(12, 37);
            this.panel6.TabIndex = 7;
            // 
            // checkitem1
            // 
            this.checkitem1.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkitem1.Location = new System.Drawing.Point(13, 13);
            this.checkitem1.Name = "checkitem1";
            this.checkitem1.Size = new System.Drawing.Size(956, 41);
            this.checkitem1.TabIndex = 0;
            this.checkitem1.Visible = false;
            // 
            // AICheckListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1319, 1009);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pan_top);
            this.Name = "AICheckListForm";
            this.Text = "文档语法检查";
            this.Load += new System.EventHandler(this.AICheckList_Load);
            this.Shown += new System.EventHandler(this.AICheckListForm_Shown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pan_bottom.ResumeLayout(false);
            this.pan_top.ResumeLayout(false);
            this.pan_top.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ItemCheckPanel checkitem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pan_bottom;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pan_top;
        private System.Windows.Forms.CheckBox chb_checkAll;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lab_info;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
    }
}