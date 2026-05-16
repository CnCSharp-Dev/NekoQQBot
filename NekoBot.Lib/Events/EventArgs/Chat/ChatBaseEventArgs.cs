using NekoBot.Lib.Models.Chat;

namespace NekoBot.Lib.Events.EventArgs.Chat
{
    /// <summary>
    /// 表示为聊天消息相关事件
    /// </summary>
    public abstract class ChatBaseEventArgs<TMessage, TRequest, TResponse> : BotServiceEventArgs
    {
        /// <summary>
        /// 当前聊天的信息
        /// </summary>
        public TMessage Message { get; }
        internal ChatBaseEventArgs(BotService service, TMessage message) : base(service)
        {
            Message = message;
        }
        /// <summary>
        /// 快速回复消息
        /// </summary>
        /// <param name="request">要发送的消息</param>
        /// <returns>消息返回对象</returns>
        public abstract Task<ChatResponse> ReplyAsync(ChatRequest request);
    }
}
