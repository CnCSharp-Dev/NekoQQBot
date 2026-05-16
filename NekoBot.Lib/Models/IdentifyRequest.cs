using NekoBot.Lib.Enums;
using Newtonsoft.Json;

namespace NekoBot.Lib.Models
{
    /// <summary>
    /// 表示登录鉴权结构，发送后返回<see cref="ReadyResponse"/>
    /// </summary>
    public struct IdentifyRequest
    {
        /// <summary>
        /// 表示AccessToken，格式为"QQBot {AccessToken}"
        /// </summary>
        [JsonProperty("token")]
        public string Token;
        /// <summary>
        /// 此次连接所需要接收的事件
        /// </summary>
        [JsonProperty("intents")]
        public Intents Intents;
        /// <summary>
        /// QQ提供了的分片逻辑，事件通知会落在不同的分片上，该参数是个拥有两个元素的数组
        /// </summary>
        [JsonProperty("shard")]
        public List<int> Shard;
        /// <summary>
        /// 无实际作用，可以进行标记
        /// </summary>
        [JsonProperty("properties")]
        public Dictionary<string, string> Properties;
    }
}
