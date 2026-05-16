using NekoBot.Lib;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;

namespace NekoBot.Service.Commands
{
    /// <summary>
    /// 异步命令标准接口
    /// </summary>
    public interface IAsyncCommand : IBasicCommand
    {
        /// <summary>
        /// 当指令被调用时执行
        /// </summary>
        /// <param name="arguments">指令参数</param>
        /// <param name="ev">当前聊天事件</param>
        /// <returns>指令的返回值，允许为空</returns>
        Task<string> OnExecuteAsync(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev);
    }
}
