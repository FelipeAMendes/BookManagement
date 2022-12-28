using FluentValidation;
using FluentValidation.Validators;

namespace BookManagement.Core.Shared.Validations;

public class UriValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => nameof(UriValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        var propValue = value as string;
        var uriValid = Uri.TryCreate(propValue, UriKind.Absolute, out var outUri) &&
                       (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);

        return uriValid;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} has an invalid URI";
    }
}