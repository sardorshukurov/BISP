using System.Text.Json.Serialization;
using Welisten.Client.Common;

namespace Welisten.Client.Models.Authorization;

public class LoginResult : RequestResult
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
}