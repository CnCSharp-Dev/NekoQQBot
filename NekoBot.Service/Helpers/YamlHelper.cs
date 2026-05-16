using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

namespace NekoBot.Service.Helpers
{
    /// <summary>
    /// 用于Yaml转换的类
    /// </summary>
    public static class YamlHelper
    {
        /// <summary>
        /// 反序列化器（YAML → 对象），使用驼峰命名约定
        /// </summary>
        public static IDeserializer Deserializer { get; } = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        /// <summary>
        /// 序列化器（对象 → YAML），使用驼峰命名约定
        /// </summary>
        public static ISerializer Serializer { get; } = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
        /// <summary>
        /// 将Yaml文本转化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="yaml">要被转换的文本</param>
        /// <returns>转换后的对象</returns>
        public static T Deserialize<T>(string yaml) => Deserializer.Deserialize<T>(yaml);

        /// <summary>
        /// 将对象转化为Yaml文本
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="target">要被转换的对象</param>
        /// <returns>转换后的文本</returns>
        public static string Serialize<T>(T target) => Serializer.Serialize(target);
        /// <summary>
        /// 将Yaml文件转化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="file">文件路径</param>
        /// <returns>转换后的对象</returns>
        public static T DeserializeFromFile<T>(string file)
        {
            return Deserializer.Deserialize<T>(File.ReadAllText(file));
        }
        /// <summary>
        /// 将Yaml文件转化为对象，如果文件不存在则创建带默认值的文件
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="file">文件路径</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的对象</returns>
        public static T DeserializeFromFile<T>(string file, T defaultValue)
        {
            if(!File.Exists(file))
            {
                File.WriteAllText(file, Serialize(defaultValue));
                return defaultValue;
            }
            return Deserialize<T>(File.ReadAllText(file));
        }
    }
}
