using System.Text.Json.Serialization;

namespace Welisten.Client.Common;

public class RequestResult
{
    public bool Successful { get; set; }
    [JsonPropertyName("error")]
    public string Error { get; set; }
    [JsonPropertyName("message")]
    public string ErrorDescription { get; set; }
}