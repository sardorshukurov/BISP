using Welisten.Client.Pages.Profile.Models;

namespace Welisten.Client.Pages.Comments.Models;

public class CommentModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    public UserModel? User { get; set; }
    public Guid PostId { get; set; }
    public DateTime Date { get; set; }
}