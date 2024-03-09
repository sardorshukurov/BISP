using System.Text.Json.Serialization;

namespace Welisten.Client.Models.Authorization;

public class LoginResult
{
    public bool Successful { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; }
}