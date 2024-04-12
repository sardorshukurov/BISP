using FluentValidation;

namespace Welisten.Client.Pages.Comments.Models;

public class CreateCommentModel
{
    public Guid PostId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsAnonymous { get; set; } = false;
}


public class CreateCommentModelValidator : AbstractValidator<CreateCommentModel>
{ 
    public CreateCommentModelValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Comment is required")
            .MinimumLength(5).WithMessage("Minimum length of comment is 5")
            .MaximumLength(1000).WithMessage("Maximum length of comment is 1000");

        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("Post is required");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateCommentModel>.CreateWithOptions((CreateCommentModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}