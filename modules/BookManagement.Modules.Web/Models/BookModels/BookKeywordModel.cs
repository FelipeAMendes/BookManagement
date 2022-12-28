using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Modules.Web.Models.BookModels;

public class BookKeywordModel
{
    public BookKeywordModel(Guid id, string description)
    {
        Id = id;
        Description = description;
    }

    [Key] public Guid? Id { get; set; }

    [Required]
    [DisplayName("Description")]
    [MaxLength(KeywordSpecification.DescriptionColumnSize)]
    public string Description { get; set; }

    public CreateBookKeywordCommand ToCreateCommand()
    {
        return new CreateBookKeywordCommand
        {
            Description = Description
        };
    }

    public UpdateBookKeywordCommand ToUpdateCommand()
    {
        return new UpdateBookKeywordCommand
        {
            Id = Id ?? Guid.Empty,
            Description = Description
        };
    }
}