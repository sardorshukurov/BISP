using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Welisten.Client.Common;
using Welisten.Client.Pages.Moods.Models;

namespace Welisten.Client.Pages.Moods.Services;

public class MoodService(HttpClient httpClient) : IMoodService
{
    public async Task<IEnumerable<MoodModel>> GetAllMoods()
    {
        var response = await httpClient.GetAsync("v1/Mood/moods");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        
        return await response.Content.ReadFromJsonAsync<IEnumerable<MoodModel>>() ?? [];
    }

    public async Task<IEnumerable<MoodRecordModel>> GetAllMoodRecords()
    {
        var response = await httpClient.GetAsync("v1/Mood/moodRecords");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        
        return await response.Content.ReadFromJsonAsync<IEnumerable<MoodRecordModel>>() ?? [];
    }

    public async Task<IEnumerable<EventModel>> GetAllEvents()
    {
        var response = await httpClient.GetAsync("v1/Mood/events");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<EventModel>>() ?? [];
    }

    public async Task<MoodRecordModel?> GetMoodRecordById(Guid id)
    {
        var response = await httpClient.GetAsync($"v1/Mood/moodRecords/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<MoodRecordModel?>();
    }

    public async Task CreateMoodRecord(CreateMoodRecordModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("v1/Mood", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            var registerResult = JsonSerializer.Deserialize<RequestResult>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new RequestResult();
            throw new Exception(registerResult.ErrorDescription);
        }
    }
}