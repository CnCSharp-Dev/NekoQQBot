# 如何制作第一个插件

本教程将指导您从头创建一个最简单的NekoQQBot插件。

> 📌 **前言:** 我们不教如何编译C#类库文件，请自觉寻找相关教程。

## 📝 编写插件代码

### 📃 准备工作:

> 您的插件项目需要引用以下框架程序集（可从机器人程序目录获取）：
> - NekoBot.Service.dll
> - NekoBot.Lib.dll			(可选)
>如果使用 `dotnet 命令行`或 `Visual Studio`，请手动添加这些引用。


### 📝 开始:
以下是一个示例Plugin文件，**必须继承`Plugin`或`Plugin<TConfig>`才会被当作插件加载**：

```csharp
using NekoBot.Service;
using NekoBot.Lib.Loggers;

namespace MyFirstPlugin
{
    public class MainPlugin : Plugin
    {
        public override string Name => "MyFirstPlugin"; //推荐英文命名，不要包含特殊字符

        public override PluginType PluginType => PluginType.Misc; //插件的类型，从框架源码或API文档中查看完整枚举

        public override void OnEnabled()
        {
            // 插件启用时执行，例如注册事件、初始化资源等
            Logger.Info($"[{Name}] 插件已启动！");
        }

        public override void OnDisabled()
        {
            // 插件关闭时执行，例如释放资源
            Logger.Info($"[{Name}] 插件已关闭。");
        }
    }
}
```

同时，我们推荐`PluginType`使用以下类型：

| 枚举项 | 描述 |
|------|------|
| `Default` | 默认归类 |
| `Tool` | 工具类，例如负责日志处理/网络包拦截与转发的工具 |
| `Extension` | 拓展类，一般只储存了指令等及其相关联的处理器的轻量级插件 |
| `Misc` | 杂项类，当你实在想不到自己插件属于什么类型时可以选择此类型 |
| `Package` | 打包类，例如储存音频/视频等文件且用处不大的程序集 |
| `Framework` | 框架类，与QQ机器人有一定关联且为运行库的库/工具 |
| `Library` | 运行库类，例如与QQ机器人毫不相干但是必须要加载的库 |

如果有需要，你可以创建**自定义指令**，以下是两个自定义指令的模板：

```csharp
using NekoBot.Lib.Enums;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Attributes;
using NekoBot.Service.Commands;
using PoolingLib.Pools;

namespace MyFirstPlugin
{
    [HideCommand] // 隐藏该指令，使其不在 help 列表中显示
    public class SayCommand : ICommand //表示非异步指令
    {
        public string Command { get; } = "say"; //指令名称，在QQ对应为“/say”

        public string Description { get; } = "复读发送的消息"; //指令介绍

        public string OnExecute(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            if (arguments.Length != 0)
                return string.Join(" ", arguments);
            return "❌ 你没有发送任何消息！";
        }
    }

    public class CxCommand : IAsyncCommand //表示异步指令
    {
        private static readonly HttpClient _client = new();

        public string Command => "cx";

        public string Description => "查询我的API信息";

        public async Task<string> OnExecuteAsync(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            try
            {
                var info = await _client.GetStringAsync("http://127.0.0.1:7891/api/getinfo"); //你的API网页

                await ev.ReplyAsync(new()
                {
                    MessageId = ev.Message.Id,
                    Type = MessageType.Markdown,
                    Markdown = new()
                    {
                        Content = string.Format("## {0}", info)
                    }
                }); //以Markdown文件输出
                return ""; //当返回值为空时不会自动返回信息，你可以在返回图片/markdown/ark等操作时让指令返回空字符串，但必须执行ReplyAsync
            }
            catch
            {
                return "❌ 出错了！";
            }
        }
    }
}
```

### 🧪 扩展：为插件添加配置文件
`Plugin<TConfig>` 允许您为插件自动加载和保存 YAML 配置文件。下面是一个带配置的插件示例：

```csharp
using NekoBot.Service;
using NekoBot.Lib.Loggers;
using YamlDotNet.Serialization;

namespace MyConfigPlugin
{
    public class MyConfig : IConfig //配置文件类要求必须继承空接口IConfig来实现代码标准化
    {
        [YamlMember(Description = "输出的文本")]
		public string OutputText { get; set; } = "你好，世界！";
    }

    public class MainPlugin : Plugin<MyConfig>
    {
        public override string Name => "配置示例插件";

        public override void OnEnabled()
        {
            Logger.Info(Config.OutputText);
        }
    }
}
```

> 框架会在 configs/plugin 目录下自动生成 $Plugin::Name$.yml 文件，用户可修改其中的值，下次启动时自动读取。

# 📦 安装插件

找到机器人程序的根目录（包含 NekoBot.ConsoleProgram.exe / NekoBot.WPFProgram.exe 的文件夹）。

将编译好的 MyFirstPlugin.dll 复制到 `plugins` 文件夹中。

（可选）如果您的插件有其他依赖项，请将依赖DLL放入 `dependencies` 文件夹。

目录结构示例：

```text
NekoQQBot/
├── $NekoBot主要内容$
│
├── plugins/
│   └── MyFirstPlugin.dll
│
├── dependencies/
│
└── configs/
	├── bot.yml //机器人配置文件，第一次启动时生成模板，*必须填写正确*
	├── program.yml //程序配置文件，只有控制台程序生成
	└── plugin/
```

# ❓ 常见问题 Q & A

** Q:  为什么我的插件没有被加载？**
** A： 确保插件类继承了`Plugin`或`Plugin<TConfig>`。**
** A： 确保插件文件放在了`plugins/`目录（而不是`dependencies/`目录）。**

** Q: 插件可以引用其他 NuGet 包吗？ **
** A：可以，您需要手动下载依赖的DLL并放入`dependencies/`目录。框架启动时会自动加载`dependencies/`下的所有DLL文件。**

** Q: 我可以从哪知道插件的错误信息？ **
** A：你可以从控制台/日志界面知道，或者从`logs/`里寻找与错误发生时间最近时间的log文件。**


---

#### 🎉 恭喜！您已经成功创建了第一个插件！现在可以发挥想象力，为您的机器人添加各种有趣的功能了。
