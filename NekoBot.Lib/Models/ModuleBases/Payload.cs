using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NekoBot.Lib.Enums;

namespace NekoBot.Lib.Models.BasicModels
{
    /// <summary>
    /// 连接上传输的数据，网关的上下行消息采用的都是同一个结构
    /// </summary>
    /// <typeparam name="T">当前事件泛型</typeparam>
    public struct Payload<T>
    {
        /// <summary>
        /// 操作码
        /// </summary>
        [JsonProperty("op")]
        public OpCode OpCode;
        /// <summary>
        /// 当<see cref="OpCode"/>为<see cref="OpCode.Dispatch"/>时，代表事件内容
        /// </summary>
        [JsonProperty("d")]
        public T Data;
        /// <summary>
        /// 当前返回的唯一序列号，客户端发送心跳时必须携带此序列号
        /// </summary>
        [JsonProperty("s")]
        public int? Serial;
        /// <summary>
        /// 当<see cref="OpCode"/>为<see cref="OpCode.Dispatch"/>时，代表事件类型
        /// </summary>
        [JsonProperty("t")]
        public string Type;
    }
    /// <summary>
    /// 连接上传输的数据，网关的上下行消息采用的都是同一个结构
    /// </summary>
    public struct Payload
    {
        /// <summary>
        /// 操作码
        /// </summary>
        [JsonProperty("op")]
        public OpCode OpCode;
        /// <summary>
        /// 当<see cref="OpCode"/>为<see cref="OpCode.Dispatch"/>时，代表事件内容
        /// </summary>
        [JsonProperty("d")]
        public JToken Data;
        /// <summary>
        /// 当前返回的唯一序列号，客户端发送心跳时必须携带此序列号
        /// </summary>
        [JsonProperty("s")]
        public int? Serial;
        /// <summary>
        /// 当<see cref="OpCode"/>为<see cref="OpCode.Dispatch"/>时，代表事件类型
        /// </summary>
        [JsonProperty("t")]
        public string Type;

        /// <summary>
        /// 将<see cref="Payload"/>转化为对应的<see cref="Payload{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Payload<T> Cast<T>(Payload payload)
        {
            return new()
            {
                OpCode = payload.OpCode,
                Data = JsonConvert.DeserializeObject<T>(payload.Data.ToString(Formatting.None)),
                Serial = payload.Serial,
                Type = payload.Type,
            };
        }
        /// <summary>
        /// 将<see cref="Payload"/>转化为对应的<see cref="Payload{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Payload<T> Cast<T>()
        {
            return new()
            {
                OpCode = OpCode,
                Data = JsonConvert.DeserializeObject<T>(Data.ToString(Formatting.None)),
                Serial = Serial,
                Type = Type,
            };
        }
    }
}
