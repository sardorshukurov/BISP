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
using Welisten.Context.Context;
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
    private readonly IDbContextFactory<MainDbContext> _dbContextFactory;
    public UserAccountService(
        IMapper mapper,
        UserManager<User> userManager,
        IModelValidator<RegisterDto> registerValidator,
        IModelValidator<LoginDto> loginValidator,
        IOptionsMonitor<JwtConfig> optionsMonitor,
        IDbContextFactory<MainDbContext> dbContextFactory)
    {
        _mapper = mapper;
        _userManager = userManager;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _jwtConfig = optionsMonitor.CurrentValue;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await _userManager.Users.AnyAsync());
    }

    public bool IsExpired(ClaimsPrincipal user)
    {
        // Check if the "exp" claim exists and is a valid DateTime
        if (long.TryParse(user.FindFirstValue("exp"), out long unixTimestamp))
        {
            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
            return expirationTime <= DateTime.UtcNow;
        }

        // If "exp" claim doesn't exist or is not a valid long, consider it expired
        return true;
    }
    
    public async Task<bool> Exists(ClaimsPrincipal user)
    {
        var id = user.FindFirst("Id").Value;
    
        if (string.IsNullOrEmpty(id))
            return false;

        var foundUser = await _userManager.FindByIdAsync(id);

        return foundUser != null;
    }
    
    public async Task<UserAccountModel> Register(RegisterDto registerDto)
    {
        await _registerValidator.CheckAsync(registerDto);

        // Check if username already exists
        var existingUser = await _userManager.FindByNameAsync(registerDto.Name);
        if (existingUser != null)
        {
            throw new ProcessException($"Username '{registerDto.Name}' is already taken.");
        }

        // Check if email already exists
        var existingEmailUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingEmailUser != null)
        {
            throw new ProcessException($"User account with email '{registerDto.Email}' already exists.");
        }

        // Create user account
        var user = new User()
        {
            Status = UserStatus.Active,
            FirstName =  registerDto.FirstName,
            LastName = registerDto.LastName,
            Name = registerDto.Name,
            UserName = registerDto.Name, // Set username
            Email = registerDto.Email,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false           
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            throw new ProcessException($"Creating user account failed. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        }
    
        return _mapper.Map<UserAccountModel>(user);
    }

    public async Task<UserAccountModel> GetUser(Guid userId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        return _mapper.Map<User, UserAccountModel>(user!);
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
        
        var token = GenerateJwtToken(existingUser).Result;
        return token;
    }
    
    private async Task<string> GenerateJwtToken(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _jwtConfig.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, _jwtConfig.Audience),
        };

        // Fetch user roles
        var userRoles = await _userManager.GetRolesAsync(user);
        if (userRoles.Any())
        {
            // Add the user's role as a claim
            claims.Add(new Claim(ClaimTypes.Role, userRoles.First()));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);
        return jwtToken;
    }

}