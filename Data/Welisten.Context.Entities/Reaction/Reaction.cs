using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Reaction : BaseEntity
{
    public required string Name { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
}