using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements
{
    /// <summary>
    /// 表示一个Markdown对象
    /// </summary>
    public class Markdown
    {
        /// <summary>
        /// 原生Markdown文本内容
        /// </summary>
        [JsonProperty("content")]
        public string Content;

        /// <summary>
        /// 申请模版后获得的Markdown模版ID
        /// </summary>
        [JsonProperty("custom_template_id")]
        public string CustomTemplateID;

        /// <summary>
        /// 模版内变量与填充值的键值映射
        /// </summary>
        [JsonProperty("params")]
        public List<MarkDownParam> Params;
    }
}
