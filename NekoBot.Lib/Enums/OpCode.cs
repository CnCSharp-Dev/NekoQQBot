namespace NekoBot.Lib.Enums
{
    /// <summary>
    /// OpCode枚举，用于信息接受/发送
    /// </summary>
    public enum OpCode
    {
        /// <summary>
        /// 服务端进行消息推送
        /// </summary>
        Dispatch = 0,
        /// <summary>
        /// 客户端或服务端发送心跳
        /// </summary>
        Heartbeat = 1,
        /// <summary>
        /// 客户端发送鉴权
        /// </summary>
        Identify = 2,
        /// <summary>
        /// 客户端恢复连接
        /// </summary>
        Resume = 6,
        /// <summary>
        /// 服务端通知客户端重新连接
        /// </summary>
        Reconnect = 7,
        /// <summary>
        /// 当identify或resume的时候，如果参数有错，服务端会返回该消息
        /// </summary>
        InvalidSession = 9,
        /// <summary>
        /// 当客户端与网关建立WebSocket连接之后，网关下发的第一条消息
        /// </summary>
        Hello = 10,
        /// <summary>
        /// 当发送心跳成功之后，就会收到该消息
        /// </summary>
        HeartbeatACK = 11,
        /// <summary>
        /// 仅用于http回调模式的回包，代表机器人收到了平台推送的数据
        /// </summary>
        HttpCallbackACK = 12,
        /// <summary>
        /// 回环地址验证，开放平台对机器人服务端进行验证
        /// </summary>
        LoopVerify = 13
    }
}
