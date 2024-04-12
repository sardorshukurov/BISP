using Welisten.Client.Models.Post;

namespace Welisten.Client.Pages.Posts.Services;

public interface IPostService
{
    Task<IEnumerable<PostModel>> GetPosts();
    Task<IEnumerable<PostModel>> GetPostsByUser();
    Task<IEnumerable<TopicModel>> GetTopics();
    Task CreatePost(CreatePostModel model);
    Task LikeOrDisLike(Guid id);
    Task<PostModel> GetPostById(Guid id);
}