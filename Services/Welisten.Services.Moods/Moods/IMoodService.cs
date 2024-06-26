namespace Welisten.Services.Moods;

public interface IMoodService
{
    Task<IEnumerable<MoodModel>> GetAllMoods();
    Task<IEnumerable<MoodRecordModel>> GetAllMoodRecords(Guid userId);
    Task<IEnumerable<EventModel>> GetAllEvents();
    Task<MoodRecordModel?> GetMoodRecordById(Guid moodRecordId, Guid userId);
    Task CreateMoodRecord(CreateMoodRecordModel model, Guid userId);
    Task<MoodRecordModel> UpdateMoodRecord(Guid id, CreateMoodRecordModel model, Guid userId);
    Task DeleteMoodRecord(Guid id, Guid userId);
}