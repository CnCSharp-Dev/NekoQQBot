namespace NekoBot.Service
{
    /// <summary>
    /// 插件的归类类型
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        /// 默认归类（无归类）
        /// </summary>
        Default = 0,
        /// <summary>
        /// 工具类，例如负责日志处理/网络包拦截与转发的工具
        /// </summary>
        Tool,
        /// <summary>
        /// 拓展类，一般只储存了指令等及其相关联的处理器的轻量级插件
        /// </summary>
        Extension,
        /// <summary>
        /// 杂项类，当你实在想不到自己插件属于什么类型时可以选择此类型
        /// </summary>
        Misc,
        /// <summary>
        /// 打包类，例如储存音频/视频等文件且用处不大的程序集
        /// </summary>
        Package,
        /// <summary>
        /// 框架类，与QQ机器人有一定关联且为运行库的库/工具
        /// </summary>
        Framework,
        /// <summary>
        /// 运行库类，例如与QQ机器人毫不相干但是必须要加载的库
        /// </summary>
        Library,


        /// <summary>
        /// 官方工具类，一般建议不要随意设置为此<see cref="PluginType"/>
        /// </summary>
        OfficialTool = 256,
        /// <summary>
        /// 官方拓展类，一般建议不要随意设置为此<see cref="PluginType"/>
        /// </summary>
        OfficialExtension,
        /// <summary>
        /// 官方杂项类，一般建议不要随意设置为此<see cref="PluginType"/>
        /// </summary>
        OfficialMisc,
        /// <summary>
        /// 官方打包类，一般建议不要随意设置为此<see cref="PluginType"/>
        /// </summary>
        OfficialPackage,
        /// <summary>
        /// 官方框架类，一般建议不要随意设置为此<see cref="PluginType"/>
        /// </summary>
        OfficialFramework,
        /// <summary>
        /// 官方运行库类，一般建议不要随意设置为此<see cref="PluginType"/>
        /// </summary>
        OfficialLibrary,
    }
}
