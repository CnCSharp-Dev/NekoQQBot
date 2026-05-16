using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Attributes;
using NekoBot.Service.Commands;

namespace NekoBot.ExtensionCommands
{
    [HideCommand]
    public class EchoCommand : ICommand
    {
        public string Command { get; } = "echo";
        public string Description { get; } = "复读发送的消息";
        public string OnExecute(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            if(arguments.Length != 0)
            {
                return "❌ 由于当前QQ审核限制，暂不支持该指令！";
                //return string.Join(" ", arguments);
            }
            return "❌ 你没有发送任何消息！";
        }
    }
}
