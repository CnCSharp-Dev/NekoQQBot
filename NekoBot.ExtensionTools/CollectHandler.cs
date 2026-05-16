using NekoBot.Lib.EventBus;
using NekoBot.Lib.Loggers;

namespace NekoBot.ExtensionTools
{
    internal class CollectHandler : CustomEvent
    {
        public static Timer CollectTimer { get; private set; }
        public override void OnRegisterEvents()
        {
            if(MainPlugin.Instance.Config.AutoCollect)
            {
                CollectTimer?.Dispose();

                CollectTimer = new Timer(_ =>
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Logger.Debug("GC回收完成");
                }, null,
                TimeSpan.FromSeconds(MainPlugin.Instance.Config.AutoCollectDelay), 
                TimeSpan.FromSeconds(MainPlugin.Instance.Config.AutoCollectDelay));
            }
        }
    }
}
