using FluentValidation;

namespace Welisten.Client.Models.Authorization;

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

public class LoginValidator : AbstractValidator<LoginModel>
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