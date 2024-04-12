using Welisten.Client.Models.User;

namespace Welisten.Client.Pages.Comments.Models;

public class CommentModel
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    public UserModel? User { get; set; }
}