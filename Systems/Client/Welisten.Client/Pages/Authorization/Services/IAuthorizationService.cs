using Welisten.Client.Models.Authorization;

namespace Welisten.Client.Pages.Authorization.Services;

public interface IAuthorizationService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
}