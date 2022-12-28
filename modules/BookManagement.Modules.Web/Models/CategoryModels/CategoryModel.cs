using BookManagement.Core.Domain.Entities.Categories.Specifications;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Modules.Web.Models.CategoryModels;

public class CategoryModel
{
    [Key] public Guid? Id { get; set; }

    [Required]
    [DisplayName("Name")]
    [MaxLength(CategorySpecification.NameColumnSize)]
    public string Name { get; set; }

    [DisplayName("Description")]
    [MaxLength(CategorySpecification.DescriptionColumnSize)]
    public string Description { get; set; }

    [DisplayName("Parent Category")] public Guid? ParentCategoryId { get; set; }

    public static CategoryModel FromQueryResult(CategoryQueryResult categoryQueryResult)
    {
        return new CategoryModel
        {
            Id = categoryQueryResult.Id,
            Name = categoryQueryResult.Name,
            Description = categoryQueryResult.Description,
            ParentCategoryId = categoryQueryResult.ParentCategoryId
        };
    }
}