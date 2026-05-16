using NekoBot.Lib.Enums;
using NekoBot.Lib.Events.EventArgs.Chat;
using NekoBot.Lib.Loggers;
using NekoBot.Lib.Models.Chat;
using NekoBot.Lib.Models.Chat.Elements.Medias;

namespace NekoBot.Lib.Image
{
    /// <summary>
    /// 图片管理类
    /// </summary>
    public static class ImageManager
    {
        /// <summary>
        /// 上传一个图片
        /// </summary>
        /// <param name="ev">聊天事件</param>
        /// <param name="url">文件Id，如果为<see cref="string.Empty"/>则切换为文件Id模式</param>
        /// <param name="id">文件Id，如果为<see cref="string.Empty"/>则切换为url模式</param>
        /// <returns>返回的<see cref="MediaResponse"/></returns>
        public static async Task<MediaResponse> GetMediaAsync(ChatBaseEventArgs<ChatReceive, ChatRequest, ChatResponse> ev, string url = "", string id = "")
        {
            if (string.IsNullOrEmpty(url) && string.IsNullOrEmpty(id))
            {
                Logger.Error(nameof(url) + "与" + nameof(id) + "不能同时为空");
                return null;
            }
            MediaRequest mediareq;
            if (!string.IsNullOrEmpty(url))
            {
                mediareq = new MediaRequest()
                {
                    ServeSendMessage = false,
                    FileType = MediaType.Image,
                    Url = url,
                };
            }
            else
            {
                var base64 = "";

                if (ImageRegistry.TryGetById(id, out var result))
                {
                    base64 = result;
                }

                mediareq = new MediaRequest()
                {
                    ServeSendMessage = false,
                    FileType = MediaType.Image,
                    FileData = base64
                };
            }


            if (!string.IsNullOrEmpty(ev.Message.GroupOpenId))
                return await ev.Service.Manager.UploadGroupMediaAsync(mediareq, ev.Message.GroupOpenId);
            else
                return await ev.Service.Manager.UploadUserMediaAsync(mediareq, ev.Message.Author.Id);
        }
    }
}
