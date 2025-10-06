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
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
namespace MDLoader
{
    [System.Runtime.InteropServices.ComVisible(true)]





    public partial class Form1 : Form
    {   
        //设置窗体
        SetupForm frm_setup = new SetupForm();
        //editor.md的C#适配层
        Adapter adapter = new Adapter();
        //标题栏内容
        String captain = "";
        HtmlElement editor;
        //图片资源的呈现方式，本地或者远程url
        public enum Picture_mode { local, remote };
        Picture_mode view=Picture_mode.local;
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

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //隐去底部的面板
            panel_upload.Visible = false;
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
            webBrowser1.Navigate(editorpath);
        }
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
            int zoomFactor =(int) GetScalePercent();

            // 100 表示 100%，200 表示 200%
            object pvaIn = (int)((zoomFactor-100)*SetupForm.cfg.MyZoomFactory/100)+100;
            object pvaOut = null;
            webBrowser1.ActiveXInstance.GetType().InvokeMember(
                "ExecWB",
                System.Reflection.BindingFlags.InvokeMethod,
                null,
                webBrowser1.ActiveXInstance,
                new object[] { 63, 1, pvaIn, pvaOut }   // 63 = OLECMDID_OPTICAL_ZOOM
            );

            Application.DoEvents();

            Thread.Sleep(300);

            ReDrawEditor();
        }
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
                panel_upload.Visible = false;
                uploadpannelToolStripMenuItem.Checked = false;
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
            adapter.SaveFile(adapter.Filename);
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
            
            MessageBox.Show(captain+"\n"+
                "Author Contact:64034373@qq.com"+ "\nBased on opensorce project editor.md \n Visit https://gitee.com/miuser00/md-fileloader for more info\n ", "About");
        }


        /// <summary>
        /// 注意！这个函数是从webbrowser的js中回调的，当用户输入文本发生变化时触发
        /// 当editor.md文本发生变化时更新adapter中markdown文本，如果有新图片插入，重新缓冲图片
        /// </summary>
        /// <returns></returns>
        public void OnContentChange()
        {
            adapter.GetUserSideMD(webBrowser1);
            if (adapter.GetIfModified())
            {
                if (!this.Text.EndsWith(" *")) this.Text = this.Text + " *";
            }
            else
            {
                if (this.Text.EndsWith(" *")) this.Text = this.Text.Substring(0, this.Text.Length - 2);
            }
            if (adapter.Filename == "") return;
            try
            {
                List<string> updatedlist = adapter.CacheMDPictures(adapter.Filename);
                HtmlDocument doc = webBrowser1.Document;
                HtmlElementCollection elementcol = doc.GetElementsByTagName("img");  //搜索所有的 img 标签
                //System.Threading.Thread.Sleep(100);
                foreach (HtmlElement ele in elementcol)
                {
                    Regex reg = new Regex(@"src=""(.*)\?(.*)"">");
                    Match math = reg.Match(ele.OuterHtml);
                    String filename = math.Groups[1].Value;
                    if (updatedlist != null && updatedlist.Contains(filename))
                    {
                        ele.OuterHtml = ele.OuterHtml.Replace("t=","id=");
                    }
                }
            }
            catch (Exception ee)
            { 
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //删除缓存目录
            MFiles.DeleteFolder(Application.StartupPath + "\\" + Program.cacheFolder );
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uploadpannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel_upload.Visible == true)
            {
                uploadpannelToolStripMenuItem.Checked = false;
                panel_upload.Visible = false;
                ReDrawEditor();
            }
            else
            {
                uploadpannelToolStripMenuItem.Checked = true;
                panel_upload.Visible = true;
                ReDrawEditor();
            }
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
                            adapter.SaveFile(adapter.Filename);
                            this.Text = captain + "  " + adapter.Filename;
                        }
                        else
                        {
                            e.Cancel = true;
                        }

                    }
                    else
                    {
                        adapter.SaveFile(adapter.Filename);
                    }
                }else if (result == DialogResult.Cancel)
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
            if (e.KeyCode == Keys.F5)
            {
                refreshToolStripMenuItem_Click(sender, e);
            }
            //检测到粘贴键
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsData(DataFormats.Bitmap))
                {
                    Image image = (Image)Clipboard.GetData(DataFormats.Bitmap);
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
                    //增加到上传文件的列表
                    adapter.PiclistfromMD.Add(fileRelativePath);

                    //利用Javascript插入图片的链接
                    //Clipboard.SetData("Text","![]("+ filePath+")");
                    Object[] objArray = new Object[1];
                    objArray[0] = (Object)("![](" + fileRelativePath + ")");
                    HtmlDocument doc = webBrowser1.Document;
                    doc.InvokeScript("insertText", objArray);
                }
            }
        }
        /// <summary>
        /// 当当前页面发生变化时触发这个函数，出发的原因可能是点击了超链，或者是Drop事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {

            //如果加载的是默认框架，则放行正常打开，打开其余页面都禁止掉
            if (e.Url.LocalPath == Application.StartupPath + "\\"+Program.cacheFolder+"\\" + "index.html")
            {
                return;
            }else
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
                        process.StartInfo.Arguments =filename;
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
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adapter.PiclistfromMD.Count > 0)
            {
                panel_upload.Visible = true;
                uploadpannelToolStripMenuItem.Checked = true;
                //上传md文件中的资源图片，并生成上传列表
                adapter.FTPUpload(ref dataGridView1);

            }
        }
        /// <summary>
        /// 图片标签本地与远程切换
        /// </summary>
        /// <returns></returns>
        private void remoteViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (view == Picture_mode.local)
            {
                view = Picture_mode.remote;
                adapter.SwitchPicture(webBrowser1, Adapter.Picture_mode.remote);
            }
            else
            {
                view = Picture_mode.local;
                adapter.SwitchPicture(webBrowser1, Adapter.Picture_mode.local);
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
            webBrowser1.Width = panel2.Width+24;
            webBrowser1.Height = panel2.Height;
        }

        private void remoteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}