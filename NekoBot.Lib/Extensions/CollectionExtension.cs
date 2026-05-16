namespace NekoBot.Lib.Extensions
{
    /// <summary>
    /// 集合/数组/列表等拓展类
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// 尝试获取<see cref="IEnumerable{T}"/>的指定元素
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/>的类型</typeparam>
        /// <param name="enumerable">指定的<see cref="IEnumerable{T}"/></param>
        /// <param name="index">在的<see cref="IEnumerable{T}"/>位置</param>
        /// <param name="element">获取到特定位置的元素</param>
        /// <returns>是否获取成功</returns>
        public static bool TryGet<T>(this IEnumerable<T> enumerable, int index, out T? element)
        {
            var list = enumerable.ToArray();

            if (index > -1 && index < list.Length)
            {
                element = list[index];
                return true;
            }

            element = default;
            return false;
        }
        /// <summary>
        /// 尝试获取<see cref="IEnumerable{T}"/>的第一个元素
        /// </summary>
        /// <typeparam name="T"><see cref="IEnumerable{T}"/>的类型</typeparam>
        /// <param name="enumerable">指定的<see cref="IEnumerable{T}"/></param>
        /// <param name="element">在的<see cref="IEnumerable{T}"/>位置</param>
        /// <param name="predicate">获取第一个元素的<see langword="lambda"/>函数判断</param>
        /// <returns>是否获取成功</returns>
        public static bool TryGetFirst<T>(this IEnumerable<T> enumerable, out T element, Predicate<T> predicate = null)
        {
            if (predicate != null)
            {
                element = enumerable.FirstOrDefault(predicate.Invoke);
            }
            else
            {
                element = enumerable.FirstOrDefault();
            }
            return element != null && enumerable.Any();
        }
    }
}
