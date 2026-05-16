using NekoBot.Lib.Enums;
using NekoBot.Lib.Models.Chat.Elements;
using NekoBot.Lib.Models.Chat.Elements.Arks;
using NekoBot.Lib.Models.Chat.Elements.Medias;
using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat
{
    /// <summary>
    /// 表示一个QQ聊天信息
    /// </summary>
    public struct ChatRequest
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        [JsonProperty("content")]
        public string Content;
        /// <summary>
        /// [必填] 消息类型
        /// </summary>
        [JsonProperty("msg_type")]
        public MessageType Type;
        /// <summary>
        /// Markdown对象
        /// </summary>
        [JsonProperty("markdown")]
        public Markdown Markdown;
        /// <summary>
        /// （选填）消息交互
        /// </summary>
        [JsonProperty("keyboard")]
        public Keyboard Keyboard;
        /// <summary>
        /// Ark
        /// </summary>
        [JsonProperty("ark")]
        public Ark Ark;
        /// <summary>
        /// 富媒体消息
        /// </summary>
        [JsonProperty("media")]
        public MediaResponse Media;
        /// <summary>
        /// 图片
        /// </summary>
        [JsonProperty("image")]
        public string Image;
        /// <summary>
        /// 消息引用
        /// </summary>
        [JsonProperty("message_reference")]
        public MessageReference Reference;
        /// <summary>
        /// 前置收到的事件ID，用于发送被动消息
        /// </summary>
        [JsonProperty("event_id")]
        public string EventID;
        /// <summary>
        /// 前置收到的用户发送过来的消息 ID，用于发送被动消息
        /// /// </summary>
        [JsonProperty("msg_id")]
        public string MessageId;
        /// <summary>
        /// 回复消息的序号，与 msg_id 联合使用，避免相同消息id回复重复发送，不填默认是1。相同的 msg_id + msg_seq 重复发送会失败。
        /// </summary>
        [JsonProperty("msg_seq")]
        public int? MessageSeq;
    }
}
