using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Domain.Queries.CategoryQueries.QueryResults;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;

namespace BookManagement.Core.Infra.Mappings.Extensions;

public static class MappingsProfileExtensions
{
    public static void ConfigureOptions(this MappingsProfile mappingsProfile)
    {
        mappingsProfile.AllowNullCollections = true;
        mappingsProfile.AllowNullDestinationValues = true;
    }

    public static void ConfigureMappings(this MappingsProfile mappingsProfile)
    {
        mappingsProfile.CreateMap<Book, BookQueryResult>();
        mappingsProfile.CreateMap<Quote, BookQuoteQueryResult>();
        mappingsProfile.CreateMap<Keyword, BookKeywordQueryResult>();
        mappingsProfile.CreateMap<Review, BookReviewQueryResult>();

        mappingsProfile.CreateMap<Author, AuthorQueryResult>();
        mappingsProfile.CreateMap<Publisher, PublisherQueryResult>();

        mappingsProfile.CreateMap<Category, CategoryDetailsQueryResult>();
        mappingsProfile.CreateMap<Category, CategoryQueryResult>()
            .ForMember(dest => dest.ParentCategoryNames, opt => opt.MapFrom(src => src.GetParentHierarchyNames()));
    }
}