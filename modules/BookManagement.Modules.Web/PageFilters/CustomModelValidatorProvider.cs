using BookManagement.Core.Shared.Commands.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookManagement.Modules.Web.PageFilters;

public class CustomModelValidatorProvider : IModelValidatorProvider
{
    public void CreateValidators(ModelValidatorProviderContext context)
    {
        var modelType = (TypeInfo) context.ModelMetadata.ContainerMetadata?.ModelType.GetInterface(nameof(ICommand));

        if (modelType != typeof(ICommand))
            return;

        var validatorType =
            typeof(CustomModelValidator).MakeGenericType(context.ModelMetadata.ContainerMetadata.ModelType);
        var validator = (IModelValidator) Activator.CreateInstance(validatorType);

        context.Results.Add(new ValidatorItem
        {
            Validator = validator,
            IsReusable = true
        });
    }
}

public class CustomModelValidator : IModelValidator
{
    public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
    {
        //var validationResult = (context.Container as ICommand)?.Validate();
        var validationResult = (context.Container as ICommand)?.Errors;

        return validationResult?.Count == 0
            ? new List<ModelValidationResult>()
            : validationResult?.Select(err => new ModelValidationResult(err.PropertyName, err.ErrorMessage));
    }
}