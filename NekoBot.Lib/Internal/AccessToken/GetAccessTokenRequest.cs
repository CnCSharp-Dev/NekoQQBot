using Newtonsoft.Json;

namespace NekoBot.Lib.Internal.AccessToken
{
    internal struct GetAccessTokenRequest(string appID, string clientSecret)
    {
        [JsonProperty("appId")]
        public string AppID = appID;

        [JsonProperty("clientSecret")]
        public string ClientSecret = clientSecret;
    }
}
