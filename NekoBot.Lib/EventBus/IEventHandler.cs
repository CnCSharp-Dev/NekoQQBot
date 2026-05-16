namespace NekoBot.Lib.EventBus
{
    /// <summary>
    /// 负责提供自动注册事件的接口
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 当注册事件时调用
        /// </summary>
        void RegisterEvents();
        /// <summary>
        /// 当注销事件时调用
        /// </summary>
        void UnregisterEvents();
    }
}
