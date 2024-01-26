using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Validator;
using Welisten.Context.Context;
using Welisten.Context.Entities;

namespace Welisten.Services.Comments;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
    private readonly IModelValidator<CreateCommentModel> _createValidator;
    private readonly IMapper _mapper;
    
    public CommentService(IDbContextFactory<MainDbContext> dbContextFactory,
        IModelValidator<CreateCommentModel> createValidator,
        IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _createValidator = createValidator;
        _mapper = mapper;
    }
    
    public async Task<CommentModel> Create(CreateCommentModel model, Guid userId)
    {
        await _createValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

        var comment = _mapper.Map<Comment>(model);

        if (comment.Date.Kind == DateTimeKind.Local)
            comment.Date = comment.Date.ToUniversalTime();

        comment.UserId = userId;

        var post = await context.Posts
            .Include(x => x.PostCount)
            .FirstOrDefaultAsync(x => comment.PostId == x.Id);

        context.Entry(post.PostCount).State = EntityState.Modified;
        post.PostCount.CommentCount += 1;

        // Add the comment to the context
        await context.Comments.AddAsync(comment);

        // Save changes
        await context.SaveChangesAsync();

        var createdComment = _mapper.Map<CommentModel>(comment);

        return createdComment;
    }
}