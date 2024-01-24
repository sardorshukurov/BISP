using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Welisten.Common.Exceptions;
using Welisten.Common.Responses;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "Account")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<AccountController> _logger;
    private readonly IUserAccountService _userAccountService;

    public AccountController(IMapper mapper, ILogger<AccountController> logger, IUserAccountService userAccountService)
    { 
        _mapper = mapper;
        _logger = logger;
        _userAccountService = userAccountService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        try
        {
            var user = await _userAccountService.Register(request);
            return Ok(user);
        }
        catch (ProcessException e)
        {
            return BadRequest(new ErrorResponse
            {
                Code = e.Code,
                Message = e.Message
            });
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        try
        {
            string token = await _userAccountService.Login(request);
            return Ok(new LoginRequestResponse
            {
                Token = token
            });
        }
        catch (ProcessException e)
        {
            return BadRequest(new ErrorResponse
            {
                Code = e.Code,
                Message = e.Message
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}