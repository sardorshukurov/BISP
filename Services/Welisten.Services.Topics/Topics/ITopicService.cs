namespace Welisten.Services.Topics;

public interface ITopicService
{
    Task<IEnumerable<TopicModel>> GetAll();
    Task<TopicModel> Create(string typeName);
    Task Update(Guid id, string typeName);
}