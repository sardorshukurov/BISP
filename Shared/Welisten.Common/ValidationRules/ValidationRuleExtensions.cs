using FluentValidation;

namespace Welisten.Common.ValidationRules;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, string> PostTitle<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Minimum length is 5")
            .MaximumLength(100).WithMessage("Maximum length is 100");
    }
    
    public static IRuleBuilderOptions<T, string> PostText<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Text of the post is required")
            .MinimumLength(50).WithMessage("Minimum length of text is 50")
            .MaximumLength(3000).WithMessage("Maximum length is 3000");
    }
    
    public static IRuleBuilderOptions<T, string> CommentText<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Comment is required")
            .MinimumLength(5).WithMessage("Minimum length of comment is 5")
            .MaximumLength(1000).WithMessage("Maximum length of comment is 1000");
    }

    public static IRuleBuilderOptions<T, Guid> UserId<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("User is required");
    }
}