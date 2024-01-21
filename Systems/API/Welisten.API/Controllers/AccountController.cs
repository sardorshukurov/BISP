using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Welisten.Services.UserAccounts;

namespace Welisten.API.Controllers;

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

    [HttpPost("")]
    public async Task<UserAccountModel> Register([FromQuery] RegisterUserAccountModel request)
    {
        var user = await _userAccountService.Create(request);
        return user;
    }
}