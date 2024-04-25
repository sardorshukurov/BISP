using FluentValidation;

namespace Welisten.Client.Pages.Posts.Models;

public class UpdatePostModel
{
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; }
    public IEnumerable<Guid> Topics { get; set; } = [];
}

public class UpdatePostModelValidator : AbstractValidator<UpdatePostModel>
{
    public UpdatePostModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Minimum length is 5")
            .MaximumLength(100).WithMessage("Maximum length is 100");
        
        RuleFor(x => x.Text)            
            .NotEmpty().WithMessage("Text of the post is required")
            .MinimumLength(15).WithMessage("Minimum length of text is 15")
            .MaximumLength(3000).WithMessage("Maximum length is 3000");

        RuleFor(x => x.Topics)
            .NotEmpty();
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<UpdatePostModel>.CreateWithOptions((UpdatePostModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}