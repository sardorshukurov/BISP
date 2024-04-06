using Welisten.Client.Models.User;

namespace Welisten.Client.Models.Post;

public class PostModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    
    public UserModel? User { get; set; }
    public required DateTime Date { get; set; }
    public required ICollection<string> Topics { get; set; }
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
}