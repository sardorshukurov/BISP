using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Mood : BaseEntity
{
    public int? UserId { get; set; }
    public virtual User User { get; set; }
    public int? MoodTypeId { get; set; }
    public virtual MoodType MoodType { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}