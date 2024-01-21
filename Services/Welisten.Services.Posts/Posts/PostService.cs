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
    //private readonly IModelValidator<CreatePostModel> _createValidator;
    //private readonly IModelValidator<UpdatePostModel> _updateValidator;

    private readonly IMapper _mapper;
    
    public PostService(IDbContextFactory<MainDbContext> dbContextFactory, 
        //IModelValidator<CreatePostModel> createValidator, 
        //IModelValidator<UpdatePostModel> updateValidator,
        IMapper mapper)
    {
        _dbContextFactory = dbContextFactory;
        //_createValidator = createValidator;
        //_updateValidator = updateValidator;
        _mapper = mapper;
    }   
    
    public async Task<IEnumerable<PostModel>> GetAll()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var posts = await context.Posts
            .Include(x => x.User)
            .Include(x => x.PostCount)
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<Post>,IEnumerable<PostModel>>(posts);
        
        return result;
    }

    public Task<PostModel> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PostModel> Create(CreatePostModel model)
    {
        throw new NotImplementedException();
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