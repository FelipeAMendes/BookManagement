using System.Text.RegularExpressions;
using BookManagement.Core.Shared.Validations;
using BookManagement.Core.Shared.Validations.Inputs;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BookManagement.Core.Shared.Extensions.FluentValidation;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> OrderBy<T>(this IRuleBuilder<T, string> ruleBuilder,
        string allowsProperties, bool allowNull)
    {
        return ruleBuilder.Must(value =>
        {
            switch (value)
            {
                case null when allowNull:
                    return true;
                case null:
                    return false;
            }

            if (allowsProperties == null)
                return false;

            var allowsPropertiesArray = allowsProperties.Split(',').Select(y => y.Trim().ToLower());
            var regexExtratProperty =
                Regex.Match(value.ToLower(), @"^(\s+)?(.*)\s+(asc|desc)(\s+)?$", RegexOptions.Compiled);

            if (!regexExtratProperty.Success)
                return false;

            var propertyToValid = regexExtratProperty.Groups[2].Value.Trim();
            var propertyAllow = allowsPropertiesArray.Contains(propertyToValid);
            return propertyAllow;
        }).WithMessage("'{PropertyValue}' cannot be used to sort");
    }

    public static IRuleBuilderOptions<T, IList<TElement>> MinCount<T, TElement>(
        this IRuleBuilder<T, IList<TElement>> ruleBuilder, int countMin)
    {
        return ruleBuilder.SetValidator(new MinCountListValidator<T, IList<TElement>>(countMin));
    }

    public static IRuleBuilderOptions<T, IReadOnlyCollection<TElement>> MinCount<T, TElement>(
        this IRuleBuilder<T, IReadOnlyCollection<TElement>> ruleBuilder, int countMin)
    {
        return ruleBuilder.SetValidator(new MinCountListValidator<T, IReadOnlyCollection<TElement>>(countMin));
    }

    public static IRuleBuilderOptions<T, IFormFile> ContentFile<T>(this IRuleBuilder<T, IFormFile> ruleBuilder,
        params FileContentValidatorInput[] formats)
    {
        if (!formats.Any())
            throw new ArgumentException("Set at least one allowed file type");

        return ruleBuilder.SetValidator(new FileContentValidator<T, IFormFile>(formats));
    }

    public static IRuleBuilderOptions<T, string> IsGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new GuidValidator<T, string>());
    }

    public static IRuleBuilderOptions<T, IList<TElement>> UniqueBy<T, TElement>(
        this IRuleBuilder<T, IList<TElement>> ruleBuilder, Func<TElement, object> keyExtractor)
    {
        var keyEqualityComparer = new KeyEqualityComparerValidator<TElement>(keyExtractor);
        return ruleBuilder.Must(list => list == null || list.Distinct(keyEqualityComparer).Count() == list.Count)
            .WithMessage("{PropertyName} cannot have duplicate values");
    }

    public static IRuleBuilderOptions<T, string> Uri<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new UriValidator<T, string>());
    }

    public static IRuleBuilderOptions<T, string> RegexPattern<T>(this IRuleBuilder<T, string> ruleBuilder,
        string pattern)
    {
        return ruleBuilder.SetValidator(new RegexValidator<T, string>(pattern));
    }

    public static IRuleBuilderOptions<T, string> Isbn10<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new Isbn10Validator<T, string>());
    }

    public static IRuleBuilderOptions<T, string> Isbn13<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new Isbn13Validator<T, string>());
    }
}