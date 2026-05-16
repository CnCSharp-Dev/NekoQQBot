using NekoBot.Lib.Models.Basic;
using Newtonsoft.Json;

namespace NekoBot.Lib.Models
{
    /// <summary>
    /// 表示返回的准备信息
    /// </summary>
    public struct ReadyResponse
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        [JsonProperty("version")]
        public int Version;
        /// <summary>
        /// 鉴权完成后的SessionId
        /// </summary>
        [JsonProperty("session_id")]
        public string SessionID;
        /// <summary>
        /// 该准备信息所有机器人在群聊的用户结构
        /// </summary>
        [JsonProperty("user")]
        public User User;
        /// <summary>
        /// 提供的分片逻辑，事件通知会落在不同的分片上，该参数是个拥有两个元素的数组
        /// </summary>
        [JsonProperty("shard")]
        public List<int> Shard;
    }
}
