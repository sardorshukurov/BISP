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

    public async Task<IEnumerable<CommentModel>> GetCommentsById(Guid postId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var post = await context.Posts
            .Include(p => p.Comments)!
                .ThenInclude(c => c.User)
            .SingleOrDefaultAsync(p => p.Uid == postId);

        if (post == null)
            throw new InvalidOperationException($"Post with ID {postId} not found.");
        
        return _mapper.Map<List<CommentModel>>(post.Comments);
    }

    public async Task<CommentModel> GetCommentById(Guid id)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var comment = await context.Comments
            .Include(c => c.Post)
            .FirstOrDefaultAsync(c => c.Uid == id);

        if (comment == null)
            throw new InvalidOperationException($"Comment with ID {id} not found.");

        return _mapper.Map<CommentModel>(comment);
    }

    public async Task<IEnumerable<CommentModel>> GetCommentsByUser(Guid userId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var comments = await context.Comments
            .Include(c => c.Post)
            .Where(c => c.UserId == userId)
            .ToListAsync();
        
        return _mapper.Map<List<CommentModel>>(comments);
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
            throw new InvalidOperationException($"Associated post not found for comment with ID: {comment.PostId}");
        
        context.Entry(post.PostCount).State = EntityState.Modified;
        post.PostCount.CommentCount++;

        comment.User = await context.Users.FindAsync(userId);

        context.Attach(comment.User);
        
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
            throw new InvalidOperationException($"Comment with ID: {id} not found");

        // Check if the user is authorized to delete the comment
        if (comment.UserId != userId)
            throw new AuthenticationException("You are not authorized to delete this comment");

        // Retrieve the associated post and update comment count
        var post = await context.Posts.Include(x => x.PostCount).FirstOrDefaultAsync(p => p.Id == comment.PostId);
        if (post == null)
            throw new InvalidOperationException($"Associated post not found for comment with ID: {id}");

        // Remove the comment
        context.Comments.Remove(comment);

        context.Entry(post.PostCount).State = EntityState.Modified;
        post.PostCount.CommentCount--;
        
        await context.SaveChangesAsync();
    }

    public async Task Update(Guid id, CreateCommentModel model)
    {
        await _createValidator.CheckAsync(model);

        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var comment = await context.Comments.FirstOrDefaultAsync(c => c.Uid == id);

        if (comment == null)
            throw new InvalidOperationException($"Comment with ID: {id} not found");
        
        context.Entry(comment).State = EntityState.Modified;
        
        comment.Text = model.Text;
        comment.IsAnonymous = model.IsAnonymous;

        // Save changes
        await context.SaveChangesAsync();
    }
}