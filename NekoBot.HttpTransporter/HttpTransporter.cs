using System.Text;
using NekoBot.HttpTransporter.Models;
using Newtonsoft.Json;

namespace NekoBot.HttpTransporter
{
    public static class HttpTransporter
    {
        private static readonly HttpClient _httpClient = new();
        public static async Task<TransporterResponse> PostAsync(string url, TransporterRequest request)
        {
            StringContent content = new(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var res = await _httpClient.PostAsync(url,content);

            var str = await res.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransporterResponse>(str);
        }
    }
}
