using NekoBot.Lib;

namespace NekoBot.Service
{
    /// <summary>
    /// 负责提供创建机器人
    /// </summary>
    public static class BotFactory
    {
        /// <summary>
        /// 机器人服务
        /// </summary>
        public static BotService Service { get; private set; }
        /// <summary>
        /// 创建机器人
        /// </summary>
        /// <param name="info">机器人信息</param>
        public static void Create(BotInfo info)
        {
            Service = new(info);
        }
    }
}
