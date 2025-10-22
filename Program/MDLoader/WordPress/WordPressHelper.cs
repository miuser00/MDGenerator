using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

class WordPressAdmin
{
    private readonly string wpBaseUrl;
    private readonly string username;
    private readonly string password;

    public WordPressAdmin(string wpBaseUrl, string username, string password)
    {
        this.wpBaseUrl = wpBaseUrl.TrimEnd('/');
        this.username = username;
        this.password = password;
    }

    /// <summary>
    /// 删除指定文章
    /// </summary>
    public async Task<bool> DeletePostAsync(int postId)
    {
        using (var handler = new HttpClientHandler { CookieContainer = new CookieContainer(), AllowAutoRedirect = true })
        using (var client = new HttpClient(handler))
        {
            // 1️⃣ 登录后台 wp-login.php
            var loginData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("log", username),
                new KeyValuePair<string,string>("pwd", password),
                new KeyValuePair<string,string>("wp-submit", "Log In"),
                new KeyValuePair<string,string>("redirect_to", $"{wpBaseUrl}/wp-admin/"),
                new KeyValuePair<string,string>("testcookie", "1")
            });

            var loginResponse = await client.PostAsync($"{wpBaseUrl}/wp-login.php", loginData);
            string loginResult = await loginResponse.Content.ReadAsStringAsync();

            if (!loginResult.Contains("wp-admin"))
            {
                Console.WriteLine("登录失败，请检查用户名或密码");
                return false;
            }

            // 2️⃣ 获取后台文章列表页 HTML，用于提取删除文章 nonce
            string editPageHtml = await client.GetStringAsync($"{wpBaseUrl}/wp-admin/edit.php");
            string nonce = ExtractNonce(editPageHtml);

            if (string.IsNullOrEmpty(nonce))
            {
                Console.WriteLine("未找到有效的删除文章 nonce");
                return false;
            }

            // 3️⃣ 调用 admin-ajax.php 删除文章
            string ajaxUrl = $"{wpBaseUrl}/wp-admin/admin-ajax.php";
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("action", "delete_post_by_ajax"),
                new KeyValuePair<string,string>("post_id", postId.ToString()),
                new KeyValuePair<string,string>("nonce", nonce)
            });

            var response = await client.PostAsync(ajaxUrl, formData);
            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && result.Contains("success"))
            {
                Console.WriteLine($"文章 {postId} 已删除！");
                return true;
            }
            else
            {
                Console.WriteLine($"删除失败: {result}");
                return false;
            }
        }
    }

    /// <summary>
    /// 从后台页面 HTML 中提取删除文章 nonce
    /// </summary>
    private string ExtractNonce(string html)
    {
        // 尝试匹配 JS 变量
        var match = Regex.Match(html, @"deletePostNonce\s*=\s*['""](?<nonce>[a-zA-Z0-9]+)['""]");
        if (match.Success) return match.Groups["nonce"].Value;

        // 尝试匹配 hidden input
        match = Regex.Match(html, @"<input[^>]*id=['""]?_wpnonce['""][^>]*value=['""](?<nonce>[a-zA-Z0-9]+)['""]");
        if (match.Success) return match.Groups["nonce"].Value;

        return null;
    }
}
