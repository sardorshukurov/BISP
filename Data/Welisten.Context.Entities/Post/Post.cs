using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Post : BaseEntity
{
    [MaxLength(100), MinLength(20)]
    public required string Title { get; set; }
    [MaxLength(3000), MinLength(50)]
    public required string Text { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public required bool IsAnonymous { get; set; } = false;
    public virtual ICollection<Reaction>? Reactions { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
}