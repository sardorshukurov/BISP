using System.Security.Authentication;
using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
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

    public PostController(IPostService postService, IUserAccountService userService)
    {
        _postService = postService;
        _userService = userService;
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            try
            {
                return Ok(await _postService.GetByUser(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        return Unauthorized();
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

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            try
            {
                await _postService.Delete(id, userId);
                return Ok();
            }
            catch (ProcessException)
            {
                return NotFound("Post not found");
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        return Unauthorized();
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdatePostModel request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            try
            {
                await _postService.Update(id, userId, request);
                return Ok();
            }
            catch (ProcessException)
            {
                return NotFound("Post not found");
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        return Unauthorized();
    }
}