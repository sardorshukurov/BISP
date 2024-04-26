using Welisten.Client.Pages.Moods.Models;

namespace Welisten.Client.Pages.Moods.Services;

public interface IMoodService
{
    Task<IEnumerable<MoodModel>> GetAllMoods();
    Task<IEnumerable<MoodRecordModel>> GetAllMoodRecords();
    Task<IEnumerable<EventModel>> GetAllEvents();
    Task<MoodRecordModel?> GetMoodRecordById(Guid id);
    Task CreateMoodRecord(CreateMoodRecordModel model);
    Task UpdateMoodRecord(Guid id, CreateMoodRecordModel model);
    Task DeleteMoodRecord(Guid id);
}