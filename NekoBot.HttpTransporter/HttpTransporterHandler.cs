using NekoBot.HttpTransporter.Models;
using NekoBot.Lib.Enums;
using NekoBot.Lib.EventBus;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Loggers;
using NekoBot.Service.Helpers;

namespace NekoBot.HttpTransporter
{
    public class HttpTransporterHandler : CustomEvent
    {
        public override async Task OnBotUserGroupChat(GroupChatEventArgs ev)
        {
            foreach (var p in MainPlugin.Targets)
            {
                if (CommandHelper.TryParseCommand(p.Command, ev.Message.Content, out var arguments))
                {
                    try
                    {
                        var httpResponse = await HttpTransporter.PostAsync(p.Url, new TransporterRequest()
                        {
                            AppId = ev.Service.Info.AppID,
                            Arguments = arguments,
                            Command = p.Command,
                            GroupOpenId = ev.Message.GroupOpenId,
                            Sender = ev.Message.Author,
                            IsGroup = true
                        });
                        await ev.ReplyAsync(new()
                        {
                            MessageId = ev.Message.Id,
                            Content = httpResponse.Response,
                            Type = MessageType.Text,
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("转发群聊消息至Url" + p.Url + "失败: " + ex.ToString());
                        await ev.ReplyAsync(new()
                        {
                            MessageId = ev.Message.Id,
                            Content = $"❌转发失败，无法连接至对应计算机！",
                            Type = MessageType.Text,
                        });
                    }
                }
            }
            await Task.CompletedTask;
        }
        public override async Task OnBotUserPrivateChat(PrivateChatEventArgs ev)
        {
            foreach (var p in MainPlugin.Targets)
            {
                if (CommandHelper.TryParseCommand(p.Command, ev.Message.Content, out var arguments))
                {
                    try
                    {
                        var httpResponse = await HttpTransporter.PostAsync(p.Url, new TransporterRequest()
                        {
                            AppId = ev.Service.Info.AppID,
                            Arguments = arguments,
                            Command = p.Command,
                            Sender = ev.Message.Author,
                            GroupOpenId = string.Empty,
                            IsGroup = false
                        });
                        await ev.ReplyAsync(new()
                        {
                            MessageId = ev.Message.Id,
                            Content = httpResponse.Response,
                            Type = MessageType.Text,
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("转发私聊消息至Url" + p.Url + "失败: " + ex.ToString());
                        await ev.ReplyAsync(new()
                        {
                            MessageId = ev.Message.Id,
                            Content = $"❌转发失败，无法连接至对应计算机！",
                            Type = MessageType.Text,
                        });
                    }
                }
            }
            await Task.CompletedTask;
        }
    }
}
