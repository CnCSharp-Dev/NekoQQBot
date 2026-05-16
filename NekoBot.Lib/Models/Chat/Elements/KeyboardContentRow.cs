using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements
{
    /// <summary>
    /// 消息交互内容行
    /// </summary>
    public struct KeyboardContentRow
    {
        /// <summary>
        /// 消息按钮列表
        /// </summary>
        [JsonProperty("buttons")]
        public List<GuildMessageButton> Buttons;
    }
}
