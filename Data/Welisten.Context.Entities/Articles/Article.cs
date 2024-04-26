using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Welisten.Context.Entities.Articles;

public class Article
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Link { get; set; }
    public virtual ArticleCategory Category { get; set; }
}