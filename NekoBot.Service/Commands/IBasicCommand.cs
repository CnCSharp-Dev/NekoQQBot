namespace NekoBot.Service.Commands
{
    /// <summary>
    /// 命令基础接口
    /// </summary>
    public interface IBasicCommand
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        string Command { get; }
        /// <summary>
        /// 命令帮助
        /// </summary>
        string Description { get; }
    }
}
