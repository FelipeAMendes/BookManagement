using System.ComponentModel.DataAnnotations;
using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Infra.Data.Configurations.Books;
using BookManagement.Core.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Hosting;

namespace BookManagement.Core.Infra.Data;

public class ApplicationContext : DbContext
{
    private readonly IHostEnvironment _hostingEnvironment;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IHostEnvironment hostingEnvironment)
        : base(options)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<ValidationResult>()
            .ConfigureCollationCaseInsensitive()
            .ConfigureEntities()
            .ConfigureGlobalFilter()
            .ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_hostingEnvironment is not null && _hostingEnvironment.IsDevelopment())
            optionsBuilder.LogTo(Console.WriteLine, new[] {RelationalEventId.CommandExecuted})
                .EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }
}

//Scaffold
//public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
//{
//    public ApplicationContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
//        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BooksDb;Trusted_Connection=True;MultipleActiveResultSets=true");

//        return new ApplicationContext(optionsBuilder.Options);
//    }
//}