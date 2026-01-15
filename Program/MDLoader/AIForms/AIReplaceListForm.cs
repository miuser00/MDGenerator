using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace MDLoader
{
    public partial class AIReplaceListForm : Form
    {
        MdAdapter ad;
        ChatGLMClient agent;
        string articleText = "";
        string originalText = "";
        string jsonData = @"";

        public AIReplaceListForm(MdAdapter adapter)
        {
            InitializeComponent();
            string sel = null;
            sel = adapter.GetSelectedText(adapter.webbrowser);
            //if (string.IsNullOrEmpty(sel)) sel = adapter.Mdcontent;
            articleText = adapter.Mdcontent;
            originalText = sel;
            ad = adapter;

            // 获取当前鼠标位置（屏幕坐标）
            Point mousePos = Cursor.Position;
            // 设置窗体位置（左上角在鼠标位置）
            this.StartPosition = FormStartPosition.Manual;
            this.Location = mousePos;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AIReplaceListForm_Load(object sender, EventArgs e)
        {


        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        // 常量定义
        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;
        private const int WM_SETREDRAW = 0x0B;

        public class JsonItem
        {
            public string optimized { get; set; }
        }
        public async Task CheckDoc(string atricle,string paragram)
        {
            Form1.ThisForm.Info_StatusBar("正在连接到AI...");
            int count = 0;
            if (agent!=null) agent.Stop();
            System.Windows.Forms.Application.DoEvents();
            agent = new ChatGLMClient(SetupForm.cfg.AgentAddress, SetupForm.cfg.AgentAPIKey,useContext:false);
            richTextBox1.Text = "";
            string text = "";
            if (!string.IsNullOrEmpty(paragram))
            {
                text = "原文如下:\n\n" + atricle + "\n\n请对上述文档中的以下段落\n\n" + paragram + "\n\n按照" + cmb_style.Text+"的风格进行优化使其文字更加优美流畅，注意保留原意。仅优化提供的段落部分";
            }else
            {
                text = "原文如下:\n\n" + atricle + "\n\n请对上述文档按照"+cmb_style.Text+"的风格进行优化，注意保留原意。";

            }
            try
                {

                    string prompt = @"
            你是一名文笔优雅的Markdown文档编写高手。请阅读以下文字，并按要求进行相应的优化。
            ";
                    agent.SetSystemPrompt(prompt);

                    await agent.ChatAsync(text, delta =>
                    {

                        this.Invoke(new Action(() =>
                        {
                            count = count + delta.Length;
                            lab_info.Text = $"AI正在进行文档优化，数据接收: {count}字节 ";
                            Form1.ThisForm.Info_StatusBar(lab_info.Text);
                            SendMessage(richTextBox1.Handle, WM_SETREDRAW, 0, 0);
                            // 追加文本
                            richTextBox1.AppendText(delta);
                            // 滚动到最底部（最平滑方式）
                            SendMessage(richTextBox1.Handle, WM_VSCROLL, SB_BOTTOM, 0);

                            // 恢复重绘并刷新
                            SendMessage(richTextBox1.Handle, WM_SETREDRAW, 1, 0);
                            richTextBox1.Invalidate();

                        }));

                        System.Windows.Forms.Application.DoEvents();
                    },0.3);
                    System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
                    panel.Height = 20;

                    lab_info.Text = $"AI优化完成。";
                    Form1.ThisForm.Info_StatusBar("完成");
            }
                catch (Exception ee)
                {
                    lab_info.Text = $"AI优化出错: {ee.Message}";
                    Form1.ThisForm.Info_StatusBar(lab_info.Text);
            }
        }

        async private void AIReplaceListForm_Shown(object sender, EventArgs e)
        {
            await CheckDoc(articleText, originalText);
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            ad.ReplaceSelectedText(ad.webbrowser, richTextBox1.Text);
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            agent.Stop();
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            await CheckDoc(articleText, originalText);
        }
    }
}
