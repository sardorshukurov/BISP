using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.ValidationRules;
using Welisten.Context.Context;
using Welisten.Context.Entities;
using Welisten.Services.Topics;

namespace Welisten.Services.Posts;

public class CreatePostModel
{
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    public required ICollection<ReactionDto> Reactions { get; set; }
    public required ICollection<Guid> Topics { get; set; }
}

public class CreatePostModelProfile : Profile
{
    public CreatePostModelProfile()
    {
        CreateMap<ReactionDto, Reaction>();
        
        CreateMap<CreatePostModel, Post>()
            .ForMember(dest => dest.Reactions, opt =>
                opt.MapFrom<PostReactionsResolver>())
            .ForMember(dest => dest.Topics, opt =>
                opt.MapFrom<PostTopicsResolver>());
    }
    
    private class PostReactionsResolver : IValueResolver<CreatePostModel, Post, ICollection<Reaction>>
    {
        public ICollection<Reaction> Resolve(CreatePostModel source, Post destination, ICollection<Reaction> member, ResolutionContext context)
        {
            return context.Mapper.Map<ICollection<Reaction>>(source.Reactions);
        }
    }
    
    private class PostTopicsResolver : IValueResolver<CreatePostModel, Post, ICollection<Topic>>
    {
        private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

        public PostTopicsResolver(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public ICollection<Topic> Resolve(CreatePostModel source, Post destination, ICollection<Topic> destMember, ResolutionContext context)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            // Fetch existing topics asynchronously by Uid
            var existingTopics = dbContext.Topics
                .Where(t => source.Topics.Contains(t.Uid))
                .ToList();

            return existingTopics;
        }
    }
}

public class CreatePostModelValidator : AbstractValidator<CreatePostModel>
{
    public CreatePostModelValidator(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        RuleFor(x => x.Title).PostTitle();
        RuleFor(x => x.Text).PostText();

        RuleFor(x => x.Reactions)
            .Must(reactions => reactions.Select(r => r.ReactionType).Distinct().Count() == reactions.Count)
            .WithMessage("Reactions must be unique");

        RuleFor(x => x.Topics)
            .Must(topics =>
            {
                // Ensure all provided topic Uids exist in the database
                using var context = dbContextFactory.CreateDbContext();
                var existingTopicUids = context.Topics
                    .Where(t => topics.Contains(t.Uid))
                    .Select(t => t.Uid)
                    .ToList();

                return existingTopicUids.Count == topics.Count;
            })
            .WithMessage("All topics must be existing topics");
    }
}
