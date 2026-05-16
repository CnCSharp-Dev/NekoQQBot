using NekoBot.Lib;
using NekoBot.Lib.EventBus;
using NekoBot.Lib.Loggers;

namespace NekoBot.ConsoleProgram.Internal
{
    internal class ConsoleHandler : CustomEvent
    {
        public override void OnBotReady(BotService service)
        {
            Logger.Info($"成功鉴权，用户名: \"{Program.Service.BotUser.UserName}\"");
            Console.Title = ConfigLoader.ProgramConfig.ConsoleTitleTemplate
                .Replace("{BotName}", service.BotUser.UserName)
                .Replace("{WebSocketUrl}", service.SocketModule.Url)
                .Replace("{LogLevel}", ConfigLoader.ProgramConfig.LogLevel);
        }
        public override void OnRegisterEvents()
        {
            Console.Title = ConfigLoader.ProgramConfig.ConsoleTitleTemplate
                .Replace("{BotName}", "Unknown")
                .Replace("{WebSocketUrl}", "Unknown")
                .Replace("{LogLevel}", "Information");
        }
    }
}
