using System.Security.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Exceptions;
using Welisten.Common.Validator;
using Welisten.Context.Context;
using Welisten.Context.Entities;
using Welisten.Services.Logger.Logger;

namespace Welisten.Services.Posts;

public class PostService : IPostService
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
    private readonly IModelValidator<CreatePostModel> _createValidator;
    private readonly IModelValidator<UpdatePostModel> _updateValidator;

    private readonly IAppLogger _logger;
    private readonly IMapper _mapper;
    
    public PostService(IDbContextFactory<MainDbContext> dbContextFactory, 
        IModelValidator<CreatePostModel> createValidator, 
        IModelValidator<UpdatePostModel> updateValidator,
        IAppLogger logger,
        IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
        _mapper = mapper;
    }   
    
    public async Task<IEnumerable<PostModel>> GetAll()
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var posts = await context.Posts
            .Include(x => x.User)
            .Include(x => x.PostCount)
            .Include(x => x.Topics)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
        
        var result = _mapper.Map<IEnumerable<Post>,IEnumerable<PostModel>>(posts);
        
        return result;
    }

    public async Task<IEnumerable<PostModel>> GetByUser(Guid userId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var posts = await context.Posts
            .Include(x => x.User)
            .Include(x => x.PostCount)
            .Include(x => x.Topics)
            .OrderByDescending(x => x.Date)
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        var result = _mapper.Map<IEnumerable<Post>,IEnumerable<PostModel>>(posts);
        
        return result;
    }

    public async Task<IEnumerable<PostModel>> GetByTopics(IEnumerable<Guid> topicIds)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        
        var posts = await context.Posts
            .Include(x => x.User)
            .Include(x => x.PostCount)
            .Include(x => x.Topics)
            .OrderByDescending(x => x.Date)
            .Where(x => 
                x.Topics.Any(t => topicIds.Contains(t.Uid)))
            .ToListAsync();
        
        var result = _mapper.Map<IEnumerable<Post>,IEnumerable<PostModel>>(posts);
        
        return result;
    }

    public async Task<PostModel?> GetById(Guid id)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var post = await context.Posts
            .Include(x => x.User)
            .Include(x => x.PostCount)
            .Include(x => x.Topics)
            .FirstOrDefaultAsync(x => x.Uid == id);

        var result = _mapper.Map<Post?, PostModel?>(post);

        return result;
    }

    public async Task<PostModel> Create(CreatePostModel model, Guid userId)
    {
        try
        {
            await _createValidator.CheckAsync(model);

            await using var context = await _dbContextFactory.CreateDbContextAsync();
        
            // Create a new Post entity
            var post = _mapper.Map<Post>(model);

            if (post.Date.Kind == DateTimeKind.Local)
                post.Date = post.Date.ToUniversalTime();

            // Create a new PostCount entity
            var postCount = new PostCount
            {
                Post = post
            };

            // Associate the Post and PostCount entities
            post.PostCount = postCount;

            post.UserId = userId;

            // Fetch existing topics by Uid
            post.Topics = await context.Topics
                .Where(t => model.Topics.Contains(t.Uid))
                .ToListAsync();
        
            context.AttachRange(post.Topics);
        
            // Add the entities to the context
            await context.Posts.AddAsync(post);
            await context.PostCounts.AddAsync(postCount);

            // Save changes to the database
            await context.SaveChangesAsync();
            
            post.User = await context.Users.FirstAsync(u => u.Id == userId);
        
            var createdPost = _mapper.Map<PostModel>(post);
        
            return createdPost;
        }
        catch (Exception e)
        {
            _logger.Error($"Error creating post. Error message: {e.Message}");
            throw;
        }
    }

    public async Task Update(Guid id, Guid userId, UpdatePostModel model)
    {
        try
        {
            await _updateValidator.CheckAsync(model);

            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var post = await context.Posts
                .Include(x => x.User)
                .Include(x => x.PostCount)
                .Include(x => x.Topics)
                .FirstOrDefaultAsync(x => x.Uid == id);

            if (post == null)
                throw new ProcessException($"Post with ID: {id} not found");

            if (post.UserId != userId)
                throw new AuthenticationException("Authentication failed");

            context.Entry(post).State = EntityState.Modified;
            context.AttachRange(post.Topics);

            // Update other properties of the post
            post.Title = model.Title;
            post.Text = model.Text;
            post.IsAnonymous = model.IsAnonymous;

            // Update topics
            var existingTopicIds = post.Topics.Select(t => t.Uid).ToList();
            var newTopicIds = model.Topics;

            // Remove topics that are not in the new list
            foreach (var topic in post.Topics.ToList())
            {
                if (!newTopicIds.Contains(topic.Uid))
                {
                    post.Topics.Remove(topic);
                }
            }
            
            // Add new topics
            foreach (var topicId in newTopicIds)
            {
                if (!existingTopicIds.Contains(topicId))
                {
                    var topic = await context.Topics.FirstOrDefaultAsync(t => t.Uid == topicId);
                    if (topic != null)
                    {
                        post.Topics.Add(topic);
                    }
                }
            }
            
            // Save changes to update the post entity
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.Error($"Error updating post with ID: {id}. Error message: {e.Message}");
            throw;
        }
    }



    public async Task Delete(Guid id, Guid userId)
    {
        try
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var post = await context.Posts.Where(x => x.Uid == id).FirstOrDefaultAsync();

            if (post == null)
                throw new ProcessException($"Post with ID: {id}. Not found");

            if (post.UserId != userId)
                throw new AuthenticationException("Authentication failed");

            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            _logger.Information($"Post with ID: {id} and related comments successfully deleted.");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error deleting post with ID: {id}");
            throw;
        }
    }
}