using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Welisten.Client.Pages.Comments.Models;

namespace Welisten.Client.Pages.Comments.Services;

public class CommentService(HttpClient httpClient) : ICommentService
{
    public async Task<IEnumerable<CommentModel>> GetComments(Guid postId)
    {
        var response = await httpClient.GetAsync($"v1/Comment/{postId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<CommentModel>>() ?? new List<CommentModel>();
    }

    public async Task<CommentModel> GetCommentById(Guid id)
    {
        var response = await httpClient.GetAsync($"v1/Comment/byId/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return (await response.Content.ReadFromJsonAsync<CommentModel>())!;
    }

    public async Task<IEnumerable<CommentModel>> GetCommentsByUser()
    {
        var response = await httpClient.GetAsync("v1/Comment/byUser");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<CommentModel>>() ?? new List<CommentModel>();
    }

    public async Task Comment(CreateCommentModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("v1/Comment", content);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception(errorContent);
        }
    }

    public async Task Delete(Guid id)
    {
        var response = await httpClient.DeleteAsync($"v1/Comment/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task Update(Guid id, CreateCommentModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync($"v1/Comment/{id}", content);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception(errorContent);
        }
    }
}