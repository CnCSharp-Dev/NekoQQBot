using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat
{
    /// <summary>
    /// QQ聊天消息返回结果
    /// </summary>
    public struct ChatResponse
    {
        /// <summary>
        /// 消息唯一Id
        /// </summary>
        [JsonProperty("id")]
        public string Id;
        /// <summary>
        /// 发送时间
        /// </summary>
        [JsonProperty("timestamp")]
        public string Timestamp;
    }
}
