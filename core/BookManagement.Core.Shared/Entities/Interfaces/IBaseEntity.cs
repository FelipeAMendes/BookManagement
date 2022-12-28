namespace BookManagement.Core.Shared.Entities.Interfaces;

public interface IBaseEntity
{
    Guid Id { get; }
    DateTime CreatedDate { get; }
    DateTime? UpdatedDate { get; }
    bool Removed { get; }
}