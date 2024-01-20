using System.ComponentModel.DataAnnotations;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class MoodType : BaseEntity
{
    [MaxLength(50)]
    public required string Name{ get; set; }
}