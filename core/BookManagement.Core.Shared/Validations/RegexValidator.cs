using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace BookManagement.Core.Shared.Validations;

public class RegexValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private readonly string _pattern;

    public RegexValidator(string pattern)
    {
        _pattern = pattern;
    }

    public override string Name => nameof(MinCountListValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value is not string valuePattern)
            return false;

        var regexPattern = Regex.IsMatch(valuePattern, _pattern);
        return regexPattern;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} invalid";
    }
}