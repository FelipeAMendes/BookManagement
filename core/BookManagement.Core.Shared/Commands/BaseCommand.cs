using BookManagement.Core.Shared.Validations;
using FluentValidation;

namespace BookManagement.Core.Shared.Commands;

public abstract class BaseCommand<TCommand, TCommandValidation> : BaseValidation<TCommandValidation>
    where TCommandValidation : AbstractValidator<TCommand>, new()
{
}