using BookManagement.Core.Domain.Entities.Authors.Specifications;
using BookManagement.Core.Shared.Extensions.FluentValidation;
using BookManagement.Core.Shared.Queries;
using BookManagement.Core.Shared.Queries.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;

public class FilterAuthorQueries : BaseQueries<FilterAuthorQueries, FilterAuthorQueriesValidation>, IQueries
{
    public const int DefaultPageSize = 10;
    public const int LimitPerPage = 30;
    public const string FieldsOrderBy = "Id, Name";

    public int CurrentPage { get; set; }
    public string Name { get; set; }
    public int? PageSize { get; set; } = DefaultPageSize;
    public string OrderBy { get; set; } = "Id DESC";
}

public class FilterAuthorQueriesValidation : AbstractValidator<FilterAuthorQueries>
{
    public FilterAuthorQueriesValidation()
    {
        RuleFor(x => x.Name).MaximumLength(AuthorSpecification.NameColumnSize);
        RuleFor(x => x.CurrentPage).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(FilterAuthorQueries.LimitPerPage);
        RuleFor(x => x.OrderBy).NotEmpty().OrderBy(FilterAuthorQueries.FieldsOrderBy, true);
    }
}