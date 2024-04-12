using Welisten.Client.Pages.Profile.Models;

namespace Welisten.Client.Pages.Profile.Services;

public interface IUserService
{
    Task<UserModel> Get();
}