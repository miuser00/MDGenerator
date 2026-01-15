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
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Net;
using Newtonsoft.Json;

namespace MDLoader
{
    public class MdAdapter
    {
        //图片路径表
        public DataTable dt_UploadPic = new DataTable();
        //从md文件读取到的原始图片列表
        public List<string> PiclistfromMD = new List<string>();
        //md文件内容
        public String Mdcontent = "";
        //md文件路径
        public String Filename = "";
        //md文件绝对路径
        public String MdFilePath = "";
        //上次保存的内容，或者新打开的内容（对于用户新打开的文件，上次保存内容自动重置为新打开的文件内容）
        public String LastSavedMdcontent = "";
        //关联的webbrowser控件
        public WebBrowser webbrowser;



        /// <summary>
        /// 加载markdown文件中描述的图片到本地
        /// 每次加载前会比对前次缓冲的目录，仅当缓冲区图片发生变化时重新加载图片缓存。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>已经缓冲的图片文件名</returns>
        public List<string> CacheMDPictures(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return new List<string>();
            //准备工作
            //清除图片文件列表
            PiclistfromMD.Clear();
            //解析文档，将本地图片文件缓存到目录并生成图片文件列表
            var reg = new Regex(@"!\[.*\]\((.*)\)");
            //用正则表达式提取提取所有图片
            MatchCollection matches = reg.Matches(Mdcontent);
            foreach (Match match in matches)
            {
                
                GroupCollection groups = match.Groups;
                var original_img = groups[1].Value;
                string path = System.IO.Path.GetDirectoryName(fileName);
                //忽略web文件
                if (original_img.Contains("http://") || original_img.Contains("https://"))
                {
                    //文件路径是绝对路径
                }
                else
                {
                    //建立复制的图片列表
                    PiclistfromMD.Add(original_img);
                }
            }
            List<string> file_to_upload = PiclistfromMD.ToList();
            if (file_to_upload.Count > 0)
            {
                if (file_to_upload.Count  == PiclistfromMD.Count)
                {
                    //重新缓存所有文件
                    //删除缓存目录
                    //try
                    //{
                    //    MFiles.DeleteFolder(Application.StartupPath + "\\" + Program.cacheFolder + "");
                    //}
                    //catch { }
                    //复制md文件解析框架文件index.html到缓存目录
                    string editorpath_org = Application.StartupPath + "\\editormd\\" + "index_0.html";
                    string editorpath = Application.StartupPath + "\\"+Program.cacheFolder+"\\" + "index.html";
                    MFiles.CopyFile(editorpath_org, editorpath);
                }
                //复制所有图片
                foreach (string original_img in file_to_upload)
                {
                    string path = System.IO.Path.GetDirectoryName(fileName);
                    //忽略web文件
                    if (original_img.Contains("http://") || original_img.Contains("https://"))
                    {
                        //文件路径是绝对路径
                    }
                    else if (original_img.Contains(":"))
                    {
                        var original_full_img = original_img;
                        string namewithoutpath = System.IO.Path.GetFileName(original_img);
                        var cached_full_image = Application.StartupPath + "\\"+Program.cacheFolder+"\\" + namewithoutpath;
                        MFiles.CopyFile(original_full_img, cached_full_image);
                    }
                    else
                    {
                        var original_full_img = path + "\\" + original_img.Replace("./", "").Replace("/", "\\");
                        var cached_full_image = Application.StartupPath + "\\"+Program.cacheFolder+"\\" + original_img.Replace("./", "").Replace("/", "\\");
                        MFiles.CopyFile(original_full_img, cached_full_image);
                    }
                }

                return file_to_upload;
            }else
            {
                //没有新的图片变化
                return null;
            }
        }

        //加载MD文件到adapter
        public bool LoadMDFile(string fileName,WebBrowser browser)
        {
            try
            {
                Filename = fileName;
                //记录下来当前打开文件的路径，并存为公有变量
                MdFilePath = System.IO.Path.GetDirectoryName(fileName);
                //读取md文件内容
                StreamReader sr = new StreamReader(fileName);
                Mdcontent = sr.ReadToEnd();
                LastSavedMdcontent = Mdcontent;
                sr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 保存文件到指定目录
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool SaveMDFile(string file)
        {
            try
            {
                //读取md文件内容
                StreamWriter sr = new StreamWriter(file);
                sr.Write(Mdcontent);
                LastSavedMdcontent = Mdcontent;
                sr.Close();
                return true;
            }
            catch (Exception ee)
            {
                //MessageBox.Show("保存文件失败，请查看文件是否已经被打开");
                return false;
            }
        }
        public void Clear(WebBrowser browser)
        {
            PiclistfromMD = new List<string>();
            //md文件内容
            Mdcontent = "";
            //md文件路径
            Filename = "";
            //md文件路径
            MdFilePath = "";

            SetUserSideMD(browser);
            //doc.InvokeScript("Redraw");  
        }
        /// <summary>
        /// 从editor.md重新加载md文本到adapter，防止用户编辑没有被及时更新
        /// </summary>
        /// <returns></returns>
        public void GetUserSideMD(WebBrowser browser)
        {
            HtmlDocument doc = browser.Document;
            HtmlElementCollection elementcol = doc.GetElementsByTagName("textarea");  //搜索所有的 textarea 标签
            foreach (HtmlElement ele in elementcol)
            {
                if (ele.GetAttribute("name") == "test-editormd-markdown-doc")
                    Mdcontent = ele.GetAttribute("value");
            }

        }
        /// <summary>
        /// 获取Mdcontent文本中的图片列表
        /// </summary>
        public void GetPictureList()
        {
            GetUserSideMD(webbrowser);
            PiclistfromMD.Clear();
            var reg = new Regex(@"!\[.*\]\((.*)\)");
            //用正则表达式提取提取所有图片
            MatchCollection matches = reg.Matches(Mdcontent);
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                var original_img = groups[1].Value;
                //建立复制的图片列表
                PiclistfromMD.Add(original_img);
            }

        }
        /// <summary>
        /// 把缓存目录中的关联文件（图片、视频等）复制回指定 Markdown 文件所在目录。
        /// 若目标文件已存在则跳过，不覆盖。
        /// </summary>
        /// <param name="mdFilePath">Markdown 文件的完整路径（含文件名），如果为 null 或空，则使用类成员 MdFilePath</param>
        public void SaveOtherFiletoLocal(string mdFilePath = null)
        {
            try
            {
                string targetDir = string.IsNullOrEmpty(mdFilePath) ? MdFilePath : Path.GetDirectoryName(mdFilePath);
                if (string.IsNullOrEmpty(targetDir)) return;

                if (PiclistfromMD == null || PiclistfromMD.Count == 0) return;

                string cacheDir = Path.Combine(Application.StartupPath, Program.cacheFolder);
                if (!Directory.Exists(cacheDir)) return;
                GetPictureList();
                foreach (string original_img in PiclistfromMD)
                {
                    if (string.IsNullOrEmpty(original_img)) continue;

                    // 跳过网络图片
                    if (original_img.StartsWith("http://") || original_img.StartsWith("https://"))
                        continue;

                    // 构造缓存文件路径（保留子目录）
                    string cachedFile = Path.Combine(cacheDir, original_img.Replace("/", "\\"));

                    // 构造目标文件路径（保留子目录）
                    string destFile = Path.Combine(targetDir, original_img.Replace("/", "\\"));

                    if (!File.Exists(cachedFile)) continue;

                    // 确保目标子目录存在
                    string destFolder = Path.GetDirectoryName(destFile);
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }

                    // 复制文件（不覆盖）
                    if (!File.Exists(destFile))
                    {
                        File.Copy(cachedFile, destFile, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveOtherFiletoLocal 异常：" + ex.Message);
            }
        }


        /// <summary>
        /// 把更新过的md内容加载到用户编辑器
        /// </summary>
        /// <returns></returns>
        public void SetUserSideMD(WebBrowser browser)
        {
            HtmlDocument doc = browser.Document;
            HtmlElementCollection elementcol = doc.GetElementsByTagName("textarea");  //搜索所有的 textarea 标签
            foreach (HtmlElement ele in elementcol)
            {
                ele.SetAttribute("value", Mdcontent);
                browser.Refresh();
            }
        }

        public bool GetIfModified()
        {
            if (LastSavedMdcontent == Mdcontent)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 获取md文本中的标题，作为文章标题
        /// </summary>
        /// <returns></returns>
        public string GetTitle()
        {
            // 按行分割
            string[] lines = Mdcontent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // 取第一行作为标题，并去掉开头 # 和空格
            string title = "-";
            int firstContentLineIndex = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    title = line.TrimStart('#', ' ', '\t');
                    firstContentLineIndex = i + 1;
                    break;
                }
            }
            return title;
        }
        /// <summary>
        /// 设置md文本中的标题
        /// </summary>
        /// <param name="newTitle"></param>
        public void SetTitle(string newTitle)
        {
            // 按行分割
            string[] lines = Mdcontent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            // 取第一行作为标题，并去掉开头 # 和空格
            string title = "-";
            int firstContentLineIndex = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (!string.IsNullOrEmpty(line))
                {
                    title = line.TrimStart('#', ' ', '\t');
                    firstContentLineIndex = i;
                    break;
                }
            }
            if (firstContentLineIndex < lines.Length)
            {
                lines[firstContentLineIndex] = "# " + newTitle;
                Mdcontent = string.Join("\r\n", lines);
            }
        }
        /// <summary>
        /// 调用网页中已存在的 replaceFirstWord(oldText, newText) 函数
        /// </summary>
        /// <param name="browser">WebBrowser 控件实例</param>
        /// <param name="oldText">要替换的旧文字</param>
        /// <param name="newText">要替换的新文字</param>
        public void ReplaceFirstWord(WebBrowser browser, string oldText, string newText)
        {
            if (browser.Document == null)
            {
                MessageBox.Show("WebBrowser 尚未加载完成。");
                return;
            }

            // 构造安全的 JavaScript 调用语句
            string jsCall = $"replaceFirstWord({EscapeJsString(oldText)}, {EscapeJsString(newText)});";

            // 调用网页中的函数
            browser.Document.InvokeScript("eval", new object[] { jsCall });
        }
        /// <summary>
        /// 从 editor.md 获取当前选中的文本
        /// </summary>
        /// <param name="browser">WebBrowser 控件实例</param>
        /// <returns>选中的字符串（若无选中则返回空字符串）</returns>
        public string GetSelectedText(WebBrowser browser)
        {
            if (browser.Document == null)
            {
                MessageBox.Show("WebBrowser 尚未加载完成。");
                return string.Empty;
            }

            try
            {
                // 直接调用网页中的函数
                object result = browser.Document.InvokeScript("getSelectedText");
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取选中文本失败: " + ex.Message);
                return string.Empty;
            }
        }
        /// <summary>
        /// 从 editor.md 获取当前选中位置前的文本
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public string GetTextBeforeSelection(WebBrowser browser)
        {
            if (browser.Document == null)
            {
                MessageBox.Show("WebBrowser 尚未加载完成。");
                return string.Empty;
            }

            try
            {
                // 直接调用网页中的函数
                object result = browser.Document.InvokeScript("getTextBeforeSelection");
                return result?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取选中文本失败: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 调用网页中已有的 replaceSelectedText(newText) 函数
        /// </summary>
        /// <param name="browser">WebBrowser 控件实例</param>
        /// <param name="newText">要替换的新文字</param>
        public void ReplaceSelectedText(WebBrowser browser, string newText)
        {
            if (browser.Document == null)
            {
                MessageBox.Show("WebBrowser 尚未加载完成。");
                return;
            }

            // 构造调用语句
            string jsCall = $"replaceSelectedText({EscapeJsString(newText)});";

            // 调用网页函数
            browser.Document.InvokeScript("eval", new object[] { jsCall });
        }

        public void InsertText(WebBrowser browser, string newText)
        {
            if (browser.Document == null)
            {
                MessageBox.Show("WebBrowser 尚未加载完成。");
                return;
            }

            // 构造调用语句
            string jsCall = $"insertText({EscapeJsString(newText)});";

            // 调用网页函数
            browser.Document.InvokeScript("eval", new object[] { jsCall });
        }


        /// <summary>
        /// 转义字符串，避免 JS 注入错误
        /// </summary>
        private string EscapeJsString(string text)
        {
            if (text == null) return "''";
            text = text.Replace("\\", "\\\\")
                       .Replace("'", "\\'")
                       .Replace("\"", "\\\"")
                       .Replace("\r", "\\r")
                       .Replace("\n", "\\n");
            return $"'{text}'";
        }


    }
}
