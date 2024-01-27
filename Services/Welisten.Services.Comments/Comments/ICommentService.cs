namespace Welisten.Services.Comments;

public interface ICommentService
{
    Task<CommentModel> Create(CreateCommentModel model, Guid userId);
    Task Delete(Guid id, Guid userId);   
}