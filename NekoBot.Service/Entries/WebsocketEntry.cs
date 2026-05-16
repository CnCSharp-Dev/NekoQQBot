using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Service.Entries
{
    /// <summary>
    /// 用于记录到数据库的WebSocket日志信息
    /// </summary>
    /// <param name="Payload">接收到的<see cref="Payload{T}"/></param>
    /// <param name="BotAppId">机器人AppId</param>
    /// <param name="BotNickname">机器人昵称，如果未加载机器人则为<see cref="string.Empty"/></param>
    public record struct WebsocketEntry(Payload Payload, string BotAppId, string BotNickname);
}
