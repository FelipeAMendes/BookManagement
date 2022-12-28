namespace BookManagement.Core.Shared.Commands.Interfaces;

public interface IUpdateCommand : ICommand
{
    Guid Id { get; set; }
}