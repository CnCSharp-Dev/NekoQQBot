using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Lib.Events.Handlers
{
    /// <summary>
    /// 一个机器人事件的集合类，一般机器人都会指向单一的机器人
    /// </summary>
    public static class BotHandler
    {
        /// <summary>
        /// 当与机器人私聊聊天时触发此事件
        /// </summary>
        public static event AsyncActionHandler<PrivateChatEventArgs> UserPrivateChat;
        internal static async Task OnUserPrivateChat(PrivateChatEventArgs ev)
        {
            try
            {
                await UserPrivateChat.Invoke(ev);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                await Task.CompletedTask;
            }
        }
        /// <summary>
        /// 当与机器人群聊聊天时触发此事件
        /// </summary>
        public static event AsyncActionHandler<GroupChatEventArgs> UserGroupChat;
        internal static async Task OnUserGroupChat(GroupChatEventArgs ev)
        {
            try
            {
                await UserGroupChat.Invoke(ev);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                await Task.CompletedTask;
            }
        }
        /// <summary>
        /// 当机器人准备就绪时触发此事件
        /// </summary>
        public static event Action<BotService> Ready;
        internal static void OnReady(BotService service) => Ready?.Invoke(service);
        /// <summary>
        /// 当机器人响应WebSocket信息时触发此事件
        /// </summary>
        public static event Action<BotService,Payload> ReceivedMessage;
        internal static void OnReceivedMessage(BotService service,Payload payload)
        {
            ReceivedMessage?.Invoke(service,payload);
        }
        /// <summary>
        /// 当机器人与WebSocket服务器连接时触发此事件
        /// </summary>
        public static event Action<BotSocketModule> WebSocketConnected;
        internal static void OnWebSocketConnected(BotSocketModule module)
        {
            WebSocketConnected?.Invoke(module);
        }
        /// <summary>
        /// 当机器人与WebSocket服务器断开连接时触发此事件
        /// </summary>
        public static event Action<BotSocketModule> WebSocketDisconnected;
        internal static void OnWebSocketDisconnected(BotSocketModule module)
        {
            WebSocketDisconnected?.Invoke(module);
        }
        /// <summary>
        /// 当机器人关闭WebSocket连接时触发此事件
        /// </summary>
        public static event Action<BotSocketModule> WebSocketClose;
        internal static void OnWebSocketClose(BotSocketModule module)
        {
            WebSocketClose?.Invoke(module);
        }
    }
}
