using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Entities.Publishers.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.PublisherCommands;

public class CreatePublisherCommand : BaseCommand<CreatePublisherCommand, CreatePublisherCommandValidation>, ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Publisher ToEntity()
    {
        return new(Name, Description);
    }
}

public class CreatePublisherCommandValidation : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(PublisherSpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(PublisherSpecification.DescriptionColumnSize);
    }
}