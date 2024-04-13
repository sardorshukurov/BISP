namespace Welisten.Services.Comments;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetCommentsById(Guid postId);
    Task<CommentModel> GetCommentById(Guid id);
    Task<IEnumerable<CommentModel>> GetCommentsByUser(Guid userId);
    Task<CommentModel> Create(CreateCommentModel model, Guid userId);
    Task Delete(Guid id, Guid userId);
    Task Update(Guid id, CreateCommentModel model);
}