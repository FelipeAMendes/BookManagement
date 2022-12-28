using BookManagement.Core.Domain.Entities.Categories.Specifications;
using BookManagement.Core.Shared.Extensions.FluentValidation;
using BookManagement.Core.Shared.Queries;
using BookManagement.Core.Shared.Queries.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Queries.CategoryQueries.Inputs;

public class FilterCategoryQueries : BaseQueries<FilterCategoryQueries, FilterCategoryQueriesValidation>, IQueries
{
    public const int LimitPerPage = 30;
    public const string FieldsOrderBy = "Id, Title";

    public int CurrentPage { get; set; }
    public string Name { get; set; }
    public int? PageSize { get; set; }
    public string OrderBy { get; set; } = "Id DESC";
}

public class FilterCategoryQueriesValidation : AbstractValidator<FilterCategoryQueries>
{
    public FilterCategoryQueriesValidation()
    {
        RuleFor(x => x.Name).MaximumLength(CategorySpecification.NameColumnSize);
        RuleFor(x => x.CurrentPage).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(FilterCategoryQueries.LimitPerPage);
        RuleFor(x => x.OrderBy).NotEmpty().OrderBy(FilterCategoryQueries.FieldsOrderBy, true);
    }
}