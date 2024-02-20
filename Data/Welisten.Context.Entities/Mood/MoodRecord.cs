using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class MoodRecord : BaseEntity
{
    public DateTime Date { get; set; } = DateTime.Today;
    public virtual required Mood Mood { get; set; }
    public Guid UserId { get; set; }
    public virtual required User User { get; set; }
    public virtual required ICollection<EventType> Event { get; set; }
}