using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Reaction : BaseEntity
{
    public required ReactionType Type { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
}