using NekoBot.Lib.Loggers;
using System.Reflection;

namespace NekoBot.Lib.EventBus
{
    /// <summary>
    /// 提供事件注册
    /// </summary>
    public static class EventManager
    {
        /// <summary>
        /// 所有被注册的事件
        /// </summary>
        public static List<IEventHandler> List { get; } = [];
        /// <summary>
        /// 注册程序集的所有事件
        /// </summary>
        /// <param name="assembly">目标程序集</param>
        /// <returns></returns>
        public static async Task InitAsync(Assembly assembly)
        {
            List.Clear();
            int count = 0;
            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(IEventHandler).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract || !type.IsClass)
                    continue;
                count++;
                try
                {
                    if (Activator.CreateInstance(type) is IEventHandler handler)
                    {
                        List.Add(handler);
                    }

                }
                catch (Exception ex)
                {
                    Logger.Error("注册事件失败!" + ex.ToString());
                }
            }
            Logger.Debug("[" + assembly.GetName().Name + "] 成功加载了 " + List.Count + "/" + count +" 个事件!");
            foreach (var p in List)
            {
                p.UnregisterEvents();
                p.RegisterEvents();

            }
            await Task.CompletedTask;
        }
    }
}
