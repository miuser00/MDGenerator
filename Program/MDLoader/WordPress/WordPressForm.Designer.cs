namespace MDLoader
{
    partial class UpwpForm
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
            this.btn_uploadPics = new System.Windows.Forms.Button();
            this.btn_uploadMD = new System.Windows.Forms.Button();
            this.btn_revisePicURL = new System.Windows.Forms.Button();
            this.dgv_articles = new System.Windows.Forms.DataGridView();
            this.btn_GetArticleList = new System.Windows.Forms.Button();
            this.btn_uploadnew = new System.Windows.Forms.Button();
            this.cmb_category = new System.Windows.Forms.ComboBox();
            this.btn_ListArticles = new System.Windows.Forms.Button();
            this.dgv_AllTags = new System.Windows.Forms.DataGridView();
            this.pic_specialpicture = new System.Windows.Forms.PictureBox();
            this.txt_title = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_GetCategoryAndTags = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_AddMainPicture = new System.Windows.Forms.Button();
            this.btn_addNewTag = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_ArticleTags = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chb_viewSpecialPicture = new System.Windows.Forms.CheckBox();
            this.btn_addCategory = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_server = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_conn = new System.Windows.Forms.TextBox();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_categories = new System.Windows.Forms.DataGridView();
            this.btn_DeleteArticle = new System.Windows.Forms.Button();
            this.btn_AddTagFromExist = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btn_DeleteTag = new System.Windows.Forms.Button();
            this.btn_DeleteCategory = new System.Windows.Forms.Button();
            this.txt_SpecialPicUrl = new System.Windows.Forms.TextBox();
            this.btn_PreviewCurrent = new System.Windows.Forms.Button();
            this.tab_view = new System.Windows.Forms.TabControl();
            this.tab_articlelist = new System.Windows.Forms.TabPage();
            this.tab_source = new System.Windows.Forms.TabPage();
            this.rtb_source = new System.Windows.Forms.RichTextBox();
            this.tab_upload = new System.Windows.Forms.TabPage();
            this.dgv_upload = new System.Windows.Forms.DataGridView();
            this.btn_reviseTitle = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AllTags)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_specialpicture)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ArticleTags)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_categories)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.tab_view.SuspendLayout();
            this.tab_articlelist.SuspendLayout();
            this.tab_source.SuspendLayout();
            this.tab_upload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_upload)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_uploadPics
            // 
            this.btn_uploadPics.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_uploadPics.Location = new System.Drawing.Point(205, 371);
            this.btn_uploadPics.Name = "btn_uploadPics";
            this.btn_uploadPics.Size = new System.Drawing.Size(128, 42);
            this.btn_uploadPics.TabIndex = 4;
            this.btn_uploadPics.Text = "上传图片";
            this.btn_uploadPics.UseVisualStyleBackColor = true;
            this.btn_uploadPics.Visible = false;
            this.btn_uploadPics.Click += new System.EventHandler(this.btn_uploadPics_Click);
            // 
            // btn_uploadMD
            // 
            this.btn_uploadMD.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_uploadMD.Location = new System.Drawing.Point(738, 371);
            this.btn_uploadMD.Name = "btn_uploadMD";
            this.btn_uploadMD.Size = new System.Drawing.Size(128, 42);
            this.btn_uploadMD.TabIndex = 6;
            this.btn_uploadMD.Text = "上传文档";
            this.btn_uploadMD.UseVisualStyleBackColor = true;
            this.btn_uploadMD.Visible = false;
            this.btn_uploadMD.Click += new System.EventHandler(this.btn_uploadMD_Click);
            // 
            // btn_revisePicURL
            // 
            this.btn_revisePicURL.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_revisePicURL.Location = new System.Drawing.Point(349, 371);
            this.btn_revisePicURL.Name = "btn_revisePicURL";
            this.btn_revisePicURL.Size = new System.Drawing.Size(157, 42);
            this.btn_revisePicURL.TabIndex = 7;
            this.btn_revisePicURL.Text = "修改图片Url";
            this.btn_revisePicURL.UseVisualStyleBackColor = true;
            this.btn_revisePicURL.Visible = false;
            this.btn_revisePicURL.Click += new System.EventHandler(this.btn_revisePicURL_Click);
            // 
            // dgv_articles
            // 
            this.dgv_articles.AllowUserToAddRows = false;
            this.dgv_articles.AllowUserToDeleteRows = false;
            this.dgv_articles.AllowUserToResizeRows = false;
            this.dgv_articles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articles.Location = new System.Drawing.Point(-3, -5);
            this.dgv_articles.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_articles.MultiSelect = false;
            this.dgv_articles.Name = "dgv_articles";
            this.dgv_articles.ReadOnly = true;
            this.dgv_articles.RowHeadersVisible = false;
            this.dgv_articles.RowHeadersWidth = 62;
            this.dgv_articles.RowTemplate.Height = 23;
            this.dgv_articles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_articles.Size = new System.Drawing.Size(1285, 511);
            this.dgv_articles.TabIndex = 9;
            this.dgv_articles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_articles_CellContentClick);
            this.dgv_articles.SelectionChanged += new System.EventHandler(this.dgv_articles_SelectionChanged);
            // 
            // btn_GetArticleList
            // 
            this.btn_GetArticleList.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_GetArticleList.Location = new System.Drawing.Point(53, 371);
            this.btn_GetArticleList.Name = "btn_GetArticleList";
            this.btn_GetArticleList.Size = new System.Drawing.Size(135, 42);
            this.btn_GetArticleList.TabIndex = 10;
            this.btn_GetArticleList.Text = "读类别";
            this.btn_GetArticleList.UseVisualStyleBackColor = true;
            this.btn_GetArticleList.Visible = false;
            this.btn_GetArticleList.Click += new System.EventHandler(this.btn_GetCategoryList_Click);
            // 
            // btn_uploadnew
            // 
            this.btn_uploadnew.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_uploadnew.Location = new System.Drawing.Point(1367, 851);
            this.btn_uploadnew.Name = "btn_uploadnew";
            this.btn_uploadnew.Size = new System.Drawing.Size(341, 57);
            this.btn_uploadnew.TabIndex = 11;
            this.btn_uploadnew.Text = "新建上传";
            this.btn_uploadnew.UseVisualStyleBackColor = true;
            this.btn_uploadnew.Click += new System.EventHandler(this.btn_uploadnew_Click);
            // 
            // cmb_category
            // 
            this.cmb_category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_category.Font = new System.Drawing.Font("宋体", 12F);
            this.cmb_category.FormattingEnabled = true;
            this.cmb_category.Location = new System.Drawing.Point(111, 685);
            this.cmb_category.Name = "cmb_category";
            this.cmb_category.Size = new System.Drawing.Size(242, 32);
            this.cmb_category.TabIndex = 12;
            this.cmb_category.SelectedIndexChanged += new System.EventHandler(this.cmb_category_SelectedIndexChanged);
            // 
            // btn_ListArticles
            // 
            this.btn_ListArticles.Font = new System.Drawing.Font("宋体", 10F);
            this.btn_ListArticles.Location = new System.Drawing.Point(1177, 16);
            this.btn_ListArticles.Name = "btn_ListArticles";
            this.btn_ListArticles.Size = new System.Drawing.Size(128, 35);
            this.btn_ListArticles.TabIndex = 13;
            this.btn_ListArticles.Text = "读文章列表";
            this.btn_ListArticles.UseVisualStyleBackColor = true;
            this.btn_ListArticles.Click += new System.EventHandler(this.btn_ListArticles_Click);
            // 
            // dgv_AllTags
            // 
            this.dgv_AllTags.AllowUserToAddRows = false;
            this.dgv_AllTags.AllowUserToDeleteRows = false;
            this.dgv_AllTags.AllowUserToResizeRows = false;
            this.dgv_AllTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AllTags.ColumnHeadersVisible = false;
            this.dgv_AllTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_AllTags.Location = new System.Drawing.Point(3, 31);
            this.dgv_AllTags.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_AllTags.Name = "dgv_AllTags";
            this.dgv_AllTags.RowHeadersVisible = false;
            this.dgv_AllTags.RowHeadersWidth = 62;
            this.dgv_AllTags.RowTemplate.Height = 23;
            this.dgv_AllTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_AllTags.Size = new System.Drawing.Size(423, 263);
            this.dgv_AllTags.TabIndex = 16;
            // 
            // pic_specialpicture
            // 
            this.pic_specialpicture.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pic_specialpicture.Location = new System.Drawing.Point(24, 34);
            this.pic_specialpicture.Name = "pic_specialpicture";
            this.pic_specialpicture.Size = new System.Drawing.Size(319, 260);
            this.pic_specialpicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_specialpicture.TabIndex = 17;
            this.pic_specialpicture.TabStop = false;
            // 
            // txt_title
            // 
            this.txt_title.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_title.Location = new System.Drawing.Point(109, 632);
            this.txt_title.Name = "txt_title";
            this.txt_title.ReadOnly = true;
            this.txt_title.Size = new System.Drawing.Size(244, 35);
            this.txt_title.TabIndex = 19;
            this.txt_title.Text = "-";
            this.txt_title.Leave += new System.EventHandler(this.txt_title_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 635);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 24);
            this.label3.TabIndex = 18;
            this.label3.Text = "标题";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(42, 688);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 24);
            this.label4.TabIndex = 20;
            this.label4.Text = "类别";
            // 
            // btn_GetCategoryAndTags
            // 
            this.btn_GetCategoryAndTags.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_GetCategoryAndTags.Location = new System.Drawing.Point(523, 371);
            this.btn_GetCategoryAndTags.Name = "btn_GetCategoryAndTags";
            this.btn_GetCategoryAndTags.Size = new System.Drawing.Size(200, 42);
            this.btn_GetCategoryAndTags.TabIndex = 11;
            this.btn_GetCategoryAndTags.Text = "读类别与标签序列号";
            this.btn_GetCategoryAndTags.UseVisualStyleBackColor = true;
            this.btn_GetCategoryAndTags.Visible = false;
            this.btn_GetCategoryAndTags.Click += new System.EventHandler(this.btn_GetCategoryAndTags_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_AllTags);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1328, 291);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 297);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标签列表";
            // 
            // btn_AddMainPicture
            // 
            this.btn_AddMainPicture.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_AddMainPicture.Location = new System.Drawing.Point(369, 736);
            this.btn_AddMainPicture.Name = "btn_AddMainPicture";
            this.btn_AddMainPicture.Size = new System.Drawing.Size(107, 39);
            this.btn_AddMainPicture.TabIndex = 28;
            this.btn_AddMainPicture.Text = "自定义";
            this.btn_AddMainPicture.UseVisualStyleBackColor = true;
            this.btn_AddMainPicture.Click += new System.EventHandler(this.btn_AddMainPicture_Click);
            // 
            // btn_addNewTag
            // 
            this.btn_addNewTag.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_addNewTag.Location = new System.Drawing.Point(257, 31);
            this.btn_addNewTag.Name = "btn_addNewTag";
            this.btn_addNewTag.Size = new System.Drawing.Size(139, 39);
            this.btn_addNewTag.TabIndex = 30;
            this.btn_addNewTag.Text = "新建Tag";
            this.btn_addNewTag.UseVisualStyleBackColor = true;
            this.btn_addNewTag.Click += new System.EventHandler(this.btn_addNewTag_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_ArticleTags);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(912, 614);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(364, 314);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "文章标签";
            // 
            // dgv_ArticleTags
            // 
            this.dgv_ArticleTags.AllowUserToAddRows = false;
            this.dgv_ArticleTags.AllowUserToDeleteRows = false;
            this.dgv_ArticleTags.AllowUserToOrderColumns = true;
            this.dgv_ArticleTags.AllowUserToResizeColumns = false;
            this.dgv_ArticleTags.AllowUserToResizeRows = false;
            this.dgv_ArticleTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ArticleTags.ColumnHeadersVisible = false;
            this.dgv_ArticleTags.Location = new System.Drawing.Point(24, 35);
            this.dgv_ArticleTags.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_ArticleTags.Name = "dgv_ArticleTags";
            this.dgv_ArticleTags.ReadOnly = true;
            this.dgv_ArticleTags.RowHeadersVisible = false;
            this.dgv_ArticleTags.RowHeadersWidth = 62;
            this.dgv_ArticleTags.RowTemplate.Height = 23;
            this.dgv_ArticleTags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ArticleTags.Size = new System.Drawing.Size(318, 259);
            this.dgv_ArticleTags.TabIndex = 16;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chb_viewSpecialPicture);
            this.groupBox4.Controls.Add(this.pic_specialpicture);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(514, 616);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(364, 314);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "首页图片        ";
            // 
            // chb_viewSpecialPicture
            // 
            this.chb_viewSpecialPicture.AutoSize = true;
            this.chb_viewSpecialPicture.Checked = true;
            this.chb_viewSpecialPicture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_viewSpecialPicture.Font = new System.Drawing.Font("宋体", 12F);
            this.chb_viewSpecialPicture.Location = new System.Drawing.Point(123, -1);
            this.chb_viewSpecialPicture.Name = "chb_viewSpecialPicture";
            this.chb_viewSpecialPicture.Size = new System.Drawing.Size(84, 28);
            this.chb_viewSpecialPicture.TabIndex = 61;
            this.chb_viewSpecialPicture.Text = "预览";
            this.chb_viewSpecialPicture.UseVisualStyleBackColor = true;
            // 
            // btn_addCategory
            // 
            this.btn_addCategory.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_addCategory.Location = new System.Drawing.Point(369, 683);
            this.btn_addCategory.Name = "btn_addCategory";
            this.btn_addCategory.Size = new System.Drawing.Size(107, 40);
            this.btn_addCategory.TabIndex = 35;
            this.btn_addCategory.Text = "新增";
            this.btn_addCategory.UseVisualStyleBackColor = true;
            this.btn_addCategory.Click += new System.EventHandler(this.btn_addnewclass_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(42, 741);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 37;
            this.label5.Text = "首图";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.DimGray;
            this.groupBox5.Location = new System.Drawing.Point(28, 609);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1729, 1);
            this.groupBox5.TabIndex = 39;
            this.groupBox5.TabStop = false;
            // 
            // txt_server
            // 
            this.txt_server.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_server.Location = new System.Drawing.Point(102, 21);
            this.txt_server.Name = "txt_server";
            this.txt_server.Size = new System.Drawing.Size(353, 30);
            this.txt_server.TabIndex = 41;
            this.txt_server.Text = "-";
            this.txt_server.TextChanged += new System.EventHandler(this.txt_server_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F);
            this.label7.Location = new System.Drawing.Point(859, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "连接";
            // 
            // txt_conn
            // 
            this.txt_conn.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_conn.ForeColor = System.Drawing.Color.White;
            this.txt_conn.Location = new System.Drawing.Point(914, 21);
            this.txt_conn.Name = "txt_conn";
            this.txt_conn.ReadOnly = true;
            this.txt_conn.Size = new System.Drawing.Size(85, 30);
            this.txt_conn.TabIndex = 43;
            this.txt_conn.Text = "-";
            // 
            // txt_user
            // 
            this.txt_user.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_user.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_user.Location = new System.Drawing.Point(555, 21);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(85, 30);
            this.txt_user.TabIndex = 46;
            this.txt_user.Text = "-";
            this.txt_user.TextChanged += new System.EventHandler(this.txt_user_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(469, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 45;
            this.label1.Text = "用户名";
            // 
            // txt_pass
            // 
            this.txt_pass.Font = new System.Drawing.Font("宋体", 10F);
            this.txt_pass.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_pass.Location = new System.Drawing.Point(713, 21);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.Size = new System.Drawing.Size(136, 30);
            this.txt_pass.TabIndex = 48;
            this.txt_pass.Text = "-";
            this.txt_pass.TextChanged += new System.EventHandler(this.txt_pass_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.Location = new System.Drawing.Point(654, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 47;
            this.label6.Text = "密码";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.Location = new System.Drawing.Point(33, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 20);
            this.label8.TabIndex = 46;
            this.label8.Text = "域名";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgv_categories);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(1331, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(429, 273);
            this.groupBox6.TabIndex = 49;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "类别列表";
            // 
            // dgv_categories
            // 
            this.dgv_categories.AllowUserToAddRows = false;
            this.dgv_categories.AllowUserToDeleteRows = false;
            this.dgv_categories.AllowUserToResizeColumns = false;
            this.dgv_categories.AllowUserToResizeRows = false;
            this.dgv_categories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_categories.ColumnHeadersVisible = false;
            this.dgv_categories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_categories.Location = new System.Drawing.Point(3, 31);
            this.dgv_categories.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_categories.Name = "dgv_categories";
            this.dgv_categories.ReadOnly = true;
            this.dgv_categories.RowHeadersVisible = false;
            this.dgv_categories.RowHeadersWidth = 62;
            this.dgv_categories.RowTemplate.Height = 23;
            this.dgv_categories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_categories.Size = new System.Drawing.Size(423, 239);
            this.dgv_categories.TabIndex = 16;
            // 
            // btn_DeleteArticle
            // 
            this.btn_DeleteArticle.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_DeleteArticle.ForeColor = System.Drawing.Color.Brown;
            this.btn_DeleteArticle.Location = new System.Drawing.Point(1367, 729);
            this.btn_DeleteArticle.Name = "btn_DeleteArticle";
            this.btn_DeleteArticle.Size = new System.Drawing.Size(341, 37);
            this.btn_DeleteArticle.TabIndex = 52;
            this.btn_DeleteArticle.Text = "删除文章";
            this.btn_DeleteArticle.UseVisualStyleBackColor = true;
            this.btn_DeleteArticle.Visible = false;
            this.btn_DeleteArticle.Click += new System.EventHandler(this.btn_DeleteArticle_Click);
            // 
            // btn_AddTagFromExist
            // 
            this.btn_AddTagFromExist.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_AddTagFromExist.Location = new System.Drawing.Point(36, 31);
            this.btn_AddTagFromExist.Name = "btn_AddTagFromExist";
            this.btn_AddTagFromExist.Size = new System.Drawing.Size(200, 39);
            this.btn_AddTagFromExist.TabIndex = 55;
            this.btn_AddTagFromExist.Text = "添加列表中的Tag";
            this.btn_AddTagFromExist.UseVisualStyleBackColor = true;
            this.btn_AddTagFromExist.Click += new System.EventHandler(this.btn_AddTagFromExist_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_addNewTag);
            this.groupBox7.Controls.Add(this.btn_AddTagFromExist);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox7.Location = new System.Drawing.Point(49, 785);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(427, 88);
            this.groupBox7.TabIndex = 56;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "标签";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("宋体", 12F);
            this.linkLabel1.Location = new System.Drawing.Point(75, 886);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 24);
            this.linkLabel1.TabIndex = 58;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btn_DeleteTag
            // 
            this.btn_DeleteTag.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_DeleteTag.ForeColor = System.Drawing.Color.Brown;
            this.btn_DeleteTag.Location = new System.Drawing.Point(1367, 688);
            this.btn_DeleteTag.Name = "btn_DeleteTag";
            this.btn_DeleteTag.Size = new System.Drawing.Size(341, 37);
            this.btn_DeleteTag.TabIndex = 59;
            this.btn_DeleteTag.Text = "删除标签";
            this.btn_DeleteTag.UseVisualStyleBackColor = true;
            this.btn_DeleteTag.Visible = false;
            // 
            // btn_DeleteCategory
            // 
            this.btn_DeleteCategory.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_DeleteCategory.ForeColor = System.Drawing.Color.Brown;
            this.btn_DeleteCategory.Location = new System.Drawing.Point(1367, 649);
            this.btn_DeleteCategory.Name = "btn_DeleteCategory";
            this.btn_DeleteCategory.Size = new System.Drawing.Size(341, 37);
            this.btn_DeleteCategory.TabIndex = 60;
            this.btn_DeleteCategory.Text = "删除类别";
            this.btn_DeleteCategory.UseVisualStyleBackColor = true;
            this.btn_DeleteCategory.Visible = false;
            // 
            // txt_SpecialPicUrl
            // 
            this.txt_SpecialPicUrl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SpecialPicUrl.Location = new System.Drawing.Point(109, 736);
            this.txt_SpecialPicUrl.Name = "txt_SpecialPicUrl";
            this.txt_SpecialPicUrl.ReadOnly = true;
            this.txt_SpecialPicUrl.Size = new System.Drawing.Size(244, 35);
            this.txt_SpecialPicUrl.TabIndex = 61;
            this.txt_SpecialPicUrl.Text = "-";
            // 
            // btn_PreviewCurrent
            // 
            this.btn_PreviewCurrent.Font = new System.Drawing.Font("宋体", 10F);
            this.btn_PreviewCurrent.Location = new System.Drawing.Point(1025, 16);
            this.btn_PreviewCurrent.Name = "btn_PreviewCurrent";
            this.btn_PreviewCurrent.Size = new System.Drawing.Size(141, 35);
            this.btn_PreviewCurrent.TabIndex = 62;
            this.btn_PreviewCurrent.Text = "读当前文章";
            this.btn_PreviewCurrent.UseVisualStyleBackColor = true;
            this.btn_PreviewCurrent.Visible = false;
            this.btn_PreviewCurrent.Click += new System.EventHandler(this.btn_PreviewCurrent_Click);
            // 
            // tab_view
            // 
            this.tab_view.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tab_view.Controls.Add(this.tab_articlelist);
            this.tab_view.Controls.Add(this.tab_source);
            this.tab_view.Controls.Add(this.tab_upload);
            this.tab_view.Font = new System.Drawing.Font("宋体", 10F);
            this.tab_view.Location = new System.Drawing.Point(19, 57);
            this.tab_view.Name = "tab_view";
            this.tab_view.SelectedIndex = 0;
            this.tab_view.Size = new System.Drawing.Size(1294, 544);
            this.tab_view.TabIndex = 63;
            this.tab_view.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tab_view_Selecting);
            // 
            // tab_articlelist
            // 
            this.tab_articlelist.Controls.Add(this.dgv_articles);
            this.tab_articlelist.Location = new System.Drawing.Point(4, 4);
            this.tab_articlelist.Name = "tab_articlelist";
            this.tab_articlelist.Padding = new System.Windows.Forms.Padding(3);
            this.tab_articlelist.Size = new System.Drawing.Size(1286, 510);
            this.tab_articlelist.TabIndex = 1;
            this.tab_articlelist.Text = "服务端文章列表";
            this.tab_articlelist.UseVisualStyleBackColor = true;
            // 
            // tab_source
            // 
            this.tab_source.Controls.Add(this.btn_uploadMD);
            this.tab_source.Controls.Add(this.btn_GetCategoryAndTags);
            this.tab_source.Controls.Add(this.btn_uploadPics);
            this.tab_source.Controls.Add(this.btn_GetArticleList);
            this.tab_source.Controls.Add(this.btn_revisePicURL);
            this.tab_source.Controls.Add(this.rtb_source);
            this.tab_source.Location = new System.Drawing.Point(4, 4);
            this.tab_source.Name = "tab_source";
            this.tab_source.Padding = new System.Windows.Forms.Padding(3);
            this.tab_source.Size = new System.Drawing.Size(1286, 510);
            this.tab_source.TabIndex = 0;
            this.tab_source.Text = "待上传源码";
            this.tab_source.UseVisualStyleBackColor = true;
            // 
            // rtb_source
            // 
            this.rtb_source.BackColor = System.Drawing.Color.White;
            this.rtb_source.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_source.Location = new System.Drawing.Point(3, 3);
            this.rtb_source.Name = "rtb_source";
            this.rtb_source.ReadOnly = true;
            this.rtb_source.Size = new System.Drawing.Size(1280, 504);
            this.rtb_source.TabIndex = 12;
            this.rtb_source.Text = "";
            // 
            // tab_upload
            // 
            this.tab_upload.Controls.Add(this.dgv_upload);
            this.tab_upload.Location = new System.Drawing.Point(4, 4);
            this.tab_upload.Name = "tab_upload";
            this.tab_upload.Size = new System.Drawing.Size(1286, 510);
            this.tab_upload.TabIndex = 2;
            this.tab_upload.Text = "上传操作";
            this.tab_upload.UseVisualStyleBackColor = true;
            // 
            // dgv_upload
            // 
            this.dgv_upload.AllowUserToAddRows = false;
            this.dgv_upload.AllowUserToDeleteRows = false;
            this.dgv_upload.AllowUserToResizeRows = false;
            this.dgv_upload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_upload.Location = new System.Drawing.Point(-4, -3);
            this.dgv_upload.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_upload.Name = "dgv_upload";
            this.dgv_upload.ReadOnly = true;
            this.dgv_upload.RowHeadersVisible = false;
            this.dgv_upload.RowHeadersWidth = 62;
            this.dgv_upload.RowTemplate.Height = 23;
            this.dgv_upload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_upload.Size = new System.Drawing.Size(1286, 509);
            this.dgv_upload.TabIndex = 10;
            // 
            // btn_reviseTitle
            // 
            this.btn_reviseTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_reviseTitle.Location = new System.Drawing.Point(369, 632);
            this.btn_reviseTitle.Name = "btn_reviseTitle";
            this.btn_reviseTitle.Size = new System.Drawing.Size(107, 40);
            this.btn_reviseTitle.TabIndex = 64;
            this.btn_reviseTitle.Text = "修改";
            this.btn_reviseTitle.UseVisualStyleBackColor = true;
            this.btn_reviseTitle.Click += new System.EventHandler(this.btn_reviseTitle_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(1832, 43);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(251, 364);
            this.listBox1.TabIndex = 65;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1833, 513);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 28);
            this.textBox1.TabIndex = 66;
            // 
            // UpwpForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1778, 938);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btn_reviseTitle);
            this.Controls.Add(this.btn_PreviewCurrent);
            this.Controls.Add(this.txt_SpecialPicUrl);
            this.Controls.Add(this.btn_DeleteCategory);
            this.Controls.Add(this.btn_DeleteTag);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btn_DeleteArticle);
            this.Controls.Add(this.btn_uploadnew);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.cmb_category);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_pass);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_user);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txt_title);
            this.Controls.Add(this.txt_conn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_server);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_ListArticles);
            this.Controls.Add(this.btn_addCategory);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_AddMainPicture);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tab_view);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpwpForm";
            this.ShowInTaskbar = false;
            this.Text = "上传至您的Wordpress服务器";
            this.Load += new System.EventHandler(this.UpwpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AllTags)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_specialpicture)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ArticleTags)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_categories)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.tab_view.ResumeLayout(false);
            this.tab_articlelist.ResumeLayout(false);
            this.tab_source.ResumeLayout(false);
            this.tab_upload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_upload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_uploadPics;
        private System.Windows.Forms.Button btn_uploadMD;
        private System.Windows.Forms.Button btn_revisePicURL;
        private System.Windows.Forms.DataGridView dgv_articles;
        private System.Windows.Forms.Button btn_GetArticleList;
        private System.Windows.Forms.Button btn_uploadnew;
        public System.Windows.Forms.ComboBox cmb_category;
        private System.Windows.Forms.Button btn_ListArticles;
        private System.Windows.Forms.DataGridView dgv_AllTags;
        private System.Windows.Forms.PictureBox pic_specialpicture;
        private System.Windows.Forms.TextBox txt_title;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_AddMainPicture;
        private System.Windows.Forms.Button btn_addNewTag;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_ArticleTags;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_addCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txt_server;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_conn;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgv_categories;
        private System.Windows.Forms.Button btn_DeleteArticle;
        private System.Windows.Forms.Button btn_AddTagFromExist;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btn_DeleteTag;
        private System.Windows.Forms.Button btn_DeleteCategory;
        private System.Windows.Forms.CheckBox chb_viewSpecialPicture;
        private System.Windows.Forms.TextBox txt_SpecialPicUrl;
        private System.Windows.Forms.Button btn_PreviewCurrent;
        private System.Windows.Forms.Button btn_GetCategoryAndTags;
        private System.Windows.Forms.TabControl tab_view;
        private System.Windows.Forms.TabPage tab_source;
        private System.Windows.Forms.TabPage tab_articlelist;
        private System.Windows.Forms.RichTextBox rtb_source;
        private System.Windows.Forms.TabPage tab_upload;
        private System.Windows.Forms.DataGridView dgv_upload;
        private System.Windows.Forms.Button btn_reviseTitle;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}