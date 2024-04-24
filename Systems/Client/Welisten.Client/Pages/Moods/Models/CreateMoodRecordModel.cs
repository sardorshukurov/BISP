using FluentValidation;

namespace Welisten.Client.Pages.Moods.Models;

public class CreateMoodRecordModel
{
    public string Text { get; set; } = string.Empty;
    public DateTime? Date { get; set; } = DateTime.UtcNow;
    public Guid MoodId { get; set; }
    public Guid EventId { get; set; }
}

public class CreateMoodRecordModelValidator : AbstractValidator<CreateMoodRecordModel>
{
    public CreateMoodRecordModelValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(1000).WithMessage("Maximum of mood record text length is 1000");

        RuleFor(x => x.Date.Value.ToLocalTime())
            .LessThanOrEqualTo(DateTime.Now.ToLocalTime()).WithMessage("You cannot write about future");

        RuleFor(x => x.MoodId)
            .NotEmpty().WithMessage("Mood is required");

        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("Event is required");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateMoodRecordModel>.CreateWithOptions((CreateMoodRecordModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}