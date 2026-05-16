using NekoBot.HttpTransporter.Models;
using NekoBot.Service;
using YamlDotNet.Serialization;

namespace NekoBot.HttpTransporter
{
    public class Config : IConfig
    {
        [YamlMember(Description = "所有要被加载的命令")]
        public HttpTargetModel[] Models { get; set; } =
        [
            new()
            {
                Command = "命令测试1",
                Url = "http://127.0.0.1:7891/myapi/cmd/testcommand_1",
                Description = "请删除此命令!"
            },
            new()
            {
                Command = "命令测试2",
                Url = "http://127.0.0.1:7891/myapi/cmd/testcommand_2",
                Description = "请删除此命令!"
            }
        ];
    }
}
