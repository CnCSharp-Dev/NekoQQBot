using NekoBot.Lib.Models.Basic;
using NekoBot.Lib.Models.Chat.Elements;
using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat
{
    /// <summary>
    /// 接收到的QQ消息
    /// </summary>
    public struct ChatReceive
    {
        /// <summary>
        /// 消息唯一Id
        /// </summary>
        [JsonProperty("id")]
        public string Id;
        /// <summary>
        /// 发送者
        /// </summary>
        [JsonProperty("author")]
        public User Author;
        /// <summary>
        /// 发送的QQ群Id
        /// </summary>
        [JsonProperty("group_openid")]
        public string GroupOpenId;
        /// <summary>
        /// 文本消息内容
        /// </summary>
        [JsonProperty("content")]
        public string Content;
        /// <summary>
        /// 消息生产时间
        /// </summary>
        [JsonProperty("timestamp")]
        public DateTime Timestamp;
        /// <summary>
        /// 富媒体文件附件
        /// </summary>
        [JsonProperty("attachments")]
        public List<Attachment> Attachments;
    }
}
