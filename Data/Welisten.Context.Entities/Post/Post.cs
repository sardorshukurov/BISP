using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Post : BaseEntity
{
    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength(3000)]
    public required string Text { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public required bool IsAnonymous { get; set; } = false;
    public virtual ICollection<Reaction>? Reactions { get; set; }
}