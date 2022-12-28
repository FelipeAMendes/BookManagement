using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Entities.Authors.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.AuthorCommands;

public class CreateAuthorCommand : BaseCommand<CreateAuthorCommand, CreateAuthorCommandValidation>, ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Author ToEntity()
    {
        return new(Name, Description);
    }
}

public class CreateAuthorCommandValidation : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AuthorSpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(AuthorSpecification.DescriptionColumnSize);
    }
}