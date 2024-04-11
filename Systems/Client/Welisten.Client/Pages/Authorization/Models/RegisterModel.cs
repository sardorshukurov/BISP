using FluentValidation;

namespace Welisten.Client.Pages.Authorization.Models;

public class RegisterModel
{
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterValidator : AbstractValidator<RegisterModel>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(5).WithMessage("Username's minimum length is 5.")
            .MaximumLength(50).WithMessage("Username's maximum length is 50.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname is required.")
            .MinimumLength(5).WithMessage("Firstname's minimum length is 5.")
            .MaximumLength(50).WithMessage("Firstname's maximum length is 50.");;

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password should have at least 8 characters.")
            .MaximumLength(50).WithMessage("Password should have at most 50 characters.");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<RegisterModel>.CreateWithOptions((RegisterModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}