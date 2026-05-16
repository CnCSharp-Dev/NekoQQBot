using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements
{
    /// <summary>
    /// 消息交互
    /// </summary>
    public class Keyboard
    {
        /// <summary>
        /// 消息交互内容
        /// </summary>
        [JsonProperty("content")]
        public KeyboardContent Content;
    }
}
