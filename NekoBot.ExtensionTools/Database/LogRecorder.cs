using LiteDB;
using NekoBot.Lib;
using NekoBot.Lib.EventBus;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.BasicModels;
using NekoBot.Service;
using NekoBot.Service.Entries;

namespace NekoBot.ExtensionTools.Database
{
    internal class LogRecorder : CustomEvent
    {
        private ConnectionString _connectionString;
        public override void OnBotReceivedMessage(BotService service, Payload payload)
        {
            if (!MainPlugin.Instance.Config.RecordWebsocketMessage)
                return;

            using LiteDatabase db = new(_connectionString);
            var collection = db.GetCollection<WebsocketEntry>("wsmsg");

            var hasName = string.IsNullOrEmpty(service.BotUser.UserName);

            collection.Insert(new WebsocketEntry()
            {
                BotAppId = service.Info.AppID,
                BotNickname = hasName ? service.BotUser.UserName : string.Empty,
                Payload = payload
            });
        }
        private void OnLogSend(string msg, LogType type)
        {
            using LiteDatabase db = new(_connectionString);
            var collection = db.GetCollection<LogEntry>("logs");
            collection.Insert(new LogEntry()
            {
                LogType = type,
                Message = msg
            });
        }
        public override void OnRegisterEvents()
        {
            if (MainPlugin.Instance.Config.RecordLog)
            {
                Logger.OnSendInfo += OnLogSend;
            }
            _connectionString = new ConnectionString
            {
                Filename = Path.Combine(Paths.Databases, "log.db"),
                Connection = ConnectionType.Shared,
                ReadOnly = false,
            };
        }
        public override void OnUnregisterEvents()
        {
            if (MainPlugin.Instance.Config.RecordLog)
            {
                Logger.OnSendInfo -= OnLogSend;
            }
        }
    }
}
