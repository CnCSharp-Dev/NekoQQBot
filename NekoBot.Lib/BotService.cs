using System.Net.WebSockets;
using NekoBot.Lib.Models.Basic;

namespace NekoBot.Lib
{
    /// <summary>
    /// 机器人服务
    /// </summary>
    public class BotService
    {
        /// <summary>
        /// 表示机器人的用户，当用户信息响应后才不为<see langword="default"/>
        /// </summary>
        public User BotUser { get; internal set; }
        /// <summary>
        /// 通过机器人信息新建一个机器人服务
        /// </summary>
        /// <param name="info">机器人信息</param>
        public BotService(BotInfo info)
        {
            Info = info;
            HttpModule = new(this);
            SocketModule = new(this);
            Manager = new(this);
        }
        /// <summary>
        /// 负责处理机器人<see cref="WebSocket"/>的类
        /// </summary>
        public BotSocketModule SocketModule { get; }
        /// <summary>
        /// 负责处理机器人<see cref="HttpClient"/>的类
        /// </summary>
        public BotHttpModule HttpModule { get; }
        /// <summary>
        /// 机器人管理类
        /// </summary>
        public BotManager Manager { get; }
        /// <summary>
        /// 机器人的信息
        /// </summary>
        public BotInfo Info { get; }
        /// <summary>
        /// 无实际作用，可以用于添加标记内容
        /// </summary>
        public Dictionary<string, string> Properties { get; set; } = new()
        {
            ["$library"] = "NekoBot.Lib"
        };
        /// <summary>
        /// 异步开启机器人服务
        /// </summary>
        public async Task StartAsync()
        {
            await SocketModule.StartAsync();
        }
        /// <summary>
        /// 异步关闭机器人服务
        /// </summary>
        public async Task StopAsync()
        {
            await SocketModule.StopAsync();
        }
    }
}
