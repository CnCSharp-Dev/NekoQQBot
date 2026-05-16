using System.Reflection;
using NekoBot.Lib;
using NekoBot.Lib.Exceptions;
using NekoBot.Lib.EventBus;
using Serilog;
using Serilog.Events;
using NekoBot.Service;
using NekoBot.Lib.Loggers;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Core;

namespace NekoBot.ConsoleProgram
{
    /// <summary>
    /// 控制台程序的入口类
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// 自定义的控制台主题颜色
        /// </summary>
        public static Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle> CustomThemeStyles { get; set; } = new()
        {
            [ConsoleThemeStyle.LevelDebug] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGreen },
            [ConsoleThemeStyle.LevelError] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Red },
            [ConsoleThemeStyle.LevelFatal] = new SystemConsoleThemeStyle {Foreground = ConsoleColor.DarkRed },
            [ConsoleThemeStyle.LevelInformation] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Cyan},
            [ConsoleThemeStyle.LevelVerbose] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.DarkGray },
            [ConsoleThemeStyle.LevelWarning] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Yellow },

            [ConsoleThemeStyle.Boolean] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
            [ConsoleThemeStyle.Number] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
            [ConsoleThemeStyle.Null] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Blue },
            [ConsoleThemeStyle.Scalar] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Green },
            [ConsoleThemeStyle.String] = new SystemConsoleThemeStyle { Foreground = ConsoleColor.Yellow },
        };
        /// <summary>
        /// 当前日志最低等级
        /// </summary>
        public static LoggingLevelSwitch LoggingLevelSwitch { get; private set; } = new(LogEventLevel.Information);
        /// <summary>
        /// 当前Exe文件的程序集
        /// </summary>
        public static Assembly Assembly { get; private set; }
        /// <summary>
        /// 创建一个Logger
        /// </summary>
        public static void CreateLog()
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.ControlledBy(LoggingLevelSwitch)
             .Enrich.FromLogContext()
             .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] : {Message:lj}{NewLine}{Exception}",theme : new SystemConsoleTheme(CustomThemeStyles))
             .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Hour)
             .CreateLogger();

            Lib.Loggers.Logger.OnSendInfo += (msg, type) =>
            {
                switch (type)
                {
                    case LogType.Info:
                        Log.Information(msg);
                        break;
                    case LogType.Fatal:
                        Log.Fatal(msg);
                        break;
                    case LogType.Error:
                        Log.Error(msg);
                        break;
                    case LogType.Warning:
                        Log.Warning(msg);
                        break;
                    case LogType.Debug:
                        Log.Debug(msg);
                        break;
                }
            };
        }
        /// <summary>
        /// 表示机器人的服务，推荐一个exe仅搭载一个机器人服务
        /// </summary>
        public static BotService Service => BotFactory.Service;
        /// <summary>
        /// 入口方法
        /// </summary>
        /// <returns></returns>
        public static async Task Main()
        {
            Assembly = Assembly.GetExecutingAssembly();

            CreateLog();

            Log.Information("\"Neko QQBot Service-{version}\" | Powered by {author}(史蒂夫)", Assembly.GetName().Version?.ToString(3), "CNCSharp-DevTeam");
            Log.Information("");
            Paths.RootPath = AppDomain.CurrentDomain.BaseDirectory;

            if (!ConfigLoader.TryLoad(out var result))
            {
                Log.Fatal("进程已退出，原因: {result}", result);
                Console.ReadLine();
            }
            if (!ConfigLoader.ProgramConfig.TryGetLogEventLevel(out var minLevel, out var rsp))
                Log.Error(rsp);
            LoggingLevelSwitch.MinimumLevel = minLevel;

            BotFactory.Create(ConfigLoader.BotConfig.ToBotInfo());

            PluginLoader.Load();

            if (!string.IsNullOrEmpty(result))
                Log.Information(result);



            await EventManager.InitAsync(Assembly);

            foreach (var p in PluginLoader.Plugins)
            {
                await EventManager.InitAsync(p.Value);
            }
            bool bc = false;
        Restart:
            try
            {
                await Task.Delay(500);

                if(int.TryParse(ConfigLoader.BotConfig.AppId,out var num) && !bc)
                {
                    bc = true;
                    Log.Information("成功加载AppId为\"{num1}\"的配置文件项", num);
                    Log.Information("");
                }

                await Service.StartAsync();
            }
            catch (Exception ex)
            {
                if (ConfigLoader.ProgramConfig.AutoRestart)
                {
                    if (ex is InterfaceException interfaceException && interfaceException.Code == 11298)
                    {
                        Log.Fatal("Ip不在机器人白名单，请将本机Ip添加至白名单，{time}秒后重新初始化机器人", ConfigLoader.ProgramConfig.AutoRestartDelay);
                    }
                    else
                    {
                        Log.Fatal("出现错误，{time}秒后重新初始化机器人。", ConfigLoader.ProgramConfig.AutoRestartDelay);
                        Log.Fatal(ex.ToString());
                    }
                    await Task.Delay(ConfigLoader.ProgramConfig.AutoRestartDelay * 1000);
                }
                else
                {
                    if (ex is InterfaceException interfaceException && interfaceException.Code == 11298)
                    {
                        Log.Fatal($"Ip不在机器人白名单，请将本机Ip添加至白名单");
                    }
                    else
                    {
                        Log.Fatal($"出现错误，机器人已退出！");
                        Log.Fatal(ex.ToString());
                    }
                    Console.ReadLine();
                }
                goto Restart;
            }
            while (true)
            {
                var cmd = Console.ReadLine(); //防止进程终止

                if (string.IsNullOrEmpty(cmd))
                    continue;

                if (cmd.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                    break;
            }
        }
    }
}