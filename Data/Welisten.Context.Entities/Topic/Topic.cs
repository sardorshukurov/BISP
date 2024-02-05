using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Topic : BaseEntity
{
    public string Type { get; set; } = string.Empty;
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
}