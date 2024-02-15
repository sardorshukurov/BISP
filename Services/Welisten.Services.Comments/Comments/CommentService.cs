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
            .SingleOrDefaultAsync(p => p.Uid == postId);

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

        // Error handling for post not found
        if (post == null)
            throw new ProcessException($"Associated post not found for comment with ID: {comment.PostId}");
        
        post.PostCount.CommentCount++;

        comment.User = await context.Users.FindAsync(userId);
        
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

        // Retrieve the comment
        var comment = await context.Comments.FirstOrDefaultAsync(x => x.Uid == id);
        if (comment == null)
            throw new ProcessException($"Comment with ID: {id} not found");

        // Check if the user is authorized to delete the comment
        if (comment.UserId != userId)
            throw new AuthenticationException("You are not authorized to delete this comment");

        // Retrieve the associated post and update comment count
        var post = await context.Posts.Include(x => x.PostCount).FirstOrDefaultAsync(p => p.Id == comment.PostId);
        if (post == null)
            throw new ProcessException($"Associated post not found for comment with ID: {id}");

        // Remove the comment
        context.Comments.Remove(comment);

        context.Entry(post.PostCount).State = EntityState.Modified;
        post.PostCount.CommentCount -= 1;
        
        await context.SaveChangesAsync();
    }
}