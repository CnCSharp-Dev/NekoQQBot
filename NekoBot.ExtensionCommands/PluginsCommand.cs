using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service;
using NekoBot.Service.Commands;
using NekoBot.Service.Extensions;
using PoolingLib.Pools;

namespace NekoBot.ExtensionCommands
{
    public class PluginsCommand : ICommand
    {
        public string Command { get; } = "plugins";
        public string Description { get; } = "当前加载的插件";
        public string OnExecute(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            var builder = StringBuilderPool.Pool.Get();

            if (PluginLoader.Plugins.Count != 0)
            {
                builder.AppendLine("✨ 加载的插件：");
                int i = 0;
                foreach (var plugin in PluginLoader.Plugins)
                {
                    i++;
                    var plg = plugin.Key;
                    string mark = plg.PluginType.IsOfficial() ? "⭐" : "  ";
                    string typeEmoji = plg.PluginType.ParseWithEmoji();
                    builder.AppendFormat("{0} {1}. {2} - {3}", mark, i, typeEmoji, plg.Name).AppendLine();
                }
            }
            else
            {
                builder.AppendLine("❌ 未安装任何插件！");
            }

            return StringBuilderPool.Pool.ToStringReturn(builder);
        }
    }
}