using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements
{
    /// <summary>
    /// Markdown的参数
    /// </summary>
    public struct MarkDownParam
    {
        /// <summary>
        /// 键
        /// </summary>
        [JsonProperty("key")]
        public string Key;
        /// <summary>
        /// 值列表
        /// </summary>
        [JsonProperty("values")]
        public List<string> Values;
    }
}
