using Newtonsoft.Json;

namespace NekoBot.Lib.Models
{
    /// <summary>
    /// 表示重新连接信息
    /// </summary>
    public struct ResumeRequest
    {
        /// <summary>
        /// 当前机器人的Token
        /// </summary>
        [JsonProperty("token")]
        public string Token;
        /// <summary>
        /// 当前事件的SessionId
        /// </summary>

        [JsonProperty("session_id")]
        public string SessionId;
        /// <summary>
        /// 当前序列号
        /// </summary>
        [JsonProperty("seq")]
        public int? Serial;
    }
}
