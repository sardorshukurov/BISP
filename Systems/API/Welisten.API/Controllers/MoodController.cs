using System.Security.Authentication;
using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
using Welisten.Services.Logger.Logger;
using Welisten.Services.Moods;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Mood")]
[Authorize]
[Route("v{version:apiVersion}/[controller]")]
public class MoodController : ControllerBase
{
    private readonly IMoodService _moodService;
    private readonly IUserAccountService _userService;
    private readonly IAppLogger _logger;
    
    public MoodController(IMoodService moodService, IUserAccountService userService, IAppLogger logger)
    {
        _moodService = moodService;
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("moods")]
    public async Task<IActionResult> GetAllMoods()
    {
        try
        {
            if (!await IsEligible()) return Unauthorized();
            return Ok(await _moodService.GetAllMoods());
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [HttpGet("moodRecords")]
    public async Task<IActionResult> GetAllMoodRecords()
    {
        try
        {
            if (!await IsEligible()) return Unauthorized();
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                var result = await _moodService.GetAllMoodRecords(userId);
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

    [HttpGet("events")]
    public async Task<IActionResult> GetAllEvents()
    {
        try
        {
            if (!await IsEligible()) return Unauthorized();
            return Ok(await _moodService.GetAllEvents());
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }
    
    [HttpGet("moodRecords/{id:guid}")]
    public async Task<IActionResult> GetMoodRecordById([FromRoute]Guid id)
    {
        try
        {
            if (!await IsEligible()) return Unauthorized();
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                var result = await _moodService.GetMoodRecordById(id, userId);

                if (result == null) return NotFound();

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

    [HttpPost("")]
    public async Task<IActionResult> Create(CreateMoodRecordModel request)
    {
        try
        {
            if (!await IsEligible()) return Unauthorized();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                await _moodService.CreateMoodRecord(request, userId);
                return Ok();
            }

            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, CreateMoodRecordModel request)
    {
        try
        {
            if (!await IsEligible()) return Unauthorized();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                return Ok(await _moodService.UpdateMoodRecord(id, request, userId));
            }

            return Unauthorized();
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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                await _moodService.DeleteMoodRecord(id, userId);
                return Ok();
            }
            
            return Unauthorized();
        }
        catch (ProcessException)
        {
            return NotFound();
        }
        catch (AuthenticationException)
        {
            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return StatusCode(500);
        }
    }
    private async Task<bool> IsEligible()
    {
        if (!await _userService.Exists(User))
            return false;

        if (_userService.IsExpired(User))
            return false;

        return true;
    }
}