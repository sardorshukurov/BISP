using System.Security.Claims;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
using Welisten.Common.Responses;
using Welisten.Services.Logger.Logger;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Account")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAppLogger _logger;
    private readonly IMapper _mapper;
    private readonly IUserAccountService _userAccountService;

    public AccountController(IMapper mapper, IAppLogger logger, IUserAccountService userAccountService)
    {
        _mapper = mapper;
        _logger = logger;
        _userAccountService = userAccountService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto request)
    {
        try
        {
            var user = await _userAccountService.Register(request);
            return Ok(user);
        }
        catch (ProcessException e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(StatusCode(500));
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromForm] LoginDto request)
    {
        try
        {
            var token = await _userAccountService.Login(request);
            return Ok(new LoginRequestResponse
            {
                Token = token
            });
        }
        catch (ProcessException e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(StatusCode(500));
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            if (!await _userAccountService.Exists(User))
                return Unauthorized();

            if (_userAccountService.IsExpired(User))
                return Unauthorized();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim != null && Guid.TryParse(userIdClaim, out var userId))
            {
                var result = await _userAccountService.GetUser(userId);
                return Ok(result);
            }

            return Unauthorized();
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(StatusCode(500));
        }
    }
}