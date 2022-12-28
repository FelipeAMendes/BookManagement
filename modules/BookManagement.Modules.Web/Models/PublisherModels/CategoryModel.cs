using BookManagement.Core.Domain.Entities.Publishers.Specifications;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Modules.Web.Models.PublisherModels;

public class PublisherModel
{
    [Key] public Guid? Id { get; set; }

    [Required]
    [DisplayName("Name")]
    [MaxLength(PublisherSpecification.NameColumnSize)]
    public string Name { get; set; }

    [DisplayName("Description")]
    [MaxLength(PublisherSpecification.DescriptionColumnSize)]
    public string Description { get; set; }

    public static PublisherModel FromQueryResult(PublisherQueryResult publisherQueryResult)
    {
        return new PublisherModel
        {
            Id = publisherQueryResult.Id,
            Name = publisherQueryResult.Name,
            Description = publisherQueryResult.Description
        };
    }
}