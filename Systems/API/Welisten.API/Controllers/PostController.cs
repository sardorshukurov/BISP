using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Welisten.Services.Logger.Logger;
using Welisten.Services.Posts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Post")]
[Route("v{version:apiVersion}/[controller]")]
public class PostController : ControllerBase
{
    private readonly IAppLogger _logger;
    private readonly IPostService _postService;

    public PostController(IAppLogger logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }
    
    [HttpGet("")]
    public async Task<IEnumerable<PostModel>> GetAll()
    {
        var result = await _postService.GetAll();

        return result;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _postService.GetById(id);
        
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<PostModel> Create(CreatePostModel request)
    {
        var result = await _postService.Create(request);

        return result;
    }
}