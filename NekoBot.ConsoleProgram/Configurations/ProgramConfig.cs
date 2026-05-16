using Serilog.Events;
using YamlDotNet.Serialization;

namespace NekoBot.ConsoleProgram.Configurations
{
    /// <summary>
    /// 表示后台的配置文件对象
    /// </summary>
    public class ProgramConfig
    {
        /// <summary>
        /// 控制台标题模板，{BotName}为机器人昵称，{WebSocketUrl}为WebSocket地址，{LogLevel}为当前最低日志输出等级
        /// </summary>
        [YamlMember(Description = "控制台标题模板，{BotName}为机器人昵称，{WebSocketUrl}为WebSocket地址，{LogLevel}为当前最低日志输出等级")]
        public string ConsoleTitleTemplate { get; set; } = "{BotName} 后台 | 监听地址 {WebSocketUrl} | 日志最低限制: {LogLevel}";
        /// <summary>
        /// 日志最低输出等级
        /// <code>Verbose ＜ Debug ＜ Information ＜ Warning ＜ Error ＜ Fatal</code>
        /// </summary>
        [YamlMember(Description = "日志最低输出等级: Verbose < Debug < Information < Warning < Error < Fatal")]
        public string LogLevel { get; set; } = "Information";
        /// <summary>
        /// 是否启动自动重启
        /// </summary>
        [YamlMember(Description = "是否启动自动重启")]
        public bool AutoRestart { get; set; } = true;
        /// <summary>
        /// 自动重启等待时间(单位为秒)，当无法正常初始化后超过该时间则重新初始化
        /// </summary>
        [YamlMember(Description = "自动重启等待时间(单位为秒)，当无法正常初始化后超过该时间则重新初始化")]
        public int AutoRestartDelay { get; set; } = 10;
        internal bool TryGetLogEventLevel(out LogEventLevel logEventLevel, out string response)
        {
            if (Enum.TryParse<LogEventLevel>(LogLevel, out var result))
            {
                logEventLevel = result;
                response = "";
                return true;
            }
            logEventLevel = LogEventLevel.Information;
            response = "无法转换日志最低输出等级，自动设置为Information";
            return false;
        }
    }
}
