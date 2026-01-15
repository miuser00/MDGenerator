using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace MDLoader
{
    public partial class UpwpForm : Form
    {   
        // 屏幕 (3840x2160) 下设计的窗体
        private readonly Size DesignResolution = new Size(1920,1080);
        WpAdapter wp = new WpAdapter();
        MdAdapter adapter;
        public UpwpForm()
        {
            InitializeComponent();
        }
        public UpwpForm(MdAdapter adp)
        {
            adapter = adp;
            InitializeComponent();
        }
        private void UpwpForm_Load(object sender, EventArgs e)
        {
            txt_server.Text = SetupForm.cfg.ServerAddress;
            txt_user.Text = SetupForm.cfg.UserName;
            txt_pass.Text = SetupForm.cfg.AppPassword;
            btn_PreviewCurrent_Click(sender, e);
            ResetFontToDesignSize(this);
        }

        /// <summary>
        /// 恢复窗体控件字体到原始设计大小
        /// </summary>
        /// <param name="form">目标窗体</param>
        public static void ResetFontToDesignSize(Form form)
        {
            // 获取当前窗口 DPI
            float dpiX;
            using (Graphics g = form.CreateGraphics())
            {
                dpiX = g.DpiX;
            }

            // 控件字体缩放比例，不让字体过大或者过小
            float scale = dpiX / 96f;
            if (scale >3)
            {
                // 递归调整控件字体
                AdjustFont(form, (float)(2.5/ scale));
            }
            else if (scale > 2)
            {
                // 递归调整控件字体
                AdjustFont(form,(float)( 1.75/ scale));
            }
        }

        private static void AdjustFont(System.Windows.Forms.Control parent, float factor)
        {
            if (parent.Font != null)
            {
                parent.Font = new Font(parent.Font.FontFamily, parent.Font.Size * factor, parent.Font.Style);
            }

            foreach (System.Windows.Forms.Control ctrl in parent.Controls)
            {
                AdjustFont(ctrl, factor);
            }
        }

        private void btn_uploadPics_Click(object sender, EventArgs e)
        {
            dgv_upload.Columns.Clear();
            if (adapter.PiclistfromMD.Count > 0)
            {
                wp.WP_PicturesUpload(ref dgv_upload,adapter.PiclistfromMD);
                dgv_upload.Columns["No"].Width = (int)dgv_articles.Width / 10;
                dgv_upload.Columns["Local File"].Width = (int)dgv_articles.Width / 10 * 2;
                dgv_upload.Columns["Remote File"].Width = (int)dgv_articles.Width / 10 * 6;
                dgv_upload.Columns["Progress"].Width = (int)dgv_articles.Width / 10;
            }
        }

        private void btn_GetCategoryList_Click(object sender, EventArgs e)
        {
            wp.WP_GetCategory(ref cmb_category, ref txt_conn);
            // 把cmb_category的内容复制到dgv_classes
            dgv_categories.Columns.Clear();
            dgv_categories.DataSource = wp.WP_Categories.Copy();
            // 隐藏id列，显示name列，并把列名变为"类别"
            if (dgv_categories.Columns.Contains("id"))
            {
                dgv_categories.Columns["id"].Visible = false;
            }
            if (dgv_categories.Columns.Contains("name"))
            {
                dgv_categories.Columns["name"].HeaderText = "类别";
                dgv_categories.Columns["name"].Visible = true;
            }

            dgv_categories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void btn_ListArticles_Click(object sender, EventArgs e)
        {
            dgv_articles.Columns.Clear();
            btn_GetCategoryList_Click(sender, e);
            wp.WP_GetTag(ref dgv_AllTags);
            wp.WP_GeArticles(ref dgv_articles);
            wp.WP_UpdateTagContent(ref dgv_articles);
            wp.WP_UpdateCategoryContent(ref dgv_articles);
            //当前tablecontrol显示table_articlelist
            tab_view.SelectTab(tab_articlelist);


            // 断开datasource的连接
            UnbindDataGridViewPreserveColumns(dgv_articles);
            // 调整列宽
            dgv_articles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // 先禁用自动列宽
            dgv_articles.Columns["编号"].Width = 100;        // 编号列不需要太宽
            dgv_articles.Columns["类别名称"].Width = 160;       // 分类列适中
            dgv_articles.Columns["标签名称"].Width = 200;       // 标签列适中
            dgv_articles.Columns["发布日期"].Width = 200;   // 日期稍长
                                                        //dgv_articles.Columns["首图编号"].Width = 100;   // 日期稍长
                                                        // 标题列自动占满剩余空间
            dgv_articles.Columns["标题"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // 设置显示顺序
            dgv_articles.Columns["标题"].DisplayIndex = 0;     // 标题最前
            dgv_articles.Columns["编号"].DisplayIndex = 1;
            dgv_articles.Columns["类别名称"].DisplayIndex = 2;
            dgv_articles.Columns["标签名称"].DisplayIndex = 3;
            dgv_articles.Columns["发布日期"].DisplayIndex = 4; // 日期最后
            //dgv_articles.Columns["首图编号"].DisplayIndex = 5; // 日期最后
            //foreach (DataGridViewColumn col in dgv_articles.Columns)
            //{
            //    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
            if (dgv_articles.Columns.Contains("首页图"))
            {
                dgv_articles.Columns["首页图"].Visible = false;
            }
            // 隐藏“类别”列
            if (dgv_articles.Columns.Contains("类别"))
            {
                dgv_articles.Columns["类别"].Visible = false;
            }            
            //把原来的标签id隐藏起来
            if (dgv_articles.Columns.Contains("标签"))
            {
                dgv_articles.Columns["标签"].Visible = false;
            }
            //显示当前编辑中的文章
            //btn_PreviewCurrent_Click(sender, e);
        }
        /// <summary>
        /// 保存 DataGridView 列信息
        /// </summary>
        class ColumnInfo
        {
            public string Name;
            public Type ValueType;
            public int Width;
            public string HeaderText;
            public DataGridViewColumn Column;
        }

        /// <summary>
        /// 断开 DataGridView 与数据源绑定，同时保留列和数据
        /// </summary>
        /// <param name="dgv">要操作的 DataGridView</param>
        public static void UnbindDataGridViewPreserveColumns(DataGridView dgv)
        {
            if (dgv == null) return;

            // 1. 保存列信息
            var columnInfos = new List<ColumnInfo>();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                columnInfos.Add(new ColumnInfo
                {
                    Name = col.Name,
                    ValueType = col.ValueType ?? typeof(string),
                    Width = col.Width,
                    HeaderText = col.HeaderText,
                    Column = col
                });
            }

            // 2. 保存行数据
            var rowData = new List<object[]>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    object[] values = new object[row.Cells.Count];
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        values[i] = row.Cells[i].Value;
                    }
                    rowData.Add(values);
                }
            }

            // 3. 断开绑定
            dgv.DataSource = null;
            dgv.Columns.Clear();

            // 4. 重新创建列
            foreach (var colInfo in columnInfos)
            {
                DataGridViewColumn newCol = new DataGridViewTextBoxColumn
                {
                    Name = colInfo.Name,
                    ValueType = colInfo.ValueType,
                    Width = colInfo.Width,
                    HeaderText = colInfo.HeaderText
                };
                dgv.Columns.Add(newCol);
            }

            // 5. 重新填充数据
            foreach (var values in rowData)
            {
                dgv.Rows.Add(values);
            }
        }

        private void btn_uploadMD_Click(object sender, EventArgs e)
        {
            //如果txt_SpecialPicUrl是数字则转换为int，否则为0
            int? mediaId = 0;
            if (int.TryParse(txt_SpecialPicUrl.Text, out int result))
            {
                mediaId = result;
            }else
            {
                mediaId = null;
            }
            tab_upload.Select();
            wp.WP_ArticleUpload(mediaId, ref dgv_upload,adapter);
            dgv_upload.Columns["No"].Width = (int)dgv_articles.Width / 10;
            dgv_upload.Columns["Local File"].Width = (int)dgv_articles.Width / 10 * 2;
            dgv_upload.Columns["Remote File"].Width = (int)dgv_articles.Width / 10 * 6;
            dgv_upload.Columns["Progress"].Width = (int)dgv_articles.Width / 10;
        }

        private void btn_revisePicURL_Click(object sender, EventArgs e)
        {
            wp.SwitchPicture(adapter,WpAdapter.Picture_mode.remote);
        }

        private void btn_uploadnew_Click(object sender, EventArgs e)
        {
            //更新adapter的Mdcontent和Title
            adapter.GetUserSideMD(adapter.webbrowser);
            //更新图片列表
            adapter.GetPictureList();
            btn_uploadPics_Click(sender, e);
            btn_revisePicURL_Click(sender, e);
            btn_uploadMD_Click(sender, e);
            //激活tab_upload
            tab_view.SelectTab(tab_upload);

            dgv_articles.Columns.Clear();
            btn_GetCategoryList_Click(sender, e);
            wp.WP_GetTag(ref dgv_AllTags);
            wp.WP_GeArticles(ref dgv_articles);
            wp.WP_UpdateTagContent(ref dgv_articles);
            wp.WP_UpdateCategoryContent(ref dgv_articles);
        }

        private void txt_server_TextChanged(object sender, EventArgs e)
        {
            SetupForm.cfg.ServerAddress= txt_server.Text;
        }

        private void txt_user_TextChanged(object sender, EventArgs e)
        {
            SetupForm.cfg.UserName = txt_user.Text;
        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {
            SetupForm.cfg.AppPassword = txt_pass.Text;
        }

        private void btn_addnewclass_Click(object sender, EventArgs e)
        {
            if (InputTextForm.ShowDialog("添加新分类", "请输入新分类名称：") is string newCateGoryName)
            {
                wp.WP_AddCategory(ref cmb_category, newCateGoryName);
                //同步dgv_categories
                btn_GetCategoryList_Click(sender, e);

            }
        }



        private void btn_AddMainPicture_Click(object sender, EventArgs e)
        {
            //打开openfiledialog选择图片，并上传
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "选择图片文件";
                ofd.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.gif;*.bmp|所有文件|*.*";
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    txt_SpecialPicUrl.Text = filePath;
                    try
                    {
                        pic_specialpicture.Image = System.Drawing.Image.FromFile(filePath);
                        //上传图片并获取mediaId
                        int mediaId = Wpapi.UploadFeaturedImage(
                            SetupForm.cfg.ServerAddress,
                            SetupForm.cfg.UserName,
                            SetupForm.cfg.AppPassword,
                            filePath
                        );
                        wp.specialPictureIndex = mediaId;
                        txt_SpecialPicUrl.Text = mediaId.ToString();
                    }
                    catch
                    {
                        pic_specialpicture.Image = null;
                    }
                }
            }

        }

        private void btn_DeleteCategory_Click(object sender, EventArgs e)
        {
            // 弹出一个 SelectTextForm 对话框，让用户选择要删除的分类
            var dt = cmb_category.DataSource as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没有可删除的分类。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 构造分类名称列表
            var categoryNames = dt.AsEnumerable().Select(r => r.Field<string>("name")).ToArray();
            string selectedCategory = SelectTextForm.ShowSelectDialog("删除分类", "请选择要删除的分类：", categoryNames);
            if (string.IsNullOrEmpty(selectedCategory))
                return;

            // 找到对应的分类ID
            var row = dt.AsEnumerable().FirstOrDefault(r => r.Field<string>("name") == selectedCategory);
            if (row == null)
            {
                MessageBox.Show("未找到该分类。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int categoryId = row.Field<int>("id");

            // 调用 DeleteCategory 删除分类
            try
            {
                bool success =  Wpapi.DeleteCategory(
                    SetupForm.cfg.ServerAddress,
                    SetupForm.cfg.UserName,
                    SetupForm.cfg.AppPassword,
                    categoryId
                );
                if (success)
                {
                    MessageBox.Show("分类已删除。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 刷新分类列表
                    wp.WP_GetCategory(ref cmb_category, ref txt_conn);
                }
                else
                {
                    MessageBox.Show("删除失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除分类时发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_DeleteArticle_Click(object sender, EventArgs e)
        {
            //删除dgv_articles选中的文章
            if (dgv_articles.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的文章。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var row = dgv_articles.SelectedRows[0];
            int articleId = (int)row.Cells["编号"].Value;
            try
            {
                bool success = Wpapi.DeleteArticle(
                    SetupForm.cfg.ServerAddress,
                    SetupForm.cfg.UserName,
                    SetupForm.cfg.AppPassword,
                    articleId
                );
                if (success)
                {
                    MessageBox.Show("文章已删除。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 刷新文章列表
                    btn_ListArticles_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("删除失败。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除文章时发生错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //var wp = new WordPressAdmin(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName,SetupForm.cfg.Password);
            //wp.DeletePostAsync(articleId); // 删除文章ID=123
        }

        private void btn_AddTagFromExist_Click(object sender, EventArgs e)
        {
            //把dgv_AllTags选中的项目复制到dgv_ArticleTags，如果dgv_ArticleTags存在于选中的项目同名的项目则不添加
            if (dgv_AllTags.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要添加的标签。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (DataGridViewRow row in dgv_AllTags.SelectedRows)
            {
                string tagName = row.Cells["标签"].Value.ToString();
                string tagNumber = row.Cells["编号"].Value.ToString();
                bool exists = false;
                foreach (DataGridViewRow articleTagRow in dgv_ArticleTags.Rows)
                {
                    if (articleTagRow.Cells["标签"].Value.ToString() == tagName)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    if (!dgv_ArticleTags.Columns.Contains("标签"))dgv_ArticleTags.Columns.Add("标签", "标签");
                    if (!dgv_ArticleTags.Columns.Contains("编号")) dgv_ArticleTags.Columns.Add("编号", "编号");
                    int newIndex = dgv_ArticleTags.Rows.Add();
                    dgv_ArticleTags.Rows[newIndex].Cells["标签"].Value = tagName;
                    dgv_ArticleTags.Rows[newIndex].Cells["编号"].Value = tagNumber;
                }
            }
            // 隐藏“编号”列
            if (dgv_ArticleTags.Columns.Contains("编号"))
                dgv_ArticleTags.Columns["编号"].Visible = false;
            // 设置“标签”列占满整个DataGridView宽度
            if (dgv_ArticleTags.Columns.Contains("标签"))
            {
                dgv_ArticleTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv_ArticleTags.Columns["标签"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            //读取dgv_ArticleTags的"编号"列到wp.ArticleTagIndexs
            //如果dgv_ArticleTags没有"编号"列则不操作
            if (!dgv_ArticleTags.Columns.Contains("编号")) return;
            wp.ArticleTagIndexs = new int[dgv_ArticleTags.Rows.Count];
            for (int i = 0; i < dgv_ArticleTags.Rows.Count; i++)
            {
                if (dgv_ArticleTags.Rows[i].Cells["编号"].Value != null && int.TryParse(dgv_ArticleTags.Rows[i].Cells["编号"].Value.ToString(), out int tagId))
                {
                    wp.ArticleTagIndexs[i] = tagId;
                }
                else
                {
                    wp.ArticleTagIndexs[i] = -1; // 无效编号
                }
            }
            listBox1.DataSource=wp.ArticleTagIndexs;
        }

        private void dgv_ArticleTags_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_addNewTag_Click(object sender, EventArgs e)
        {
            //通过InputTextForm对话框添加新标签
            if (InputTextForm.ShowDialog("添加新标签", "请输入新标签名称：") is string newTagName)
            {
                //查询dgv_ArticleTags是否包含"标签"列与newTagName同名的行，如果不存在则新建一行
                bool exists = false;
                foreach (DataGridViewRow row in dgv_ArticleTags.Rows)
                {
                    if (row.Cells["标签"].Value != null && row.Cells["标签"].Value.ToString() == newTagName)
                    {
                        exists = true;
                        MessageBox.Show($"标签 \"{newTagName}\" 已存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }

                if (!exists)
                {
                    //查询dgv_AllTags是否包含"标签"列与newTagName同名的行，如果存在则使用该行的编号，否则编号为-1
                    if (!dgv_AllTags.Columns.Contains("标签")) dgv_AllTags.Columns.Add("标签", "标签");
                    if (!dgv_AllTags.Columns.Contains("编号")) dgv_AllTags.Columns.Add("编号", "编号");
                    int existingTagId = -1;
                    foreach (DataGridViewRow row in dgv_AllTags.Rows)
                    {
                        if (row.Cells["标签"].Value != null && row.Cells["标签"].Value.ToString() == newTagName)
                        {
                            existingTagId = (int)row.Cells["编号"].Value;
                            break;
                        }
                    }
                    if (existingTagId != -1)
                    {
                        MessageBox.Show($"标签 \"{newTagName}\" 已存在于标签列表中，编号为 {existingTagId}。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        int newIndex = dgv_ArticleTags.Rows.Add();
                        dgv_ArticleTags.Rows[newIndex].Cells["标签"].Value = newTagName;

                        try
                        {
                            //将新增标签上传服务器
                            int newTagId = Wpapi.AddTag(
                                SetupForm.cfg.ServerAddress,
                                SetupForm.cfg.UserName,
                                SetupForm.cfg.AppPassword,
                                newTagName
                            );
                            dgv_ArticleTags.Rows[newIndex].Cells["编号"].Value = newTagId; // 新标签没有编号
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show("添加标签时发生错误：" + ee.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                        wp.WP_GetTag(ref dgv_AllTags);

                }
                // 隐藏“编号”列
                dgv_ArticleTags.Columns["编号"].Visible = false;
                // 设置“标签”列占满整个DataGridView宽度
                dgv_ArticleTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv_ArticleTags.Columns["标签"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //读取dgv_ArticleTags的"编号"列到wp.ArticleTagIndexs
                //如果dgv_ArticleTags没有"编号"列则不操作
                if (!dgv_ArticleTags.Columns.Contains("编号")) return;
                wp.ArticleTagIndexs = new int[dgv_ArticleTags.Rows.Count];
                for (int i = 0; i < dgv_ArticleTags.Rows.Count; i++)
                {
                    if (dgv_ArticleTags.Rows[i].Cells["编号"].Value != null && int.TryParse(dgv_ArticleTags.Rows[i].Cells["编号"].Value.ToString(), out int tagId))
                    {
                        wp.ArticleTagIndexs[i] = tagId;
                    }
                    else
                    {
                        wp.ArticleTagIndexs[i] = -1; // 无效编号
                    }
                }
                listBox1.DataSource = wp.ArticleTagIndexs;
            }
        }

        private void dgv_articles_SelectionChanged(object sender, EventArgs e)
        {try
            {
                if (dgv_articles.CurrentRow == null || dgv_articles.CurrentRow.Index < 0)
                {
                    // 当前没有选中行，说明可能是点击了标题栏
                    return;
                }
                //linkLabel1设置为选中的文章的链接
                if (dgv_articles.SelectedRows.Count > 0)
                {
                    var row = dgv_articles.SelectedRows[0];
                    //标题抄上
                    if (row.Cells["标题"].Value!=null) txt_title.Text = row.Cells["标题"].Value.ToString();
                    //根据编号生成链接
                    string link = $"{SetupForm.cfg.ServerAddress.TrimEnd('/')}/?p={row.Cells["编号"].Value}";
                    linkLabel1.Text = link;
                    //根据链接打开网页
                    linkLabel1.Links.Clear();
                    linkLabel1.Links.Add(0, link.Length, link);

                    //根据首图编号生成图片下载地址$"{siteUrl}/?rest_route=/wp/v2/media/{mediaId}";
                    if (row.Cells["首页图"].Value != null && int.TryParse(row.Cells["首页图"].Value.ToString(), out int mediaId) && mediaId > 0)
                    {
                        txt_SpecialPicUrl.Text = mediaId.ToString();
                        if (chb_viewSpecialPicture.Checked)
                        {
                            //下载图片并显示在pic_specialpicture
                            string picUrl = Wpapi.GetPictureUrlFromMediaApi(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, mediaId);
                            try
                            {
                                using (var webClient = new System.Net.WebClient())
                                {
                                    var imageData = webClient.DownloadData(picUrl);
                                    using (var ms = new System.IO.MemoryStream(imageData))
                                    {
                                        pic_specialpicture.Image = System.Drawing.Image.FromStream(ms);
                                    }
                                }
                            }
                            catch
                            {
                                pic_specialpicture.Image = null;
                            }
                        }
                        else
                        {
                            pic_specialpicture.Image = null;
                        }

                    }
                    else
                    {
                        txt_SpecialPicUrl.Text = "-";
                        pic_specialpicture.Image = null;
                    }




                    if (!dgv_articles.Columns.Contains("类别名称")) return;
                    //如果包括"类别名称"列则cmb_Category选中与文章类别名称相同的行
                    string categoryName="";
                    if (row.Cells["类别名称"].Value != null)
                    {
                        categoryName = row.Cells["类别名称"].Value.ToString();
                    }
                    for (int i = 0; i < cmb_category.Items.Count; i++)
                    {
                        var item = cmb_category.Items[i] as DataRowView;
                        if (item != null && item["Name"].ToString() == categoryName)
                        {
                            cmb_category.SelectedIndex = i;
                            break;
                        }
                    }

                    if (!dgv_articles.Columns.Contains("标签名称")) return;
                    string tagNames = "";
                    if (row.Cells["标签名称"].Value != null)
                    {
                        tagNames = row.Cells["标签名称"].Value.ToString();
                    }
                    //如果包含"标签名称"列，把标签名称按逗号分割，去掉前后空格

                    string[] tags = tagNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim()).ToArray();
                    //清空dgv_ArticleTags
                    dgv_ArticleTags.Columns.Clear();
                    dgv_ArticleTags.Rows.Clear();
                    //把tags添加到dgv_ArticleTags
                    foreach (string tag in tags)
                    {
                        if (!dgv_ArticleTags.Columns.Contains("标签")) dgv_ArticleTags.Columns.Add("标签", "标签");
                        if (!dgv_ArticleTags.Columns.Contains("编号")) dgv_ArticleTags.Columns.Add("编号", "编号");
                        int newIndex = dgv_ArticleTags.Rows.Add();
                        dgv_ArticleTags.Rows[newIndex].Cells["标签"].Value = tag;
                        dgv_ArticleTags.Rows[newIndex].Cells["编号"].Value = "-1"; // 从文章列表加载的标签没有编号
                                                                                 // 隐藏“编号”列
                        dgv_ArticleTags.Columns["编号"].Visible = false;
                        // 设置“标签”列占满整个DataGridView宽度
                        dgv_ArticleTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dgv_ArticleTags.Columns["标签"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                }
            }
            catch { }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //用默认浏览器打开链接
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = e.Link.LinkData.ToString(),
                UseShellExecute = true
            });
            
        }

        private void btn_PreviewCurrent_Click(object sender, EventArgs e)
        {
            txt_title.Text = adapter.GetTitle();
            rtb_source.Text = adapter.Mdcontent;
            txt_SpecialPicUrl.Text = wp.specialPictureIndex.ToString();
            //如果txt_SpecialPicUrl是数字则转换为int，否则为0
            int mediaId = 0;
            if (int.TryParse(txt_SpecialPicUrl.Text, out int result)) mediaId = result;
            if (chb_viewSpecialPicture.Checked &&mediaId!=0)
            {
                //下载图片并显示在pic_specialpicture
                string picUrl = Wpapi.GetPictureUrlFromMediaApi(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, mediaId);
                try
                {
                    using (var webClient = new System.Net.WebClient())
                    {
                        var imageData = webClient.DownloadData(picUrl);
                        using (var ms = new System.IO.MemoryStream(imageData))
                        {
                            pic_specialpicture.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }
                }
                catch
                {
                    pic_specialpicture.Image = null;
                }
            }else
            {
                pic_specialpicture.Image = null;
            }
        }

        private void btn_GetCategoryAndTags_Click(object sender, EventArgs e)
        {
            //根据cmb_category的"Name"更新"ID"
            //跟dgv_ArticleTags的"标签"更新"编号"
        }

        private void tab_view_Selected(object sender, TabControlEventArgs e)
        {
        }
        //当tab_source 被选中
        private void tab_view_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tab_source)
            {
                btn_PreviewCurrent_Click(sender, e);
            }
            else if (e.TabPage == tab_articlelist)
            {
                //btn_ListArticles_Click(sender, e);
            }
        }

        private void btn_reviseTitle_Click(object sender, EventArgs e)
        {
            txt_title.ReadOnly = false;
        }

        private void txt_title_Leave(object sender, EventArgs e)
        {
            txt_title.ReadOnly = true;
            adapter.SetTitle(txt_title.Text);
            rtb_source.Text = adapter.Mdcontent;
        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            wp.ArticleCategoryIndex = (cmb_category.SelectedItem as DataRowView)?["id"] is int id ? id : 0;
            textBox1.Text = wp.ArticleCategoryIndex.ToString();
        }

    }
}
