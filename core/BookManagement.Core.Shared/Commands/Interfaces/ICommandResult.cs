using BookManagement.Core.Shared.Handlers;
using FluentValidation.Results;

namespace BookManagement.Core.Shared.Commands.Interfaces;

public interface ICommandResult
{
    bool Success { get; set; }
    string Message { get; set; }
    IEnumerable<ValidationFailure> Errors { get; }
    string GetMessage(string message = null);
}

public interface ICommandResult<TCommandResult> : ICommandResult where TCommandResult : IResult
{
    TCommandResult Result { get; set; }
    ValidationType ValidationType { get; set; }
}