using System.Reflection;
using NekoBot.Lib.EventBus;
using NekoBot.Lib.Loggers;
using PoolingLib.Pools;

namespace NekoBot.Service
{
    /// <summary>
    /// 插件加载器管理类
    /// </summary>
    public static class PluginLoader
    {
        /// <summary>
        /// 所有被加载的依赖
        /// </summary>
        public static HashSet<Assembly> Dependencies { get; } = [];
        /// <summary>
        /// 所有被加载的插件
        /// </summary>
        public static Dictionary<IPlugin, Assembly> Plugins { get; } = [];
        /// <summary>
        /// 加载插件
        /// </summary>
        public static async void Load()
        {
            if(!Directory.Exists(Paths.Dependencies))
                Directory.CreateDirectory(Paths.Dependencies);
            if (!Directory.Exists(Paths.Plugins))
                Directory.CreateDirectory(Paths.Plugins);
            if (!Directory.Exists(Paths.PluginConfigs))
                Directory.CreateDirectory(Paths.PluginConfigs);
            if(!Directory.Exists(Paths.Databases))
                Directory.CreateDirectory(Paths.Databases);

            LoadDependencies(new DirectoryInfo(Paths.Dependencies).GetFiles("*.dll", SearchOption.AllDirectories));
            LoadPlugins(new DirectoryInfo(Paths.Plugins).GetFiles("*.dll", SearchOption.AllDirectories));

            if (Dependencies.Count != 0)
            {
                Logger.Info("被加载的依赖:");

                foreach (var p in Dependencies)
                {
                    Logger.Info("  - " + p.GetName().Name);
                }
                Logger.Info("");
            }
            if (Plugins.Count != 0)
            {
                foreach(var p in Plugins)
                    p.Key.LoadConfig();

                Logger.Info("被加载的插件:");

                foreach (var p in Plugins)
                {
                    Logger.Info("  - " + p.Key.Name);
                    p.Key.OnDisabled();
                    p.Key.OnEnabled();
                }
                Logger.Info("");
            }

            await EventManager.InitAsync(Assembly.GetExecutingAssembly());
        }
        private static void LoadDependencies(IEnumerable<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                try
                {
                    Assembly assembly = Assembly.Load(File.ReadAllBytes(file.FullName));
                    Dependencies.Add(assembly);
                }
                catch (Exception ex)
                {
                    Logger.Error("无法加载依赖 \"" + file.Name + "\":" + ex.ToString());
                }
            }
        }
        private static void LoadPlugins(IEnumerable<FileInfo> files)
        {
            var list = ListPool<Assembly>.Pool.Get();

            foreach (FileInfo file in files)
            {
                try
                {
                    FileInfo fileInfo = new(Path.ChangeExtension(file.FullName, ".pdb"));
                    Assembly item = fileInfo.Exists ? Assembly.Load(File.ReadAllBytes(file.FullName), File.ReadAllBytes(fileInfo.FullName)) : Assembly.Load(File.ReadAllBytes(file.FullName));
                    list.Add(item);
                }
                catch (Exception ex)
                {
                    Logger.Error("无法加载插件程序集 \"" + file.Name + "\":" + ex.ToString());
                }
            }

            foreach (var x in list)
            {
                try
                {
                    InstantiatePlugins(x.GetTypes(), x);
                }
                catch (Exception ex)
                {
                    Logger.Error("无法加载插件程序集 \"" + x.GetName().Name + "\":" + ex.ToString());
                }
            }
            ListPool<Assembly>.Pool.Return(list);
        }
        private static void InstantiatePlugins(Type[] types, Assembly assembly)
        {
            foreach (Type type in types)
            {
                if (typeof(IPlugin).IsAssignableFrom(type) && !type.IsAbstract && Activator.CreateInstance(type) is IPlugin plugin)
                {
                    Plugins.Add(plugin, assembly);
                }
            }
        }
    }
}
