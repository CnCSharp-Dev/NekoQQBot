using YamlDotNet.Serialization;

namespace NekoBot.HttpTransporter.Models
{
    public class HttpTargetModel
    {
        [YamlMember(Description = "目标的Url")]
        public string Url { get; set; }
        [YamlMember(Description = "要求的命令，当命令匹配后才发送")]
        public string Command { get; set; }
        [YamlMember(Description = "要求的命令的帮助文本")]
        public string Description { get; set; }
    }
}
