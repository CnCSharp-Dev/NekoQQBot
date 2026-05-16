using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using NekoBot.Lib;
using NekoBot.Lib.Enums;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Events.Handlers;
using NekoBot.Lib.Exceptions;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.BasicModels;
using NekoBot.Service;
using NekoBot.Service.Helpers;
using NekoBot.WPFProgram.Loggers;
using Serilog;
using EventManager = NekoBot.Lib.EventBus.EventManager;

namespace NekoBot.WPFProgram
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<LogEntry> _logEntries = [];
        private const int MaxLogCount = 500;
        private bool _isOpen;
        private long _totalMessages = 0;
        private long _totalChatMessages = 0;
        public static BotService Service { get; private set; }
        public static Assembly Assembly { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            Assembly = Assembly.GetExecutingAssembly();
            LogItemsControl.ItemsSource = _logEntries;

            TcpStatusColor.Fill = Brushes.Red;
            TcpStatus.Text = " Tcp WebSocket 已断开";
            MessageCountText.Text = "0";
            _isOpen = false;

            BotStatusText.Text = "未启动";
            BotStatusText.Foreground = Brushes.Red;
            BotStatusColor.Fill = Brushes.Red;

            StartServiceBorder.Style = (Style)FindResource("EnabledBorderStyle");
            StopServiceBorder.Style = (Style)FindResource("DisabledBorderStyle");

            Log.Logger = new LoggerConfiguration().WriteTo.File("logs/log-.txt",
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] : {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Hour).CreateLogger();

            Logger.OnSendInfo += OnLoggerSendInfo;

            Paths.RootPath = AppDomain.CurrentDomain.BaseDirectory;
        }
        public override void EndInit()
        {
            if (!ConfigLoader.TryLoad(out var result))
            {
                Logger.Fatal("进程已退出，原因: " + result);
                return;
            }

            PluginLoader.Load();
            BotPluginsText.Text = PluginLoader.Plugins.Count.ToString();
            Service = new BotService(ConfigLoader.BotConfig.ToBotInfo());
            AppIdTextBar.Text = "机器人AppId: " + (string.IsNullOrEmpty(ConfigLoader.BotConfig.AppId) ? "空" : ConfigLoader.BotConfig.AppId);

            BotHandler.WebSocketConnected += OnServerConnected;
            BotHandler.WebSocketDisconnected += OnServerDisconnected;
            BotHandler.WebSocketClose += OnServerClose;

            BotHandler.Ready += OnBotReady;
            BotHandler.ReceivedMessage += OnMessageReceived;
            BotHandler.UserPrivateChat += OnUserPrivateChat;
            BotHandler.UserGroupChat += OnUserGroupChat;

            Task.Run(async () =>
            {
                await EventManager.InitAsync(Assembly);
                foreach (var p in PluginLoader.Plugins)
                {
                    await EventManager.InitAsync(p.Value);
                }
            });

            Task.Run(async () =>
            {
                await Task.Delay(200);
                await Dispatcher.InvokeAsync(() =>
                {
                    foreach (var asm in PluginLoader.Plugins)
                        CommandHandler.Init(asm.Value);
                    BotCommandsText.Text = CommandHandler.Commands.Count().ToString();
                });
            });
            base.EndInit();
        }
        private void OnServerConnected(BotSocketModule module)
        {
            Dispatcher.Invoke(() =>
            {
                BotStatusText.Text = "已启动";
                BotStatusText.Foreground = Brushes.Green;
                BotStatusColor.Fill = Brushes.Green;
                TcpStatusColor.Fill = Brushes.Green;
                TcpStatus.Text = " Tcp WebSocket 已连接";
            });
        }
        private void OnServerDisconnected(BotSocketModule module)
        {
            Dispatcher.Invoke(() =>
            {
                TcpStatusColor.Fill = Brushes.Red;
                TcpStatus.Text = " Tcp WebSocket 已断开";
            });
        }
        private void OnServerClose(BotSocketModule module)
        {
            Dispatcher.Invoke(() =>
            {
                TcpStatusColor.Fill = Brushes.Red;
                TcpStatus.Text = " Tcp WebSocket 已关闭";
            });
        }
        private void OnBotReady(BotService service)
        {
            Dispatcher.Invoke(() =>
            {
                BotStatusText.Text = "已启动";
                BotStatusText.Foreground = Brushes.Green;
                BotStatusColor.Fill = Brushes.Green;
                Logger.Info("成功鉴权，用户名: \"" + Service.BotUser.UserName + "\"");
                BotNicknameText.Text = Service.BotUser.UserName;
            });
        }
        private void OnMessageReceived(BotService service,Payload payload)
        {
            Dispatcher.Invoke(() =>
            {
                _totalMessages++;

                string displayText;
                if (_totalMessages >= 1000)
                {
                    double kValue = _totalMessages / 1000.0;
                    displayText = kValue.ToString("F1") + "K";
                }
                else
                {
                    displayText = _totalMessages.ToString();
                }
                MessageCountText.Text = displayText;
            });
        }
        private async Task OnUserPrivateChat(PrivateChatEventArgs ev)
        {
            Dispatcher.Invoke(() =>
            {
                _totalChatMessages++;
                if (_totalChatMessages >= 1000)
                {
                    double kValue = _totalChatMessages / 1000.0;
                    ChatCountText.Text = kValue.ToString("F1") + "K";
                }
                else
                {
                    ChatCountText.Text = _totalChatMessages.ToString();
                }
            });

            Logger.Debug($"接受\"" + ev.Message.Author.Id + "\"发送的私聊聊天信息:" + ev.Message.Content.Replace("\n", ""));

            var msg = await CommandHandler.ExecuteAsync(ev);
            Logger.Debug(msg);
            if (!string.IsNullOrEmpty(msg))
            {
                await ev.ReplyAsync(new()
                {
                    Content = msg,
                    Type = MessageType.Text,
                    MessageId = ev.Message.Id,
                });
            }
            await Task.CompletedTask;
        }
        private async Task OnUserGroupChat(GroupChatEventArgs ev)
        {
            Dispatcher.Invoke(() =>
            {
                _totalChatMessages++;
                if (_totalChatMessages >= 1000)
                {
                    double kValue = _totalChatMessages / 1000.0;
                    ChatCountText.Text = kValue.ToString("F1") + "K";
                }
                else
                {
                    ChatCountText.Text = _totalChatMessages.ToString();
                }
            });

            Logger.Debug($"接受\"" + ev.Message.Author.Id + "\"发送的群聊聊天信息:" + ev.Message.Content.Replace("\n", ""));

            var msg = await CommandHandler.ExecuteAsync(ev);
            Logger.Debug(msg);
            if (!string.IsNullOrEmpty(msg))
            {
                await ev.ReplyAsync(new()
                {
                    Content = msg,
                    Type = MessageType.Text,
                    MessageId = ev.Message.Id,
                });
            }
            await Task.CompletedTask;
        }
        private void OnLoggerSendInfo(string txt, LogType type)
        {
            if (string.IsNullOrEmpty(txt))
                AddLog("");
            else
            {
                switch (type)
                {
                    case LogType.Info:
                        Log.Information(txt);
                        AddLog("[Info] " + txt, Brushes.WhiteSmoke);
                        break;
                    case LogType.Fatal:
                        Log.Fatal(txt);
                        AddLog("[Fatal] " + txt, Brushes.DarkRed);
                        break;
                    case LogType.Error:
                        Log.Error(txt);
                        AddLog("[Error] " + txt, Brushes.Red);
                        break;
                    case LogType.Warning:
                        Log.Warning(txt);
                        AddLog("[Warning] " + txt, Brushes.Yellow);
                        break;
                    case LogType.Debug:
                        Log.Debug(txt);
                        // AddLog("[Debug] " + txt, Brushes.Yellow);
                        break;
                }
            }
        }
        private async void StartService(object sender, MouseButtonEventArgs e)
        {
            if (_isOpen) return;

            StartServiceBorder.Style = (Style)FindResource("DisabledBorderStyle");
            StopServiceBorder.Style = (Style)FindResource("EnabledBorderStyle");
            _isOpen = true;

            BotStatusText.Text = "启动中";
            BotStatusText.Foreground = Brushes.Yellow;
            BotStatusColor.Fill = Brushes.Yellow;

            BotCommandsText.Text = CommandHandler.Commands.Count().ToString();

            try
            {
                await Service.StartAsync().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                _isOpen = false;
                if (ex is InterfaceException interfaceException && interfaceException.Code == 11298)
                {
                    Logger.Fatal("Ip不在机器人白名单，请将本机Ip添加至白名单");
                }
                else
                {
                    Logger.Fatal($"出现错误，机器人已退出！" + ex.ToString());
                }
                await Dispatcher.InvokeAsync(() =>
                {
                    BotStatusText.Text = "已关闭";
                    BotStatusText.Foreground = Brushes.Red;
                    BotStatusColor.Fill = Brushes.Red;
                    StopServiceBorder.Style = (Style)FindResource("DisabledBorderStyle");
                    StartServiceBorder.Style = (Style)FindResource("EnabledBorderStyle");
                });
            }
        }
        private async void StopService(object sender, MouseButtonEventArgs e)
        {
            if (!_isOpen) return;

            StopServiceBorder.Style = (Style)FindResource("DisabledBorderStyle");
            StartServiceBorder.Style = (Style)FindResource("EnabledBorderStyle");
            _isOpen = false;

            BotCommandsText.Text = CommandHandler.Commands.Count().ToString();

            BotStatusText.Text = "已关闭";
            BotStatusText.Foreground = Brushes.Red;
            BotStatusColor.Fill = Brushes.Red;

            try
            {
                if(Service != null)
                    await Service.StopAsync().ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                _isOpen = false;
                Logger.Fatal($"关闭时出现错误:" + ex.ToString());

                await Dispatcher.InvokeAsync(() =>
                {
                    BotStatusText.Text = "已关闭";
                    BotStatusText.Foreground = Brushes.Red;
                    BotStatusColor.Fill = Brushes.Red;
                    StopServiceBorder.Style = (Style)FindResource("DisabledBorderStyle");
                    StartServiceBorder.Style = (Style)FindResource("EnabledBorderStyle");
                });
            }
        }
        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(20);
            Close();
        }
        public void AddLog(string message, Brush color = null)
        {
            Dispatcher.Invoke(() =>
            {
                var finalColor = color ?? Brushes.White;
                var entry = new LogEntry(message, finalColor);
                _logEntries.Add(entry);

                while (_logEntries.Count > MaxLogCount)
                    _logEntries.RemoveAt(0);

                Dispatcher.BeginInvoke(new Action(() => LogScrollViewer?.ScrollToBottom()), DispatcherPriority.Background);
            });
        }
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T result)
                    return result;
                var descendant = FindVisualChild<T>(child);
                if (descendant != null)
                    return descendant;
            }
            return null;
        }
        protected override void OnClosed(EventArgs e)
        {
            Logger.OnSendInfo -= OnLoggerSendInfo;

            if (Service != null)
            {
                BotHandler.WebSocketConnected -= OnServerConnected;
                BotHandler.WebSocketDisconnected -= OnServerDisconnected;
                BotHandler.WebSocketClose -= OnServerClose;

                BotHandler.Ready -= OnBotReady;
                BotHandler.ReceivedMessage -= OnMessageReceived;
                BotHandler.UserPrivateChat -= OnUserPrivateChat;
                BotHandler.UserGroupChat -= OnUserGroupChat;
            }

            _logEntries?.Clear();
            LogItemsControl.ItemsSource = null;

            base.OnClosed(e);
        }
    }
}