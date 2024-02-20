using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Mood : BaseEntity
{
    [MinLength(3), MaxLength(50)]
    public required string Name { get; set; }
    [MinLength(5), MaxLength(2048)]
    public required string ImageLink { get; set; }
}