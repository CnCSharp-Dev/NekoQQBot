using NekoBot.Lib.Enums;
using Newtonsoft.Json;

namespace NekoBot.Lib.Models.Chat.Elements
{
    /// <summary>
    /// 消息按钮
    /// </summary>
    public struct GuildMessageButton
    {
        /// <summary>
        /// 按钮ID：在一个KeyBoard消息内设置唯一
        /// </summary>
        [JsonProperty("id")]
        public string Id;
        /// <summary>
        /// [必填]按钮文字
        /// </summary>
        [JsonProperty("render_data.label")]
        public string Label;
        /// <summary>
        /// [必填]点击后按钮的文字
        /// </summary>
        [JsonProperty("render_data.visited_label")]
        public string VisitedLabel;

        /// <summary>
        /// [必填]按钮样式
        /// </summary>
        [JsonProperty("render_data.style")]
        public GuildMessageButtonStyle Style;

        /// <summary>
        /// [必填]按钮行为
        /// </summary>
        [JsonProperty("action.type")]
        public GuildMessageButtonAction Action;
        /// <summary>
        /// [必填]操作权限
        /// </summary>
        [JsonProperty("action.permisson.type")]
        public GuildMessageButtonPermission Permission;
        /// <summary>
        /// 有权限的身份组ID的列表（仅频道可用）
        /// </summary>
        [JsonProperty("action.permisson.specify_role_ids")]
        public List<string> SpecifiedRoleIDs;
        /// <summary>
        /// 有权限的用户ID的列表
        /// </summary>
        [JsonProperty("action.permisson.specify_user_ids")]
        public List<string> SpecifiedUserIDs;
        /// <summary>
        /// [必填]操作相关的数据
        /// </summary>
        [JsonProperty("data")]
        public string Data;
        /// <summary>
        /// 指令按钮可用，指令是否带引用回复本消息
        /// </summary>
        [JsonProperty("reply")]
        public bool? Reply;
        /// <summary>
        /// 指令按钮可用，点击按钮后直接自动发送 data
        /// </summary>
        [JsonProperty("enter")]
        public bool? Enter;
        /// <summary>
        /// 指令按钮可用，自动锚点到选图器，设置<see langword="true"/>后会忽略<see cref="Enter"/>配置
        /// </summary>
        [JsonProperty("anchor")]
        public bool? Anchor;
        /// <summary>
        /// 可操作点击的次数，默认不限
        /// </summary>
        [JsonProperty("click_limit")]
        public int ClickLimit;
        /// <summary>
        /// [必填]客户端不支持本action的时候，弹出的toast文案
        /// </summary>
        [JsonProperty("unsupport_tips")]
        public string UnsupportTips;
    }
}
