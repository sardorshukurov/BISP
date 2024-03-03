using System.Security.Authentication;
using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
using Welisten.Services.Comments;
using Welisten.Services.Logger.Logger;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Post")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IAppLogger _logger;
    private readonly IUserAccountService _userService;

    public CommentController(IAppLogger logger, ICommentService commentService, IUserAccountService userService)
    {
        _logger = logger;
        _commentService = commentService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentModel request)
    {
        if (!await _userService.Exists(User))
            return Unauthorized();

        if (await _userService.IsExpired(User))
            return Unauthorized();

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
        {
            var result = await _commentService.Create(request, userId);
            return Ok(result);
        }

        return Unauthorized();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            try
            {
                await _commentService.Delete(id, userId);
                return Ok();
            }
            catch (ProcessException)
            {
                return NotFound("Comment not found");
            }
            catch (AuthenticationException)
            {
                return Unauthorized();
            }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetComments([FromRoute] Guid id)
    {
        try
        {
            var comments = await _commentService.GetCommentsById(id);
            return Ok(comments);
        }
        catch (InvalidOperationException e)
        {
            _logger.Warning(e, "Post not found with ID: {Id}", id);
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error occurred while fetching comments for post with ID: {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An unexpected error occurred. Please try again later.");
        }
    }
}