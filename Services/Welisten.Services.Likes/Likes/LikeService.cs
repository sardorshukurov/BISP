using Microsoft.EntityFrameworkCore;
using Welisten.Common.Exceptions;
using Welisten.Context.Context;
using Welisten.Context.Entities;
using Welisten.Services.Logger.Logger;

namespace Welisten.Services.Likes;

public class LikeService : ILikeService
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
    private readonly IAppLogger _logger;

    public LikeService(IDbContextFactory<MainDbContext> dbContextFactory,
        IAppLogger logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }
    
    public async Task LikeUnlike(Guid userId, Guid postId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var post = await context.Posts
            .Include(p => p.PostCount)
            .Include(p => p.Likes).ThenInclude(like => like.User)
            .FirstOrDefaultAsync(p => p.Uid == postId);
        
        if (post == null)
        {
            _logger.Error($"Error liking/unliking post with Id: {postId}. Post not found");
            throw new ProcessException($"Post with ID: {postId}. Not found");
        }
        
        context.Entry(post).State = EntityState.Modified;
        context.Entry(post.PostCount).State = EntityState.Modified;
        if (post.Likes.Select(l => l.User.Id).Contains(userId))
        {
            var likeToRemove = post.Likes.First(l => l.User.Id == userId);
            context.Likes.Remove(likeToRemove);
            post.Likes.Remove(post.Likes.First(l => l.User.Id == userId));
            post.PostCount.LikeCount--;
        }
        else
        {
            post.Likes.Add(new Like
            {
                User = context.Users.First(u => u.Id == userId)
            });
            post.PostCount.LikeCount++;
        }
        
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Error($"Error saving changes - like/unlike: {e.Message}");
            throw;
        }
    }
}