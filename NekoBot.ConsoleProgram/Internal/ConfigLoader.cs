using NekoBot.ConsoleProgram.Configurations;
using NekoBot.Service;
using NekoBot.Service.Helpers;

namespace NekoBot.ConsoleProgram
{
    /// <summary>
    /// 配置文件加载器
    /// </summary>
    public static class ConfigLoader
    {
        /// <summary>
        /// 机器人配置文件
        /// </summary>
        public static BotConfig BotConfig { get; private set; }
        /// <summary>
        /// 程序配置文件
        /// </summary>
        public static ProgramConfig ProgramConfig { get; private set; }
        private static ProgramConfig ProgramConfigTemp { get; } = new();
        private static BotConfig BotConfigTemp { get; } = new();
        internal static bool TryLoad(out string result)
        {
            if (!Directory.Exists(Paths.Configs))
                Directory.CreateDirectory(Paths.Configs);
            if (!Directory.Exists(Paths.PluginConfigs))
                Directory.CreateDirectory(Paths.PluginConfigs);

            if (!File.Exists(Paths.Configs + "\\program.yml"))
            {
                File.WriteAllText(Paths.Configs + "\\program.yml", YamlHelper.Serialize(ProgramConfigTemp));
            }
            if (!File.Exists(Paths.Configs + "\\bot.yml"))
            {
                File.WriteAllText(Paths.Configs + "\\bot.", YamlHelper.Serialize(BotConfigTemp));
                result = "未检测到配置文件，已新建配置文件，请手动设置\"bot.yml\"!";
                return false;
            }


            try
            {
                ProgramConfig = YamlHelper.Deserialize<ProgramConfig>(File.ReadAllText(Paths.Configs + "\\program.yml"));
                BotConfig = YamlHelper.Deserialize<BotConfig>(File.ReadAllText(Paths.Configs + "\\bot.yml"));

                result = "";
                return true;
            }
            catch (Exception ex)
            {
                result = ex.ToString();
                return false;
            }
        }
    }
}
