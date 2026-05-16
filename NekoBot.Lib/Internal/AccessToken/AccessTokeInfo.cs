namespace NekoBot.Lib.Internal.AccessToken
{
    internal class AccessTokenInfo
    {
        private string _accessToken = string.Empty;
        public string AccessToken => _accessToken;
        private DateTime _expireTime;
        public DateTime ExpireTime => _expireTime;
        public bool IsExpired => DateTime.Now >= _expireTime;
        public void FromRes(GetAccessTokenResponse res)
        {
            _accessToken = res.AccessToken;
            _expireTime = DateTime.Now.AddSeconds(res.ExpiresIn);
        }
        public override string ToString()
        {
            return $"AccessToken={_accessToken}, ExpireTime={_expireTime:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
