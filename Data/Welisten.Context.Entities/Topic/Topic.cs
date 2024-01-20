using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Topic : BaseEntity
{
    public required string Name { get; set; }
    public ICollection<User> Users { get; set; }
}