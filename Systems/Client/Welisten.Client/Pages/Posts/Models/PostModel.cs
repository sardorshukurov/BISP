using Welisten.Client.Pages.Profile.Models;

namespace Welisten.Client.Models.Post;

public class PostModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    
    public UserModel? User { get; set; }
    public DateTime Date { get; set; }
    public ICollection<TopicModel> Topics { get; set; } = [];
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
}