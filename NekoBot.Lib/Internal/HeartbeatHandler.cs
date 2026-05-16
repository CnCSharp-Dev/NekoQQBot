using NekoBot.Lib.Constants;
using NekoBot.Lib.Enums;
using NekoBot.Lib.Events;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.BasicModels;
using Newtonsoft.Json.Linq;

namespace NekoBot.Lib.Internal
{
    internal class HeartbeatHandler : StandardEvent
    {
        public override async Task OnHandlePayloadAsync(Payload payload)
        {
            if(payload.OpCode == OpCode.HeartbeatACK)
                Logger.Debug("WebSocket机器人心跳完成!");
            else
                UpdateSeq(payload.Serial);

            await Task.CompletedTask;
        }
        private int? _seq;
        public int? Seq => _seq;
        private void UpdateSeq(int? serial)
        {
            lock (this)
            {
                if (serial != null)
                {
                    _seq = serial;
                }
            }
        }
        private CancellationTokenSource _cts;
        private string _sessionID;
        public string SessionID => _sessionID;
        public void Start() => Start(_sessionID);
        public void Start(string sessionID)
        {
            Logger.Debug("开始心跳任务");

            _sessionID = sessionID;
            _cts?.Cancel();
            _cts = new();
            _ = HeartbeatTask(Manager.Get<HelloHandler>().HeartbeatInterval - ConnectConstant.Timeout, _cts.Token);
        }
        public void OnDisconnect()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
            }
        }
        private async Task HeartbeatTask(int interval, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Manager.Service.SocketModule.SendMessageAsync(new Payload()
                    {
                        OpCode = OpCode.Heartbeat,
                        Data = new JValue(_seq),
                    });

                    await Task.Delay(interval, cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
                Logger.Debug("已经取消心跳");
            }
            catch (Exception ex)
            {
                Logger.Debug($"心跳退出: {ex}");
            }
        }
        public override void Dispose()
        {
            base.Dispose();

            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
            }
        }
    }
}
