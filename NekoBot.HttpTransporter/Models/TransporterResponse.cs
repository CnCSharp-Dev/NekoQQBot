using Newtonsoft.Json;

namespace NekoBot.HttpTransporter.Models
{
    public struct TransporterResponse
    {
        [JsonProperty("response")]
        public string Response;
    }
}
