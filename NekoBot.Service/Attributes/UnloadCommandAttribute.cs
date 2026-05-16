namespace NekoBot.Service.Attributes
{
    /// <summary>
    /// 表示该命令不会被自动识别并注册
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class UnloadCommandAttribute : Attribute
    {
    }
}
