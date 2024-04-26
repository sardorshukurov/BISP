using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.ValidationRules;
using Welisten.Context.Context;
using Welisten.Context.Entities;

namespace Welisten.Services.Moods;

public class CreateMoodRecordModel
{
    public string Text { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public Guid MoodId { get; set; }
    public Guid EventId { get; set; }
}

public class CreateMoodRecordModelProfile : Profile
{
    public CreateMoodRecordModelProfile()
    {
        CreateMap<CreateMoodRecordModel, MoodRecord>()
            .ForMember(dest => dest.Text, opt =>
                opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Date, opt =>
                opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Mood, opt =>
                opt.MapFrom<MoodRecordMoodResolver>())
            .ForMember(dest => dest.Event, opt =>
                opt.MapFrom<MoodRecordEventResolver>());
    }

    private class MoodRecordMoodResolver : IValueResolver<CreateMoodRecordModel, MoodRecord, Mood>
    {
        private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
        
        public MoodRecordMoodResolver(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Mood Resolve(CreateMoodRecordModel source, MoodRecord destination, Mood destMember, ResolutionContext context)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var existingMood = dbContext.Moods
                .First(m => source.MoodId == m.Uid);

            return existingMood;
        }
    }
    
    private class MoodRecordEventResolver : IValueResolver<CreateMoodRecordModel, MoodRecord, EventType>
    {
        private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
        
        public MoodRecordEventResolver(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public EventType Resolve(CreateMoodRecordModel source, MoodRecord destination, EventType destMember, ResolutionContext context)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var existingEvent = dbContext.EventTypes
                .First(e => source.EventId == e.Uid);

            return existingEvent;
        }
    }
}

public class CreateMoodRecordModelValidator : AbstractValidator<CreateMoodRecordModel>
{
    public CreateMoodRecordModelValidator(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        RuleFor(mr => mr.Text).MoodRecordText();
        RuleFor(mr => mr.MoodId)
            .NotEqual(Guid.Empty)
            .WithMessage("What's your mood, body?");
        RuleFor(mr => mr.EventId)
            .NotEqual(Guid.Empty)
            .WithMessage("What event caused this mood, body?");
        RuleFor(mr => mr.Date.ToUniversalTime())
            .Must(BeNotInFuture)
            .WithMessage("You cannot write about the future :(");
    }

    private bool BeNotInFuture(DateTime date)
    {
        return date.ToUniversalTime() <= DateTime.Now.ToUniversalTime();
    }
}
