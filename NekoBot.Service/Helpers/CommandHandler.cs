using System.Reflection;
using NekoBot.Lib.EventBus;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Extensions;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Attributes;
using NekoBot.Service.Commands;

namespace NekoBot.Service.Helpers
{
    /// <summary>
    /// 命令管理类
    /// </summary>
    public class CommandHandler : CustomEvent
    {
        private static readonly Dictionary<string, IBasicCommand> _commands = [];
        /// <summary>
        /// 所有被注册的命令
        /// </summary>
        public static IEnumerable<IBasicCommand> Commands => _commands.Values;
        /// <summary>
        /// 将一个<see cref="IBasicCommand"/>注册到命令里面
        /// </summary>
        /// <param name="command">命令</param>
        public static void RegisterCommand(IBasicCommand command)
        {
            _commands.TryAdd(command.Command.ToUpper(),command);
            Logger.Debug("注册了自定义命令:" + command.Command);
        }
        /// <inheritdoc/>
        public override void OnRegisterEvents()
        {
            foreach(var s in PluginLoader.Plugins)
            {
                Init(s.Value);
            }
            Init(Assembly.GetExecutingAssembly());
        }
        /// <summary>
        /// 初始化程序集的命令
        /// </summary>
        /// <param name="assembly">目标程序集</param>
        public static void Init(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                if (type.GetCustomAttribute<UnloadCommandAttribute>() != null)
                    continue;

                if (typeof(IBasicCommand).IsAssignableFrom(type))
                {
                    if (Activator.CreateInstance(type) is IBasicCommand cmd)
                    {
                        var cacheCmd = cmd.Command.ToUpper();
                        _commands[cacheCmd] = cmd;
                        Logger.Debug("注册了命令:" + cmd.Command);
                    }
                }
            }
        }
        /// <summary>
        /// 异步将一个聊天转化为命令并执行
        /// </summary>
        /// <param name="ev">当前聊天事件</param>
        /// <returns>命令的返回值</returns>
        public static async Task<string> ExecuteAsync(ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            try
            {
                foreach (var p in _commands)
                {
                    if (CommandHelper.TryParseCommand(p.Key, ev.Message.Content, out var arguments))
                    {
                        return await ExecuteAsync(p.Key, arguments, ev);
                    }
                }
                return string.Empty;
            }
            catch(Exception ex) 
            {
                Logger.Error("无法匹配命令:" + ex.ToString());
                return string.Empty;
            }
        }
        /// <summary>
        /// 执行一个命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="arguments">参数</param>
        /// <param name="ev">当前聊天事件</param>
        /// <returns>命令的返回值</returns>
        public static async Task<string> ExecuteAsync(string command, string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            if (_commands.TryGetFirst(out var cmd, x => x.Key.Equals(command, StringComparison.CurrentCultureIgnoreCase)))
            {
                if(cmd.Value is IAsyncCommand asyncCommand)
                {
                    return await asyncCommand.OnExecuteAsync(arguments,ev);
                }
                else if(cmd.Value is ICommand syncCommand)
                {
                    return syncCommand.OnExecute(arguments,ev);
                }
    
            }
            await Task.CompletedTask;
            return string.Empty;
        }
    }
}
