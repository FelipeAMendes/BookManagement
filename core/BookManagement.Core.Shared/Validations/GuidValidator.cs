using FluentValidation;
using FluentValidation.Validators;

namespace BookManagement.Core.Shared.Validations;

public class GuidValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => nameof(GuidValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        return value is Guid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} must be a valid Guid";
    }
}