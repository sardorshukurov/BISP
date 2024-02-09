using System.Net.Http.Json;
using Welisten.Client.Models.Post;

namespace Welisten.Client.Pages.Posts.Services;

public class PostService(HttpClient httpClient) : IPostService
{
    public async Task<IEnumerable<PostModel>> GetPosts()
    {
        var response = await httpClient.GetAsync("v1/Post");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<IEnumerable<PostModel>>() ?? new List<PostModel>();
    }
}