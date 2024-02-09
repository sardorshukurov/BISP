using Welisten.Client.Models.Post;

namespace Welisten.Client.Pages.Posts.Services;

public interface IPostService
{
    Task<IEnumerable<PostModel>> GetPosts();
}