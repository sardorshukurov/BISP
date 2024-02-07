using System.Security.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Exceptions;
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

    public async Task<List<CommentModel>> GetCommentsById(Guid postId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var post = await context.Posts
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(p => p.Uid == postId);

        if (post == null)
            throw new InvalidOperationException($"Post with ID {postId} not found.");
        
        return _mapper.Map<List<CommentModel>>(post.Comments);
    }

    public async Task<CommentModel> Create(CreateCommentModel model, Guid userId)
    {
        await _createValidator.CheckAsync(model);

        await using var context = await _dbContextFactory.CreateDbContextAsync();

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
    
    public async Task Delete(Guid id, Guid userId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        var comment = await context.Comments.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (comment == null)
            throw new ProcessException($"Comment with ID: {id}. Not found");

        if (comment.UserId != userId)
            throw new AuthenticationException("Authentication failed");
            
        context.Comments.Remove(comment);

        await context.SaveChangesAsync();
    }
}