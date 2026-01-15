using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using static MDLoader.ChatGLMClient;
using System.Security.Policy;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Win32;

namespace MDLoader
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class Form1 : Form
    {
        //设置窗体
        SetupForm frm_setup = new SetupForm();
        //editor.md的C#适配层
        MdAdapter adapter = new MdAdapter();
        //标题栏内容
        String captain = "";
        //editor.md对象
        HtmlElement editor;
        //图片资源的呈现方式，本地或者远程url
        public enum Picture_mode { local, remote };
        Picture_mode view = Picture_mode.local;

        public static Form1 ThisForm;

        //浏览器字体缩放比例
        int zoomFactor = 100;

        public Form1()
        {
            InitializeComponent();
            //webBrowser1 = new WebBrowser();
            //webBrowser1.Parent = this.panel2;
            webBrowser1.ScrollBarsEnabled = true;
            //webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.ObjectForScripting = this;//允许使用ObjectForScripting
            webBrowser1.ScriptErrorsSuppressed = true; //错误脚本提示  
            webBrowser1.IsWebBrowserContextMenuEnabled = true; // 右键菜单  
            webBrowser1.WebBrowserShortcutsEnabled = true; //快捷键  
            menuStrip1.Font = new Font(menuStrip1.Font.FontFamily, SetupForm.cfg.FontSize);
            adapter.webbrowser = webBrowser1;
            ThisForm = this;

        }


        /// <summary>
        /// 主窗体加载完成后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //Dock editormd window
            panel2_SizeChanged(sender, e);
            //获取打开文件的路径
            adapter.Filename = Program.mdFileToOpen;
            //保存标题栏文本
            captain = this.Text;
            //设置新的标题栏，显示打开的文件
            this.Text = captain + "  " + adapter.Filename;
            //MessageBox.Show(Program.fileName);

            //复制index.html
            string editorpath_org = Application.StartupPath + "\\editormd\\" + "index_0.html";
            string editorpath = Application.StartupPath + "\\" + Program.cacheFolder + "\\" + "index.html";
            MFiles.CopyFile(editorpath_org, editorpath);
            //打开editor.md
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.ObjectForScripting = new JsCallback(this, contextMenuStrip1);
            webBrowser1.Navigate(editorpath);
        }
        /// <summary>
        /// 拦截鼠标滚轮事件，实现Ctrl+滚轮缩放菜单栏字体大小
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_MOUSEWHEEL = 0x020A;

            if (m.Msg == WM_MOUSEWHEEL)
            {
                if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    int delta = (short)((m.WParam.ToInt32() >> 16) & 0xffff);
                    if (m.Msg == WM_MOUSEWHEEL)
                    {
                        if (delta > 0)
                            menuStrip1.Font = new Font(menuStrip1.Font.FontFamily, menuStrip1.Font.Size + 1);
                        else
                        {
                            menuStrip1.Font = new Font(menuStrip1.Font.FontFamily, menuStrip1.Font.Size - 1);
                        }
                    }
                }

            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// 文档加载完成后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;
            //读取editor.md html文档对象，防止在尚未加载完文档的时候误发触动
            editor = doc.GetElementById("test-editormd");
            //参数方式打开md文件
            if (!(Program.mdFileToOpen == ""))
            {
                adapter.LoadMDFile(Program.mdFileToOpen, webBrowser1);
                adapter.CacheMDPictures(Program.mdFileToOpen);
                adapter.SetUserSideMD(webBrowser1);

                adapter.MdFilePath = Program.mdFileToOpen.Substring(0, Program.mdFileToOpen.LastIndexOf("\\"));
            }
            else
            {
                //显示md语法简介
                adapter.LoadMDFile(Application.StartupPath + "\\intro\\intro.md", webBrowser1);
                adapter.CacheMDPictures(Application.StartupPath + "\\intro\\intro.md");
                adapter.SetUserSideMD(webBrowser1);
                //去除文件关联，防止用户误保存
                adapter.Filename = "";
            }
            adapter.SetUserSideMD(webBrowser1);
            //设置缩放比例
            zoomFactor = (int)GetScalePercent();
            ZoomBrowser(zoomFactor);
            Application.DoEvents();
            Thread.Sleep(300);
            ReDrawEditor();
        }
        /// <summary>
        /// 获取当前系统DPI缩放百分比
        /// </summary>
        /// <returns></returns>
        float GetScalePercent()
        {
            using (Graphics g = this.CreateGraphics()) // this 指 Form
            {
                float dpi = g.DpiX; // 横向 DPI
                return dpi / 96.0f * 100; // 96dpi = 100%
            }
        }
        /// <summary>
        /// 刷新浏览器
        /// </summary>
        /// <returns></returns>
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开editor.md
            adapter.SetUserSideMD(webBrowser1);
        }
        private void ZoomBrowser(int zoomFactor)
        {
            // 100 表示 100%，200 表示 200%
            object pvaIn = (int)((zoomFactor - 100) * SetupForm.cfg.ViewZoomFactor / 100) + 100;
            object pvaOut = null;
            webBrowser1.ActiveXInstance.GetType().InvokeMember(
                "ExecWB",
                System.Reflection.BindingFlags.InvokeMethod,
                null,
                webBrowser1.ActiveXInstance,
                new object[] { 63, 1, pvaIn, pvaOut }   // 63 = OLECMDID_OPTICAL_ZOOM
            );
        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <returns></returns>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            //获取所打开文件的文件名
            Program.mdFileToOpen = openFileDialog1.FileName;
            if (dr == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(Program.mdFileToOpen))
            {
                adapter.LoadMDFile(Program.mdFileToOpen, webBrowser1);
                adapter.CacheMDPictures(Program.mdFileToOpen);
                this.Text = captain + "  " + Program.mdFileToOpen;
                adapter.SetUserSideMD(webBrowser1);
                //webBrowser1.Refresh();
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adapter.GetUserSideMD(webBrowser1);
            if (adapter.Filename == "")
            {
                DialogResult dr = saveFileDialog1.ShowDialog();
                //获取所打开文件的文件名,如果是多个文件选择FileNames
                string fileName = saveFileDialog1.FileName;
                if (dr == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(fileName))
                {
                    adapter.MdFilePath = fileName.Substring(0, fileName.LastIndexOf("\\"));
                    adapter.Filename = fileName;
                }
            }
            adapter.SaveMDFile(adapter.Filename);
            adapter.SaveOtherFiletoLocal(adapter.Filename);
            this.Text = captain + "  " + adapter.Filename;
        }
        /// <summary>
        /// 另存为
        /// </summary>
        /// <returns></returns>
        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            adapter.Filename = "";
            saveToolStripMenuItem_Click(sender, e);

        }
        /// <summary>
        /// 新建文件
        /// </summary>
        /// <returns></returns>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adapter.Clear(webBrowser1);
            this.Text = captain;
        }

        /// <summary>
        /// 同步浏览器窗体与主窗体大小
        /// </summary>
        /// <returns></returns>
        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            if (editor == null) return;
            if (Math.Abs(editor.ClientRectangle.Height - this.ClientRectangle.Height) > 20)
            {
                ReDrawEditor();
            }
        }
        /// <summary>
        /// 透过js脚本改变editor.md窗体大小
        /// </summary>
        /// <returns></returns>
        private void ReDrawEditor()
        {
            Object[] objArray = new Object[2];
            var w = webBrowser1.Width;
            var h = webBrowser1.Height;
            objArray[0] = (Object)(w);
            objArray[1] = (Object)(h);
            HtmlDocument doc = webBrowser1.Document;
            doc.InvokeScript("setClientSize", objArray);
        }

        /// <summary>
        /// About窗体
        /// </summary>
        /// <returns></returns>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.ThisForm.Info_StatusBar("Click here to visit www.luafans.com for more informations", true);
            MessageBox.Show(captain + "\n" +
                "Author Contact:64034373@qq.com" + "\nBased on opensorce project editor.md \n Visit https://gitee.com/miuser00/md-fileloader for more info\n ", "About");
        }


        ///// <summary>
        ///// 注意！这个函数是从webbrowser的js中回调的，当用户输入文本发生变化时触发
        ///// 当editor.md文本发生变化时更新adapter中markdown文本，如果有新图片插入，重新缓冲图片
        ///// </summary>
        ///// <returns></returns>
        //public void OnContentChange()
        //{
        //    adapter.GetUserSideMD(webBrowser1);
        //    if (adapter.GetIfModified())
        //    {
        //        if (!this.Text.EndsWith(" *")) this.Text = this.Text + " *";
        //    }
        //    else
        //    {
        //        if (this.Text.EndsWith(" *")) this.Text = this.Text.Substring(0, this.Text.Length - 2);
        //    }
        //    if (adapter.Filename == "") return;
        //    try
        //    {
        //        List<string> updatedlist = adapter.CacheMDPictures(adapter.Filename);
        //        HtmlDocument doc = webBrowser1.Document;
        //        HtmlElementCollection elementcol = doc.GetElementsByTagName("img");  //搜索所有的 img 标签
        //        //System.Threading.Thread.Sleep(100);
        //        foreach (HtmlElement ele in elementcol)
        //        {
        //            Regex reg = new Regex(@"src=""(.*)\?(.*)"">");
        //            Match math = reg.Match(ele.OuterHtml);
        //            String filename = math.Groups[1].Value;
        //            if (updatedlist != null && updatedlist.Contains(filename))
        //            {
        //                ele.OuterHtml = ele.OuterHtml.Replace("t=", "id=");
        //            }
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //    }

        //}

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //删除缓存目录
            MFiles.DeleteFolder(Application.StartupPath + "\\" + Program.cacheFolder);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (adapter.GetIfModified())
            {
                var result = MessageBox.Show("文件修改尚未被保存，请问是否需要保存后退出。", "文件未保存", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(adapter.Filename))
                    {
                        adapter.GetUserSideMD(webBrowser1);
                        DialogResult dr = saveFileDialog1.ShowDialog();
                        //获取所打开文件的文件名,如果是多个文件选择FileNames
                        string fileName = saveFileDialog1.FileName;
                        if (dr == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(fileName))
                        {
                            adapter.MdFilePath = fileName.Substring(0, fileName.LastIndexOf("\\"));
                            adapter.Filename = fileName;
                            adapter.SaveMDFile(adapter.Filename);
                            this.Text = captain + "  " + adapter.Filename;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        adapter.SaveMDFile(adapter.Filename);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 拦截刷新快捷键，并触发页面内容刷新
        /// 拦截粘贴快捷键，并把粘贴板的位图数据转储到本地文件，并粘贴地址文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {


        }
        /// <summary>
        /// 当当前页面发生变化时触发这个函数，出发的原因可能是点击了超链，或者是Drop事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

            //如果加载的是默认框架，则放行正常打开，打开其余页面都禁止掉
            if (e.Url.LocalPath == Application.StartupPath + "\\" + Program.cacheFolder + "\\" + "index.html")
            {
                return;
            }
            else
            {
                e.Cancel = true;
                //如果是打开嵌入的MD文件，则开启新的进程并打开文件
                if (e.Url.ToString().EndsWith(".md"))
                {
                    //尚未打开文件
                    if (string.IsNullOrEmpty(Program.mdFileToOpen))
                    {
                        Program.mdFileToOpen = e.Url.LocalPath;
                        adapter.LoadMDFile(Program.mdFileToOpen, webBrowser1);
                        adapter.CacheMDPictures(Program.mdFileToOpen);
                        adapter.SetUserSideMD(webBrowser1);
                        this.Text = captain + "  " + Program.mdFileToOpen;
                    }
                    else
                    {
                        string filename = "";
                        //在cache目录中的视为采用了相对路径
                        if (e.Url.LocalPath.Contains(Application.StartupPath + "\\" + Program.cacheFolder + "\\"))
                        {
                            filename = e.Url.LocalPath.Replace(Application.StartupPath + "\\" + Program.cacheFolder + "\\", "");
                            string path = Path.GetDirectoryName(Program.mdFileToOpen);
                            filename = path + "\\" + filename;
                        }
                        else
                        {
                            filename = e.Url.LocalPath;
                        }

                        Process process = new Process();
                        process.StartInfo.Arguments = filename;
                        process.StartInfo.FileName = Application.ExecutablePath;
                        process.StartInfo.UseShellExecute = true;
                        process.Start();
                    }
                }
                else
                {
                    Process process = new Process();
                    process.StartInfo.FileName = e.Url.ToString();
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                }
            }

        }

        /// <summary>
        /// 打开设置窗体
        /// </summary>
        /// <returns></returns>
        private void configToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetupForm frm_setup = new SetupForm();
            frm_setup.Show();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            //类似于Dock=Fill，但是隐藏右侧的多余滚动条
            webBrowser1.Width = panel2.Width + 24;
            webBrowser1.Height = panel2.Height;
        }


        private void 提交ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpwpForm frm_upwp = new UpwpForm(adapter);
            frm_upwp.Show();
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomFactor = (int)(zoomFactor * 1.1);
            ZoomBrowser(zoomFactor);
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoomFactor = (int)(zoomFactor * 0.9);
            ZoomBrowser(zoomFactor);
        }

        private async void AI查错ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adapter.GetUserSideMD(webBrowser1);
            AICheckListForm frm = new AICheckListForm(adapter);
            frm.Show();


        }

        private void AI文字优化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adapter.GetUserSideMD(webBrowser1);
            AIReplaceListForm frm = new AIReplaceListForm(adapter);
            frm.Show();
        }

        private async void aI续写ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AIContinue(sender,e);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            删除ToolStripMenuItem1_Click(sender, e);
        }

        private void 代理续写ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aI续写ToolStripMenuItem_Click(sender, e);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            复制ToolStripMenuItem_Click(sender, e);
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            粘贴ToolStripMenuItem1_Click(sender, e);
        }

        private void 撤销UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            撤销ToolStripMenuItem_Click(sender, e);
        }

        private void 代理找图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aI搜图ToolStripMenuItem_Click(sender, e);
        }

        private void 文字优化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AI文字优化ToolStripMenuItem_Click(sender, e);
        }

        private void 语法检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AI查错ToolStripMenuItem_Click(sender, e);
        }
        bool AISearchRunning = false;
        private void aI搜图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AISearchPicture(sender, e);
        }

        private void 打开百度搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            浏览器搜索ToolStripMenuItem_Click(sender, e);
        }
        // 1. 全屏透明窗体
        Form overlay;
        private void 桌面截图ToolStripMenuItem_Click(object sender, EventArgs ee)
        {
            string imagesDir = Path.Combine(Application.StartupPath, Program.cacheFolder, "images");
            if (!Directory.Exists(imagesDir))
                Directory.CreateDirectory(imagesDir);

            string fileNameOnly = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            string fullPath = Path.Combine(imagesDir, fileNameOnly);

            if (overlay is null)
            {
                overlay = new Form();
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.WindowState = FormWindowState.Maximized;
                overlay.TopMost = true;
                overlay.BackColor = Color.Black;
                overlay.Opacity = 0.1; // 只是为了让用户看到暗色遮罩
                overlay.KeyPreview = true; // 捕获键盘事件
                overlay.Show();

                Pen pen = new Pen(Color.Red, 4);
                Point start = Point.Empty, end = Point.Empty;

                overlay.MouseDown += (s, e) =>
                {
                    start = e.Location;
                };

                overlay.MouseMove += (s, e) =>
                {
                    end = e.Location;
                    overlay.Invalidate();
                };

                overlay.Paint += (s, e) =>
                {
                    if (start != Point.Empty && end != Point.Empty)
                    {
                        e.Graphics.DrawRectangle(pen, GetRectangle(start, end));
                    }
                };

                overlay.MouseUp += (s, e) =>
                {
                    Rectangle rect = GetRectangle(start, e.Location);

                    // 忽略太小的选区
                    if (rect.Width < 5 || rect.Height < 5)
                    {
                        start = Point.Empty;
                        end = Point.Empty;
                        overlay.Invalidate();
                        return;
                    }

                    try
                    {
                        // **隐藏 overlay，避免截图被遮罩影响**
                        overlay.Visible = false;
                        using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.CopyFromScreen(rect.Location, System.Drawing.Point.Empty, rect.Size);
                            //bmp按ScreenShotFactor比例缩放后保存
                            double factor = (double)SetupForm.cfg.ScreenShotFactor / 100;
                            if (factor != 1.0)
                            {
                                int newWidth = (int)(bmp.Width * factor);
                                int newHeight = (int)(bmp.Height * factor);

                                using (Bitmap scaledBmp = new Bitmap(newWidth, newHeight))
                                using (Graphics g2 = Graphics.FromImage(scaledBmp))
                                {
                                    // 设置高质量缩放
                                    g2.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                    g2.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                    g2.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                                    g2.DrawImage(bmp, new Rectangle(0, 0, newWidth, newHeight));

                                    // 保存缩放后的图像
                                    scaledBmp.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                                }
                            }
                            else
                            {
                                // 不缩放，直接保存
                                bmp.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                            }

                        }


                        overlay.Visible = true;

                        // 插入 Markdown 图片
                        string relativePath = Path.Combine("images", fileNameOnly).Replace("\\", "/");
                        adapter.InsertText(webBrowser1, "![](" + relativePath + ")\n");
                        adapter.GetUserSideMD(webBrowser1);
                    }
                    catch { }

                    overlay.Close();
                    overlay = null;
                };

                overlay.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        overlay.Close();
                        overlay = null;
                    }
                };
            }

            Rectangle GetRectangle(Point p1, Point p2)
            {
                return new Rectangle(
                    Math.Min(p1.X, p2.X),
                    Math.Min(p1.Y, p2.Y),
                    Math.Abs(p1.X - p2.X),
                    Math.Abs(p1.Y - p2.Y)
                );
            }
        }





        private void 屏幕截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            桌面截图ToolStripMenuItem_Click(sender, e);
        }

        private void 浏览器搜索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string keyword = adapter.GetSelectedText(webBrowser1);
            if (string.IsNullOrEmpty(keyword))
            {
                keyword = " ";
            }
            string url = $"https://image.baidu.com/search/index?tn=baiduimage&word={Uri.EscapeDataString(keyword)}";
            //string url = $"https://duckduckgo.com/?q={WebUtility.UrlEncode(keyword)}&t=h_&iar=images&iax=images&ia=images";
            try
            {
                // 打开默认浏览器
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
            }
        }

        private void 根据提纲生成文章ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adapter.GetUserSideMD(webBrowser1);
            AIReplaceListForm frm = new AIReplaceListForm(adapter);
            frm.Show();
        }
        private void AI按要求编辑文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AI自定义编辑ToolStripMenuItem_Click(sender, e);
        }
        private void AI自定义编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AICustomizedModify(sender, e);
        }
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //撤销上一步web的操作
            webBrowser1.Document.InvokeScript("undoEditor");
        }
        private void 粘贴ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData(DataFormats.Bitmap))
            {
                System.Drawing.Image image = (System.Drawing.Image)Clipboard.GetData(DataFormats.Bitmap);
                //设置图片文件保存路径和图片格式，格式可以自定义
                string dir = adapter.MdFilePath + "\\images";
                if (adapter.MdFilePath == "")
                {
                    MessageBox.Show("请先选择保存路径", "提示");
                    return;
                }
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                //保存图片
                string fileRelativePath = new StringBuilder("images/").Append(DateTime.Now.ToString("yyyyMMddHHmmss.")).Append("png").ToString();
                string filePath = new StringBuilder(dir).Append("\\").Append(DateTime.Now.ToString("yyyyMMddHHmmss.")).Append("png").ToString();
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                //缓存图片
                var original_full_img = filePath;
                var cached_full_image = Application.StartupPath + "\\" + Program.cacheFolder + "\\" + fileRelativePath.Replace("images/", "images\\");
                MFiles.CopyFile(original_full_img, cached_full_image);

                //利用Javascript插入图片的链接
                //Clipboard.SetData("Text","![]("+ filePath+")");
                Object[] objArray = new Object[1];
                objArray[0] = (Object)("![](" + fileRelativePath + ")");
                HtmlDocument doc = webBrowser1.Document;
                doc.InvokeScript("insertText", objArray);
                adapter.GetUserSideMD(webBrowser1);
            }
            //把剪贴板的内容粘贴到当前光标位置

            else if (Clipboard.ContainsData(DataFormats.Text))
            {
                if (sender != null)
                {
                    string text = (string)Clipboard.GetData(DataFormats.Text);
                    adapter.InsertText(webBrowser1, text);
                }
            }
        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //删除选中的内容
            adapter.ReplaceSelectedText(adapter.webbrowser, "");
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //把选中的内容复制到剪贴板
            adapter.GetUserSideMD(webBrowser1);
            string sel = adapter.GetSelectedText(webBrowser1);
            Clipboard.SetData("Text", sel);
        }
        public void Info_StatusBar(string info, bool islink = false)
        {
            if (islink)
            {
                toolStripStatusLabel1.IsLink = true;
                toolStripStatusLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            }
            else
            {
                toolStripStatusLabel1.IsLink = false;
            }
            toolStripStatusLabel1.Text = info;
        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel1.IsLink)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.luafans.com",
                    UseShellExecute = true
                });
            }
        }
        [ComVisible(true)]
        public class JsCallback
        {
            Form1 _form;
            ContextMenuStrip _menu;
            public JsCallback(Form1 form, ContextMenuStrip menu) { _form = form; _menu = menu; }
            public void OnRightClick(int x, int y)
            {
                //MessageBox.Show($"右键点击位置: X={x}, Y={y}");
                // 获取当前鼠标屏幕坐标
                Point mousePos = System.Windows.Forms.Control.MousePosition;
                _menu.Show(mousePos);

            }
            public void ReceiveKey(string json)
            {
                _form.OnKeyFromWeb(json);
            }
        }
        // JS 触发的回调函数
        public void OnKeyFromWeb(string json)
        {
            try
            {
                var info = JsonConvert.DeserializeObject<KeyInfo>(json);
                string combo = "";

                if (info.ctrl) combo += "Ctrl+";
                if (info.alt) combo += "Alt+";
                if (info.shift) combo += "Shift+";
                if (info.meta) combo += "Meta+";

                combo += info.key;

                // 输出到控制台或 UI
                Console.WriteLine($"键盘组合: {combo}");
                //if (e.KeyCode == Keys.F5)
                if (combo == "F5")
                {
                    refreshToolStripMenuItem_Click(null, null);
                }

                //if (e.Control && e.KeyCode == Keys.V)
                else if (combo == "Ctrl+v")
                {
                    粘贴ToolStripMenuItem1_Click(null, null);
                }
                //else if (e.Control && e.KeyCode == Keys.Right)
                else if (combo == "Ctrl+Right")
                {
                    AI按要求编辑文字ToolStripMenuItem_Click(null, null);

                }
                //else if (e.Control && e.KeyCode == Keys.Down)
                else if (combo == "Ctrl+Down")
                {
                    aI续写ToolStripMenuItem_Click(null, null);

                }
                //else if (e.Control && e.KeyCode == Keys.Up)
                else if (combo == "Ctrl+Up")
                {
                    AI文字优化ToolStripMenuItem_Click(null, null);


                }
                //else if (e.Control && e.KeyCode == Keys.Left)
                else if (combo == "Ctrl+Left")
                {
                    AI查错ToolStripMenuItem_Click(null, null);
                }
                //else if (e.Control && e.Alt && e.KeyCode == Keys.P)
                else if (combo == "Ctrl+Alt+p")
                {
                    桌面截图ToolStripMenuItem_Click(null, null);
                }
                //else if (e.Control && e.Alt && e.KeyCode == Keys.S)
                else if (combo == "Ctrl+Alt+s")
                {
                    浏览器搜索ToolStripMenuItem_Click(null, null);
                }
                //else if (e.Control && e.Alt && e.KeyCode == Keys.T)
                else if (combo == "Ctrl+Alt+t")
                {
                    代理找图ToolStripMenuItem_Click(null, null);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("JSON 解析失败: " + ex.Message);
            }
        }


        // 键盘事件结构
        public class KeyInfo
        {
            public string key { get; set; }
            public string code { get; set; }
            public bool ctrl { get; set; }
            public bool alt { get; set; }
            public bool shift { get; set; }
            public bool meta { get; set; }
            public long time { get; set; }
        }

        private void 同步本地图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            同步本地图片ToolStripMenuItem1_Click(sender, e);
        }

        private void 关联自动打开md文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 程序路径
            string exePath = Application.ExecutablePath;

            // 关联文件扩展名
            string extension = ".md";

            // 在注册表中的程序标识名称（随意取，保持唯一）
            string progId = "MyMarkdownEditor";

            // 关联到当前可执行程序
            RegisterFileAssociation(extension, progId, exePath, "Markdown 文件");

            MessageBox.Show($".md 文件已成功关联", "文件关联", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        static void RegisterFileAssociation(string extension, string progId, string applicationPath, string description)
        {
            try
            {
                // 1️⃣ 关联扩展名 -> ProgID
                Registry.SetValue($@"HKEY_CLASSES_ROOT\{extension}", "", progId);

                // 2️⃣ ProgID -> 描述
                Registry.SetValue($@"HKEY_CLASSES_ROOT\{progId}", "", description);

                // 3️⃣ 设置图标
                Registry.SetValue($@"HKEY_CLASSES_ROOT\{progId}\DefaultIcon", "", $"\"{applicationPath}\",0");

                // 4️⃣ 设置打开命令
                Registry.SetValue($@"HKEY_CLASSES_ROOT\{progId}\shell\open\command", "", $"\"{applicationPath}\" \"%1\"");

                // 通知系统刷新关联（可选）
                SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册文件关联失败：" + ex.Message);
            }
        }

        // 导入 Windows API 以刷新图标缓存
        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        private void 打印PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("printPreview");
        }

        private void 清理系统缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //把当前目录中所有Cache开头的目录除了当前使用的缓存目录都删除掉

            var dirs = Directory.GetDirectories(Application.StartupPath, "Cache*");
            foreach (var dir in dirs)
            {
                if (dir.EndsWith(Program.cacheFolder)) continue;
                if (Directory.Exists(dir))
                {
                    MFiles.DeleteFolder(dir);
                }

            }
            Info_StatusBar("系统缓存清理完成");
        }

        private void 剪切CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //把选中的内容复制到剪贴板
            adapter.GetUserSideMD(webBrowser1);
            string sel = adapter.GetSelectedText(webBrowser1);
            Clipboard.SetData("Text", sel);
            adapter.ReplaceSelectedText(webBrowser1,"");
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            剪切CToolStripMenuItem_Click(sender, e);
        }

        private void 同步本地图片ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            adapter.GetPictureList();
            //把本地文件缓存起来
            adapter.CacheMDPictures(adapter.Filename);
            //把缓存文件保存到本地图片目录
            adapter.SaveOtherFiletoLocal(adapter.Filename);
            //自动刷新预览区图片，这个功能还不可用，原因是js代码部分功能没实现，现在得手工把文件改下名才能刷新
            webBrowser1.Document.InvokeScript("refreshImages");
            Info_StatusBar("缓存图片已经同步到本地");

        }
    }
}