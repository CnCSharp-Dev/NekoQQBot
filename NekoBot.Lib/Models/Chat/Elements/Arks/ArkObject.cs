using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements.Arks
{
    /// <summary>
    /// <see cref="Ark"/>消息的对象
    /// </summary>
    public struct ArkObject
    {
        /// <summary>
        /// 消息Ark对象键值对表
        /// </summary>
        [JsonProperty("obj_kv")]
        public List<ArkObjectKeyValue> ObjectKeyValues;
    }
}
