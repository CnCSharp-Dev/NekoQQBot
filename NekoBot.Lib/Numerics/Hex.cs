using PoolingLib.Pools;

namespace NekoBot.Lib.Numerics
{
    /// <summary>
    /// 表示一个16进制单位
    /// </summary>
    public struct Hex
    {
        /// <summary>
        /// 将该Hex单位转化为文本
        /// </summary>
        /// <returns>转化后的文本</returns>
        public readonly string ToText()
        {
            var builder = StringBuilderPool.Pool.Get();

            foreach(var p in Code)
            {
                builder.Append(Convert.ToString(p,16));
            }

            return StringBuilderPool.Pool.ToStringReturn(builder);
        }
        /// <summary>
        /// 该16进制单位所对应的<see langword="byte"/>
        /// </summary>
        public byte[] Code;
    }
}
