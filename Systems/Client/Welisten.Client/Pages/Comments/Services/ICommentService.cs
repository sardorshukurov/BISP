using Welisten.Client.Pages.Comments.Models;

namespace Welisten.Client.Pages.Comments.Services;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetComments(Guid postId);
}