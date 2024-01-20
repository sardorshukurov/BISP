using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Comment : BaseEntity
{
    [MaxLength(1000)]
    public required string Text { get; set; }
    public required DateTime Date { get; set; } = DateTime.Now;
    public required bool IsAnonymous { get; set; } = false;
}