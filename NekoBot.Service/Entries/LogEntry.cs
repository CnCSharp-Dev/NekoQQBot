using NekoBot.Lib.Loggers;

namespace NekoBot.Service.Entries
{
    /// <summary>
    /// 用于记录到数据库的日志信息
    /// </summary>
    /// <param name="Message">日志信息</param>
    /// <param name="LogType">日志类型</param>
    public record struct LogEntry(string Message, LogType LogType);
}
