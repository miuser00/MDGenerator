///<summary>
         ///模块编号：20180730
         ///模块名：本地配置模块
         ///作用：生成本地的存储信息
         ///作者：Miuser
         ///编写日期：20180730
         ///版本：1.0
///</summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Common reference
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Reflection;

namespace MDLoader
{

    public partial class SetupForm : Form
    {
        public static Config cfg;
        /// <summary>
        /// 从config.xml中加载配置到Config对象
        /// </summary>
        public SetupForm()
        {
            cfg=Config.LoadfromFile(Application.StartupPath+"\\config.xml");
            if (cfg == null)
            {
                cfg = new Config();
                cfg.ServerAddress = "www.miuser.net";
                cfg.MyZoomFactor = 200;
                cfg.FontSize = 9;
                cfg.TimeOut = 30;
                cfg.UserName = "miuser";   
                cfg.AppPassword = "";
                //cfg.AgentAPIKey = "";
                //cfg.AgentAddress = "https://api.openai.com/v1/chat/completions";
            }
            InitializeComponent();
        }

        private void SetupForm_Load(object sender, EventArgs e)
        {
            prg_config.SelectedObject = cfg;
            MoveSplitterTo(prg_config, 160);
            prg_config.HelpVisible = true; // 确保说明区显示
            AdjustPropertyGridDescriptionHeight(prg_config, 100); // 设置为 100 像素高
            SetupForm_Resize(null, null);

        }
        /// <summary>
        /// 调整栏目宽度，由于这个方法本身的propertygrid没有公有方法支持，所以需要用反射技术调用私有方法实现
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="x"></param>
        public void MoveSplitterTo(PropertyGrid grid, int x)
        {
            // HEALTH WARNING: reflection can be brittle...
            FieldInfo field = typeof(PropertyGrid)
                .GetField("gridView",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            field.FieldType
                .GetMethod("MoveSplitterTo",
                    BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(field.GetValue(grid), new object[] { x });
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            cfg.SavetoFile(Application.StartupPath + "\\config.xml");
            Application.Restart();

        }

        private void SetupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Reload parameter form in case cancel the cange
           cfg=Config.LoadfromFile(Application.StartupPath + "\\config.xml");

        }

        private void SetupForm_Resize(object sender, EventArgs e)
        {
            const int margin = 10;
            btn_save.Left = this.ClientSize.Width - btn_save.Width - margin;
            btn_save.Top = this.ClientSize.Height - btn_save.Height - margin;
        }

        private void prg_config_Click(object sender, EventArgs e)
        {

        }

        private void SetupForm_ResizeEnd(object sender, EventArgs e)
        {

        }
        private void AdjustPropertyGridDescriptionHeight(PropertyGrid grid, int newHeight)
        {
            // 通过反射获取私有字段 doccomment
            var docComment = grid.GetType().GetField("doccomment",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.GetValue(grid);

            if (docComment != null)
            {
                // 设置说明区的高度
                var userSizedField = docComment.GetType().GetField("userSized",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                userSizedField?.SetValue(docComment, true); // 允许手动设置高度

                // 调整高度
                ((Control)docComment).Height = newHeight;
            }
        }

    }
    public class Config
    {

        [CategoryAttribute("1.WP Config")]
        [DescriptionAttribute("您的Wordpress服务器的地址，比如 https://yourwordpresssite.com")]
        public string ServerAddress { get; set; }
        [CategoryAttribute("1.WP Config")]
        [DescriptionAttribute("登录您的Wordpress服务器用户名，比如 miuser")]
        public string UserName { get; set; }


        [CategoryAttribute("1.WP Config")]
        [DescriptionAttribute("对应用户名的Wordpress应用密码，比如 *****")]
        public string AppPassword { get; set; }

        [CategoryAttribute("1.WP Config")]
        [DescriptionAttribute("服务端超时设置，单位为秒")]
        public int TimeOut { get; set; }

        [CategoryAttribute("2.View Setup")]
        [DescriptionAttribute("显示比例，比如设置为100，或者150,200")]
        public int MyZoomFactor { get; set; }

        [CategoryAttribute("2.View Setup")]
        [DescriptionAttribute("菜单字体大小")]
        public int FontSize { get; set; }

        //[CategoryAttribute("3.Agent Setup")]
        //[DescriptionAttribute("AI Agent的 APIkey，Agent需要兼容OpenAI REST 格式")]
        //public string AgentAPIKey { get; set; }

        //[CategoryAttribute("3.Agent Setup")]
        //[DescriptionAttribute("AI Agent的 APIkey，Agent需要兼容OpenAI REST 格式")]
        //public string AgentAddress { get; set; }


        public int SavetoFile(String filename)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, this);
                writer.Close();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.StackTrace, ee.Message);
                return 0;
            }

            return 1;
        }
        public static Config LoadfromFile(String filename)
        {
            try
            {
                Config sptr;
                XmlSerializer serializer = new XmlSerializer(typeof(Config));
                TextReader reader = new StreamReader(filename);
                sptr = (Config)(serializer.Deserialize(reader));
                reader.Close();
                return sptr;

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.StackTrace, ee.Message);
                return null;
            }

        }

    }

}
