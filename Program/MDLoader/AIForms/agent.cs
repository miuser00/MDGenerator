using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MDLoader
{
    public class ChatGLMClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://open.bigmodel.cn/api/paas/v4/chat/completions";
        private readonly string _model;

        // 聊天上下文（仅当 UseContext = true 时使用）
        private readonly List<object> _messages = new List<object>();

        /// <summary>
        /// 是否启用上下文（多轮对话）
        /// </summary>
        public bool UseContext { get; set; }
        private bool UserBreak { get; set; } = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChatGLMClient(string apiUrl,string apiKey, string model = "glm-4-flash", bool useContext = true)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
            _model = model;
            UseContext = useContext;
            if (!string.IsNullOrEmpty(apiUrl)) _apiUrl = apiUrl;
        }
        public void Stop()
        {
            UserBreak = true;
        }
        /// <summary>
        /// 设置系统提示（定义AI角色）
        /// </summary>
        public void SetSystemPrompt(string content)
        {
            if (UseContext)
            {
                // 系统提示仅在使用上下文时有效
                _messages.Add(new { role = "system", content });
            }
        }

        /// <summary>
        /// 发起一次聊天请求（可流式输出，通过回调处理每段生成内容）
        /// </summary>
        public async Task ChatAsync(string userInput, Action<string> onDelta = null,double temp=0.1)
        {
            UserBreak = false;
            // 动态构造消息列表
            List<object> currentMessages;

            if (UseContext)
            {
                // 上下文模式：保留全部历史消息
                _messages.Add(new { role = "user", content = userInput });
                currentMessages = new List<object>(_messages);
            }
            else
            {
                // 无上下文模式：只发送当前输入
                currentMessages = new List<object>
        {
            new { role = "user", content = userInput }
        };
            }

            var requestBody = new
            {
                model = _model,
                messages = currentMessages,
                stream = true,
                temperature= temp
            };

            //  使用 Newtonsoft.Json 序列化
            string jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl)
            {
                Content = content
            };

            try
            {
                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    string aiReply = await ProcessStreamAsync(response, onDelta);

                    // 如果启用了上下文，把AI回复加入历史
                    if (UseContext && !string.IsNullOrWhiteSpace(aiReply))
                        _messages.Add(new { role = "assistant", content = aiReply });
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 处理流式响应输出（可通过回调逐条处理）
        /// </summary>
        private async Task<string> ProcessStreamAsync(HttpResponseMessage response, Action<string> onDelta = null)
        {
            var sb = new StringBuilder();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (!line.StartsWith("data: ")) continue;

                    string jsonData = line.Substring(6).Trim();
                    if (jsonData == "[DONE]") break;

                    try
                    {
                        var obj = JObject.Parse(jsonData);
                        var choices = obj["choices"] as JArray;
                        if (choices != null && choices.Count > 0)
                        {
                            var delta = choices[0]["delta"];
                            if (delta != null && delta["content"] != null)
                            {
                                string text = delta["content"].ToString();

                                // 累积完整文本
                                sb.Append(text);

                                // 实时输出到控制台
                                Console.Write(text);

                                // 触发回调（边生成边处理）
                                onDelta?.Invoke(text);
                            }
                        }
                    }
                    catch (JsonReaderException)
                    {
                        // 忽略无法解析的行
                    }
                    if (UserBreak)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine();
            return sb.ToString();
        }


        /// <summary>
        /// 清空历史上下文（重新开始）
        /// </summary>
        public void ResetContext()
        {
            _messages.Clear();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public class UnsplashSearch
        {
            /// <summary>
            /// 搜索 Unsplash 图片
            /// </summary>
            /// <param name="query"></param>
            /// <param name="orientation">
            /// orientation	图片的方向。可选值有：
            /// landscape（横向）
            /// portrait（纵向）
            /// squarish（方形）
            /// </param>
            /// <returns></returns>
            public static string[] SearchUnsplashImages(string query,string orientation= "landscape")
            {

                using (HttpClient client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                    client.Timeout = TimeSpan.FromSeconds(10);
                    client.DefaultRequestHeaders.Add("Authorization", "Client-ID " + SetupForm.cfg.SearchAPIKey);

                    string url = SetupForm.cfg.SearchAddress + "/search/photos?query=" +
                                 Uri.EscapeDataString(query) + "&per_page=1&orientation="+orientation;

                    try
                    {
                        Console.WriteLine("请求地址: " + url);

                        using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                        using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                        {
                            HttpResponseMessage response = client.SendAsync(request, cts.Token).Result;

                            if (!response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("❌ 请求失败: " + response.StatusCode);
                                return new string[0];
                            }

                            string json = response.Content.ReadAsStringAsync().Result;
                            Console.WriteLine("✅ 已收到响应");

                            JObject result = JObject.Parse(json);
                            var results = result["results"];
                            if (results == null || !results.HasValues)
                            {
                                Console.WriteLine("⚠️ 没有搜索到结果");
                                return new string[0];
                            }

                            string[] urls = new string[results.Count()];
                            for (int i = 0; i < results.Count(); i++)
                            {
                                urls[i] = results[i]["urls"]["regular"] != null
                                    ? results[i]["urls"]["regular"].ToString()
                                    : "";
                            }

                            return urls;
                        }
                    }
                    catch (AggregateException ae)
                    {
                        foreach (var ex in ae.InnerExceptions)
                        {
                            if (ex is TaskCanceledException)
                                Console.WriteLine("⏱️ 请求超时！");
                            else
                                Console.WriteLine("💥 异常: " + ex.Message);
                        }
                        return new string[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("💥 其他错误: " + ex.Message);
                        return new string[0];
                    }
                }
            }
        }
    }
}
