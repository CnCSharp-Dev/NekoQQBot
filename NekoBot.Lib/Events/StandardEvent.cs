using NekoBot.Lib.Models.BasicModels;

namespace NekoBot.Lib.Events
{
    /// <summary>
    /// 一个标准的机器人事件
    /// </summary>
    public abstract class StandardEvent : IDisposable
    {
        internal EventManager Manager { get; set; }
        /// <summary>
        /// 表示机器人服务
        /// </summary>
        public BotService Service => Manager.Service;
        /// <summary>
        /// 当接收到<see cref="Payload"/>后触发该方法
        /// </summary>
        /// <param name="payload">当前Payload</param>
        /// <returns></returns>
        public abstract Task OnHandlePayloadAsync(Payload payload);
        /// <inheritdoc/>
        public virtual void Dispose()
        {

        }
    }
}
