using FluentValidation;
using FluentValidation.Validators;

namespace BookManagement.Core.Shared.Validations;

public class Isbn10Validator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => nameof(MinCountListValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        return value is string isbn10Value && Isbn10Validator.IsValid(isbn10Value);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} invalid";
    }
}

public static class Isbn10Validator
{
    public static bool IsValid(string value)
    {
        if (string.IsNullOrEmpty(value))
            return true;

        value = value.Replace("-", "");

        var length = value.Length;
        if (length != 10)
            return false;

        var sum = 0;
        for (var i = 0; i < 9; i++)
        {
            var digit = value[i] - '0';

            if (0 > digit || 9 < digit)
                return false;

            sum += digit * (10 - i);
        }

        var last = value[9];
        if (last != 'X' && (last < '0' || last > '9'))
            return false;

        sum += last == 'X' ? 10 : last - '0';

        return sum % 11 == 0;
    }
}