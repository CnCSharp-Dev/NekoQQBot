using NekoBot.Lib.Models.Basic;
using Newtonsoft.Json;

namespace NekoBot.HttpTransporter.Models
{
    public struct TransporterRequest
    {
        [JsonProperty("cmd")]
        public string Command;
        [JsonProperty("args")]
        public string[] Arguments;
        [JsonProperty("sender")]
        public User Sender;
        [JsonProperty("bot_appid")]
        public string AppId;
        [JsonProperty("group_id")]
        public string GroupOpenId;
        [JsonProperty("isGroup")]
        public bool IsGroup;
    }
}
