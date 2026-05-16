using NekoBot.Lib.Enums;
using NekoBot.Lib.EventBus;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Loggers;
using NekoBot.Service.Helpers;

namespace NekoBot.ConsoleProgram.Internal
{
    internal class ChatHandler : CustomEvent
    {
        public override async Task OnBotUserPrivateChat(PrivateChatEventArgs ev)
        {
            Logger.Debug($"接受\"" + ev.Message.Author.Id + "\"发送的私信聊天信息:" + ev.Message.Content.Replace("\n", ""));

            var msg = await CommandHandler.ExecuteAsync(ev);
            if (!string.IsNullOrEmpty(msg))
            {
                await ev.ReplyAsync(new()
                {
                    Content = msg,
                    Type = MessageType.Text,
                    MessageId = ev.Message.Id,
                });
            }
        }
        public override async Task OnBotUserGroupChat(GroupChatEventArgs ev)
        {
            Logger.Debug($"接受\"" + ev.Message.Author.Id + "\"发送的群聊聊天信息:" + ev.Message.Content.Replace("\n", ""));

            var msg = await CommandHandler.ExecuteAsync(ev);
            if (!string.IsNullOrEmpty(msg))
            {
                await ev.ReplyAsync(new()
                {
                    Content = msg,
                    Type = MessageType.Text,
                    MessageId = ev.Message.Id,
                });
            }
        }
    }
}
