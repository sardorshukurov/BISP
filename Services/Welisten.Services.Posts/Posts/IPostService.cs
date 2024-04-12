namespace Welisten.Services.Posts;

public interface IPostService
{
    Task<IEnumerable<PostModel>> GetAll();
    Task<IEnumerable<PostModel>> GetByUser(Guid userId);
    Task<PostModel> GetById(Guid id);
    Task<PostModel> Create(CreatePostModel model, Guid userId);
    Task Update(Guid id, Guid userId, UpdatePostModel model);
    Task Delete(Guid id, Guid userId);
}