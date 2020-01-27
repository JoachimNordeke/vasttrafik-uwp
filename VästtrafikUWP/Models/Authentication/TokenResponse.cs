using Newtonsoft.Json;

namespace VästtrafikUWP.Models.Authentication
{
    class TokenResponse
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}
