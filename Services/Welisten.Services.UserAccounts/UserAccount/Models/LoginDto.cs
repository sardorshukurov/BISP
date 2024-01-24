using FluentValidation;

namespace Welisten.Services.UserAccounts;

public class LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password should have at least 8 characters.")
            .MaximumLength(50).WithMessage("Password should have at most 50 characters.");
    }
}