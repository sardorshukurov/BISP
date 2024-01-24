using FluentValidation;

namespace Welisten.Services.UserAccounts;

public class RegisterDto
{
    public required string Name { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(5).WithMessage("Username's minimum length is 10.")
            .MaximumLength(50).WithMessage("Username's maximum length is 50.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required.")
            .MinimumLength(5).WithMessage("Firstname's minimum length is 10.")
            .MaximumLength(50).WithMessage("Firstname's maximum length is 50.");;

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password should have at least 8 characters.")
            .MaximumLength(50).WithMessage("Password should have at most 50 characters.");
    }
}