using Welisten.Context.Entities;

namespace Welisten.Services.Posts;

public class CreatePostModel
{
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; } = false;
    public Guid UserId { get; set; }
    public required IEnumerable<Reaction> Reactions { get; set; }
}