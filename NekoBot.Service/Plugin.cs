using NekoBot.Lib.EventBus;
using NekoBot.Lib.Loggers;
using NekoBot.Service.Helpers;

namespace NekoBot.Service
{
    /// <summary>
    /// 用于实现插件的抽象类，声明后不需要自己手动注册<see cref="IEventHandler"/>
    /// </summary>
    public abstract class Plugin : IPlugin
    {
        /// <inheritdoc/>
        public abstract string Name { get; }
        /// <inheritdoc/>
        public virtual PluginType PluginType { get; } = PluginType.Misc;
        /// <inheritdoc/>
        public virtual void OnEnabled()
        {
            
        }
        /// <inheritdoc/>
        public virtual void OnDisabled()
        {
            
        }
        /// <inheritdoc/>
        public virtual void LoadConfig()
        {

        }
    }
    /// <summary>
    /// 用于实现携带自定义配置文件的插件的抽象类，声明后不需要自己手动注册<see cref="IEventHandler"/>
    /// </summary>
    public abstract class Plugin<TConfig> : Plugin ,IPlugin<TConfig> where TConfig : IConfig, new()
    {
        /// <inheritdoc/>
        public virtual string ConfigName => Name.ToLower().Replace(".","_") + ".yml";
        /// <inheritdoc/>
        public virtual TConfig Config { get; set; }
        /// <inheritdoc/>
        public override void LoadConfig()
        {
            var path = Paths.PluginConfigs + "\\" + ConfigName;
            if (File.Exists(path))
            {
                try
                {
                    Config = YamlHelper.Deserialize<TConfig>(File.ReadAllText(path));
                    return;
                }
                catch (Exception ex)
                {
                    Logger.Error("读取插件\"" + Name + "\"的配置文件失败:" + ex.ToString());
                }
            }
            Config = new();
            File.WriteAllText(path, YamlHelper.Serialize(Config));
        }
    }
}
