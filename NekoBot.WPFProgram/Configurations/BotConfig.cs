using NekoBot.Lib;
using NekoBot.Lib.Enums;
using NekoBot.Lib.Loggers;
using YamlDotNet.Serialization;

namespace NekoBot.WPFProgram.Configurations
{
    /// <summary>
    /// 表示机器人的配置文件对象
    /// </summary>
    public class BotConfig
    {
        /// <summary>
        /// 注册的事件，具体参考QQ官方开发文档
        /// </summary>
        [YamlMember(Description = "注册的事件，具体参考QQ官方开发文档")]
        public string Intent { get; set; } = "ALL_PUBLIC";
        /// <summary>
        /// 机器人的AppId
        /// </summary>
        [YamlMember(Description = "机器人的AppId")]
        public string AppId { get; set; } = string.Empty;
        /// <summary>
        /// 机器人的Secret
        /// </summary>
        [YamlMember(Description = "机器人的Secret")]
        public string Secret { get; set; } = string.Empty;
        /// <summary>
        /// 转化为<see cref="BotInfo"/>
        /// </summary>
        /// <returns>转化后的<see cref="BotInfo"/></returns>
        public BotInfo ToBotInfo()
        {
            if (Enum.TryParse(Intent, out Intents intent))
                return new(intent, AppId, Secret);

            Logger.Error("无法转换" + Intent + "为对应的Intent，默认订阅所有事件!");
            return new(Intents.ALL, AppId, Secret);
        }
    }
}
