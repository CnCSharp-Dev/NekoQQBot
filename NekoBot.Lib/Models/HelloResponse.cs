using Newtonsoft.Json;

namespace NekoBot.Lib.Models
{
    /// <summary>
    /// 表示连接成功WebSocket后返回的信息
    /// </summary>
    public struct HelloResponse
    {
        /// <summary>
        /// 心跳的周期
        /// </summary>
        [JsonProperty("heartbeat_interval")]
        public int HeartbeatInterval;
    }
}
