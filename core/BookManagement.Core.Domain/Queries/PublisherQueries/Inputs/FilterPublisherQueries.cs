using BookManagement.Core.Domain.Entities.Publishers.Specifications;
using BookManagement.Core.Shared.Extensions.FluentValidation;
using BookManagement.Core.Shared.Queries;
using BookManagement.Core.Shared.Queries.Interfaces;
using FluentValidation;

namespace BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;

public class FilterPublisherQueries : BaseQueries<FilterPublisherQueries, FilterPublisherQueriesValidation>, IQueries
{
    public const int LimitPerPage = 30;
    public const string FieldsOrderBy = "Id, Name";

    public int CurrentPage { get; set; }
    public string Name { get; set; }
    public int? PageSize { get; set; }
    public string OrderBy { get; set; } = "Id DESC";
}

public class FilterPublisherQueriesValidation : AbstractValidator<FilterPublisherQueries>
{
    public FilterPublisherQueriesValidation()
    {
        RuleFor(x => x.Name).MaximumLength(PublisherSpecification.NameColumnSize);
        RuleFor(x => x.CurrentPage).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(FilterPublisherQueries.LimitPerPage);
        RuleFor(x => x.OrderBy).NotEmpty().OrderBy(FilterPublisherQueries.FieldsOrderBy, true);
    }
}