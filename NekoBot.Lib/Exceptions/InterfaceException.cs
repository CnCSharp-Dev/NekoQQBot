namespace NekoBot.Lib.Exceptions
{
    /// <summary>
    /// 一个基本的API错误类型
    /// </summary>
    public class InterfaceException : Exception
    {
        /// <summary>
        /// 创建一个基本的<see cref="InterfaceException"/>类
        /// </summary>
        public InterfaceException() : base() { }
        /// <summary>
        /// 创建一个基本的<see cref="InterfaceException"/>类
        /// </summary>
        public InterfaceException(string message) : base(message) { }
        /// <summary>
        /// 创建一个基本的<see cref="InterfaceException"/>类
        /// </summary>
        public InterfaceException(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ResponseMessage { get; }
        internal InterfaceException(int code, string message) : base($"返回代码:\"{code}\"，无法连接机器人：{message}")
        {
            Code = code;
            ResponseMessage = message;
        }
        internal InterfaceException(int code, string message, Exception innerException) : base($"返回代码:\"{code}\"，无法连接机器人：{message}", innerException)
        {
            Code = code;
            ResponseMessage = message;
        }
    }
}
