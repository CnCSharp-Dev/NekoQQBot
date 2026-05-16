using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Exceptions;
using NekoBot.Lib.Internal.AccessToken;
using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Lib
{
    /// <summary>
    /// 用于处理机器人服务的<see cref="HttpClient"/>
    /// </summary>
    /// <param name="service">机器人服务</param>
    public class BotHttpModule(BotService service) : IDisposable
    {
        /// <summary>
        /// 机器人服务
        /// </summary>
        public BotService Service { get; } = service;
        private readonly HttpClient _httpClient = new();
        internal readonly AccessTokenUpdater AccessTokenUpdater = new(service);
        /// <summary>
        /// 发送一个Get信息
        /// </summary>
        /// <typeparam name="TResponse">响应数据类型</typeparam>
        /// <param name="url">目标Url</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InterfaceException"></exception>
        public async Task<Response<TResponse>> GetAsync<TResponse>(string url)
        {
            Logger.Debug($"Get: {url}");

            _httpClient.DefaultRequestHeaders.Authorization = new("QQBot", await AccessTokenUpdater.GetAccessTokenAsync());

            var response = await _httpClient.GetAsync(url);
            var str = await response.Content.ReadAsStringAsync();
            var jToken = JToken.Parse(str);
            var res = new Response<TResponse>();

            Logger.Debug($"Get Response<无数据>: {url}");
            if (jToken is JObject jObj && jObj.ContainsKey("code"))
            {
                res.Code = jObj["code"].Value<int>();
                res.Message = jObj["message"].Value<string>();
                res.Data = default;
            }
            else
            {
                res.Code = 0;
                res.Message = "ok";
                res.Data = JsonConvert.DeserializeObject<TResponse>(str);
            }
            if (!res.IsSuccess)
            {
                var ex = new InterfaceException(res.Code, res.Message);
                throw ex;
            }
            return res;
        }
        /// <summary>
        /// 发送一个Get信息
        /// </summary>
        /// <typeparam name="TRequest">请求数据类型</typeparam>
        /// <typeparam name="TResponse">响应数据类型</typeparam>
        /// <param name="url">目标Url</param>
        /// <param name="requestData">请求的数据</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InterfaceException"></exception>
        public async Task<Response<TResponse>> GetAsync<TRequest, TResponse>(string url, TRequest requestData)
        {
            Logger.Debug($"Get: {url}");

            var json = JsonConvert.SerializeObject(requestData, Formatting.None);
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            foreach (var kv in _httpClient.DefaultRequestHeaders)
                request.Headers.Add(kv.Key, kv.Value);

            request.Headers.Authorization = new("QQBot", await AccessTokenUpdater.GetAccessTokenAsync());

            var response = await _httpClient.SendAsync(request);

            var str = await response.Content.ReadAsStringAsync();

            Logger.Debug($"Get Response: {url}");

            var jToken = JToken.Parse(str);
            var res = new Response<TResponse>();

            if (jToken is JObject jObj && jObj.ContainsKey("code"))
            {
                res.Code = jObj["code"].Value<int>();
                res.Message = jObj["message"].Value<string>();
                res.Data = default;
            }
            else
            {
                res.Code = 0;
                res.Message = "ok";
                res.Data = JsonConvert.DeserializeObject<TResponse>(str);
            }
            if (!res.IsSuccess)
            {
                var ex = new InterfaceException(res.Code, res.Message);
                throw ex;
            }
            return res;
        }
        /// <summary>
        /// 发送一个Get-Post信息
        /// </summary>
        /// <typeparam name="TRequest">请求数据类型</typeparam>
        /// <typeparam name="TResponse">响应数据类型</typeparam>
        /// <param name="url">目标Url</param>
        /// <param name="requestData">请求的数据</param>
        /// <param name="noAuth">是否不需要验证Token</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InterfaceException"></exception>
        public async Task<Response<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest requestData, bool noAuth = false)
        {
            Logger.Debug($"Post: {url}");

            if (!noAuth)
                _httpClient.DefaultRequestHeaders.Authorization = new("QQBot", await AccessTokenUpdater.GetAccessTokenAsync());

            var json = JsonConvert.SerializeObject(requestData, Formatting.None);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var str = await response.Content.ReadAsStringAsync();

            Logger.Debug($"Post: {url}");
            var jToken = JObject.Parse(str);
            var res = new Response<TResponse>();
            if (jToken is JObject jObj && jObj.ContainsKey("code"))
            {
                res.Code = jObj["code"].Value<int>();
                res.Message = jObj["message"].Value<string>();
                res.Data = default;
            }
            else
            {
                res.Code = 0;
                res.Message = "ok";
                res.Data = JsonConvert.DeserializeObject<TResponse>(str);
            }
            if (!res.IsSuccess)
            {
                var ex = new InterfaceException(res.Code, res.Message);
                throw ex;
            }
            return res;
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
