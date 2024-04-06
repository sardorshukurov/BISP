using Welisten.Client.Common;
using Welisten.Client.Models.Authorization;
using Welisten.Client.Pages.Authorization.Models;

namespace Welisten.Client.Pages.Authorization.Services;

public interface IAuthorizationService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task<RequestResult> Register(RegisterModel registerModel);
    Task Logout();
}