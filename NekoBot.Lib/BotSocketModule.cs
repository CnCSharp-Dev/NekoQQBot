using NekoBot.Lib.Constants;
using NekoBot.Lib.Events;
using NekoBot.Lib.Events.Handlers;
using NekoBot.Lib.Internal;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.BasicModels;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using WatsonWebsocket;

namespace NekoBot.Lib
{
    /// <summary>
    /// 机器人WebSocket核心模块
    /// </summary>
    public class BotSocketModule : IDisposable
    {
        /// <summary>
        /// 表示获取到的WebSocket连接的地址
        /// </summary>
        public string Url { get; private set; } = string.Empty;
        private Task reconnectTask;
        private WatsonWsClient _webSocket;
        private readonly BotService _botService;
        private readonly EventManager _manager;
        /// <summary>
        /// 机器人服务核心
        /// </summary>
        public BotService Service => _botService;

        /// <param name="service">机器人服务</param>
        public BotSocketModule(BotService service)
        {
            _botService = service;
            _manager = new(service);

            foreach (var eventType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if(eventType.IsSubclassOf(typeof(StandardEvent)))
                    _manager.Add(eventType);
            }
        }
        private bool _disconnect = false;
        /// <summary>
        /// 异步启动机器人WebSocket
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            Url = await SocketUrlManager.AsycnActiveUrl(_botService);

            _webSocket?.Dispose();
            _webSocket = new(new(Url));
            _webSocket.ServerConnected += OnServerConnected;
            _webSocket.ServerDisconnected += OnServerDisconnected;
            _webSocket.MessageReceived += OnMessageReceived;
            await _webSocket.StartAsync();
        }
        /// <summary>
        /// 异步停止机器人WebSocket
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync()
        {
            _disconnect = true;
            BotHandler.OnWebSocketClose(this);
            Logger.Warning("已终止WebSocket服务!");
            await _webSocket.StopAsync();
        }
        /// <summary>
        /// 发送一段信息至WebSocketURL
        /// </summary>
        /// <param name="req">信息</param>
        /// <returns></returns>
        public async Task SendMessageAsync(Payload req)
        {
            var data = JsonConvert.SerializeObject(req, Formatting.None);
            Logger.Debug($"WebSocket发送 : " + data);
            await _webSocket.SendAsync(Encoding.UTF8.GetBytes(data));
        }
        /// <summary>
        /// 发送一段信息至WebSocketURL
        /// </summary>
        /// <typeparam name="T">信息数据类型</typeparam>
        /// <param name="req">信息</param>
        /// <returns></returns>
        public async Task SendMessageAsync<T>(Payload<T> req)
        {
            var data = JsonConvert.SerializeObject(req, Formatting.None);
            Logger.Debug($"WebSocket发送 : " + data);
            await _webSocket.SendAsync(Encoding.UTF8.GetBytes(data));
        }
        private async void OnMessageReceived(object sender, MessageReceivedEventArgs ev)
        {
            var data = Encoding.UTF8.GetString(ev.Data);
            Logger.Debug($"WebSocket接收 : " + data);
            var payload = JsonConvert.DeserializeObject<Payload>(data);
            BotHandler.OnReceivedMessage(_botService,payload);

            await _manager.DispatchAsync(payload);
        }
        private void OnServerConnected(object sender, EventArgs ev)
        {
            _disconnect = false;
            BotHandler.OnWebSocketConnected(this);
            Logger.Info($"连接WebSocket地址: " + Url);
        }
        private void OnServerDisconnected(object sender, EventArgs ev)
        {
            if (_disconnect)
                return;
            BotHandler.OnWebSocketDisconnected(this);
            Logger.Info("WebSocket与服务器断开连接，计划在" + ConnectConstant.Timeout + "毫秒后重连");
            _manager.Get<HeartbeatHandler>().OnDisconnect();
            lock (this)
            {
                reconnectTask ??= ReconnectTask();
            }
        }
        private async Task ReconnectTask()
        {
            await Task.Delay(ConnectConstant.Timeout);
            await StartAsync();
            lock (this)
            {
                reconnectTask = null;
            }
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            _manager.Dispose();
            _webSocket.Dispose();
        }
    }
}
