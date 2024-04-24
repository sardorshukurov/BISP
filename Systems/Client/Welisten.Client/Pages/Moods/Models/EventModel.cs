namespace Welisten.Client.Pages.Moods.Models;

public class EventModel
{
    public Guid Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public override bool Equals(object o)
    {
        var other = o as EventModel;
        return other?.Name == Name;
    }

    public override int GetHashCode() => Name?.GetHashCode() ?? 0;

    public override string ToString() => Name;
}