using FluentValidation;
using FluentValidation.Validators;

namespace BookManagement.Core.Shared.Validations;

public class Isbn13Validator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => nameof(MinCountListValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        return value is string isbn13Value && Isbn13Validator.IsValid(isbn13Value);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} invalid";
    }
}

public static class Isbn13Validator
{
    public static bool IsValid(string value)
    {
        if (string.IsNullOrEmpty(value))
            return true;

        value = value.Replace("-", "");

        var length = value.Length;
        if (length != 13)
            return false;

        if (!long.TryParse(value, out _))
            return false;

        var sum = 0;
        for (var i = 0; i < 12; i++) sum += int.Parse(value[i].ToString()) * (i % 2 == 1 ? 3 : 1);

        var remainder = sum % 10;
        var checkDigit = 10 - remainder;

        if (checkDigit == 10)
            checkDigit = 0;

        return checkDigit == int.Parse(value[12].ToString());
    }
}