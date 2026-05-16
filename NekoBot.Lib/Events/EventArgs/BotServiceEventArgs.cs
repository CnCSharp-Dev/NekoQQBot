namespace NekoBot.Lib.Events.EventArgs
{
    /// <summary>
    /// QQ机器人服务事件
    /// </summary>
    /// <param name="service">QQ机器人服务</param>
    public abstract class BotServiceEventArgs(BotService service)
    {
        /// <summary>
        /// QQ机器人服务
        /// </summary>
        public BotService Service { get; } = service;
    }
}
