using Welisten.Client.Models.Post;
using Welisten.Client.Pages.Posts.Models;

namespace Welisten.Client.Pages.Posts.Services;

public interface IPostService
{
    Task<IEnumerable<PostModel>> GetPosts();
    Task<PostPageResponse> GetPostsWithPages(int pageNumber, int pageSize);
    Task<IEnumerable<PostModel>> GetPostsByUser();
    Task<IEnumerable<PostModel>> GetPostsByTopics(IEnumerable<Guid> topicIds);
    Task<PostPageResponse> GetPostsByTopicsWithPages(IEnumerable<Guid> topicIds, int pageNumber, int pageSize);
    Task<IEnumerable<TopicModel>> GetTopics();
    Task CreatePost(CreatePostModel model);
    Task LikeOrDisLike(Guid id);
    Task<PostModel> GetPostById(Guid id);
    Task Delete(Guid id);
    Task Update(Guid id, UpdatePostModel model);
}