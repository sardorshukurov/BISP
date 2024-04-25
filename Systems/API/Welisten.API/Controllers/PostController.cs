using System.Security.Authentication;
using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
using Welisten.Services.Logger.Logger;
using Welisten.Services.Posts;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Post")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IUserAccountService _userService;
    private readonly IAppLogger _logger;

    public PostController(IPostService postService, IUserAccountService userService, IAppLogger logger)
    {
        _postService = postService;
        _userService = userService;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IEnumerable<PostModel>> GetAll()
    {
        return await _postService.GetAll();
    }

    [Authorize]
    [HttpGet("byUser")]
    public async Task<IActionResult> GetByUser()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
                return Ok(await _postService.GetByUser(userId));

            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [AllowAnonymous]
    [HttpGet("byTopics")]
    public async Task<IActionResult> GetByTopics([FromQuery] IEnumerable<Guid> topicIds)
    {
        try
        {
            var posts = await _postService.GetByTopics(topicIds);
            return Ok(posts);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var result = await _postService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> Create(CreatePostModel request)
    {
        try
        {
            if (!await _userService.Exists(User))
                return Unauthorized();

            if (_userService.IsExpired(User))
                return Unauthorized();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                var result = await _postService.Create(request, userId);
                return Ok(result);
            }

            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                await _postService.Delete(id, userId);
                return Ok();
            }
            
            return Unauthorized();
        }            
        catch (ProcessException)
        {
            return NotFound();
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdatePostModel request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                await _postService.Update(id, userId, request);
                return Ok();
            }
            
            return Unauthorized();
        }
        catch (ProcessException)
        {
            return NotFound("Post not found");
        }
        catch (AuthenticationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }
}