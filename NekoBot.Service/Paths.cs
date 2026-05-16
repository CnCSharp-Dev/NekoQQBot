namespace NekoBot.Service
{
    /// <summary>
    /// 路径管理类
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// 插件配置文件提供的路径
        /// </summary>
        public static string PluginConfigs => RootPath + "\\configs\\plugin";
        /// <summary>
        /// 插件路径
        /// </summary>
        public static string Configs => RootPath + "\\configs";
        /// <summary>
        /// 插件路径
        /// </summary>
        public static string Plugins => RootPath + "\\plugins";
        /// <summary>
        /// 数据库路径
        /// </summary>
        public static string Databases => RootPath + "\\databases";
        /// <summary>
        /// 依赖路径
        /// </summary>
        public static string Dependencies => RootPath + "\\dependencies";
        /// <summary>
        /// Exe根目录
        /// </summary>
        public static string RootPath { get; set; }
    }
}
