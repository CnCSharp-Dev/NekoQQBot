using NekoBot.Lib.Models.Basic;
using NekoBot.Lib.Models.Chat;
using NekoBot.Lib.Models.Chat.Elements.Medias;

namespace NekoBot.Lib
{
    /// <summary>
    /// 机器人管理类
    /// </summary>
    /// <param name="service">机器人服务</param>
    public class BotManager(BotService service)
    {
        /// <summary>
        /// 机器人服务
        /// </summary>
        public BotService Service => service;
        /// <summary>
        /// 发送用户单聊消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="openId">用户openId</param>
        /// <returns>响应消息</returns>
        public async Task<ChatResponse> SendUserMessageAsync(ChatRequest message, string openId)
        {
            return await service.HttpModule.PostAsync<ChatRequest, ChatResponse>("https://api.sgroup.qq.com/v2/users/" + openId + "/messages", message);
        }
        /// <summary>
        /// 发送群聊聊天消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="groupOpenId">群聊openId</param>
        /// <returns>响应消息</returns>
        public async Task<ChatResponse> SendGroupMessageAsync(ChatRequest message, string groupOpenId)
        {
            return await service.HttpModule.PostAsync<ChatRequest, ChatResponse>("https://api.sgroup.qq.com/v2/groups/" + groupOpenId + "/messages", message);
        }
        /// <summary>
        /// 获取机器人的用户信息
        /// </summary>
        /// <returns>机器人用户信息</returns>
        public async Task<User> GetCurrentUserAsync()
        {
            return await service.HttpModule.GetAsync<User>("https://api.sgroup.qq.com/users/@me");
        }
        /// <summary>
        /// 获取频道信息
        /// </summary>
        /// <param name="guildId">频道Id</param>
        /// <returns>频道信息</returns>
        public async Task<Guild> GetGuildAsync(string guildId)
        {
            return await service.HttpModule.GetAsync<Guild>("https://api.sgroup.qq.com/guilds/" + guildId);
        }
        /// <summary>
        /// 上传用户媒体
        /// </summary>
        /// <param name="media">媒体</param>
        /// <param name="openId">目标用户的OpenID</param>
        /// <returns>上传媒体结果</returns>
        public async Task<MediaResponse> UploadUserMediaAsync(MediaRequest media, string openId)
        {
            return await service.HttpModule.PostAsync<MediaRequest, MediaResponse>("https://api.sgroup.qq.com/v2/users/" + openId + "/files", media);
        }

        /// <summary>
        /// 上传群媒体
        /// </summary>
        /// <param name="media">媒体</param>
        /// <param name="groupOpenId">群聊的OpenID</param>
        /// <returns>上传媒体结果</returns>
        public async Task<MediaResponse> UploadGroupMediaAsync(MediaRequest media, string groupOpenId)
        {
            return await service.HttpModule.PostAsync<MediaRequest, MediaResponse>("https://api.sgroup.qq.com/v2/groups/" + groupOpenId  + "/files", media);
        }
    }
}
