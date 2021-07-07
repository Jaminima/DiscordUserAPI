using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DiscordUserAPI
{
    public static class Requests
    {
        public static async Task<T> DoRequest<T>(string path, object data)
        {
            return await DoRequest<T>(path, JsonConvert.SerializeObject(data));
        }

        public static async Task<T> DoRequest<T>(string path, string data)
        {
            string body = await DoRequest(path, data);
            return JsonConvert.DeserializeObject<T>(body);
        }

        public static async Task<string> DoRequest(string path, string data)
        {
            var handler = new HttpClientHandler();

            // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
            handler.AutomaticDecompression = ~DecompressionMethods.None;

            // In production code, don't destroy the HttpClient through using, but better reuse an existing instance
            // https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://discord.com/api/v9/"+path))
                {
                    request.Headers.TryAddWithoutValidation("authority", "discord.com");
                    request.Headers.TryAddWithoutValidation("accept-language", "en-US");
                    request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Headers.TryAddWithoutValidation("sec-gpc", "1");
                    request.Headers.TryAddWithoutValidation("origin", "https://discord.com");
                    request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                    request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                    request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                    request.Headers.TryAddWithoutValidation("referer", "https://discord.com/login");

                    request.Content = new StringContent(data);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    try
                    {
                        var response = await httpClient.SendAsync(request);
                        string body = await response.Content.ReadAsStringAsync();
                        return body;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return null;
                    }
                }
            }
        }
    }
}
