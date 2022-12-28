using BookManagement.Core.Shared.Validations;
using BookManagement.Core.Shared.ValueObjects.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Shared.ValueObjects;

public abstract class BaseValueObject<TEntityValidation> : BaseValidation<TEntityValidation>, IValueObject
    where TEntityValidation : IValidator, new()
{
}