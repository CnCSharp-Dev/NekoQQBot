using NekoBot.Service;
using YamlDotNet.Serialization;

namespace NekoBot.ExtensionTools
{
    public class Config : IConfig
    {
        [YamlMember(Description = "启动自动GC回收")]
        public bool AutoCollect { get; set; } = true;
        [YamlMember(Description = "自动GC回收等待时间(单位为秒)")]
        public int AutoCollectDelay { get; set; } = 240;

        [YamlMember(Description = "记录WebSocket信息到数据库")]
        public bool RecordWebsocketMessage { get; set; } = true;
        [YamlMember(Description = "记录Logger日志到数据库")]
        public bool RecordLog { get; set; } = true;

        [YamlMember(Description = "记录群聊聊天到数据库")]
        public bool RecordGroupChat { get; set; } = true;
        [YamlMember(Description = "记录私聊聊天到数据库")]
        public bool RecordPrivateChat { get; set; } = true;
    }
}