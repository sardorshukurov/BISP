using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Validator;
using Welisten.Context.Context;
using Welisten.Context.Entities;

namespace Welisten.Services.Moods;

public class MoodService : IMoodService
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
    private readonly IModelValidator<CreateMoodRecordModel> _createValidator;
    
    private readonly IMapper _mapper;

    public MoodService(IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper, IModelValidator<CreateMoodRecordModel> createValidator)
    {
        _dbContextFactory = dbContextFactory;
        _mapper = mapper;
        _createValidator = createValidator;
    }
    
    public async Task<IEnumerable<MoodModel>> GetAllMoods()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var moods = await context.Moods.ToListAsync();

        return _mapper.Map<IEnumerable<Mood>, IEnumerable<MoodModel>>(moods);
    }

    public async Task<IEnumerable<MoodRecordModel>> GetAllMoodRecords(Guid userId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var moodRecords = await context.MoodRecords
            .Include(mr => mr.Mood)
            .Include(mr => mr.Event)
            .Include(mr => mr.User)
            .OrderByDescending(mr => mr.Date)
            .Where(mr => mr.UserId == userId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MoodRecord>, IEnumerable<MoodRecordModel>>(moodRecords);
    }

    public async Task<IEnumerable<EventModel>> GetAllEvents()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var events = await context.EventTypes.ToListAsync();

        var result = _mapper.Map<IEnumerable<EventType>, IEnumerable<EventModel>>(events);
        
        return result;
    }

    public async Task<MoodRecordModel?> GetMoodRecordById(Guid moodRecordId, Guid userId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var moodRecord = await context.MoodRecords
            .Include(mr => mr.Mood)
            .Include(mr => mr.Event)
            .Include(mr => mr.User)
            .OrderByDescending(mr => mr.Date)
            .Where(mr => mr.UserId == userId)
            .FirstOrDefaultAsync(mr => mr.Uid == moodRecordId);

        return _mapper.Map<MoodRecord?, MoodRecordModel?>(moodRecord);
    }

    public async Task CreateMoodRecord(CreateMoodRecordModel model, Guid userId)
    {
        await _createValidator.CheckAsync(model);

        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var moodRecord = _mapper.Map<MoodRecord>(model);
        
        if (moodRecord.Date.Kind == DateTimeKind.Local) 
            moodRecord.Date = moodRecord.Date.ToUniversalTime();

        moodRecord.UserId = userId;

        context.Attach(moodRecord.Mood);
        context.Attach(moodRecord.Event);

        await context.MoodRecords.AddAsync(moodRecord);
        await context.SaveChangesAsync();
    }
}