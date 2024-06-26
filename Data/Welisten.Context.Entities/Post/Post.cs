using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Post : BaseEntity
{
    [MaxLength(100), MinLength(5)]
    public required string Title { get; set; }
    [MaxLength(3000), MinLength(15)]
    public required string Text { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public required bool IsAnonymous { get; set; } = false;

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public PostCount PostCount { get; set; } = null!;
    public virtual ICollection<Comment>? Comments { get; set; }
    public virtual ICollection<Topic> Topics { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
}