namespace NekoBot.Lib.EventBus
{
    /// <summary>
    /// 提供事件绑定的管理类
    /// </summary>
    public static class Hook
    {
        private static readonly Dictionary<string, List<Action<object>>> _hookDict = [];
        /// <summary>
        /// 调用指定HookId的<see cref="Action"/>
        /// </summary>
        /// <param name="hookName">Id</param>
        /// <param name="arg">参数</param>
        public static void Run(string hookName,object arg = null)
        {
            if (_hookDict.TryGetValue(hookName, out var list))
            {
                foreach(var lmd in list)
                {
                    lmd?.Invoke(arg);
                }
            }
        }
        /// <summary>
        /// 添加一个方法到指定的HookId
        /// </summary>
        /// <param name="hookName">Id</param>
        /// <param name="action">绑定的方法</param>
        public static void Add(string hookName,Action<object> action)
        {
            if (!_hookDict.ContainsKey(hookName))
                _hookDict.Add(hookName, []);

            _hookDict[hookName].Add(action);
        }
    }
}
