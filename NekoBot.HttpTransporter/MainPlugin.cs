using NekoBot.HttpTransporter.Models;
using NekoBot.Lib.Loggers;
using NekoBot.Service;
using NekoBot.Service.Helpers;

namespace NekoBot.HttpTransporter
{
    public class MainPlugin : Plugin<Config>
    {
        public static HttpTargetModel[] Targets { get; set; } = [];
        public override string Name => "NekoBot.HttpTransporter";
        public override PluginType PluginType => PluginType.OfficialTool;
        public override void OnEnabled()
        {
            Targets = Config.Models;
            try
            {
                Task.Run(() =>
                {
                    Task.Delay(10);
                    try
                    {
                        Logger.Info("加载转发命令:");
                        foreach (var p in Config.Models)
                        {
                            Logger.Info("  - " + p.Command + " : \t" + p.Url);
                            CommandHandler.RegisterCommand(new BasedCommand(p.Command, p.Description));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("无法加载转发命令:" + ex.ToString());
                    }

                });
            }
            catch (Exception ex)
            { 
                Logger.Error("无法加载转发配置文件:" + ex.ToString()); 
            }
        }
    }
}
