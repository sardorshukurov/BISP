using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Welisten.Common.Validator;
using Welisten.Context.Context;
using Welisten.Context.Entities;

namespace Welisten.Services.Posts;

public class PostService : IPostService
{
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
    private readonly IModelValidator<CreatePostModel> _createValidator;
    //private readonly IModelValidator<UpdatePostModel> _updateValidator;

    private readonly IMapper _mapper;
    
    public PostService(IDbContextFactory<MainDbContext> dbContextFactory, 
        IModelValidator<CreatePostModel> createValidator, 
        //IModelValidator<UpdatePostModel> updateValidator,
        IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        _createValidator = createValidator;
        //_updateValidator = updateValidator;
        _mapper = mapper;
    }   
    
    public async Task<IEnumerable<PostModel>> GetAll()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var posts = await context.Posts
            .Include(x => x.User)
            .Include(x => x.PostCount)
            .Include(x => x.Reactions)
            .ToListAsync();
        
        var result = _mapper.Map<IEnumerable<Post>,IEnumerable<PostModel>>(posts);
        
        return result;
    }

    public async Task<PostModel> GetById(Guid id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var post = await context.Posts
            .Include(x => x.User)
            .Include(x => x.PostCount)
            .Include(x => x.Reactions)
            .FirstOrDefaultAsync(x => x.Uid == id);

        var result = _mapper.Map<Post, PostModel>(post);

        return result;
    }

    public async Task<PostModel> Create(CreatePostModel model, Guid userId)
    {
        await _createValidator.CheckAsync(model);

        using var context = await _dbContextFactory.CreateDbContextAsync();

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
        // Add the entities to the context
        await context.Posts.AddAsync(post);
        await context.PostCounts.AddAsync(postCount);

        // Save changes to the database
        await context.SaveChangesAsync();

        var createdPost = _mapper.Map<PostModel>(post);
        createdPost.UserId = userId;
        
        return createdPost;
    }
    public Task Update(Guid id, UpdatePostModel model)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}