using NekoBot.Lib.Models.Chat;

namespace NekoBot.Lib.Events.EventArgs.Chat
{
    /// <summary>
    /// 用户单独聊天事件
    /// </summary>
    /// <param name="service">机器人服务</param>
    /// <param name="message">信息</param>
    public class PrivateChatEventArgs(BotService service, ChatReceive message) : ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse>(service, message)
    {
        /// <inheritdoc/>
        public override async Task<ChatResponse> ReplyAsync(ChatRequest request)
        {
            request.MessageId = Message.Id;
            return await Service.Manager.SendUserMessageAsync(request, Message.Author.Id);
        }
    }
}
