using System.Reflection;
using NekoBot.Lib.Loggers;

namespace NekoBot.Service.Commands
{
    internal static class CommandRegistry
    {
        public static Dictionary<string, IBasicCommand> Commands { get; internal set; } = [];
        public static void Init()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                try
                {
                    if (typeof(IBasicCommand).IsAssignableFrom(type))
                    {
                        if (Activator.CreateInstance(type) is IBasicCommand cmd)
                        {
                            UnregisterCommand(cmd);
                            RegisterCommand(cmd);
                        }
                    }
                }
                catch(Exception ex) { Logger.Error("注册命令失败:" + ex.ToString()); }
            }
        }
        public static bool RegisterCommand(IBasicCommand command)
        {
            if (Commands.ContainsKey(command.Command))
                return false;
            Commands[command.Command.ToUpper()] = command;
            return true;
        }
        public static bool UnregisterCommand(IBasicCommand command)
        {
            if (!Commands.ContainsKey(command.Command.ToUpper()))
                return true;
            return Commands.Remove(command.Command.ToUpper());
        }
    }
}
