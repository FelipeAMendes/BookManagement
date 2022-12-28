using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Shared.Entities.Interfaces;
using BookManagement.Core.Shared.Extensions.ModelBuilderExtensions;
using BookManagement.Core.Shared.ValueObjects.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Core.Infra.Data.Extensions;

public static class ModelBuilderExtension
{
    public static ModelBuilder ConfigureEntities(this ModelBuilder modelBuilder)
    {
        var entities = typeof(Book).Assembly
            .GetTypes()
            .Where(t => t.IsClass
                        && !t.IsAbstract
                        && t.IsPublic
                        && typeof(IBaseEntity).IsAssignableFrom(t)
                        && !typeof(IValueObject).IsAssignableFrom(t));

        foreach (var entity in entities)
        {
            var nameSchema = entity.Namespace?.Split('.').Last();
            var entityTypeBuilder = modelBuilder.Entity(entity);

            modelBuilder
                .Entity(entity)
                .HasKey(nameof(IBaseEntity.Id));

            modelBuilder
                .Entity(entity)
                .Property(nameof(IBaseEntity.CreatedDate))
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            modelBuilder
                .Entity(entity)
                .Property(nameof(IBaseEntity.UpdatedDate))
                .IsRequired(false);

            modelBuilder
                .Entity(entity)
                .Property(nameof(IBaseEntity.Removed))
                .IsRequired()
                .HasDefaultValue(false);

            entityTypeBuilder.ToTable(entity.Name, nameSchema);
        }

        return modelBuilder;
    }

    public static ModelBuilder ConfigureCollationCaseInsensitive(this ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

        return modelBuilder;
    }

    public static ModelBuilder ConfigureGlobalFilter(this ModelBuilder modelBuilder)
    {
        modelBuilder.SetQueryFilterOnAllEntities<IBaseEntity>(x => !x.Removed);

        return modelBuilder;
    }
}