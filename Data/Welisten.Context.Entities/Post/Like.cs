using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class Like : BaseEntity
{
    public virtual User User { get; set; }
    public virtual Post Post { get; set; }
}