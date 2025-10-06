namespace MDLoader
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadpannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上传图片到服务器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换图片存储位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel_upload = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.panel_upload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.viewVToolStripMenuItem,
            this.configToolStripMenuItem,
            this.remoteToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1622, 32);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(84, 28);
            this.newToolStripMenuItem.Text = "文件(&F)";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
            this.另存为ToolStripMenuItem.Text = "另存为...";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click_1);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewVToolStripMenuItem
            // 
            this.viewVToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem,
            this.uploadpannelToolStripMenuItem});
            this.viewVToolStripMenuItem.Name = "viewVToolStripMenuItem";
            this.viewVToolStripMenuItem.Size = new System.Drawing.Size(86, 28);
            this.viewVToolStripMenuItem.Text = "视图(&V)";
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // uploadpannelToolStripMenuItem
            // 
            this.uploadpannelToolStripMenuItem.Name = "uploadpannelToolStripMenuItem";
            this.uploadpannelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.uploadpannelToolStripMenuItem.Size = new System.Drawing.Size(257, 34);
            this.uploadpannelToolStripMenuItem.Text = "上传面板";
            this.uploadpannelToolStripMenuItem.Click += new System.EventHandler(this.uploadpannelToolStripMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.configToolStripMenuItem.Size = new System.Drawing.Size(84, 28);
            this.configToolStripMenuItem.Text = "设置(&S)";
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click_1);
            // 
            // remoteToolStripMenuItem
            // 
            this.remoteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.上传图片到服务器ToolStripMenuItem,
            this.切换图片存储位置ToolStripMenuItem});
            this.remoteToolStripMenuItem.Name = "remoteToolStripMenuItem";
            this.remoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.remoteToolStripMenuItem.Size = new System.Drawing.Size(86, 28);
            this.remoteToolStripMenuItem.Text = "远程(&R)";
            this.remoteToolStripMenuItem.Click += new System.EventHandler(this.remoteToolStripMenuItem_Click);
            // 
            // 上传图片到服务器ToolStripMenuItem
            // 
            this.上传图片到服务器ToolStripMenuItem.Name = "上传图片到服务器ToolStripMenuItem";
            this.上传图片到服务器ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.上传图片到服务器ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.上传图片到服务器ToolStripMenuItem.Text = "上传图片到服务器";
            this.上传图片到服务器ToolStripMenuItem.Click += new System.EventHandler(this.uploadToolStripMenuItem_Click);
            // 
            // 切换图片存储位置ToolStripMenuItem
            // 
            this.切换图片存储位置ToolStripMenuItem.Name = "切换图片存储位置ToolStripMenuItem";
            this.切换图片存储位置ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.切换图片存储位置ToolStripMenuItem.Size = new System.Drawing.Size(303, 34);
            this.切换图片存储位置ToolStripMenuItem.Text = "切换图片为网络存储";
            this.切换图片存储位置ToolStripMenuItem.Click += new System.EventHandler(this.remoteViewToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(88, 28);
            this.helpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(146, 34);
            this.关于ToolStripMenuItem.Text = "关于";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 843);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1622, 15);
            this.splitter1.TabIndex = 23;
            this.splitter1.TabStop = false;
            // 
            // panel_upload
            // 
            this.panel_upload.Controls.Add(this.dataGridView1);
            this.panel_upload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_upload.Location = new System.Drawing.Point(0, 858);
            this.panel_upload.Margin = new System.Windows.Forms.Padding(4);
            this.panel_upload.Name = "panel_upload";
            this.panel_upload.Size = new System.Drawing.Size(1622, 192);
            this.panel_upload.TabIndex = 24;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1622, 192);
            this.dataGridView1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Markdown 文件|*.md";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "md";
            this.saveFileDialog1.Filter = "Markdown 文件|*.md";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.webBrowser1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1622, 811);
            this.panel2.TabIndex = 25;
            this.panel2.SizeChanged += new System.EventHandler(this.panel2_SizeChanged);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(4);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(30, 30);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1645, 807);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            this.webBrowser1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.webBrowser1_PreviewKeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1622, 1050);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel_upload);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "MD FileLoader V0.6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_upload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel_upload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上传图片到服务器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换图片存储位置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadpannelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

