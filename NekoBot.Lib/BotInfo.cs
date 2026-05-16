using NekoBot.Lib.Enums;

namespace NekoBot.Lib
{
    /// <summary>
    /// 机器人的基础信息
    /// </summary>
    public class BotInfo
    {
        /// <summary>
        /// 订阅的事件
        /// </summary>
        public Intents Intents { get; }
        /// <summary>
        /// 机器人的AppID
        /// </summary>
        public string AppID { get; }
        /// <summary>
        /// 机器人的密钥
        /// </summary>
        public string Secret { get; }
        /// <summary>
        /// 创建一个机器人信息类
        /// </summary>
        /// <param name="intents">订阅的事件</param>
        /// <param name="appID">AppID</param>
        /// <param name="secret">Token</param>
        public BotInfo(Intents intents, string appID, string secret)
        {
            Intents = intents;
            AppID = appID;
            Secret = secret;
        }
        /// <summary>
        /// 创建一个空白的机器人信息类
        /// </summary>
        public BotInfo()
        {
        }
    }
}
