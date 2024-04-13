using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Welisten.Client.Common;
using Welisten.Client.Models.Authorization;
using Welisten.Client.Pages.Authorization.Models;
using Welisten.Client.Providers;

namespace Welisten.Client.Pages.Authorization.Services;

public class AuthorizationService : IAuthorizationService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";
    
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthorizationService(HttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }
    
    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var url = $"{Settings.ApiRoot}/v1/Account/login";

        var requestBody = new[] 
        {
            new KeyValuePair<string, string>("email", loginModel.Email!),
            new KeyValuePair<string, string>("password", loginModel.Password!)
        };

        var requestContent = new FormUrlEncodedContent(requestBody);

        var response = await _httpClient.PostAsync(url, requestContent);
    
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        loginResult.Successful = response.IsSuccessStatusCode;
        
        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await _localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.Token);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email!);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

        return loginResult;
    }

    public async Task<RequestResult> Register(RegisterModel registerModel)
    {
        var url = $"{Settings.ApiRoot}/v1/Account/register";

        var requestBody = new[] 
        {        
            new KeyValuePair<string, string>("name", registerModel.Name),
            new KeyValuePair<string, string>("firstName", registerModel.FirstName),
            new KeyValuePair<string, string>("lastName", registerModel.LastName),
            new KeyValuePair<string, string>("email", registerModel.Email),
            new KeyValuePair<string, string>("password", registerModel.Password)
        };

        var requestContent = new FormUrlEncodedContent(requestBody);

        var response = await _httpClient.PostAsync(url, requestContent);
        
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        var registerResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new RequestResult();
        registerResult.Successful = response.IsSuccessStatusCode;
        
        return registerResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}