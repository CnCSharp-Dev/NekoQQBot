namespace NekoBot.Lib.Events
{
    /// <summary>
    /// 异步事件处理器
    /// </summary>
    /// <typeparam name="TEventArgs">事件参数类型</typeparam>
    /// <param name="sender">发送者</param>
    /// <param name="ev">事件参数</param>
    /// <returns></returns>
    public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs ev);
    /// <summary>
    /// 无Sender的异步事件处理器
    /// </summary>
    /// <typeparam name="TEventArgs">事件参数类型</typeparam>
    /// <param name="ev">事件参数</param>
    /// <returns></returns>
    public delegate Task AsyncActionHandler<TEventArgs>(TEventArgs ev);
}
