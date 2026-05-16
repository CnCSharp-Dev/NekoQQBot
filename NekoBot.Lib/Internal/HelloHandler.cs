using NekoBot.Lib.Enums;
using NekoBot.Lib.Events;
using NekoBot.Lib.Models;
using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Lib.Internal
{
    internal class HelloHandler : StandardEvent
    {
        public override async Task OnHandlePayloadAsync(Payload payload)
        {
            if (payload.OpCode != OpCode.Hello)
            {
                return;
            }
            await OnHello(payload.Cast<HelloResponse>());
        }
        public int HeartbeatInterval { get;internal set; }

        private async Task OnHello(Payload<HelloResponse> payload)
        {
            HeartbeatInterval = payload.Data.HeartbeatInterval;

            var accessToken = await Service.HttpModule.AccessTokenUpdater.GetAccessTokenAsync();
            var heartbeatEvent = Manager.Get<HeartbeatHandler>();
            if (string.IsNullOrEmpty(heartbeatEvent.SessionID))
                await ConnectAsync(accessToken);
            else
                await ReconnectAsync(accessToken, heartbeatEvent.SessionID, heartbeatEvent.Seq);
        }
        private async Task ConnectAsync(string accessToken)
        {
            await Service.SocketModule.SendMessageAsync<IdentifyRequest>(new()
            {
                OpCode = OpCode.Identify,
                Data = new()
                {
                    Token = $"QQBot {accessToken}",
                    Intents = Intents.ALL_PUBLIC,
                    Shard = [0, 1],
                    Properties = Service.Properties,
                },
            });
        }
        private async Task ReconnectAsync(string accessToken, string sessionID, int? seq)
        {
            await Manager.Service.SocketModule.SendMessageAsync<ResumeRequest>(new()
            {
                OpCode = OpCode.Resume,
                Data = new()
                {
                    Token = $"QQBot {accessToken}",
                    SessionId = sessionID,
                    Serial = seq,
                },
            });
        }
    }
}
