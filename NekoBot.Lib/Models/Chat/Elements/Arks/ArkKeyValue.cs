using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements.Arks
{
    /// <summary>
    /// Ark的键与值组合
    /// </summary>
    public struct ArkKeyValue
    {
        /// <summary>
        /// 键
        /// </summary>
        [JsonProperty("key")]
        public string Key;
        /// <summary>
        /// 值
        /// </summary>
        [JsonProperty("value")]
        public string Value;
        /// <summary>
        /// 消息Ark对象表
        /// </summary>
        [JsonProperty("obj")]
        public List<ArkObject> Objects;
    }
}
