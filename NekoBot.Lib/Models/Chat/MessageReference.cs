using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat
{
    /// <summary>
    /// 消息引用
    /// </summary>
    public struct MessageReference
    {
        /// <summary>
        /// 需要引用回复的消息Id
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId;
        /// <summary>
        /// 是否忽略获取引用消息详情错误
        /// </summary>
        [JsonProperty("ignore_get_message_error")]
        public bool IgnoreGetMessageError;
    }
}
