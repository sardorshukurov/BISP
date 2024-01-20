using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Comment : BaseEntity
{
    public required int PostId { get; set; }
    [MaxLength(1000), MinLength(10)]
    public required string Text { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public required bool IsAnonymous { get; set; } = false;
    public virtual ICollection<Comment>? Comments { get; set; }
}