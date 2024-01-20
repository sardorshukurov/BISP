using System.ComponentModel.DataAnnotations;

namespace Welisten.Context.Entities;

public class PostCount
{
    [Key]
    public int Id { get; set; }
    public virtual required Post Post { get; set; }
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
}