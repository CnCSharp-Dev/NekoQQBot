using System.Reflection;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Attributes;
using NekoBot.Service.Commands;
using NekoBot.Service.Helpers;
using PoolingLib.Pools;

namespace NekoBot.ExtensionCommands
{
    internal class HelpCommand : ICommand
    {
        public string Command { get; } = "help";
        public string Description { get; } = "获取所有指令及其帮助";
        public string OnExecute(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            var builder = StringBuilderPool.Pool.Get();

            var list = CommandHandler.Commands.Where(x => !string.IsNullOrEmpty(x.Description) && x.GetType().GetCustomAttribute<HideCommandAttribute>() == null);
            if (list.Any())
            {
                builder.AppendLine($"✨ 共 {list.Count()} 项命令:");
            }
            else
            {
                builder.AppendLine($"❌ 没有任何指令被注册!");
            }

            foreach (var p in list)
            {
                builder.AppendLine($"/{p.Command} \t {p.Description}");
            }

            return StringBuilderPool.Pool.ToStringReturn(builder);
        }
    }
}