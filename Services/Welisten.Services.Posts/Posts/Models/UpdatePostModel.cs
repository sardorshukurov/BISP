using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.ValidationRules;
using Welisten.Context.Context;
using Welisten.Context.Entities;
using Welisten.Services.Topics;

namespace Welisten.Services.Posts;

public class UpdatePostModel
{
    public required string Title { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; }
    public required ICollection<Guid> Topics { get; set; }
}

public class UpdatePostModelProfile : Profile
{
    public UpdatePostModelProfile()
    {
        CreateMap<UpdatePostModel, Post>()
            .ForMember(dest => dest.Topics, opt =>
                opt.MapFrom<PostTopicsResolver>());
    }
    
    private class PostTopicsResolver : IValueResolver<UpdatePostModel, Post, ICollection<Topic>>
    {
        private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

        public PostTopicsResolver(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public ICollection<Topic> Resolve(UpdatePostModel source, Post destination, ICollection<Topic> destMember, ResolutionContext context)
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

public class UpdatePostModelValidator : AbstractValidator<UpdatePostModel>
{
    public UpdatePostModelValidator(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        RuleFor(x => x.Title).PostTitle();
        RuleFor(x => x.Text).PostText();
        // RuleFor(x => x.Topics)
        //     .MustAsync(async (topics, cancellationToken) =>
        //     {
        //         // Check if all provided topic IDs exist in the database
        //         using var context = await dbContextFactory.CreateDbContextAsync();
        //         var existingTopicIds = await context.Topics
        //             .Where(t => topics.Contains(t.Uid))
        //             .Select(t => t.Uid)
        //             .ToListAsync();
        //
        //         return existingTopicIds.Count == topics.Count;
        //     })
        //     .WithMessage("One or more provided topics do not exist.");
    }
}