using System.Text.Json.Serialization;
using AutoMapper;
using Welisten.Context.Entities;

namespace Welisten.Services.Comments;

public class CommentModel
{
    public Guid Id { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    [JsonIgnore]
    public Guid? UserId { get; set; }
    public UserDto? User { get; set; }
    public Guid PostId { get; set; }
    public DateTime Date { get; set; }
}

public class CommentModelProfile : Profile
{
    public CommentModelProfile()
    {
        CreateMap<Comment, CommentModel>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.IsAnonymous ? (Guid?)null : src.UserId))
            .ForMember(dest => dest.PostId, opt =>
                opt.MapFrom(src => src.Post.Uid))
            .ForMember(dest => dest.Date, opt =>
                opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.User, opt => 
                opt.MapFrom<CommentUserResolver>());
    }

    private class CommentUserResolver : IValueResolver<Comment, CommentModel, UserDto>
    {
        public UserDto? Resolve(Comment source, CommentModel destination, UserDto? member, ResolutionContext context)
        {
            return source.IsAnonymous ? null : context.Mapper.Map<UserDto>(source.User);
        }
    }
}