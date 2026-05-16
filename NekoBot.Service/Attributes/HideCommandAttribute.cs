namespace NekoBot.Service.Attributes
{
    /// <summary>
    /// 表示该命令是隐藏指令，不会出现在Help指令内
    /// <br>需要配合NekoBot.ExtensionCommands官方插件</br>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class HideCommandAttribute : Attribute
    {
    }
}
