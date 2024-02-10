using System.Security.Authentication;
using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Exceptions;
using Welisten.Context.Context;
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
    
    public PostController(IPostService postService, IUserAccountService userService)
    {
        _postService = postService;
        _userService = userService;
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
        if (!await _userService.Exists(User))
            return Unauthorized();
        
        if (await _userService.IsExpired(User))
            return Unauthorized();
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId))
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
        
        if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId))
        {
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
        }

        return Unauthorized();
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdatePostModel request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId))
        {
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
        }
        
        return Unauthorized();
    }
}