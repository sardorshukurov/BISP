using Microsoft.AspNetCore.Authentication;

namespace Welisten.Common.Responses;

public class LoginRequestResponse : AuthenticateResult
{
    public string Token { get; set; } = string.Empty;
}