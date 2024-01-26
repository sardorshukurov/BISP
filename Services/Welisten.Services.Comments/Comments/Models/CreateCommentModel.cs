using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.ValidationRules;
using Welisten.Context.Context;
using Welisten.Context.Entities;

namespace Welisten.Services.Comments;

public class CreateCommentModel
{
    public Guid PostId { get; set; }
    public required string Text { get; set; }
    public required bool IsAnonymous { get; set; } = false;
}

public class CreateCommentModelProfile : Profile
{
    public CreateCommentModelProfile()
    {
        CreateMap<CreateCommentModel, Comment>()
            .ForMember(dest => dest.PostId, opt => opt.Ignore())
            .AfterMap<CreateCommentModelActions>();
    }

    public class CreateCommentModelActions : IMappingAction<CreateCommentModel, Comment>
    {
        private readonly IDbContextFactory<MainDbContext> _dbContextFactory;

        public CreateCommentModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public void Process(CreateCommentModel source, Comment destination, ResolutionContext context)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            var post = dbContext.Posts.FirstOrDefault(x => x.Uid == source.PostId);

            destination.PostId = post.Id;
        }
    }
}

public class CreateCommentModelValidator : AbstractValidator<CreateCommentModel>
{ 
    public CreateCommentModelValidator(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        RuleFor(x => x.Text).CommentText();

        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("Post is required")
            .Must((id) =>
            {
                using var context = dbContextFactory.CreateDbContext();
                var found = context.Posts.Any(a => a.Uid == id);
                return found;
            }).WithMessage("Post is not found");
    }
}