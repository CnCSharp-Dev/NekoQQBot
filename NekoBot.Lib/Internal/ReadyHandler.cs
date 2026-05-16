using NekoBot.Lib.Enums;
using NekoBot.Lib.Events;
using NekoBot.Lib.Events.Handlers;
using NekoBot.Lib.Models;
using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Lib.Internal
{
    internal class ReadyHandler : StandardEvent
    {
        public override async Task OnHandlePayloadAsync(Payload payload)
        {
            if (payload.OpCode != OpCode.Dispatch)
            {
                return;
            }
            if (payload.Type != "READY")
            {
                return;
            }
            OnReady(payload.Cast<ReadyResponse>());
            await Task.CompletedTask;
        }
        private void OnReady(Payload<ReadyResponse> payload)
        {
            Manager.Get<HeartbeatHandler>().Start(payload.Data.SessionID);
            Manager.Service.BotUser = payload.Data.User;

            BotHandler.OnReady(Service);
        }
    }
}
