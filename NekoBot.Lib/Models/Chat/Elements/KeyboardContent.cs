using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements
{
    /// <summary>
    /// 消息交互内容
    /// </summary>
    public struct KeyboardContent
    {
        /// <summary>
        /// 消息交互内容行列表
        /// </summary>
        [JsonProperty("rows")]
        public List<KeyboardContentRow> Rows;
    }
}
