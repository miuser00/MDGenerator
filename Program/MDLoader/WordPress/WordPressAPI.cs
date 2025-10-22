using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MDLoader
{
    public class Wpapi
    {
        /// <summary>
        /// 同步上传图片到 WordPress，并返回图片 URL。
        /// </summary>
        /// <param name="wpUrl">WordPress 上传接口（如：https://example.com/?rest_route=/wp/v2/media）</param>
        /// <param name="wpUser">用户名</param>
        /// <param name="wpAppPassword">应用密码</param>
        /// <param name="picLocalPath">图片本地路径</param>
        /// <param name="timeoutSeconds">超时秒数（默认30秒）</param>
        /// <returns>上传成功返回图片 URL，失败返回 null</returns>
        public static string UploadImageFile(string wpUrl, string wpUser, string wpAppPassword, string picLocalPath, int timeoutSeconds = 10)
        {
            if (!File.Exists(picLocalPath))
            {
                throw new Exception("文件不存在：" + picLocalPath);
            }

            try
            {
                // 读取文件字节
                byte[] fileBytes = File.ReadAllBytes(picLocalPath);
                string fileName = Path.GetFileName(picLocalPath);

                // 自动识别 MIME 类型
                string ext = Path.GetExtension(fileName).ToLower();
                string mime = ext == ".jpg" || ext == ".jpeg" ? "image/jpeg" :
                              ext == ".gif" ? "image/gif" :
                              ext == ".webp" ? "image/webp" :
                              ext == ".bmp" ? "image/bmp" :
                              "image/png";

                // 创建 HTTP 请求
                var request = (HttpWebRequest)WebRequest.Create(wpUrl);
                request.Method = "POST";
                request.Timeout = timeoutSeconds * 1000; // 超时设置
                request.ReadWriteTimeout = timeoutSeconds * 1000;
                request.KeepAlive = false;

                // 设置认证头
                string authInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{wpUser}:{wpAppPassword}"));
                request.Headers["Authorization"] = "Basic " + authInfo;

                string boundary = "----Boundary" + DateTime.Now.Ticks.ToString("x");
                request.ContentType = "multipart/form-data; boundary=" + boundary;

                using (var reqStream = request.GetRequestStream())
                {
                    var boundaryBytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
                    reqStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                    // 文件头
                    string header = $"Content-Disposition: form-data; name=\"file\"; filename=\"{fileName}\"\r\n" +
                                    $"Content-Type: {mime}\r\n\r\n";
                    var headerBytes = Encoding.UTF8.GetBytes(header);
                    reqStream.Write(headerBytes, 0, headerBytes.Length);

                    // 文件数据
                    reqStream.Write(fileBytes, 0, fileBytes.Length);

                    // 结束标志
                    var trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                    reqStream.Write(trailer, 0, trailer.Length);
                }

                // 获取响应
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();

                    if (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine(json);
                        throw new Exception("上传失败: " + response.StatusCode);

                    }

                    try
                    {
                        JObject obj = JObject.Parse(json);
                        string url = (string)obj["source_url"];
                        return string.IsNullOrEmpty(url) ? null : url;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("JSON 解析失败: " + ex.Message);
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception("请求出错: " + ex.Message);

            }
            catch (Exception ex)
            {
                throw new Exception("其他异常: " + ex.Message);
            }
        }
        /// <summary>
        /// 根据 Markdown 文本创建 WordPress 文章（支持可选分类和标签）
        /// </summary>
        /// <param name="categoryId">分类 ID，可为空</param>
        /// <param name="tagIds">标签 ID 数组，可为空</param>
        /// <param name="wpUrl">WordPress REST API URL，例如：https://www.example.com/?rest_route=/wp/v2/posts</param>
        /// <param name="wpUser">用户名</param>
        /// <param name="wpAppPassword">应用密码</param>
        /// <param name="markdownText">Markdown 文本内容</param>
        /// <param name="featuredMediaId">特色图片 ID，可选</param>
        /// <param name="status">文章状态，默认 publish</param>
        /// <param name="timeoutSeconds">超时秒数</param>
        /// <returns>文章传统 URL</returns>
        public static string UploadMarkdownText(
            int? categoryId,
            int[] tagIds,
            string wpUrl,
            string wpUser,
            string wpAppPassword,
            string markdownText,
            int? featuredMediaId = null,
            string status = "publish",
            int timeoutSeconds = 10)
        {
            if (string.IsNullOrWhiteSpace(markdownText))
                throw new Exception("Markdown 文本不能为空");

            // 按行分割
            string[] lines = markdownText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // 取第一行作为标题
            string title = "无标题";
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

            // 剩余部分为正文
            string content = string.Join("\n", lines, firstContentLineIndex, lines.Length - firstContentLineIndex);

            try
            {
                // 构造 JSON 数据
                var postData = new JObject
                {
                    ["title"] = title,
                    ["content"] = content,
                    ["status"] = status
                };

                // 分类可选
                if (categoryId.HasValue && categoryId.Value > 0)
                    postData["categories"] = new JArray(categoryId.Value);

                // 标签可选
                if (tagIds != null && tagIds.Length > 0)
                    postData["tags"] = new JArray(tagIds);

                // 可选特色图片
                if (featuredMediaId.HasValue)
                    postData["featured_media"] = featuredMediaId.Value;

                string jsonData = postData.ToString();

                // 创建 HTTP 请求
                var request = (HttpWebRequest)WebRequest.Create(wpUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = timeoutSeconds * 1000;
                request.ReadWriteTimeout = timeoutSeconds * 1000;
                request.KeepAlive = false;

                string authInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{wpUser}:{wpAppPassword}"));
                request.Headers["Authorization"] = "Basic " + authInfo;

                using (var stream = request.GetRequestStream())
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
                    stream.Write(dataBytes, 0, dataBytes.Length);
                }

                // 获取响应
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();

                    // 提取有效 JSON
                    string jsonOnly = ExtractLastJson(responseText);
                    JObject obj = JObject.Parse(jsonOnly);

                    int postId = (int)obj["id"];
                    string domain = new Uri(wpUrl).GetLeftPart(UriPartial.Authority);
                    string traditionalUrl = $"{domain}/?p={postId}";

                    return traditionalUrl;
                }
            }
            catch (WebException ex)
            {
                throw new Exception("请求出错: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("其他异常: " + ex.Message);
            }
        }

        public static string ExtractLastJson(string text)
        {
            int lastBrace = text.LastIndexOf('{');
            while (lastBrace >= 0)
            {
                string candidate = text.Substring(lastBrace);
                try
                {
                    JObject.Parse(candidate); // 尝试解析
                    return candidate; // 成功就返回
                }
                catch
                {
                    lastBrace = text.LastIndexOf('{', lastBrace - 1); // 继续向前找
                }
            }
            throw new Exception("未能找到有效 JSON");
        }
        /// <summary>
        /// 向 WordPress 添加一个新的分类（Category）
        /// </summary>
        /// <param name="wpBaseUrl">WordPress 网站主地址，例如：https://www.example.com</param>
        /// <param name="wpUser">用户名</param>
        /// <param name="wpAppPassword">应用密码</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="parentId">父分类 ID（默认 0 表示顶级分类）</param>
        /// <param name="description">分类描述（可选）</param>
        /// <param name="timeoutSeconds">超时秒数</param>
        /// <returns>分类的 ID</returns>
        public static int AddCategory(
            string wpBaseUrl,
            string wpUser,
            string wpAppPassword,
            string categoryName,
            int parentId = 0,
            string description = "",
            int timeoutSeconds = 10)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new Exception("分类名称不能为空");

            // ✅ 自动补全 API 地址
            string wpUrl = wpBaseUrl.TrimEnd('/');
            wpUrl += "?rest_route=/wp/v2/categories";

            try
            {
                // 构造 JSON 数据
                var categoryData = new JObject
                {
                    ["name"] = categoryName
                };

                if (parentId > 0)
                    categoryData["parent"] = parentId;

                if (!string.IsNullOrEmpty(description))
                    categoryData["description"] = description;

                string jsonData = categoryData.ToString();

                // 创建 HTTP 请求
                var request = (HttpWebRequest)WebRequest.Create(wpUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = timeoutSeconds * 1000;
                request.ReadWriteTimeout = timeoutSeconds * 1000;
                request.KeepAlive = false;

                string authInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{wpUser}:{wpAppPassword}"));
                request.Headers["Authorization"] = "Basic " + authInfo;

                // 写入数据
                using (var stream = request.GetRequestStream())
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
                    stream.Write(dataBytes, 0, dataBytes.Length);
                }

                // 获取响应
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();

                    // 提取有效 JSON
                    string jsonOnly = ExtractLastJson(responseText);

                    JObject obj = JObject.Parse(jsonOnly);

                    int categoryId = (int)obj["id"];
                    return categoryId;
                }
            }
            catch (WebException ex)
            {
                string err = "";
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        err = reader.ReadToEnd();
                    }
                }
                throw new Exception("请求出错: " + ex.Message + "\n服务器响应: " + err);
            }
            catch (Exception ex)
            {
                throw new Exception("其他异常: " + ex.Message);
            }
        }


        /// <summary>
        /// 获取 WordPress 分类（Categories），一次请求，返回默认数量（最多 10 条）
        /// </summary>
        /// <param name="siteUrl">WordPress 网站地址</param>
        /// <param name="username">App Password 用户名（可选）</param>
        /// <param name="appPassword">App Password（可选）</param>
        /// <param name="timeoutMs">请求超时时间（毫秒）</param>
        /// <returns>DataTable 包含分类 ID 和名称</returns>
        public static DataTable GetCategories(string siteUrl, string username = null, string appPassword = null, int timeoutMs = 10000)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            string url = $"{siteUrl}/?rest_route=/wp/v2/categories&per_page=100";

            try
            {
                string json = GetJson(url, username, appPassword, timeoutMs);
                JArray categories = JArray.Parse(json);

                foreach (var cat in categories)
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = cat["id"].Value<int>();
                    row["Name"] = cat["name"].Value<string>();
                    dt.Rows.Add(row);
                }
            }
            catch (WebException)
            {
                DataRow row = dt.NewRow();
                row["ID"] = 0;
                row["Name"] = "服务器无法连接";
                dt.Rows.Add(row);
            }
            catch (Exception ex)
            {
                DataRow row = dt.NewRow();
                row["ID"] = 0;
                row["Name"] = "解析错误: " + ex.Message;
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// 删除指定的 WordPress 分类（Category）
        /// 使用传统风格 URL (?rest_route=...) 并通过 POST + _method=DELETE 模拟删除
        /// </summary>
        /// <param name="siteUrl">WordPress 网站地址，例如 https://www.example.com</param>
        /// <param name="username">用户名</param>
        /// <param name="appPassword">应用密码</param>
        /// <param name="categoryId">要删除的分类ID</param>
        /// <param name="timeoutMs">请求超时时间（毫秒）</param>
        /// <returns>删除成功返回 true，失败抛出异常</returns>
        public static bool DeleteCategory(string siteUrl, string username, string appPassword, int categoryId, int timeoutMs = 10000)
        {
            if (categoryId <= 0)
                throw new ArgumentException("分类ID必须大于0");

            string url = $"{siteUrl.TrimEnd('/')}/?rest_route=/wp/v2/categories/{categoryId}&force=true";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE"; // 使用 POST 模拟 DELETE
            request.Timeout = timeoutMs;
            request.ContentType = "application/json";

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(appPassword))
            {
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{appPassword}"));
                request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            }

            // 模拟 DELETE
            string body = "{\"_method\":\"DELETE\"}";
            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bodyBytes, 0, bodyBytes.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    // WordPress 传统风格下，可能返回 200 或 200 + JSON
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                        return true;
                    else
                        throw new Exception("删除失败: " + result);
                }
            }
            catch (WebException ex)
            {
                string err = "";
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        err = reader.ReadToEnd();
                    }
                }
                throw new Exception("请求出错: " + ex.Message + "\n服务器响应: " + err);
            }
            catch (Exception ex)
            {
                throw new Exception("其他异常: " + ex.Message);
            }
        }
        //编写AddTag
        public static int AddTag(
            string wpBaseUrl,
            string wpUser,
            string wpAppPassword,
            string tagName,
            string description = "",
            int timeoutSeconds = 10)
        {
            if (string.IsNullOrWhiteSpace(tagName))
                throw new Exception("标签名称不能为空");
            // ✅ 自动补全 API 地址
            string wpUrl = wpBaseUrl.TrimEnd('/');
            wpUrl += "?rest_route=/wp/v2/tags";
            try
            {
                // 构造 JSON 数据
                var tagData = new JObject
                {
                    ["name"] = tagName
                };
                if (!string.IsNullOrEmpty(description))
                    tagData["description"] = description;
                string jsonData = tagData.ToString();
                // 创建 HTTP 请求
                var request = (HttpWebRequest)WebRequest.Create(wpUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = timeoutSeconds * 1000;
                request.ReadWriteTimeout = timeoutSeconds * 1000;
                request.KeepAlive = false;
                string authInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{wpUser}:{wpAppPassword}"));
                request.Headers["Authorization"] = "Basic " + authInfo;
                // 写入数据
                using (var stream = request.GetRequestStream())
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
                    stream.Write(dataBytes, 0, dataBytes.Length);
                }
                // 获取响应
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    // 提取有效 JSON
                    string jsonOnly = ExtractLastJson(responseText);
                    JObject obj = JObject.Parse(jsonOnly);
                    int tagId = (int)obj["id"];
                    return tagId;
                }
            }
            catch (WebException ex)
            {
                string err = "";
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        err = reader.ReadToEnd();
                    }
                }
                throw new Exception("请求出错: " + ex.Message + "\n服务器响应: " + err);

            }
            catch (Exception ex)
            {
                throw new Exception("其他异常: " + ex.Message);
            }
        }

        /// 
        /// 
        /// <summary>
        /// 获取 WordPress 标签（Tags），一次请求，返回默认数量（最多 10 条）
        /// </summary>
        /// <param name="siteUrl">WordPress 网站地址</param>
        /// <param name="username">App Password 用户名（可选）</param>
        /// <param name="appPassword">App Password（可选）</param>
        /// <param name="timeoutMs">请求超时时间（毫秒）</param>
        /// <returns>DataTable 包含标签 ID 和名称</returns>
        public static DataTable GetTags(string siteUrl, string username = null, string appPassword = null, int timeoutMs = 10000)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("编号", typeof(int));
            dt.Columns.Add("标签", typeof(string));

            string url = $"{siteUrl}/?rest_route=/wp/v2/tags&per_page=100";

            try
            {
                string json = GetJson(url, username, appPassword, timeoutMs);
                JArray tags = JArray.Parse(json);

                foreach (var tag in tags)
                {
                    DataRow row = dt.NewRow();
                    row["编号"] = tag["id"]?.Value<int>() ?? 0;
                    row["标签"] = tag["name"]?.Value<string>() ?? "";
                    dt.Rows.Add(row);
                }
            }
            catch (WebException)
            {
                DataRow row = dt.NewRow();
                row["编号"] = 0;
                row["标签"] = "服务器无法连接";
                dt.Rows.Add(row);
            }
            catch (Exception ex)
            {
                DataRow row = dt.NewRow();
                row["编号"] = 0;
                row["标签"] = "解析错误: " + ex.Message;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private static string GetJson(string url, string username = null, string appPassword = null, int timeoutMs = 10000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = timeoutMs;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(appPassword))
            {
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{appPassword}"));
                request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// 获取 WordPress 文章，并返回 DataTable，包含 ID、标题、分类ID列表、标签ID列表、发布日期
        /// </summary>
        /// <param name="serverUrl">WordPress 服务器地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码或 App Password</param>
        /// <param name="timeoutMs">请求超时（毫秒）</param>
        /// <returns>DataTable</returns>
        public static DataTable GetArticles(string serverUrl, string username, string password, int timeoutMs = 10000)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                if (!string.IsNullOrEmpty(username))
                {
                    string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
                    client.Headers[HttpRequestHeader.Authorization] = $"Basic {auth}";
                }

                // REST API 传统兼容方式 + 只获取必要字段id,title,categories,tags,date,首图片id
                string url = $"{serverUrl.TrimEnd('/')}/?rest_route=/wp/v2/posts&per_page=100&_fields=id,title,categories,tags,date,featured_media";
                //string url = $"{serverUrl.TrimEnd('/')}/?rest_route=/wp/v2/posts";
                string json = client.DownloadString(url);

                DataTable dt = new DataTable();
                dt.Columns.Add("编号", typeof(int));
                dt.Columns.Add("标题", typeof(string));
                dt.Columns.Add("类别", typeof(string));
                dt.Columns.Add("标签", typeof(string));
                dt.Columns.Add("发布日期", typeof(DateTime));
                dt.Columns.Add("首页图", typeof(string));

                JArray arr = JArray.Parse(json);

                foreach (var item in arr)
                {
                    int id = item["id"].Value<int>();
                    string title = item["title"]["rendered"].Value<string>();
                    int featured_media = item["featured_media"].Value<int>();

                    string categories = "";
                    if (item["categories"] is JArray catArr)
                    {
                        List<string> cats = new List<string>();
                        foreach (var c in catArr)
                            cats.Add(c.Value<int>().ToString());
                        categories = string.Join(",", cats);
                    }

                    string tags = "";
                    if (item["tags"] is JArray tagArr)
                    {
                        List<string> tgs = new List<string>();
                        foreach (var t in tagArr)
                            tgs.Add(t.Value<int>().ToString());
                        tags = string.Join(",", tgs);
                    }

                    DateTime date = DateTime.MinValue;
                    if (item["date"] != null)
                        date = DateTime.Parse(item["date"].Value<string>());

                    dt.Rows.Add(id, title, categories, tags, date, featured_media);
                }

                return dt;
            }
        }
        /// <summary>
        /// 删除指定的 WordPress 文章（Post）
        /// 使用传统风格 URL (?rest_route=...) 并通过 POST + _method=DELETE 模拟删除
        /// </summary>
        /// <param name="siteUrl">WordPress 网站地址，例如 https://www.example.com</param>
        /// <param name="username">用户名</param>
        /// <param name="appPassword">应用密码</param>
        /// <param name="articleId">要删除的文章ID</param>
        /// <param name="timeoutMs">请求超时时间（毫秒）</param>
        /// <returns>删除成功返回 true，失败抛出异常</returns>
        public static bool DeleteArticle(string siteUrl, string username, string appPassword, int articleId, int timeoutMs = 10000)
        {
            if (articleId <= 0)
                throw new ArgumentException("文章ID必须大于0");

            //string url = $"{siteUrl.TrimEnd('/')}?rest_route=/wp/v2/posts/{articleId}force=true";
            string url = $"{siteUrl.TrimEnd('/')}/wp-json/wp/v2/posts/{articleId}?force=true";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method = "POST"; // 或者 POST + _method=DELETE
            request.Method = "DELETE"; // 或者 POST + _method=DELETE
            request.Timeout = timeoutMs;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(appPassword))
            {
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{appPassword}"));
                request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                // 模拟 DELETE
                string body = "{\"_method\":\"DELETE\"}";
                byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bodyBytes, 0, bodyBytes.Length);
                }
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                        return true;
                    else
                        throw new Exception("删除失败: " + result);
                }
            }
            catch (WebException ex)
            {
                string err = "";
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        err = reader.ReadToEnd();
                    }
                }
                throw new Exception("请求出错: " + ex.Message + "\n服务器响应: " + err);
            }
        }

        //编写根据mediaId的url获取的图片URL
        public static string GetPictureUrlFromMediaApi(string siteUrl, string username, string appPassword, int mediaId, int timeoutMs = 10000)
        {
            if (mediaId <= 0)
                return null;
            string url = $"{siteUrl.TrimEnd('/')}/?rest_route=/wp/v2/media/{mediaId}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = timeoutMs;
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(appPassword))
            {
                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{appPassword}"));
                request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            }
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        JObject obj = JObject.Parse(result);
                        string mediaUrl = obj["source_url"]?.Value<string>();
                        return mediaUrl;
                    }
                    else
                    {
                        throw new Exception("获取媒体信息失败: " + result);
                    }
                }
            }
            catch (WebException ex)
            {
                string err = "";
                if (ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        err = reader.ReadToEnd();
                    }
                }
                throw new Exception("请求出错: " + ex.Message + "\n服务器响应: " + err);
            }
            catch (Exception ex)
            {
                throw new Exception("其他异常: " + ex.Message);
            }
        }
        /// <summary>
        /// 上传文章特色图片到服务器，并返回图片的 URL
        /// </summary>
        /// <param name="wpUrl">WordPress 上传接口（如：https://example.com/?rest_route=/wp/v2/media）</param>
        /// <param name="wpUser">用户名</param>
        /// <param name="wpAppPassword">应用密码</param>
        /// <param name="picLocalPath">图片本地路径</param>
        /// <param name="timeoutSeconds">超时秒数（默认30秒）</param>
        /// <returns>上传成功返回图片 URL，失败抛出异常</returns>
        public static int UploadFeaturedImage(string wpUrl, string wpUser, string wpAppPassword, string picLocalPath, int timeoutSeconds = 30)
        {
            if (!File.Exists(picLocalPath))
            {
                throw new Exception("文件不存在：" + picLocalPath);
            }

            try
            {
                // 读取文件字节
                byte[] fileBytes = File.ReadAllBytes(picLocalPath);
                string fileName = Path.GetFileName(picLocalPath);

                // 自动识别 MIME 类型
                string ext = Path.GetExtension(fileName).ToLower();
                string mime = ext == ".jpg" || ext == ".jpeg" ? "image/jpeg" :
                              ext == ".gif" ? "image/gif" :
                              ext == ".webp" ? "image/webp" :
                              ext == ".bmp" ? "image/bmp" :
                              "image/png";

                // 创建 HTTP 请求
                //根据wp的url生成media的上传地址
                string mediaApiUrl = $"{wpUrl.TrimEnd('/')}/?rest_route=/wp/v2/media";
                var request = (HttpWebRequest)WebRequest.Create(mediaApiUrl);
                request.Method = "POST";
                request.Timeout = timeoutSeconds * 1000; // 超时设置
                request.ReadWriteTimeout = timeoutSeconds * 1000;
                request.KeepAlive = false;

                // 设置认证头
                string authInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{wpUser}:{wpAppPassword}"));
                request.Headers["Authorization"] = "Basic " + authInfo;

                string boundary = "----Boundary" + DateTime.Now.Ticks.ToString("x");
                request.ContentType = "multipart/form-data; boundary=" + boundary;

                using (var reqStream = request.GetRequestStream())
                {
                    var boundaryBytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
                    reqStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                    // 文件头
                    string header = $"Content-Disposition: form-data; name=\"file\"; filename=\"{fileName}\"\r\n" +
                                    $"Content-Type: {mime}\r\n\r\n";
                    var headerBytes = Encoding.UTF8.GetBytes(header);
                    reqStream.Write(headerBytes, 0, headerBytes.Length);

                    // 文件数据
                    reqStream.Write(fileBytes, 0, fileBytes.Length);

                    // 结束标志
                    var trailer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                    reqStream.Write(trailer, 0, trailer.Length);
                }

                // 获取响应
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string json = reader.ReadToEnd();

                    if (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("上传失败: " + response.StatusCode);
                    }

                    try
                    {
                        JObject obj = JObject.Parse(json);
                        int url = (int)obj["id"];
                        return url;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("JSON 解析失败: " + ex.Message);
                    }
                }
            }
            catch (WebException ex)
            {
                throw new Exception("请求出错: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("其他异常: " + ex.Message);
            }
        }

    }
}
