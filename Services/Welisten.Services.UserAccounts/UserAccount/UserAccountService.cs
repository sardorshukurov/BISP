using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Welisten.Common.Exceptions;
using Welisten.Common.Validator;
using Welisten.Context.Entities;

namespace Welisten.Services.UserAccounts;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IModelValidator<RegisterUserAccountModel> _registerUserAccountModelValidator;

    public UserAccountService(
        IMapper mapper, 
        UserManager<User> userManager, 
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator
    )
    {
        _mapper = mapper;
        _userManager = userManager;
        _registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await _userManager.Users.AnyAsync());
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        _registerUserAccountModelValidator.Check(model);

        // Find user by email
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        // Create user account
        user = new User()
        {
            Status = UserStatus.Active,
            FirstName =  model.FirstName,
            LastName = model.LastName,
            Name = model.Name,
            UserName = model.Name,
            Email = model.Email,
            EmailConfirmed = true,
            PhoneNumber = null,
            PhoneNumberConfirmed = false           
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
         
        return _mapper.Map<UserAccountModel>(user);
    }
}