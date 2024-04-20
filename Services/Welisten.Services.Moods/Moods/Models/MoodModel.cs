namespace Welisten.Services.Moods;

public class MoodModel
{
    public Guid Id { get; set; }
    public string ImageLink { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}