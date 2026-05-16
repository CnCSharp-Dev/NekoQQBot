using System.Text.RegularExpressions;

namespace NekoBot.Service.Helpers
{
    /// <summary>
    /// 命令帮助类，用于解析命令
    /// </summary>
    public static class CommandHelper
    {
        private static readonly Regex CommandPattern = new(
            @"^\s*/(?<cmd>.+?)\s+(?<args>.*)$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture,
            TimeSpan.FromMilliseconds(100)
        );
        private static readonly Regex CommandOnlyPattern = new(
            @"^\s*/(?<cmd>.+?)\s*$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture,
            TimeSpan.FromMilliseconds(100)
        );
        private static readonly Regex ArgSplitPattern = new(
            @"\S+",
            RegexOptions.Compiled,
            TimeSpan.FromMilliseconds(100)
        );

        /// <summary>
        /// 尝试解析无参数命令
        /// </summary>
        /// <param name="command">命令的内容</param>
        /// <param name="context">原始文本</param>
        /// <returns>是否解析成功</returns>
        public static bool TryParseCommand(string command, string context)
        {
            if (string.IsNullOrEmpty(command) || string.IsNullOrEmpty(context))
                return false;

            var match = CommandOnlyPattern.Match(context);
            if (match.Success)
            {
                string cmd = match.Groups["cmd"].Value;
                if (!string.Equals(cmd, command, StringComparison.OrdinalIgnoreCase))
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 尝试解析带参数命令
        /// </summary>
        /// <param name="command">命令的内容</param>
        /// <param name="context">原始文本</param>
        /// <param name="arguments">解析的参数，当命令无参则为空</param>
        /// <returns>是否解析成功</returns>
        public static bool TryParseCommand(string command, string context, out string[] arguments)
        {
            arguments = [];

            if (string.IsNullOrEmpty(command) || string.IsNullOrEmpty(context))
                return false;

            Match match = CommandPattern.Match(context);
            if (match.Success)
            {
                string cmd = match.Groups["cmd"].Value;
                if (!string.Equals(cmd, command, StringComparison.OrdinalIgnoreCase))
                    return false;

                string argsPart = match.Groups["args"].Value;
                if (string.IsNullOrWhiteSpace(argsPart))
                {
                    arguments = [];
                    return true;
                }

                var argMatches = ArgSplitPattern.Matches(argsPart);
                arguments = new string[argMatches.Count];
                for (int i = 0; i < argMatches.Count; i++)
                {
                    arguments[i] = argMatches[i].Value;
                }
                return true;
            }
            match = CommandOnlyPattern.Match(context);
            if (match.Success)
            {
                string cmd = match.Groups["cmd"].Value;
                if (!string.Equals(cmd, command, StringComparison.OrdinalIgnoreCase))
                    return false;
                arguments = [];
                return true;
            }

            return false;
        }
    }
}