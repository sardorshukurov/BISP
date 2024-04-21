using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class MoodRecord : BaseEntity
{
    [MaxLength(1000)] public string Text { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public virtual required Mood Mood { get; set; }
    public Guid UserId { get; set; }
    public virtual required User User { get; set; }
    public virtual required EventType Event { get; set; }
}