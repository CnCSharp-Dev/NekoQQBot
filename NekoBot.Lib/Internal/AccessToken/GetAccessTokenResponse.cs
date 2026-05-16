using Newtonsoft.Json;

namespace NekoBot.Lib.Internal.AccessToken
{
    internal struct GetAccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken;

        [JsonProperty("expires_in")]
        public int ExpiresIn;
    }
}
