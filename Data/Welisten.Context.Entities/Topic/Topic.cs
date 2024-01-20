using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Topic : BaseEntity
{
    public required string Name { get; set; }
    public virtual ICollection<User> Users { get; set; }
}