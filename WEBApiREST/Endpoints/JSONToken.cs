using System.Text.Json.Serialization;

namespace WEBApiREST.Endpoints
{
    public class JSONToken
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
    }
}
