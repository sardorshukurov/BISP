using AutoMapper;
using Welisten.Context.Entities;

namespace Welisten.Services.Moods;

public class EventModel
{
    public Guid Uid { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
}

public class EventModelProfile : Profile
{
    public EventModelProfile()
    {
        CreateMap<EventModel, EventType>()
            .ForMember(dest => dest.Uid, opt =>
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore());

        CreateMap<EventType, EventModel>()
            .ForMember(dest => dest.Uid, opt =>
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Name));
    }
}