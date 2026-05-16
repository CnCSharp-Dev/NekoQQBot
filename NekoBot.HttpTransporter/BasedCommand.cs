using NekoBot.Service.Attributes;
using NekoBot.Service.Commands;

namespace NekoBot.HttpTransporter
{
    [UnloadCommand]
    public class BasedCommand(string command, string description) : IBasicCommand
    {
        public string Command { get; } = command;
        public string Description { get; } = description;
    }
}
