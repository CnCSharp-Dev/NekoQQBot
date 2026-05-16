using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Events.Handlers;
using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Lib.EventBus
{
    /// <summary>
    /// 负责提供自动注册事件的抽象类，不需要强制实现方法
    /// </summary>
    public abstract class CustomEvent : IEventHandler
    {
        /// <summary>
        /// 当机器人准备就绪时调用此方法
        /// </summary>
        /// <param name="service">机器人服务</param>
        public virtual void OnBotReady(BotService service)
        {

        }
        /// <summary>
        /// 当机器人接收到WebSocket信息时调用此方法
        /// </summary>
        /// <param name="service">机器人服务</param>
        /// <param name="payload">转换后的<see cref="Payload"/></param>
        public virtual void OnBotReceivedMessage(BotService service, Payload payload)
        {

        }
        /// <summary>
        /// 当机器人接收到私聊消息时调用此方法
        /// </summary>
        /// <param name="ev">事件封装类</param>
        /// <returns></returns>
        public virtual async Task OnBotUserPrivateChat(PrivateChatEventArgs ev)
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// 当机器人接收到群聊@聊天消息时调用此方法
        /// </summary>
        /// <param name="ev">事件封装类</param>
        /// <returns></returns>
        public virtual async Task OnBotUserGroupChat(GroupChatEventArgs ev)
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// 当机器人WebSocket关闭时调用此方法
        /// </summary>
        /// <param name="module">机器人<see cref="BotSocketModule"/>模块</param>
        public virtual void OnBotWebSocketClose(BotSocketModule module)
        {

        }
        /// <summary>
        /// 当机器人WebSocket连接时调用此方法
        /// </summary>
        /// <param name="module">机器人<see cref="BotSocketModule"/>模块</param>
        public virtual void OnBotWebSocketConnected(BotSocketModule module)
        {

        }
        /// <summary>
        /// 当机器人WebSocket断开连接时调用此方法
        /// </summary>
        /// <param name="module">机器人<see cref="BotSocketModule"/>模块</param>
        public virtual void OnBotWebSocketDisconnected(BotSocketModule module)
        {

        }
        /// <summary>
        /// 当注册事件时调用
        /// </summary>
        public virtual void OnRegisterEvents()
        {

        }
        /// <summary>
        /// 当注销事件时调用
        /// </summary>
        public virtual void OnUnregisterEvents()
        {

        }
        /// <inheritdoc/>
        public void RegisterEvents()
        {
            BotHandler.Ready += OnBotReady;
            BotHandler.ReceivedMessage += OnBotReceivedMessage;

            BotHandler.UserGroupChat += OnBotUserGroupChat;
            BotHandler.UserPrivateChat += OnBotUserPrivateChat;

            BotHandler.WebSocketConnected += OnBotWebSocketConnected;
            BotHandler.WebSocketDisconnected += OnBotWebSocketDisconnected;
            BotHandler.WebSocketClose += OnBotWebSocketClose;
            OnRegisterEvents();
        }
        /// <inheritdoc/>
        public void UnregisterEvents()
        {
            BotHandler.Ready -= OnBotReady;
            BotHandler.ReceivedMessage -= OnBotReceivedMessage;

            BotHandler.UserGroupChat -= OnBotUserGroupChat;
            BotHandler.UserPrivateChat -= OnBotUserPrivateChat;

            BotHandler.WebSocketConnected -= OnBotWebSocketConnected;
            BotHandler.WebSocketDisconnected -= OnBotWebSocketDisconnected;
            BotHandler.WebSocketClose -= OnBotWebSocketClose;
            OnUnregisterEvents();
        }
    }
}