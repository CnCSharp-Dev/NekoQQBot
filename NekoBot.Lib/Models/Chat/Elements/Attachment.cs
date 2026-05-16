using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements
{
    /// <summary>
    /// QQ消息附件
    /// </summary>
    public struct Attachment
    {
        /// <summary>
        /// 内容类型
        /// </summary>
        [JsonProperty("content_type")]
        public string ContentType;
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("filename")]
        public string Filename;
        /// <summary>
        /// 高度
        /// </summary>
        [JsonProperty("height")]
        public string Height;
        /// <summary>
        /// 宽度
        /// </summary>
        [JsonProperty("width")]
        public string Width;
        /// <summary>
        /// 尺寸
        /// </summary>
        [JsonProperty("size")]
        public string Size;
        /// <summary>
        /// Url
        /// </summary>
        [JsonProperty("url")]
        public string Url;
    }
}
