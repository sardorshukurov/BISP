using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Mood : BaseEntity
{
    public Guid? UserId { get; set; }
    public virtual User User { get; set; }
    
    public ICollection<int>? MoodTypeId { get; set; }
    public virtual ICollection<MoodType>? MoodTypes { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;
}