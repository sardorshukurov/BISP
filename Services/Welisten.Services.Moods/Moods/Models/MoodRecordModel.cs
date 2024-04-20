using AutoMapper;
using Welisten.Context.Entities;

namespace Welisten.Services.Moods;

public class MoodRecordModel
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public MoodModel? Mood { get; set; }
    public Guid UserId { get; set; }
    public UserDto? User { get; set; }
    public ICollection<EventModel> Events { get; set; } = [];
}

public class MoodRecordProfile : Profile
{
    public MoodRecordProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<EventType, EventModel>();

        CreateMap<MoodRecord, MoodRecordModel>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt =>
                opt.MapFrom<MoodRecordUserResolver>())
            .ForMember(dest => dest.Events, opt =>
                opt.MapFrom<MoodRecordEventResolver>());
    }

    private class MoodRecordUserResolver : IValueResolver<MoodRecord, MoodRecordModel, UserDto>
    {
        public UserDto Resolve(MoodRecord source, MoodRecordModel destination, UserDto member,
            ResolutionContext context)
        {
            return context.Mapper.Map<UserDto>(source.User);
        }
    }
    
    private class MoodRecordEventResolver : IValueResolver<MoodRecord, MoodRecordModel, ICollection<EventModel>>
    {
        public ICollection<EventModel> Resolve(MoodRecord source, MoodRecordModel destination, ICollection<EventModel> destMember, ResolutionContext context)
        {
            return context.Mapper.Map<ICollection<EventModel>>(source.Event);
        }
    }
}