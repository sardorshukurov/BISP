namespace Welisten.Services.UserAccounts;

public interface IUserAccountService
{
    Task<bool> IsEmpty();
    Task<UserAccountModel> Create(RegisterUserAccountModel model);
}