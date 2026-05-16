using Newtonsoft.Json;

namespace NekoBot.Lib.Models.BasicModels
{
    /// <summary>
    /// 返回的信息
    /// </summary>
    /// <typeparam name="T">信息类型</typeparam>
    public struct Response<T>
    {
        /// <summary>
        /// 返回的代码
        /// </summary>
        [JsonProperty("code")]
        public int Code;
        /// <summary>
        /// 返回的信息
        /// </summary>
        [JsonProperty("message")]
        public string Message;
        /// <summary>
        /// 返回是否成功
        /// </summary>
        [JsonIgnore]
        public readonly bool IsSuccess => Code == 0;
        /// <summary>
        /// 返回的数据
        /// </summary>
        [JsonProperty("data")]
        public T Data;
        /// <summary>
        /// 判断返回信息是否一致
        /// </summary>
        /// <param name="response">另一个返回的信息</param>
        public static implicit operator T(Response<T> response) => response.Data;
    }
    /// <summary>
    /// 返回的信息
    /// </summary>
    internal struct Response
    {
        /// <summary>
        /// 返回的代码
        /// </summary>
        [JsonProperty("code")]
        public int Code;
        /// <summary>
        /// 返回的信息
        /// </summary>
        [JsonProperty("message")]
        public string Message;
        /// <summary>
        /// 返回是否成功
        /// </summary>
        [JsonIgnore]
        public readonly bool IsSuccess => Code == 0;
    }
}
