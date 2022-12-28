using FluentValidation.Results;

namespace BookManagement.Core.Shared.Commands.Interfaces;

public interface ICommand
{
    IList<ValidationFailure> Errors { get; }
    ValidationResult Validate();
}