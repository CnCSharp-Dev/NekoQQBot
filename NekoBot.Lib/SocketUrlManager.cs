using NekoBot.Lib.Models;

namespace NekoBot.Lib
{
    /// <summary>
    /// 一个管理类，用于获取进行WebSocket的地址
    /// </summary>
    public class SocketUrlManager
    {
        /// <summary>
        /// WebSocket的Url
        /// </summary>
        public static string Url { get; private set; } = string.Empty;
        /// <summary>
        /// 尝试激活Url
        /// </summary>
        public static async Task<string> AsycnActiveUrl(BotService context)
        {
            var res = await context.HttpModule.GetAsync<GatewayResponse>("https://api.sgroup.qq.com/gateway");

            Url = res.Data.Url;

            return res.Data.Url;
        }
    }
}
