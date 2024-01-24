using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.ValidationRules;
using Welisten.Context.Context;
using Welisten.Context.Entities;

namespace Welisten.Services.Posts;

public class CreatePostModel
{
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; } = false;
    public Guid UserId { get; set; }
    public required IEnumerable<ReactionDto> Reactions { get; set; }
    public required IEnumerable<Topic> Topics { get; set; }
}

public class CreatePostModelProfile : Profile
{
    public CreatePostModelProfile()
    {
        CreateMap<ReactionDto, Reaction>();
        CreateMap<CreatePostModel, Post>()
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Reactions, opt => 
                opt.MapFrom<PostReactionsResolver>());
    }
    
    public class PostReactionsResolver : IValueResolver<CreatePostModel, Post, ICollection<Reaction>>
    {
        public ICollection<Reaction> Resolve(CreatePostModel source, Post destination, ICollection<Reaction> member, ResolutionContext context)
        {
            return context.Mapper.Map<ICollection<Reaction>>(source.Reactions);
        }
    }
}
public class CreatePostModelValidator : AbstractValidator<CreatePostModel>
{
    public CreatePostModelValidator(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        RuleFor(x => x.Title).PostTitle();
        RuleFor(x => x.Text).PostText();
        RuleFor(x => x.UserId)
            .UserId()
            .Must((id) =>
            {
                using var context = dbContextFactory.CreateDbContext();
                var found = context.Users.Any(u => u.Id == id);
                return found;
            }).WithMessage("User not found");
    }
}