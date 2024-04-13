using System.Collections;
using Welisten.Client.Pages.Comments.Models;

namespace Welisten.Client.Pages.Comments.Services;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetComments(Guid postId);
    Task<CommentModel> GetCommentById(Guid id);
    Task<IEnumerable<CommentModel>> GetCommentsByUser();
    Task Comment(CreateCommentModel model);
    Task Delete(Guid id);
    Task Update(Guid id, CreateCommentModel model);
}