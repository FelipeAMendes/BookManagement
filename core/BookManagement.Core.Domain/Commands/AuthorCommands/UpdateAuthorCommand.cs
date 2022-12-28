using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Entities.Authors.Specifications;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Commands.AuthorCommands;

public class UpdateAuthorCommand : BaseCommand<UpdateAuthorCommand, UpdateAuthorCommandValidation>, ICommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Author UpdateEntity(Author author)
    {
        author.Edit(Name, Description);
        return author;
    }
}

public class UpdateAuthorCommandValidation : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidation()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(AuthorSpecification.NameColumnSize);
        RuleFor(x => x.Description).MaximumLength(AuthorSpecification.DescriptionColumnSize);
    }
}