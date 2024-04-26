using System.Security.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Exceptions;
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

    public async Task<MoodRecordModel> UpdateMoodRecord(Guid id, CreateMoodRecordModel model, Guid userId)
    {
        await _createValidator.CheckAsync(model);

        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var moodRecord = await context.MoodRecords
            .Include(mr => mr.Event)
            .Include(mr => mr.User)
            .Include(mr => mr.Mood)
            .FirstOrDefaultAsync(mr => mr.Uid == id);

        if (moodRecord == null)
            throw new ProcessException($"Mood record with ID: {id} not found");

        if (moodRecord.UserId != userId)
            throw new AuthenticationException("Authentication  failed");

        context.Entry(moodRecord).State = EntityState.Modified;

        moodRecord.Text = model.Text;
        moodRecord.Mood = await context.Moods.FirstAsync(m => m.Uid == model.MoodId);
        moodRecord.Event = await context.EventTypes.FirstAsync(e => e.Uid == model.EventId);
        moodRecord.Date = model.Date;
        
        await context.SaveChangesAsync();

        var updatedMoodRecord = await context.MoodRecords
            .Include(mr => mr.Event)
            .Include(mr => mr.User)
            .Include(mr => mr.Mood)
            .FirstAsync(mr => mr.Uid == id);
        
        return _mapper.Map<MoodRecord, MoodRecordModel>(updatedMoodRecord);
    }

    public async Task DeleteMoodRecord(Guid id, Guid userId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var moodRecord = await context.MoodRecords.FirstOrDefaultAsync(mr => mr.Uid == id);

        if (moodRecord == null)
            throw new ProcessException($"Mood record with ID: {id} not found");

        if (moodRecord.UserId != userId)
            throw new AuthenticationException();

        context.MoodRecords.Remove(moodRecord);
        await context.SaveChangesAsync();
    }
}