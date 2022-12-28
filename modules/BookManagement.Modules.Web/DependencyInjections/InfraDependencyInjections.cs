using BookManagement.Core.Domain.Repositories.AuthorRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.BookRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.CategoryRepositories.Interfaces;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using BookManagement.Core.Infra.Data;
using BookManagement.Core.Infra.Data.Interfaces;
using BookManagement.Core.Infra.Mappings;
using BookManagement.Core.Infra.Repositories.AuthorRepositories;
using BookManagement.Core.Infra.Repositories.BookRepositories;
using BookManagement.Core.Infra.Repositories.CategoryRepositories;
using BookManagement.Core.Infra.Repositories.PublisherRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.Modules.Web.DependencyInjections;

public static class InfraDependencyInjections
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(connectionString!));
        services.AddScoped<IBookContext, BookContext>();

        return services;
    }

    public static IServiceCollection AddInfraDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();

        services.AddAutoMapper(typeof(MappingsProfile).Assembly);

        return services;
    }
}