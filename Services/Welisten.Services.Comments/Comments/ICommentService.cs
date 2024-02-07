namespace Welisten.Services.Comments;

public interface ICommentService
{
    Task<List<CommentModel>> GetCommentsById(Guid postId);
    Task<CommentModel> Create(CreateCommentModel model, Guid userId);
    Task Delete(Guid id, Guid userId);   
}