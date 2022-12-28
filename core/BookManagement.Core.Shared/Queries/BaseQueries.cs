using BookManagement.Core.Shared.Validations;
using FluentValidation;

namespace BookManagement.Core.Shared.Queries;

public abstract class BaseQueries<TQueries, TQueriesValidation> : BaseValidation<TQueriesValidation>
    where TQueriesValidation : AbstractValidator<TQueries>, new()
{
}