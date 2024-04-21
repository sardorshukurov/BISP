using Welisten.Client.Pages.Profile.Models;

namespace Welisten.Client.Pages.Moods.Models;

public class MoodRecordModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public MoodModel? Mood { get; set; }
    public Guid UserId { get; set; }
    public UserModel? User { get; set; }
    public EventModel? Event { get; set; }
}