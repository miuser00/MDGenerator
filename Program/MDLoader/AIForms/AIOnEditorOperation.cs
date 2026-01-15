using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MDLoader.ChatGLMClient;

using System.IO;
using System.Text.RegularExpressions;

namespace MDLoader
{
    public partial class Form1 : Form
    {
        private async void AIContinue(object sender, EventArgs e)
        {
            Form1.ThisForm.Info_StatusBar("正在连接到AI...");
            int count = 0;
            ChatGLMClient agent;
            System.Windows.Forms.Application.DoEvents();
            agent = new ChatGLMClient(SetupForm.cfg.AgentAddress, SetupForm.cfg.AgentAPIKey, useContext: false);
            string atricle = adapter.GetTextBeforeSelection(webBrowser1);
            string paragram = adapter.GetSelectedText(webBrowser1);

            string text = "";
            if (!string.IsNullOrEmpty(paragram))
            {
                text = "待续写的文字如下:\n\n" + atricle + "\n\n请根据如下用户提示信息\n\n" + paragram + "\n\n" + "请紧接着最后一个字对以上文字进行续写，注意要保持与上面文字的衔接，但不能重复，要使语句完全通顺。只写完一小段即可，字数不要太多。";
            }
            else
            {
                text = "待续写的文字如下:\n\n" + atricle + "\n\n" + "请紧接着最后一个字对以上文字进行续写，注意要保持与上面文字的衔接，但不能重复，要使语句完全通顺。只写完一小段即可，字数不要太多。";

            }
            try
            {

                string prompt = @"
            你是一名文笔优雅的Markdown文档编写高手。请阅读以下文字，并按要求续写。
            ";
                agent.SetSystemPrompt(prompt);

                await agent.ChatAsync(text, delta =>
                {
                    this.Invoke(new Action(() =>
                    {
                        count = count + delta.Length;
                        adapter.InsertText(webBrowser1, delta);
                        Form1.ThisForm.Info_StatusBar($"AI正在进行续写，数据接收: {count}字节 ");
                    }));

                    System.Windows.Forms.Application.DoEvents();
                }, 0.3);
                System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
                panel.Height = 20;
                Form1.ThisForm.Info_StatusBar("完成");
            }
            catch (Exception ee)
            {
                //lab_info.Text = $"AI优化出错: {ee.Message}";
                Form1.ThisForm.Info_StatusBar($"AI续写出错: {ee.Message}");
            }
        }

        private async void AICustomizedModify(object sender, EventArgs e)
        {
            Form1.ThisForm.Info_StatusBar("正在连接到AI...");
            string request = InputTextForm.ShowDialog("提示", "请输入prompt：", "您对文章的编辑要求");
            if (!string.IsNullOrEmpty(request))
            {
                int count = 0;
                ChatGLMClient agent;
                System.Windows.Forms.Application.DoEvents();
                agent = new ChatGLMClient(SetupForm.cfg.AgentAddress, SetupForm.cfg.AgentAPIKey, useContext: false);
                string atricle = adapter.GetTextBeforeSelection(webBrowser1);
                string paragram = adapter.GetSelectedText(webBrowser1);

                string text = "";
                if (!string.IsNullOrEmpty(paragram))
                {
                    text = "待编辑的文字如下:\n\n" + paragram + "\n\n请根据如下用户提示信息进行修改\n\n" + request;
                }
                else
                {
                    text = "待编辑的文字如下:\n\n" + atricle + "\n\n请根据如下用户提示信息进行修改\n\n" + request;
                }
                try
                {

                    string prompt = @"你是一名文笔优雅的Markdown文档编写高手。请按要求编辑下面的文字。";
                    agent.SetSystemPrompt(prompt);

                    await agent.ChatAsync(text, delta =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            count = count + delta.Length;
                            adapter.InsertText(webBrowser1, delta);
                            Form1.ThisForm.Info_StatusBar($"AI正在根据您的要求进行文档修改，数据接收: {count}字节 ");
                        }));

                        System.Windows.Forms.Application.DoEvents();
                    }, 0.3);
                    System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
                    panel.Height = 20;
                    //lab_info.Text = $"AI优化完成。";
                    Form1.ThisForm.Info_StatusBar("完成");
                }
                catch (Exception ee)
                {
                    //lab_info.Text = $"AI优化出错: {ee.Message}";
                    Form1.ThisForm.Info_StatusBar($"AI文档修改出错: {ee.Message}");
                }
            }
        }
        private async void AISearchPicture(object sender, EventArgs e)
        {
            if (!AISearchRunning)
            {
                Form1.ThisForm.Info_StatusBar("正在连接到AI...");
                AISearchRunning = true;
                string keyword = "";
                int count = 0;
                ChatGLMClient agent;
                System.Windows.Forms.Application.DoEvents();
                agent = new ChatGLMClient(SetupForm.cfg.AgentAddress, SetupForm.cfg.AgentAPIKey, useContext: false);
                string paragram = adapter.GetSelectedText(webBrowser1);

                string text = "";
                if (!string.IsNullOrEmpty(paragram))
                {
                    text = "请根据以下内容，给出几个简短的英文关键词以空格为间隔" + paragram;
                }
                else
                {
                    MessageBox.Show("请先选择图片上下文");
                    return;
                }
                try
                {

                    string prompt = @"你是一名搜索助手。";

                    agent.SetSystemPrompt(prompt);

                    await agent.ChatAsync(text, delta =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            count = count + delta.Length;
                            keyword = keyword + delta;
                            Form1.ThisForm.Info_StatusBar($"AI正在接收搜图关键字，数据接收: {count}字节 ");
                        }));

                        System.Windows.Forms.Application.DoEvents();
                    }, 0.3);
                    System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
                    panel.Height = 20;

                    //lab_info.Text = $"AI优化完成。";
                    Form1.ThisForm.Info_StatusBar($"接收完成，关键字为 {keyword}");
                }
                catch (Exception ee)
                {
                    //lab_info.Text = $"AI优化出错: {ee.Message}";
                    Form1.ThisForm.Info_StatusBar($"AI接收搜图关键字: {ee.Message}");
                }

                keyword = Regex.Replace(keyword, @"[^a-zA-Z0-9 ]", "");
                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("请先选择关键词文本。如果搜不到请用英文单词试试", "提示");
                    return;
                }
                string[] result = UnsplashSearch.SearchUnsplashImages(keyword);
                Form1.ThisForm.Info_StatusBar("正在连接图片搜索服务器...");
                HttpClient client = new HttpClient();
                foreach (string url in result)
                {
                    try
                    {
                        byte[] data = await client.GetByteArrayAsync(url);

                        // 生成本地文件名
                        string fileNameOnly = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";

                        // 图片保存目录，例如 cacheFolder\images
                        string imagesDir = Path.Combine(Application.StartupPath, Program.cacheFolder, "images");

                        // 确保目录存在
                        if (!Directory.Exists(imagesDir))
                            Directory.CreateDirectory(imagesDir);

                        // 完整保存路径
                        string fullPath = Path.Combine(imagesDir, fileNameOnly);

                        // 同步写入文件（在 Task.Run 里异步执行）
                        await Task.Run(() =>
                        {
                            File.WriteAllBytes(fullPath, data);
                        });

                        // 插入到编辑器，使用相对路径
                        string relativePath = Path.Combine("images", fileNameOnly).Replace("\\", "/"); // editor.md 里用 / 
                        adapter.InsertText(webBrowser1, "\n");
                        adapter.InsertText(webBrowser1, "![](" + relativePath + ")\n");
                        adapter.InsertText(webBrowser1, paragram + "\n");

                        Form1.ThisForm.Info_StatusBar("完成");
                        return;
                    }
                    catch
                    {
                        // 下载失败跳过
                    }
                }
            }
            AISearchRunning = false;
        }
    }
}
