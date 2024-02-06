using AutoMapper;
using Newtonsoft.Json;
using Welisten.Context.Entities;
using Welisten.Services.Topics;

namespace Welisten.Services.Posts;

public class PostModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    
    [JsonIgnore]
    public Guid? UserId { get; set; }
    public UserDto? User { get; set; }
    public required DateTime Date { get; set; }
    public required ICollection<string> Topics { get; set; }
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
}

public class PostModelProfile : Profile
{
    public PostModelProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Topic, TopicModel>();

        CreateMap<Post, PostModel>()
            .ForMember(dest => dest.Id, opt => 
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserId, opt => 
                opt.MapFrom(src => src.IsAnonymous ? (Guid?)null : src.UserId))
            .ForMember(dest => dest.CommentCount, opt =>
                opt.MapFrom(src => src.PostCount.CommentCount))
            .ForMember(dest => dest.LikeCount, opt =>
                opt.MapFrom(src => src.PostCount.LikeCount))
            .ForMember(dest => dest.User, opt => 
                opt.MapFrom<PostUserResolver>())
            .ForMember(dest => dest.Topics, opt =>
                opt.MapFrom<PostTopicsResolver>());
    }
    
    private class PostUserResolver : IValueResolver<Post, PostModel, UserDto>
    {
        public UserDto? Resolve(Post source, PostModel destination, UserDto? member, ResolutionContext context)
        {
            return source.IsAnonymous ? null : context.Mapper.Map<UserDto>(source.User);
        }
    }
    
    private class PostTopicsResolver : IValueResolver<Post, PostModel, ICollection<string>>
    {
        public ICollection<string> Resolve(Post source, PostModel destination, ICollection<string> destMember, ResolutionContext context)
        {
            return context.Mapper.Map<ICollection<string>>(source.Topics.Select(x => x.Type));
        }
    }
}

