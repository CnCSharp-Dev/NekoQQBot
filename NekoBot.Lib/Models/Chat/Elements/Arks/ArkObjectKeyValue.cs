using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements.Arks
{
    /// <summary>
    /// <see cref="Ark"/>对象键值封装类
    /// </summary>
    public struct ArkObjectKeyValue
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
    }
}
