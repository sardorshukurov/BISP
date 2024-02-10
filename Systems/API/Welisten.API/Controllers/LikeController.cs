using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
using Welisten.Services.Likes;
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
    private readonly IUserAccountService _userService;

    public LikeController(ILikeService likeService, IUserAccountService userService)
    {
        _likeService = likeService;
        _userService = userService;
    }

    [HttpPost("{postId:guid}")]
    public async Task<IActionResult> LikeUnlike([FromRoute] Guid postId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId))
        {
            try
            {
                await _likeService.LikeUnlike(userId, postId);
                return Ok();
            }
            catch (ProcessException)
            {
                return NotFound("Post not found");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        return Unauthorized();
    }
}