using AutoMapper;
using Welisten.Context.Entities;

namespace Welisten.Services.Posts;

public class PostModel
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    
    public Guid? UserId { get; set; }
    public UserDto? User { get; set; }
    
    public required ICollection<TopicDto> Topics { get; set; }
    public required ICollection<ReactionDto> Reactions { get; set; }
    public int CommentCount { get; set; }
    public int LikeCount { get; set; }
}

public class PostModelProfile : Profile
{
    public PostModelProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Reaction, ReactionDto>();
        CreateMap<Topic, TopicDto>();
        
        CreateMap<Post, PostModel>()
            .ForMember(dest => dest.Id, opt => 
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserId, opt => 
                opt.MapFrom(src => src.IsAnonymous ? (Guid?)null : src.UserId))
            .ForMember(dest => dest.User, opt => 
                opt.MapFrom<PostUserResolver>())
            .ForMember(dest => dest.Reactions, opt => 
                opt.MapFrom<PostReactionsResolver>())
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

    private class PostReactionsResolver : IValueResolver<Post, PostModel, ICollection<ReactionDto>>
    {
        public ICollection<ReactionDto> Resolve(Post source, PostModel destination, ICollection<ReactionDto> member, ResolutionContext context)
        {
            return context.Mapper.Map<ICollection<ReactionDto>>(source.Reactions);
        }
    }
    
    private class PostTopicsResolver : IValueResolver<Post, PostModel, ICollection<TopicDto>>
    {
        public ICollection<TopicDto> Resolve(Post source, PostModel destination, ICollection<TopicDto> member, ResolutionContext context)
        {
            return context.Mapper.Map<ICollection<TopicDto>>(source.Topics);
        }
    }
}

