using NekoBot.Lib.Loggers;

namespace NekoBot.Lib.Internal.AccessToken
{
    internal class AccessTokenUpdater(BotService context)
    {
        private readonly BotService _context = context;
        private readonly AccessTokenInfo _info = new();
        public async Task<string> GetAccessTokenAsync()
        {
            if (!_info.IsExpired)
            {
                return _info.AccessToken;
            }
            try
            {
                var res = await _context.HttpModule.PostAsync<GetAccessTokenRequest, GetAccessTokenResponse>(
                    "https://bots.qq.com/app/getAppAccessToken",
                    new(_context.Info.AppID, _context.Info.Secret),
                    true
                    );
                _info.FromRes(res);
                Logger.Debug($"成功更新AccessToken");
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return _info.AccessToken;
        }
    }
}
