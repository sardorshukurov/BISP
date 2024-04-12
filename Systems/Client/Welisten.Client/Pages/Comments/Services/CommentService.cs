using System.Net.Http.Json;
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
}