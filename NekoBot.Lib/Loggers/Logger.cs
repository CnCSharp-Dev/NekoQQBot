namespace NekoBot.Lib.Loggers
{
    /// <summary>
    /// 日志封装类
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 当日志获取信息时自动触发此事件
        /// </summary>
        public static event Action<string, LogType> OnSendInfo;
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        public static void Info(object msg) => OnSendInfo?.Invoke(msg != null ? msg.ToString() : "null", LogType.Info);
        /// <summary>
        /// 发送错误信息
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        public static void Error(object msg) => OnSendInfo?.Invoke(msg != null ? msg.ToString() : "null", LogType.Error);
        /// <summary>
        /// 发送严重错误信息
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        public static void Fatal(object msg) => OnSendInfo?.Invoke(msg != null ? msg.ToString() : "null" , LogType.Fatal);
        /// <summary>
        /// 发送调试信息
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        public static void Debug(object msg) => OnSendInfo?.Invoke(msg != null ? msg.ToString() : "null", LogType.Debug);
        /// <summary>
        /// 发送警告信息
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        public static void Warning(object msg) => OnSendInfo?.Invoke(msg != null ? msg.ToString() : "null", LogType.Warning);
    }
}
