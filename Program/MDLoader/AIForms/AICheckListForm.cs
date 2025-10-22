using Newtonsoft.Json;
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
    public partial class AICheckListForm : Form
    {
        MdAdapter ad;
        string originalText = "";  
        string jsonData = @"[
    {""orginal"":""Markdown 编写的文档后缀为 .md，可以用记事本和任何纯文本编辑器打开，基本语法在3分钟内可以学会。"",""corrected"":""Markdown 编写的文档后缀为 .md，可以用记事本和任何纯文本编辑器打开，基本语法在 3 分钟内可以学会。"",""reason"":""数字前应有空格""},
    {""orginal"":""使用 # 号可表示 1-6 级标题，一级标题对应一个 # 号，二级标题对应两个 # 号，以此类推。"",""corrected"":""使用 # 号可表示 1-6 级标题，一级标题对应一个 # 号，二级标题对应两个 # 号，以此类推。"",""reason"":""数字前应有空格""}]";


        public AICheckListForm(MdAdapter adapter)
        {
            InitializeComponent();
            string sel = null;
            sel = adapter.GetSelectedText(adapter.webbrowser);
            if (string.IsNullOrEmpty(sel)) sel = adapter.Mdcontent;
            originalText = sel;
            ad = adapter;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AICheckList_Load(object sender, EventArgs e)
        {


        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            int scrollbarWidth = flowLayoutPanel1.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0;
            int availableWidth = flowLayoutPanel1.ClientSize.Width - flowLayoutPanel1.Padding.Horizontal;

            foreach (System.Windows.Forms.Control c in flowLayoutPanel1.Controls)
            {
                c.Width = availableWidth - 10;
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chb_checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_checkAll.Checked)
            {
                foreach (System.Windows.Forms.Control c in flowLayoutPanel1.Controls)
                {
                    if (c is ItemCheckPanel)
                    ((ItemCheckPanel)c).chb_check.Checked = true;
                }
            }
            else
            {
                foreach (System.Windows.Forms.Control c in flowLayoutPanel1.Controls)
                {
                    if (c is ItemCheckPanel)
                        ((ItemCheckPanel)c).chb_check.Checked = false;
                }
            }
        }
        public class JsonItem
        {
            public string orginal { get; set; }
            public string corrected { get; set; }
            public string reason { get; set; }
        }
        public async Task CheckDoc(string text)
        {
            //try
            //{
            //    jsonData = "";
            //    ChatGLMClient agent = new ChatGLMClient(SetupForm.cfg.AgentAddress, SetupForm.cfg.AgentAPIKey);
            //    string prompt = @"
            //你是一名校对助手。
            //请分析下以下markdown文档有哪些潜在的用词用字错误，并返回如下格式的json文档
            //不要添加任何解释或说明，只输出纯 JSON。每发现一个错误，就输出一行，下个新建一行输出，
            //返回格式如下：
            //{""orginal"": ""原句"",""corrected"": ""修改后的句子""""reason"":""修正的理由""}
            //特别注意，无论是原始文字还是修改后的文字如果有特殊字符要响应的进行转义处理，但不用提供任何json注释。
            //";
            //    int reportedCorrectionCount = 0;
            //    int nochangeCount = 0;
            //    agent.SetSystemPrompt(prompt);

            //    flowLayoutPanel1.SuspendLayout();
            //    flowLayoutPanel1.Controls.Clear();
            //    await agent.ChatAsync(text, delta =>
            //    {
            //        jsonData += delta;
            //        this.Invoke(new Action(() =>
            //        {
            //            lab_info.Text = $"AI正在校对数据，数据分析接收: {jsonData.Length}字节 ";
            //            // 按换行分割，每一行都是一个独立的 JSON
            //            List<string> lines =  jsonData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //            for (int i = 0; i < lines.Count; i++)
            //            {
            //                var line = lines[i];
            //                // 测试该行是否为完整 JSON
            //                try
            //                {
            //                    var item = JsonConvert.DeserializeObject<JsonItem>(line);
            //                }catch
            //                {
            //                    lines.Remove(line);
            //                }
            //            }
            //                int currentLineCount = 0;
            //            foreach (var line in lines)
            //            {

            //                try
            //                {
            //                    var item = JsonConvert.DeserializeObject<JsonItem>(line);
            //                    if (item != null)
            //                    {
            //                        currentLineCount++;
            //                    }
            //                }
            //                catch
            //                {
            //                    // 如果某行不是完整 JSON，可以先忽略
            //                }
            //            }
            //            if (currentLineCount <= reportedCorrectionCount)
            //            {
            //                return; // 没有新增有效行，直接返回
            //            }


            //            for (int i = reportedCorrectionCount; i < currentLineCount; i++)
            //            {
            //                var line = lines[i];

            //                try
            //                {
            //                    var item = JsonConvert.DeserializeObject<JsonItem>(line);
            //                    if (item != null)
            //                    {
            //                        ItemCheckPanel checkitem = new ItemCheckPanel();
            //                        checkitem.SetText(item.orginal, item.corrected, item.reason);
            //                        checkitem.HighlightWord(item.orginal, Color.DarkRed);
            //                        checkitem.HighlightWord(item.corrected, Color.DarkGreen);
            //                        checkitem.HighlightWord(item.reason, Color.Blue);
            //                        flowLayoutPanel1.Controls.Add(checkitem);
            //                        if (item.orginal == item.corrected)
            //                        {
            //                            checkitem.Visible = false;
            //                            nochangeCount++;
            //                        }
            //                    }
            //                }
            //                catch
            //                {
            //                    // 如果某行不是完整 JSON，可以先忽略
            //                }
            //            }
            //            reportedCorrectionCount = currentLineCount;

            //            flowLayoutPanel1.ResumeLayout();
            //            flowLayoutPanel1_SizeChanged(null, null);

            //        }));

            //        System.Windows.Forms.Application.DoEvents();
            //    });
            //    System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
            //    panel.Height = 20;
            //    panel.Width = flowLayoutPanel1.ClientSize.Width - 20;
            //    flowLayoutPanel1.Controls.Add(panel);
            //    lab_info.Text = $"AI校对完成，共发现 {reportedCorrectionCount-nochangeCount} 处问题。请选择要修改的项目，然后点击应用按钮";
            //}
            //catch (Exception ee)
            //{
            //    lab_info.Text = $"AI校对出错: {ee.Message}";
            //}
        }

        async private void AICheckListForm_Shown(object sender, EventArgs e)
        {
            await CheckDoc(originalText);
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control c in flowLayoutPanel1.Controls)
            {
                if (c is ItemCheckPanel)
                   if (((ItemCheckPanel)c).chb_check.Checked == true)
                    {
                        string originalText = ((ItemCheckPanel)c).OriginalText;
                        string correctedText = ((ItemCheckPanel)c).CorrectedText;
                        ad.Mdcontent = ad.Mdcontent.Replace(originalText, correctedText);
                        ad.SetUserSideMD(ad.webbrowser);
                        ad.webbrowser.Refresh();
                        this.Close();
                    }

            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
