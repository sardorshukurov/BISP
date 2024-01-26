using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.API.Common;
using Welisten.Services.Comments;
using Welisten.Services.Logger.Logger;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Post")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class CommentController : ControllerBase
{
    private readonly IAppLogger _logger;
    private readonly ICommentService _commentService;

    public CommentController(IAppLogger logger, ICommentService commentService)
    {
        _logger = logger;
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentModel request)
    {
        if (UserHelper.IsExpired(User))
            return Unauthorized();
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userIdClaim != null && Guid.TryParse(userIdClaim, out Guid userId))
        {
            var result = await _commentService.Create(request, userId);
            return Ok(result);
        }

        return Unauthorized();
    }
}