namespace Welisten.Client.Pages.Articles.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public ArticleCategory? Category { get; set; }
}