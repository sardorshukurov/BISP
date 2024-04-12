using Welisten.Client.Pages.Profile.Models;

namespace Welisten.Client.Pages.Comments.Models;

public class CommentModel
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    public UserModel? User { get; set; }
    public Guid PostId { get; set; }
}