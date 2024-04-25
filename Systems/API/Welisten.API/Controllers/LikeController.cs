using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
using Welisten.Services.Likes;
using Welisten.Services.Logger.Logger;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Post")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;
    private readonly IAppLogger _logger;
    
    public LikeController(ILikeService likeService, IAppLogger logger)
    {
        _likeService = likeService;
        _logger = logger;
    }

    [HttpPost("{postId:guid}")]
    public async Task<IActionResult> LikeUnlike([FromRoute] Guid postId)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                await _likeService.LikeUnlike(userId, postId);
                return Ok();
            }

            return Unauthorized();
        }
        catch (ProcessException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(StatusCode(500));
        }
    }
}