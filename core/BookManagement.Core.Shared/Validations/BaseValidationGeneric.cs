using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;

namespace BookManagement.Core.Shared.Validations;

public abstract class BaseValidation<TValidation> where TValidation : IValidator, new()
{
    [NotMapped] public virtual IList<ValidationFailure> Errors => Validate()?.Errors;

    public ValidationResult Validate()
    {
        var validation = new TValidation();
        var context = new ValidationContext<object>(this);
        var validationResult = validation.Validate(context);
        return validationResult;
    }
}