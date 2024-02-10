using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Welisten.Context.Entities.Common;

namespace Welisten.Context.Entities;

public class PostCount : BaseEntity
{
    public Post Post { get; set; } = null!;
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
}