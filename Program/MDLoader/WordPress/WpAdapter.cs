using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDLoader
{
    public class WpAdapter
    {
        //上传图片进程表
        public DataTable dt_UploadProgress = new DataTable();

        //Wordpress相关对象
        public DataTable WP_Tags;
        public DataTable WP_Categories;
        public DataTable WP_Articles;
        public int specialPictureIndex = 0;

        //当前文章的分类和标签
        public int ArticleCategoryIndex = 0;
        public int[] ArticleTagIndexs = new int[] { };

        public void WP_GetCategory(ref ComboBox cmb_categroy, ref TextBox txt_conn)
        {
            WP_Categories = Wpapi.GetCategories(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, SetupForm.cfg.TimeOut * 1000);
            cmb_categroy.DataSource = WP_Categories.Copy();
            cmb_categroy.DisplayMember = "Name";
            cmb_categroy.ValueMember = "ID";
            if (WP_Categories != null)
            {
                txt_conn.BackColor = Color.DarkGreen;
                txt_conn.Text = "已连接";
            }
            else
            {
                txt_conn.BackColor = Color.Red;
                txt_conn.Text = "未连接";
            }
        }
        public void WP_AddCategory(ref ComboBox cmb_categroy, string txt_newCategoryName)
        {
            try
            {
                if (Wpapi.AddCategory(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, txt_newCategoryName, 0, "", SetupForm.cfg.TimeOut * 1000) > 0)
                {
                    MessageBox.Show("添加分类成功");
                }

                WP_Categories = Wpapi.GetCategories(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, SetupForm.cfg.TimeOut * 1000);
                cmb_categroy.DataSource = WP_Categories.Copy();
                cmb_categroy.DisplayMember = "Name";
                cmb_categroy.ValueMember = "ID";
            }
            catch (Exception ee)
            {
                MessageBox.Show("添加分类失败，" + ee.Message);
            }
        }
        /// <summary>
        /// 更新文章列表中的标签内容，把标签ID转换为标签名称
        /// </summary>
        /// <param name="dgv_articles"></param>
        /// 
        public void WP_UpdateTagContent(ref DataGridView dgv_articles)
        {
            // 检查 WP_Tags 和 dgv_articles 是否有效
            if (WP_Tags == null || dgv_articles.DataSource == null)
                return;

            // 检查“标签”列是否存在，不存在则添加
            if (!dgv_articles.Columns.Contains("标签名称"))
            {
                dgv_articles.Columns.Add("标签名称", "标签名称");
            }

            // 构建标签ID到标签名称的映射
            Dictionary<string, string> tagIdNameMap = new Dictionary<string, string>();
            foreach (DataRow row in WP_Tags.Rows)
            {
                string id = row["编号"].ToString();
                string name = row["标签"].ToString();
                tagIdNameMap[id] = name;
            }

            // 遍历每一行，解析“标签”列（ID列表），转换为标签名称
            foreach (DataGridViewRow dgvr in dgv_articles.Rows)
            {
                if (dgvr.IsNewRow) continue;
                string tagIds = dgvr.Cells["标签"].Value?.ToString();
                if (string.IsNullOrEmpty(tagIds))
                {
                    dgvr.Cells["标签名称"].Value = "";
                    continue;
                }
                var ids = tagIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> names = new List<string>();
                foreach (var id in ids)
                {
                    if (tagIdNameMap.TryGetValue(id.Trim(), out string name))
                        names.Add(name);
                }
                dgvr.Cells["标签名称"].Value = string.Join(",", names);
            }


        }
        // 在 WP_UpdateCategoryContent 方法中，将“类别”列隐藏起来
        public void WP_UpdateCategoryContent(ref DataGridView dgv_articles)
        {
            // 检查 WP_Categories 和 dgv_articles 是否有效
            if (WP_Categories == null || dgv_articles.DataSource == null)
                return;
            // 检查“类别名称”列是否存在，不存在则添加
            if (!dgv_articles.Columns.Contains("类别名称"))
            {
                dgv_articles.Columns.Add("类别名称", "类别名称");
            }
            // 构建分类ID到分类名称的映射
            Dictionary<string, string> categoryIdNameMap = new Dictionary<string, string>();
            foreach (DataRow row in WP_Categories.Rows)
            {
                string id = row["ID"].ToString();
                string name = row["Name"].ToString();
                categoryIdNameMap[id] = name;
            }
            // 遍历每一行，解析“类别”列（ID列表），转换为分类名称
            foreach (DataGridViewRow dgvr in dgv_articles.Rows)
            {
                if (dgvr.IsNewRow) continue;
                string categoryIds = dgvr.Cells["类别"].Value?.ToString();
                if (string.IsNullOrEmpty(categoryIds))
                {
                    dgvr.Cells["类别名称"].Value = "";
                    continue;
                }
                var ids = categoryIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> names = new List<string>();
                foreach (var id in ids)
                {
                    if (categoryIdNameMap.TryGetValue(id.Trim(), out string name))
                        names.Add(name);
                }
                dgvr.Cells["类别名称"].Value = string.Join(",", names);

            }

        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="dgv_articles"></param>
        public void WP_GeArticles(ref DataGridView dgv_articles)
        {
            WP_Articles = Wpapi.GetArticles(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, SetupForm.cfg.TimeOut * 1000);
            dgv_articles.DataSource = WP_Articles;




        }
        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="dgv_tags"></param>
        public void WP_GetTag(ref DataGridView dgv_tags)
        {
            WP_Tags = Wpapi.GetTags(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, SetupForm.cfg.TimeOut * 1000);
            dgv_tags.DataSource = WP_Tags;
            // 隐藏“编号”列
            if (dgv_tags.Columns.Contains("编号"))
            dgv_tags.Columns["编号"].Visible = false;
            // 设置“标签”列占满整个DataGridView宽度
            if (dgv_tags.Columns.Contains("标签"))
            {
                dgv_tags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv_tags.Columns["标签"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="dgv_tags"></param>
        /// <param name="txt_newTagName"></param>
        //参考WP_AddCategory 编写WP_AddTag
        public void WP_AddTag(ref DataGridView dgv_tags, string txt_newTagName)
        {
            try
            {
                if (Wpapi.AddTag(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, txt_newTagName, "", SetupForm.cfg.TimeOut * 1000) > 0)
                {
                    MessageBox.Show("添加标签成功");
                }
                WP_Tags = Wpapi.GetTags(SetupForm.cfg.ServerAddress, SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, SetupForm.cfg.TimeOut * 1000);
                dgv_tags.DataSource = WP_Tags;
                // 隐藏“编号”列
                if (dgv_tags.Columns.Contains("编号"))
                    dgv_tags.Columns["编号"].Visible = false;
                // 设置“标签”列占满整个DataGridView宽度
                if (dgv_tags.Columns.Contains("标签"))
                {
                    dgv_tags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgv_tags.Columns["标签"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("添加标签失败，" + ee.Message);
            }
        }
        /// <summary>
        /// 上传图片到Wordpress
        /// </summary>
        /// <param name="dgvtorefresh"></param>
        public void WP_PicturesUpload(ref DataGridView dgvtorefresh, List<string> PiclistfromMD)
        {

            //添加4列，一列是编号，LocalFile是本地文件，RemoteFile是要上传到的路径,Progress是否上传完成
            dt_UploadProgress.Rows.Clear();
            dt_UploadProgress.Columns.Clear();
            dt_UploadProgress.Columns.Add("No");
            dt_UploadProgress.Columns.Add("Local File");
            dt_UploadProgress.Columns.Add("Remote File");
            dt_UploadProgress.Columns.Add("Progress");
            dgvtorefresh.DataSource = dt_UploadProgress;
            //上传图片
            var ftproot = SetupForm.cfg.ServerAddress + ":" + "/" + DateTime.Now.ToLocalTime().ToString().Replace(" ", "_").Replace(":", "-") + "/";
            //var httproot = SetupForm.cfg.HttpUrlHead + "/" + DateTime.Now.ToLocalTime().ToString().Replace(" ", "_").Replace(":", "-") + "/";
            for (int i = 0; i < PiclistfromMD.Count; i++)
            {
                var pic = PiclistfromMD[i];

                //生成本地图片路径
                string s_cached_full_image;

                if (!pic.Contains(":"))
                {
                    s_cached_full_image = Application.StartupPath + "\\" + Program.cacheFolder + "\\" + pic.Replace("./", "").Replace("/", "\\");
                }
                else
                {
                    s_cached_full_image = Application.StartupPath + "\\" + Program.cacheFolder + "\\" + System.IO.Path.GetFileName(pic);
                }
                DataRow dr = dt_UploadProgress.NewRow();
                try
                {
                    string s_remotehttpurl = Wpapi.UploadImageFile(SetupForm.cfg.ServerAddress + "/?rest_route=/wp/v2/media", SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, s_cached_full_image, SetupForm.cfg.TimeOut);
                    dr["No"] = i + 1;
                    dr["Local File"] = pic;
                    dr["Remote File"] = s_remotehttpurl;
                    dr["Progress"] = "Done";
                    dt_UploadProgress.Rows.Add(dr);
                }
                catch (Exception ee)
                {
                    dr["No"] = i + 1;
                    dr["Local File"] = pic;
                    dr["Progress"] = "Failed";
                    dr["Remote File"] = ee.Message;
                    dt_UploadProgress.Rows.Add(dr);
                }
                if (dgvtorefresh != null)
                {
                    dgvtorefresh.FirstDisplayedScrollingRowIndex = dgvtorefresh.Rows.Count - 1;
                    dgvtorefresh.Refresh();
                    Application.DoEvents();
                }

            }
        }
        //图片资源的呈现方式，本地或者远程url
        public enum Picture_mode { local, remote };
        /// <summary>
        /// 切换md文本中的图片地址
        /// 本地图片与网络图片互相切换
        /// </summary>
        /// <returns></returns>
        public void SwitchPicture(MdAdapter adapter, Picture_mode type)
        {
            if (type == Picture_mode.remote)
            {
                //view = Picture_mode.remote;
                for (int i = 0; i < dt_UploadProgress.Rows.Count; i++)
                {
                    adapter.Mdcontent = adapter.Mdcontent.Replace(dt_UploadProgress.Rows[i]["Local File"].ToString(), dt_UploadProgress.Rows[i]["Remote File"].ToString());
                }
            }
            else
            {
                //view = Picture_mode.local;
                for (int i = 0; i < dt_UploadProgress.Rows.Count; i++)
                {
                    adapter.Mdcontent = adapter.Mdcontent.Replace(dt_UploadProgress.Rows[i]["Remote File"].ToString(), dt_UploadProgress.Rows[i]["Local File"].ToString());
                }
            }
        }
        /// <summary>
        ///上传文章到Wordpress
        /// </summary>
        /// <param name="topicPic"></param>
        /// <param name="dgvtorefresh"></param>
        public void WP_ArticleUpload(int? topicPic, ref DataGridView dgvtorefresh,MdAdapter adapter)
        {
            if (dt_UploadProgress.Columns.Count == 0)
            {
                dt_UploadProgress.Columns.Add("No");
                dt_UploadProgress.Columns.Add("Local File");
                dt_UploadProgress.Columns.Add("Remote File");
                dt_UploadProgress.Columns.Add("Progress");
                dgvtorefresh.DataSource = dt_UploadProgress;
            }
            DataRow dr = dt_UploadProgress.NewRow();
            //从cmb_category提取分类ID

            try
            {
                string s_remotehttpurl = Wpapi.UploadMarkdownText(ArticleCategoryIndex,ArticleTagIndexs,
                SetupForm.cfg.ServerAddress + "/?rest_route=/wp/v2/posts", SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, adapter.Mdcontent, topicPic, "publish", SetupForm.cfg.TimeOut);
                //string s_remotehttpurl = Wpapi.UploadMarkdownText(ArticleCategoryIndex,ArticleTagIndexs,
                //SetupForm.cfg.ServerAddress + "/wp-json/wp/v2/posts", SetupForm.cfg.UserName, SetupForm.cfg.AppPassword, adapter.Mdcontent, topicPic, "publish", SetupForm.cfg.TimeOut);

                dr["No"] = 0;
                dr["Local File"] = adapter.MdFilePath;
                dr["Remote File"] = s_remotehttpurl;
                dr["Progress"] = "Done";
                dt_UploadProgress.Rows.Add(dr);
            }
            catch (Exception ee)
            {
                dr["No"] = 1;
                dr["Local File"] = adapter.MdFilePath;
                dr["Progress"] = "Failed";
                dr["Remote File"] = ee.Message;
                dt_UploadProgress.Rows.Add(dr);
            }
            DataRow drr = dt_UploadProgress.NewRow();
            drr["No"] = "All Done";
            drr["Local File"] = "";
            drr["Remote File"] = "";
            drr["Progress"] = "";
            dt_UploadProgress.Rows.Add(drr);
        }
    }
}
