using System.Net.Http.Json;
using Welisten.Client.Pages.Profile.Models;

namespace Welisten.Client.Pages.Profile.Services;

public class UserService(HttpClient httpClient) : IUserService
{
    public async Task<UserModel> Get()
    {
        var response = await httpClient.GetAsync($"v1/Account/");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return (await response.Content.ReadFromJsonAsync<UserModel>())!;
    }
}