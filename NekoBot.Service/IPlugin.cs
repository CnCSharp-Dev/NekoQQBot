using NekoBot.Lib.EventBus;

namespace NekoBot.Service
{
    /// <summary>
    /// 插件接口，声明后不需要自己手动注册<see cref="IEventHandler"/>
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 当前插件的类型，用于简略介绍插件类型
        /// </summary>
        PluginType PluginType { get; }
        /// <summary>
        /// 插件启动时调用此方法
        /// </summary>
        void OnEnabled();
        /// <summary>
        /// 插件注销时调用此方法
        /// </summary>
        void OnDisabled();
        /// <summary>
        /// 当加载配置文件时调用此方法，可以自行添加内容，一般配合<see cref="IPlugin{TConfig}"/>
        /// </summary>
        void LoadConfig();
    }
    /// <summary>
    /// 包含自定义配置文件的插件接口，支持自动加载插件<see cref="IEventHandler"/>
    /// </summary>
    public interface IPlugin<TConfig> : IPlugin where TConfig : IConfig, new()
    {
        /// <summary>
        /// 配置文件名，默认为<see cref="IPlugin.Name"/>将"."改为"_"后小写的字符串
        /// </summary>
        string ConfigName { get; }
        /// <summary>
        /// 插件配置文件
        /// </summary>
        TConfig Config { get; internal set; }
    }
}
