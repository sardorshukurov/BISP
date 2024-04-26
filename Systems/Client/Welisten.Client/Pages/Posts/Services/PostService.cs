using System.Collections;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Welisten.Client.Models.Post;
using Welisten.Client.Pages.Posts.Models;

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
        return await response.Content.ReadFromJsonAsync<IEnumerable<PostModel>>() ?? [];
    }

    public async Task<PostPageResponse> GetPostsWithPages(int pageNumber, int pageSize = 10)
    {
        var response = await httpClient.GetAsync($"v1/Post/byPageNumber?pageNumber={pageNumber}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<PostPageResponse>();

        /*// Extract the data from the dynamic object
        var posts = responseContent.item1;
        var totalPages = responseContent.item2;

        return (posts, totalPages);*/
    }

    public async Task<IEnumerable<PostModel>> GetPostsByUser()
    {
        var response = await httpClient.GetAsync("v1/Post/byUser");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<IEnumerable<PostModel>>() ?? [];
    }

    public async Task<IEnumerable<PostModel>> GetPostsByTopics(IEnumerable<Guid> topicIds)
    {
        var queryString = string.Join("&", topicIds.Select(id => $"topicIds={id}"));
        var response = await httpClient.GetAsync($"v1/Post/byTopics?{queryString}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<IEnumerable<PostModel>>() ?? [];
    }

    public async Task<PostPageResponse> GetPostsByTopicsWithPages(IEnumerable<Guid> topicIds, int pageNumber, int pageSize)
    {
        var queryString = string.Join("&", topicIds.Select(id => $"topicIds={id}"));
        var response = await httpClient.GetAsync($"v1/Post/byTopics/byPageNumber?{queryString}&pageNumber={pageNumber}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<PostPageResponse>();
    }

    public async Task<IEnumerable<TopicModel>> GetTopics()
    {
        var response = await httpClient.GetAsync("v1/Topic");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<TopicModel>>() ?? [];
    }

    public async Task CreatePost(CreatePostModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("v1/Post", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception(errorContent);
        }
    }

    public async Task LikeOrDisLike(Guid id)
    {
        var response = await httpClient.PostAsync($"v1/Like/{id}", null);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception(errorContent);
        }
    }

    public async Task<PostModel> GetPostById(Guid id)
    {
        var response = await httpClient.GetAsync($"v1/Post/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return (await response.Content.ReadFromJsonAsync<PostModel>())!;
    }

    public async Task Delete(Guid id)
    {
        var response = await httpClient.DeleteAsync($"v1/Post/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task Update(Guid id, UpdatePostModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PutAsync($"v1/Post/{id}", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception(errorContent);
        }
    }
}