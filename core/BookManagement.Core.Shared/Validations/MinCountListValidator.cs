using FluentValidation;
using FluentValidation.Validators;

namespace BookManagement.Core.Shared.Validations;

public class MinCountListValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public MinCountListValidator(int countMin)
    {
        ValueToCompare = countMin;
    }

    public int ValueToCompare { get; }

    public override string Name => nameof(MinCountListValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value is not IList<object> list)
            return true;

        var valid = list.Count >= ValueToCompare;
        return valid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} must be less than or equal to";
    }
}