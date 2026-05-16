using NekoBot.Service;

namespace NekoBot.ExtensionTools
{
    public class MainPlugin : Plugin<Config>
    {
        public override string Name => "NekoBot.ExtensionTools";
        public override PluginType PluginType => PluginType.OfficialTool;
        public static MainPlugin Instance { get; private set; }
        public override void OnEnabled()
        {
            Instance = this;
        }
        public override void OnDisabled()
        {
            Instance = null;
        }
    }
}
