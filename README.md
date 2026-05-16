# NekoQQBot

![License](https://img.shields.io/badge/license-MIT-blue.svg) ![License](https://img.shields.io/badge/dotnet-8.0-red.svg) ![Releases](https://img.shields.io/github/v/release/CnCSharp-Dev/NekoQQBot) ![Last Commit](https://img.shields.io/github/last-commit/CnCSharp-Dev/NekoQQBot) ![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux%20%7C%20macOS-lightgrey)

## ✨ 介绍

> ⚠️ 本框架为开源社区项目，与腾讯官方无直接关联。使用前请阅读 QQ 机器人开放平台相关条款。
> 📌 **早期测试版** – 框架仍在开发中，可能随时发生变动，欢迎提交issue/PR。
> ❌ **未完成** - 频道事件过于复杂，需要很长的时间才能完成频道事件。

适用于QQ官方机器人的社区框架，灵感与部分代码来源于[QQBot.NET](https://github.com/ZiYuKing/QQBot.NET)，提供高性能、可扩展的机器人服务核心。

 - 内置对象池库[PoolingLib](https://github.com/CNCSharp-Dev/PoolingLib) & 结构化Json减少GC压力(因为堆栈分配问题，后续会进行优化)
 - 高效的事件驱动模型，自动注册插件事件，告别大量繁琐的手动注册。
 - 允许加载第三方程序集/依赖来扩展命令、自定义内容。
 - 一个程序只能指定单个机器人，防止多机器人造成混乱。
 - 利用[LiteDB](https://github.com/litedb-org/LiteDB) 和 [Serilog.Sinks.File](https://github.com/serilog/serilog-sinks-file)储存WebSocket日志/聊天记录于子目录`logs`与`databases`中。

## 📦 项目结构

```text
NekoQQBot/
├── NekoBot.Lib						# 底层库，提供WebSocket连接
├── NekoBot.Service					# 服务处理库，用于处理插件/指令/配置文件注册&机器人创建
|
├── Official-Plugins
|   ├── NekoBot.HttpTransporter    	# Http(s)转发插件，可通过外接ASP.NET快速实现指令
|   └── NekoBot.ExtensionCommands  	# 指令扩展插件，仅提供help/plugins/copyright三种指令示例
|
└── Official-Tools/
	├── NekoBot.WPFProgram			 # WPF窗口版应用程序
	├── NekoBot.ConsoleProgram		 # 控制台版应用程序，使用Serilog
	└── NekoBot.ExtensionTools		 # 提供日志/聊天记录与自动GC回收
```

| 项目 | 描述 |
|------|------|
| `NekoBot.Lib` | 实现机器人核心、事件系统、WebSocket客户端、对象池等基础功能 |
| `NekoBot.Service` | 实现高级服务API、命令系统、配置管理 |
| `NekoBot.WPFProgram` | 现代化WPF桌面GUI，适合需要可视化管理的用户 |
| `NekoBot.ConsoleProgram` | 轻量级控制台版本，适合服务器部署 |

### 依赖:
- `Newtonsoft.Json`
- `LiteDB`
- `WatsonWebsocket`
- `YamlDotNet`
- `Serilog`与衍生的`Serilog.Sinks.Console`、`Serilog.Sinks.Async`、`Serilog.Sinks.File`

## 📦 插件:

**不要盲目信任他人**，你需要寻找受信任的插件。

插件需要装在 `程序根目录/plugins`目录内，依赖需要装在 `程序根目录/dependencies`目录内。

一般情况下，只有直接或间接继承`Plugin`或`Plugin<TConfig>`才会使程序集被当作插件，其他 DLL 会被当作依赖加载但不激活插件逻辑。

这是一个最基础的无配置文件类的插件抽象类，必须继承此类，程序集才会被识别为插件，具体参考开发文档[插件开发教程](docs/plugin.md)：
```csharp
public abstract class Plugin : IPlugin
{
    public abstract string Name { get; } //插件的名称，推荐为程序集名称

    public virtual PluginType PluginType { get; } = PluginType.Misc; //插件类型，参考本枚举的源代码，推荐使用插件

    public virtual void OnEnabled() //当插件启动时调用
    {
    }

    public virtual void OnDisabled() //当插件关闭时调用，一般为了重置值会在插件启动前调用一次
    {
    }

    public virtual void LoadConfig() //加载配置文件调用的方法，为底层提供，通常不建议重写
    {
    }
}
```

加载流程图如下，具体参考NekoBot.Service的内容。

#### 📦 插件加载流程：

```flow
st=>start: 开始加载
op_load_dep=>operation: 加载依赖程序集
op_load_plugin=>operation: 加载插件程序集
cond_plugin=>condition: 是否继承 Plugin?
op_register=>operation: 注册插件
op_load_config=>operation: 加载插件配置
op_disable=>operation: 调用 OnDisabled()
op_enable=>operation: 调用 OnEnabled()
op_init_eb=>operation: 初始化事件总线
e=>end: 加载完成

st->op_load_dep->op_load_plugin->cond_plugin
cond_plugin(yes)->op_register->op_load_config->op_disable->op_enable->op_init_eb->e
cond_plugin(no)->e
```

## 🚀 快速开始

### 环境要求

- 需要安装[.NET 8 SDK](https://dotnet.microsoft.com/download) 或更高版本。
- Windows / Linux / macOS（控制台版）。
- **确保后台所在的磁盘有充足的空间储存日志数据**。
- **确认能访问**[QQ机器人API网页](https://api.sgroup.qq.com/)。

### 安装

从已发布的[最新 Release](https://github.com/CnCSharp-Dev/NekoQQBot/releases/latest)里面选择`NekoBot.ConsoleProgram.zip`、`NekoBot.WPFProgram.zip`或下载更加快速的安装程序`NekoBot.Installer.exe`，无需手动编译项目。

### 配置
首次运行后会在工作目录/configs文件夹内生成`bot.yml`，编辑以下内容：

```yaml
# 注册的事件，具体参考QQ官方开发文档
intent: ALL_PUBLIC # 参照下表
# 机器人的AppId
appId: '$这里填写你机器人的AppId$'
# 机器人的Secret
secret: '$这里填写你机器人的Secret$'
```

> 推荐订阅的事件如下，具体内容/对照表可从[Intents.md](intents.md)获取:

>| 事件名称 | 配置值 | 推荐使用范围 |
|-----|-----|-----|
| 所有公域事件 | `ALL_PUBLIC` | 公域机器人 |
| 所有事件 | `ALL` | 私域机器人 |

如果使用的是`NekoBot.ConsoleProgram`则会生成`program.yml`，可以自行编辑：

```yaml
# 控制台标题模板，{BotName}为机器人昵称，{WebSocketUrl}为WebSocket地址，{LogLevel}为当前最低日志输出等级
consoleTitleTemplate: '{BotName} 后台 | 监听地址 {WebSocketUrl} | 日志最低限制: {LogLevel}'
# 日志最低输出等级: Verbose < Debug < Information < Warning < Error < Fatal
logLevel: Information
# 是否启动自动重启
autoRestart: true
# 自动重启等待时间，当无法正常初始化后超过该时间则重新初始化
autoRestartDelay: 10 # 单位为秒
```

### 开发

#### 内容可以参考[插件开发教程](docs/plugin.md)，下面是拓展命令示例:

通过实现 `ICommand`接口创建非异步指令，下面为一个最简易的命令：

```csharp
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Commands;

namespace YourPlugin
{
    public class MyCommand : ICommand
	{
        public string Command => "mycommand"; //命令名，推荐小写

        public string Description => "我的第一个命令"; //命令注释

        public string OnExecute(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            return "指令已执行"; // 返回的字符串会作为文本消息自动回复
        }
    }
}
```

与此同时，你也可以通过`IAsyncCommand`接口实现异步指令，下面是一个发送百度logo图片的命令:

```csharp
using NekoBot.Lib.Enums;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Image;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Commands;

namespace YourPlugin
{
    public class BaiduLogoCommand : IAsyncCommand
    {
        public string Command => "baidulogo";

        public string Description => "百度logo图片";

        public async Task<string> OnExecuteAsync(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            var media = await ImageManager.GetMediaAsync(ev, url: "https://www.baidu.com/img/PCtm_d9c8750bed0b3c7d089fa7d55720d6cf.png"); //上传并获取图片
            await ev.ReplyAsync(new() //回复图片
            {
                MessageId = ev.Message.Id,
                Type = MessageType.Media,
                Media = media
            });
            return string.Empty; //为空则不自动回复
        }
    }
}
```

如果你想要发送的图片不在网页上，你可以注册自定义的图片：

```csharp
using NekoBot.Lib.Enums;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Image;
using NekoBot.Lib.Models.Chat;
using NekoBot.Service.Commands;
using NekoBot.Service;

namespace YourPlugin
{
    public class MyPhotoCommand : IAsyncCommand
    {
        public string Command => "myphoto";

        public string Description => "输出我的图片";

        public async Task<string> OnExecuteAsync(string[] arguments, ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev)
        {
            ImageRegistry.RegisterImageFromFile(Path.Combine(Paths.RootPath, "111.png"), "myphoto", false); //注册程序根目录的111.png文件
            var media = await ImageManager.GetMediaAsync(ev, id: "myphoto"); //通过Id上传并获取myphoto

            await ev.ReplyAsync(new() //回复图片
            {
                MessageId = ev.Message.Id,
                Type = MessageType.Media,
                Media = media
            });
            return string.Empty; //为空则不自动回复
        }
    }
}

```

## 🤝 贡献

我们欢迎任何形式的贡献

### 如何提交PR🤔
- **1**.Fork本仓库。
- **2**.更改你要更改的代码。
- **3**.提交更改。
- **4**.提交 Pull Request，写下您修改或改进的主要内容并指出好处。

### 如何提交issue (错误)🤔
- **1**.出现的Bug与问题需要完整的填写，切勿提交例如"为什么没有修复这个bug?"或"有bug，快去修"等含糊不明的issue。
- **2**.具体的描述触发的方法。
- **3**.**我们不算卦**，我们始终推荐您上传完整的日志文件。
- **4**.**注意隐私保护**，内容切勿含有机器人的隐私信息。

### 如何提交issue (想法)🤓
- **1**.可以不具体的描述想法，但请提交**合理且现实**的想法。
- **2**.如果有能能力可以提交PR或者部分源代码。
- **3**.提交您的想法。


## 📃  📞 版权与联系方式

作者团队：[CNCSharp-Dev](https://github.com/CNCSharp-Dev)，QQ群 : 1091972542

项目主页：[NekoQQBot](https://github.com/CNCSharp-Dev/NekoQQBot)

问题反馈：[Issues](https://github.com/CNCSharp-Dev/NekoQQBot/issues)

协议: [MIT](LICENSE)

以下为版权命令的内容，具体信息可以从仓库源码中获取。

```text
© CnCSharp-Dev 2026

本项目采用 MIT 许可证 – 详见 [LICENSE](LICENSE) 文件。

NekoBot Official Framework
━━━━━━━━━━━━━━━━━━━━━
👥 开发者：CNCSharp-Dev
🗺 仓库(github)：https://www.github.com/CNCSharp-dev/NekoQQBot
━━━━━━━━━━━━━━━━━━━━━
📌 框架处于早期测试版，出现问题请尽快上报
```
