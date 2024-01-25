using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Welisten.Common.Exceptions;
using Welisten.Common.Security;
using Welisten.Common.Validator;
using Welisten.Context.Entities;

namespace Welisten.Services.UserAccounts;

public class UserAccountService
    : IUserAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IModelValidator<RegisterDto> _registerValidator;
    private readonly IModelValidator<LoginDto> _loginValidator;
    private readonly JwtConfig _jwtConfig;
    public UserAccountService(
        IMapper mapper,
        UserManager<User> userManager,
        IModelValidator<RegisterDto> registerValidator,
        IModelValidator<LoginDto> loginValidator,
        IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _mapper = mapper;
        _userManager = userManager;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await _userManager.Users.AnyAsync());
    }

    public async Task<UserAccountModel> Register(RegisterDto registerDto)
    {
        await _registerValidator.CheckAsync(registerDto);

        // Find user by email
        var user = await _userManager.FindByEmailAsync(registerDto.Email);
        if (user != null)
            throw new ProcessException($"User account with email {registerDto.Email} already exists.");

        // Create user account
        user = new User()
        {
            Status = UserStatus.Active,
            FirstName =  registerDto.FirstName,
            LastName = registerDto.LastName,
            Name = registerDto.Name,
            UserName = registerDto.Name,
            Email = registerDto.Email,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false           
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
         
        return _mapper.Map<UserAccountModel>(user);
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        await _loginValidator.CheckAsync(loginDto);

        var existingUser = await _userManager.FindByEmailAsync(loginDto.Email);

        if (existingUser == null)
            throw new ProcessException($"Either email or password is wrong. Try again");
        
        bool isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, loginDto.Password);
        
        if (!isPasswordValid)
            throw new ProcessException($"Either email or password is wrong. Try again");
        
        var token = GenerateJwtToken(existingUser);
        return token;
    }
    
    private string GenerateJwtToken(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtConfig.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtConfig.Audience)
            }),
            Expires = DateTime.Now.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);
        return jwtToken;
    }
}