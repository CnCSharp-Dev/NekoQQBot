namespace NekoBot.Lib.Numerics
{
    /// <summary>
    /// 一个数学库，内涵实用的数学方法
    /// </summary>
    public static class Mathf
    {
        /// <summary>
        /// 获取两个值范围的随机值(min ≤ num ＜ max)
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>返回的随机值</returns>
        public static int Next(int min, int max) 
        {
            return Random.Shared.Next(Min(min, max), Max(min, max));
        }
        /// <summary>
        /// 判断一个值是否在两个值范围内(min ≤ value ≤ max)
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>如果要判断的值在范围内就返回<see langword="true"/>，否则返回<see langword="false"/></returns>
        public static bool InRange(int value,int min,int max)
        {
            int realmin = Min(min, max);
            int realmax = Max(min, max);

            return realmin <= value && value <= realmax;
        }
        /// <summary>
        /// 从两个数字中获取最大值
        /// </summary>
        /// <param name="num1">数字1</param>
        /// <param name="num2">数字2</param>
        /// <returns>两个数字中最大的值</returns>
        public static int Max(int num1, int num2) 
        {
            if (num1 < num2)
                return num2;
            return num1;
        }
        /// <summary>
        /// 从两个数字中获取最小值
        /// </summary>
        /// <param name="num1">数字1</param>
        /// <param name="num2">数字2</param>
        /// <returns>两个数字中最小的值</returns>
        public static int Min(int num1, int num2)
        {
            if (num1 > num2)
                return num2;
            return num1;
        }

    }
}
