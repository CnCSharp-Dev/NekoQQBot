using NekoBot.Lib.Enums;
using NekoBot.Lib.Events;
using NekoBot.Lib.Events.Handlers;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.BasicModels;
using NekoBot.Lib.Models.Chat;

namespace NekoBot.Lib.Internal
{
    internal class MessageHandler : StandardEvent
    {
        public override async Task OnHandlePayloadAsync(Payload payload)
        {
            if (payload.OpCode != OpCode.Dispatch)
            {
                return;
            }

            Logger.Debug(payload.Type);

            if (payload.Type == "C2C_MESSAGE_CREATE")
            {
                await BotHandler.OnUserPrivateChat(new(Manager.Service, payload.Cast<ChatReceive>().Data));
            }
            else if (payload.Type == "C2C_GROUP_AT_MESSAGE_CREATE" || payload.Type == "GROUP_AT_MESSAGE_CREATE")
            {
                await BotHandler.OnUserGroupChat(new(Manager.Service, payload.Cast<ChatReceive>().Data));
            }
        }
    }
}
