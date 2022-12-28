using BookManagement.Core.Shared.Entities.Interfaces;
using BookManagement.Core.Shared.Validations;
using FluentValidation;

namespace BookManagement.Core.Shared.Entities;

public abstract class BaseEntity<TEntityValidation> : BaseValidation<TEntityValidation>, IBaseEntity
    where TEntityValidation : IValidator, new()
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }

    public Guid Id { get; protected set; }
    public DateTime CreatedDate { get; protected set; }
    public DateTime? UpdatedDate { get; protected set; }
    public bool Removed { get; protected set; }

    public void SetUpdatedDate(DateTime updatedDate)
    {
        UpdatedDate = updatedDate;
    }

    public virtual void Remove()
    {
        Removed = true;
        SetUpdatedDate(DateTime.Now);
    }
}