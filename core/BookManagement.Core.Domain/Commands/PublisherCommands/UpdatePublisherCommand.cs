using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Entities.Publishers.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.PublisherCommands;

public class UpdatePublisherCommand : BaseCommand<UpdatePublisherCommand, UpdatePublisherCommandValidation>, ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void UpdateEntity(Publisher publisher)
    {
        publisher.Edit(Name, Description);
    }
}

public class UpdatePublisherCommandValidation : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(PublisherSpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(PublisherSpecification.DescriptionColumnSize);
    }
}