namespace Welisten.Services.UserAccounts;

public interface IUserAccountService
{
    Task<bool> IsEmpty();
    Task<UserAccountModel> Register(RegisterDto registerDto);
    Task<string> Login(LoginDto loginDto);
}