using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Handlers;
using FluentValidation.Results;

namespace BookManagement.Core.Shared.Commands;

public class CommandResult<TCommandResult> : ICommandResult<TCommandResult> where TCommandResult : IResult
{
    private readonly IList<ValidationFailure> _errors = new List<ValidationFailure>();

    public CommandResult(bool success)
    {
        Success = success;
    }

    public CommandResult(ValidationType validationType)
    {
        Success = false;
        ValidationType = validationType;
    }

    public CommandResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public CommandResult(bool success, IList<ValidationFailure> errors)
    {
        Success = success;
        _errors = errors;
    }

    public CommandResult(bool success, TCommandResult result)
    {
        Success = success;
        Result = result;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public TCommandResult Result { get; set; }
    public ValidationType ValidationType { get; set; }

    public IEnumerable<ValidationFailure> Errors =>
        _errors.Select(x => new ValidationFailure(x.PropertyName, x.ErrorMessage));

    public string GetMessage(string message)
    {
        return !string.IsNullOrEmpty(Message) ? Message : message;
    }

    public static ICommandResult<TCommandResult> CreateResult(bool success)
    {
        return new CommandResult<TCommandResult>(success);
    }

    public static ICommandResult<TCommandResult> CreateResult(ValidationType validationType)
    {
        return new CommandResult<TCommandResult>(validationType);
    }

    public static ICommandResult<TCommandResult> CreateResult(bool success, string message)
    {
        return new CommandResult<TCommandResult>(success, message);
    }

    public static ICommandResult<TCommandResult> CreateResult(bool success, IList<ValidationFailure> errors)
    {
        return new CommandResult<TCommandResult>(success, errors);
    }

    public static ICommandResult<TCommandResult> CreateResult(bool success, TCommandResult result)
    {
        return new CommandResult<TCommandResult>(success, result);
    }
}