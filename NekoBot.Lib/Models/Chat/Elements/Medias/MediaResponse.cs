using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements.Medias
{
    /// <summary>
    /// 富媒体消息返回类型
    /// </summary>
    public class MediaResponse
    {
        /// <summary>
        /// 文件信息，用于发消息接口的 media 字段使用
        /// </summary>
        [JsonProperty("file_info")]
        public string FileInfo;
        /// <summary>
        /// 文件Id
        /// </summary>
        [JsonProperty("file_uuid")]
        public string FileId;
        /// <summary>
        /// 有效期，表示剩余多少秒到期，到期后file_info失效，当等于0时，表示可长期使用
        /// </summary>
        [JsonProperty("ttl")]
        public string Ttl;
        /// <summary>
        /// 发送消息的唯一ID，当srv_send_msg设置为true时返回
        /// </summary>
        [JsonProperty("id")]
        public string Id;
    }
}
