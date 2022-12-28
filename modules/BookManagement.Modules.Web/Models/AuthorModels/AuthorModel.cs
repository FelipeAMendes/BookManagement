using BookManagement.Core.Domain.Entities.Authors.Specifications;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Modules.Web.Models.AuthorModels;

public class AuthorModel
{
    [Key] public Guid? Id { get; set; }

    [Required]
    [DisplayName("Name")]
    [MaxLength(AuthorSpecification.NameColumnSize)]
    public string Name { get; set; }

    [DisplayName("Description")]
    [MaxLength(AuthorSpecification.DescriptionColumnSize)]
    public string Description { get; set; }

    public static AuthorModel FromQueryResult(AuthorQueryResult authorQueryResult)
    {
        return new AuthorModel
        {
            Id = authorQueryResult.Id,
            Name = authorQueryResult.Name,
            Description = authorQueryResult.Description
        };
    }
}