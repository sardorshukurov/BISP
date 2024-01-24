using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Welisten.Context.Entities;

public class PostCount
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public virtual required Post Post { get; set; }
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
}