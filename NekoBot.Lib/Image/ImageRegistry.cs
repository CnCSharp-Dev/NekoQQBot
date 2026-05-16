namespace NekoBot.Lib.Image
{
    /// <summary>
    /// 图片管理器
    /// </summary>
    public static class ImageRegistry
    {
        private static readonly HttpClient _httpClient = new();
        private static readonly Dictionary<string, string> _dict = [];
        /// <summary>
        /// 所有被注册的字典
        /// </summary>
        public static IReadOnlyDictionary<string, string> Dictionary => _dict;
        /// <summary>
        /// 将图片以<see langword="base64"/>形式注册
        /// </summary>
        /// <param name="filePath">图片文件的绝对路径</param>
        /// <param name="id">文件Id</param>
        /// <param name="override">是否替换已有图片</param>
        /// <returns>如果添加成功就返回true，否则返回false</returns>
        public static bool RegisterImageFromFile(string filePath, string id, bool @override = false)
        {
            try
            {
                if (!@override && Dictionary.ContainsKey(id))
                    return false;

                byte[] imageBytes = File.ReadAllBytes(filePath);
                string base64 = Convert.ToBase64String(imageBytes);
                _dict[id] = base64;
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// 将图片以<see langword="base64"/>形式注册
        /// </summary>
        /// <param name="url">图片文件的Url</param>
        /// <param name="id">文件Id</param>
        /// <param name="client">进行操作的<see cref="HttpClient"/>，为<see langword="null"/>则为默认</param>
        /// <param name="override">是否替换已有图片</param>
        /// <returns>如果添加成功就返回true，否则返回false</returns>
        public static bool RegisterImageFromFile(string url, string id, HttpClient client = null, bool @override = false)
        {
            try
            {
                if (!@override && Dictionary.ContainsKey(id))
                    return false;

                client ??= _httpClient;

                Task.Run(async () =>
                {
                    var data = await client.GetByteArrayAsync(url);
                    string base64 = Convert.ToBase64String(data);
                    _dict[id] = base64;
                });
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// 将图片以<see langword="base64"/>形式注册
        /// </summary>
        /// <param name="url">图片文件的Url</param>
        /// <param name="id">文件Id</param>
        /// <param name="client">进行操作的<see cref="HttpClient"/>，为<see langword="null"/>则为默认</param>
        /// <param name="override">是否替换已有图片</param>
        /// <returns>如果添加成功就返回true，否则返回false</returns>
        public static async Task<bool> RegisterImageFromFileAsync(string url, string id, HttpClient client = null, bool @override = false)
        {
            try
            {
                if (!@override && Dictionary.ContainsKey(id))
                    return false;

                client ??= _httpClient;

                var data = await client.GetByteArrayAsync(url);
                string base64 = Convert.ToBase64String(data);
                _dict[id] = base64;
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// 通过Id获取指定的<see langword="base64"/>图片
        /// </summary>
        /// <param name="id">图片注册的Id</param>
        /// <returns>获取到的<see langword="base64"/>化图片</returns>
        public static string GetById(string id)
        {
            if (Dictionary.TryGetValue(id, out var value))
            {
                return value;
            }
            return string.Empty;
        }
        /// <summary>
        /// 尝试通过Id获取指定的<see langword="base64"/>图片
        /// </summary>
        /// <param name="id">图片注册的Id</param>
        /// <param name="result">返回的<see langword="base64"/>化图片</param>
        /// <returns>如果获取成功就返回<see langword="true"/>，否则返回<see langword="false"/></returns>
        public static bool TryGetById(string id, out string result)
        {
            if (Dictionary.TryGetValue(id, out var value))
            {
                result = value;
                return true;
            }
            result = string.Empty;
            return false;
        }
    }
}
