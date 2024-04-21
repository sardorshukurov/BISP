using AutoMapper;
using Welisten.Context.Entities;

namespace Welisten.Services.Moods;

public class MoodModel
{
    public Guid Id { get; set; }
    public string ImageLink { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class MoodModelProfile : Profile
{
    public MoodModelProfile()
    {
        CreateMap<Mood, MoodModel>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.ImageLink, opt =>
                opt.MapFrom(src => src.ImageLink))
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Name));
    }
}