using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Welisten.API.Common;
using Welisten.Context.Context;
using Welisten.Services.Logger.Logger;
using Welisten.Services.Posts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Post")]
[Authorize]
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
    
    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IEnumerable<PostModel>> GetAll()
    {
        var result = await _postService.GetAll();

        return result;
    }
    
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _postService.GetById(id);
        
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> Create(CreatePostModel request)
    {
        if (UserHelper.IsExpired(User))
            return Unauthorized();
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId))
        {
            var result = await _postService.Create(request, userId);
            return Ok(result);
        }
        return Unauthorized();
    }
}