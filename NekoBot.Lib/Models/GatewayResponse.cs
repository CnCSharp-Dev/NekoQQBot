using Newtonsoft.Json;

namespace NekoBot.Lib.Models
{
    /// <summary>
    /// 获取的WebSocket地址，能建立长连接WebSocket
    /// </summary>
    public struct GatewayResponse
    {
        /// <summary>
        /// 返回的Url地址
        /// </summary>
        [JsonProperty("url")]
        public string Url;
        /// <inheritdoc/>
        public readonly override string ToString()
        {
            return $"Url={Url}";
        }
    }
}
