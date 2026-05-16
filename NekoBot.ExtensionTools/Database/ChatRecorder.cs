using LiteDB;
using NekoBot.Lib.EventBus;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service;

namespace NekoBot.ExtensionTools.Database
{
    internal class ChatRecorder : CustomEvent
    {
        private ConnectionString _connectionString;
        public override async Task OnBotUserGroupChat(GroupChatEventArgs ev)
        {
            if (MainPlugin.Instance.Config.RecordGroupChat)
            {
                using LiteDatabase db = new(_connectionString);
                var collection = db.GetCollection<ChatReceive>("group_chat");
                collection.Insert(ev.Message);
            }
            await Task.CompletedTask;
        }
        public override async Task OnBotUserPrivateChat(PrivateChatEventArgs ev)
        {
            if (MainPlugin.Instance.Config.RecordPrivateChat)
            {
                using LiteDatabase db = new(_connectionString);
                var collection = db.GetCollection<ChatReceive>("private_chat");
                collection.Insert(ev.Message);
            }
            await Task.CompletedTask;
        }
        public override void OnRegisterEvents()
        {
            _connectionString = new ConnectionString
            {
                Filename = Path.Combine(Paths.Databases, "crlog.db"),
                Connection = ConnectionType.Shared,
                ReadOnly = false,
            };
        }
    }
}
