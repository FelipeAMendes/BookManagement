using FluentValidation.Results;

namespace BookManagement.Core.Shared.Handlers.Interfaces;

public interface IBaseHandler
{
    IList<ValidationFailure> Errors { get; }
}