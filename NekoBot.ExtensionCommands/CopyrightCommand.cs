using NekoBot.Lib;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Commands;
using PoolingLib.Pools;

namespace NekoBot.ExtensionCommands
{
    public class CopyrightCommand : ICommand
    {
        private static Version _frameworkVersion;
        private static Version FrameworkVersion
        { 
            get 
            {
                if (_frameworkVersion == null)
                    _frameworkVersion = typeof(BotService).Assembly.GetName().Version;
                return _frameworkVersion;
            } 
        }
        public string Command => "copyright";
        public string Description => "当前框架的版权信息";
        public string OnExecute(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            var builder = StringBuilderPool.Pool.Get();

            builder.AppendFormat("✨ NekoBot Official Framework - ver.{0}",FrameworkVersion.ToString(3)).AppendLine();
            builder.AppendLine("━━━━━━━━━━━━━━━━━━━━━");
            builder.AppendLine($"👥 开发者：CNCSharp-DevTeam");
            builder.AppendLine($"🗺 仓库(github)：CNCSharp-dev/NekoBot");
            builder.AppendLine("━━━━━━━━━━━━━━━━━━━━━");
            builder.AppendLine("📌 框架处于早期测试版，出现问题请尽快上报");
            
            return StringBuilderPool.Pool.ToStringReturn(builder);
        }
    }
}
