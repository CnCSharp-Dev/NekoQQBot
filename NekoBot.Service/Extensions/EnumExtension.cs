namespace NekoBot.Service.Extensions
{
    /// <summary>
    /// 提供给本框架枚举的拓展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 该枚举是否为官方插件枚举
        /// </summary>
        /// <param name="type">选中的枚举</param>
        /// <returns>如果是则返回<see langword="true"/></returns>
        public static bool IsOfficial(this PluginType type)
        {
            return type switch
            {
                PluginType.OfficialTool => true,
                PluginType.OfficialExtension => true,
                PluginType.OfficialMisc => true,
                PluginType.OfficialPackage => true,
                PluginType.OfficialFramework => true,
                PluginType.OfficialLibrary => true,
                _ => false,
            };
        }
        /// <summary>
        /// 该插件是否为官方插件
        /// </summary>
        /// <param name="plugin">当前所指向的插件</param>
        /// <returns>如果是则返回<see langword="true"/></returns>
        public static bool IsOfficial(this Plugin plugin)
        {
            if (!plugin.Name.Contains("NekoBot"))
                return false;

            return plugin.IsOfficial();
        }
        /// <summary>
        /// 将该枚举转化为字符串
        /// </summary>
        /// <param name="type">选中的枚举</param>
        /// <returns>返回的字符串</returns>
        public static string ParseToString(this PluginType type)
        {
            return type switch
            {
                PluginType.Tool or PluginType.OfficialTool => "工具",
                PluginType.Extension or PluginType.OfficialExtension => "拓展",
                PluginType.Misc or PluginType.OfficialMisc => "杂项",
                PluginType.Package or PluginType.OfficialPackage => "资源",
                PluginType.Framework or PluginType.OfficialFramework => "框架",
                PluginType.Library or PluginType.OfficialLibrary => "类库",
                _ => "",
            };
        }
        /// <summary>
        /// 将该枚举转化为附带枚举的字符串
        /// </summary>
        /// <param name="type">选中的枚举</param>
        /// <returns>返回的字符串</returns>
        public static string ParseWithEmoji(this PluginType type)
        {
            var txt = type.ParseToString();
            if (string.IsNullOrEmpty(txt))
                return string.Empty;

            return type switch
            {
                PluginType.Tool or PluginType.OfficialTool => "🛠️" + txt,
                PluginType.Extension or PluginType.OfficialExtension => "🧩" + txt,
                PluginType.Misc or PluginType.OfficialMisc => "📦" + txt,
                PluginType.Package or PluginType.OfficialPackage => "🗂️" + txt,
                PluginType.Framework or PluginType.OfficialFramework => "⚙️" + txt,
                PluginType.Library or PluginType.OfficialLibrary => "🔧" + txt,
                _ => string.Empty,
            };
        }
    }
}
