namespace Welisten.Client.Pages.Moods.Models;

public class MoodModel
{
    public Guid Id { get; set; }
    public string ImageLink { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public override bool Equals(object o)
    {
        var other = o as MoodModel;
        return other?.Name == Name;
    }
    public override int GetHashCode() => Name?.GetHashCode() ?? 0;
    public override string ToString() => $"{ImageLink} {Name}";
}