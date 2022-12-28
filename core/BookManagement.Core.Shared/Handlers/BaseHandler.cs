using System.Linq.Expressions;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Extensions.EnumExtensions;
using BookManagement.Core.Shared.Extensions.ExpressionExtensions;
using BookManagement.Core.Shared.Handlers.Interfaces;
using FluentValidation.Results;

namespace BookManagement.Core.Shared.Handlers;

public abstract class BaseHandler : IBaseHandler
{
    private readonly List<ValidationFailure> _errors;

    protected BaseHandler()
    {
        _errors = new List<ValidationFailure>();
    }

    public bool IsValid => _errors.Count == 0;

    public IList<ValidationFailure> Errors => _errors;

    public void AddError(string propertyName, string errorMessage)
    {
        var validationFailure = new ValidationFailure(propertyName, errorMessage);

        _errors.Add(validationFailure);
    }

    public void AddError<T>(Expression<Func<T, object>> expression, string errorMessage)
    {
        var propertyName = expression.Body.GetMemberName();

        AddError(propertyName, errorMessage);
    }

    public void AddError<T>(Expression<Func<T, object>> expression, ValidationType validationType)
    {
        var propertyName = expression.Body.GetMemberName();

        AddError(propertyName, validationType.GetDescription());
    }

    public void AddErrors(IList<ValidationFailure> errors)
    {
        if (errors != null)
            _errors.AddRange(errors);
    }

    public ICommandResult<CommandNoneResult> CreateDefaultCommandResult(bool success)
    {
        return CommandResult<CommandNoneResult>.CreateResult(success);
    }

    public ICommandResult<CommandNoneResult> CreateNotFoundCommandResult()
    {
        return CommandResult<CommandNoneResult>.CreateResult(ValidationType.ItemNotFound);
    }

    public ICommandResult<CommandNoneResult> CreateErrorCommandResult(IList<ValidationFailure> errors)
    {
        return CommandResult<CommandNoneResult>.CreateResult(false, errors);
    }

    private ICommandResult<CommandNoneResult> CreateDefaultCommandResult(ValidationType validationType)
    {
        return CommandResult<CommandNoneResult>.CreateResult(validationType);
    }

    public ICommandResult<CommandNoneResult> CreateDefaultCommandResult(bool success, ValidationType validationType)
    {
        return success
            ? CreateDefaultCommandResult(true)
            : CreateDefaultCommandResult(validationType);
    }
}