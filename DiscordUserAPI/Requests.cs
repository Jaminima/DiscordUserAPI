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
        public static async Task<T> DoRequest<T>(string path, object data, string authorization = null)
        {
            return await DoRequest<T>(path, JsonConvert.SerializeObject(data),authorization);
        }

        public static async Task<T> DoRequest<T>(string path, string data, string authorization=null)
        {
            string body = await DoRequest(path, data, authorization);
            return JsonConvert.DeserializeObject<T>(body);
        }

        public static async Task<string> DoRequest(string path, string data, string authorization = null)
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
                    request.Headers.TryAddWithoutValidation("x-super-properties", "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6ImVuLVVTIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzkxLjAuNDQ3Mi4xMjQgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6IjkxLjAuNDQ3Mi4xMjQiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6Imh0dHBzOi8vd3d3LmVjb3NpYS5vcmcvIiwicmVmZXJyaW5nX2RvbWFpbiI6Ind3dy5lY29zaWEub3JnIiwicmVmZXJyZXJfY3VycmVudCI6Imh0dHBzOi8vd3d3LmVjb3NpYS5vcmcvIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50Ijoid3d3LmVjb3NpYS5vcmciLCJyZWxlYXNlX2NoYW5uZWwiOiJzdGFibGUiLCJjbGllbnRfYnVpbGRfbnVtYmVyIjo4OTU1MCwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=");
                    request.Headers.TryAddWithoutValidation("x-fingerprint", "862254724391895091.LpB088cjDuUdNBcAAstpUGN0WKP");
                    request.Headers.TryAddWithoutValidation("accept-language", "en-US");
                    request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Headers.TryAddWithoutValidation("sec-gpc", "1");
                    request.Headers.TryAddWithoutValidation("origin", "https://discord.com");
                    request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                    request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                    request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                    request.Headers.TryAddWithoutValidation("referer", "https://discord.com/login");

                    if (authorization != null) request.Headers.TryAddWithoutValidation("authorization", authorization);

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
