namespace Welisten.Services.Posts;

public interface IPostService
{
    Task<IEnumerable<PostModel>> GetAll();
    Task<PostModel> GetById(Guid id);
    Task<PostModel> Create(CreatePostModel model, Guid userId);
    Task Update(Guid id, UpdatePostModel model);
    Task Delete(Guid id);
}