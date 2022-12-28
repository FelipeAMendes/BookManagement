using BookManagement.Core.Domain.Entities.Books.Specifications;
using BookManagement.Core.Shared.Extensions.FluentValidation;
using BookManagement.Core.Shared.Queries;
using BookManagement.Core.Shared.Queries.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Queries.BookQueries.Inputs;

public class FilterBookQueries : BaseQueries<FilterBookQueries, FilterBookQueriesValidation>, IQueries
{
    public const int LimitPerPage = 30;
    public const string FieldsOrderBy = "Id, Title";

    public int CurrentPage { get; set; }
    public string Title { get; set; }
    public int? PageSize { get; set; }
    public string OrderBy { get; set; } = "Id DESC";
}

public class FilterBookQueriesValidation : AbstractValidator<FilterBookQueries>
{
    public FilterBookQueriesValidation()
    {
        RuleFor(x => x.Title).MaximumLength(BookSpecification.TitleColumnSize);
        RuleFor(x => x.CurrentPage).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(FilterBookQueries.LimitPerPage);
        RuleFor(x => x.OrderBy).NotEmpty().OrderBy(FilterBookQueries.FieldsOrderBy, true);
    }
}