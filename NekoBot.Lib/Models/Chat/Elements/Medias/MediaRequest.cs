using NekoBot.Lib.Enums;
using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements.Medias
{
    /// <summary>
    /// 富媒体消息
    /// </summary>
    public struct MediaRequest
    {
        /// <summary>
        /// [必填]媒体类型
        /// </summary>
        [JsonProperty("file_type")]
        public MediaType FileType;
        /// <summary>
        /// [必填]需要发送媒体资源的Url
        /// </summary>
        [JsonProperty("url")]
        public string Url;
        /// <summary>
        /// [必填]是否直接发送消息到目标端(会占用主动消息频次)
        /// </summary>
        [JsonProperty("srv_send_msg")]
        public bool ServeSendMessage;
        /// <summary>
        /// Base64二进制数据
        /// </summary>
        [JsonProperty("file_data")]
        public object FileData;
    }
}
