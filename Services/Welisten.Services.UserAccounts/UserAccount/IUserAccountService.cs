using System.Security.Claims;

namespace Welisten.Services.UserAccounts;

public interface IUserAccountService
{
    Task<bool> IsEmpty();
    Task<UserAccountModel> Register(RegisterDto registerDto);
    Task<string> Login(LoginDto loginDto);
    Task<bool> IsExpired(ClaimsPrincipal user);
    Task<bool>  Exists(ClaimsPrincipal user);
}