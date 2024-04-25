using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Exceptions;
using Welisten.Context.Context;
using Welisten.Context.Entities;
using Welisten.Services.Logger.Logger;

namespace Welisten.Services.Topics;

public class TopicService : ITopicService
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

    private readonly IMapper _mapper;
    public TopicService(IDbContextFactory<MainDbContext> dbContextFactory,
            IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TopicModel>> GetAll()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var topics = await context.Topics
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicModel>>(topics);

        return result;
    }

    public async Task<TopicModel> Create(string typeName)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var topic = _mapper.Map<Topic>(new TopicModel { Id = Guid.NewGuid(), Type = typeName });

        await context.Topics.AddAsync(topic);
        await context.SaveChangesAsync();
        
        return _mapper.Map<TopicModel>(topic);
    }

    public async Task Update(Guid id, string typeName)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var topic = await context.Topics.FirstOrDefaultAsync(t => t.Uid == id);

        if (topic == null)
            throw new ProcessException($"Topic with ID: {id} not found");

        context.Entry(topic).State = EntityState.Modified;

        topic.Type = typeName;

        await context.SaveChangesAsync();
    }
}