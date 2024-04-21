namespace Welisten.Client.Pages.Moods.Models;

public class MoodModel
{
    public Guid Id { get; set; }
    public string Emoji { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}