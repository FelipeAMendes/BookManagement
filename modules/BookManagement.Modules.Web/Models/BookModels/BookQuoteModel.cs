using BookManagement.Core.Domain.Commands.BookCommands;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Modules.Web.Models.BookModels;

public class BookQuoteModel
{
    public BookQuoteModel(Guid id, string description)
    {
        Id = id;
        Description = description;
    }

    [Key] public Guid? Id { get; set; }

    [Required]
    [DisplayName("Description")]
    [MaxLength(QuoteSpecification.DescriptionColumnSize)]
    public string Description { get; set; }

    public CreateBookQuoteCommand ToCreateCommand()
    {
        return new CreateBookQuoteCommand
        {
            Description = Description
        };
    }

    public UpdateBookQuoteCommand ToUpdateCommand()
    {
        return new UpdateBookQuoteCommand
        {
            Id = Id ?? Guid.Empty,
            Description = Description
        };
    }
}