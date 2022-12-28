using FluentValidation.Results;

namespace BookManagement.Core.Shared.Queries.Interfaces;

public interface IQueries
{
    ValidationResult Validate();
}