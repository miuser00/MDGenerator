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
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class BaiduBrowser : Form
    {
        public BaiduBrowser()
        {
            InitializeComponent();
        }

        private void BaiduBrowser_Load(object sender, EventArgs e)
        {
            webBrowser1.ScrollBarsEnabled = true;
            webBrowser1.Dock = DockStyle.Fill;
            //webBrowser1.ObjectForScripting = this;//允许使用ObjectForScripting
            webBrowser1.ScriptErrorsSuppressed = true; //错误脚本提示  
            webBrowser1.IsWebBrowserContextMenuEnabled = true; // 右键菜单  
            webBrowser1.WebBrowserShortcutsEnabled = true; //快捷键  
            //webBrowser1.DocumentCompleted += WebBrowser_DocumentCompleted;
            //webBrowser1.Navigate("http://www.baidu.com");
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Document != null)
            {
                // 注入 JavaScript 拦截图片点击
                string script = @"
                    var imgs = document.getElementsByTagName('img');
                    for (var i = 0; i < imgs.length; i++) {
                        imgs[i].onclick = function(e) {
                            e.preventDefault();
                            window.external.OnImageClick(this.src);
                        }
                    }
                ";

                // 注册对象供 JS 调用
                webBrowser1.ObjectForScripting = new ScriptManager(this);

                webBrowser1.Document.InvokeScript("execScript", new object[] { script, "JavaScript" });
            }

        }
        // 处理点击事件的回调
        public void OnImageClick(string url)
        {
            MessageBox.Show("图片链接: " + url);
        }

        [System.Runtime.InteropServices.ComVisible(true)]
        public class ScriptManager
        {
            private BaiduBrowser _form;
            public ScriptManager(BaiduBrowser form)
            {
                _form = form;
            }

            public void OnImageClick(string url)
            {
                _form.OnImageClick(url);
            }
        }
    }
}
