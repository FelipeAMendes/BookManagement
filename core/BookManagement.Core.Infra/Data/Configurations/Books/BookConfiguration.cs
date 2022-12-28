using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Core.Infra.Data.Configurations.Books;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.Title).IsRequired().HasMaxLength(BookSpecification.TitleColumnSize);
        builder.Property(x => x.Description).HasMaxLength(BookSpecification.DescriptionColumnSize);

        builder.Property(x => x.Isbn10)
            .HasConversion(x => x.Value.Value, x => (Isbn10?) x)
            .HasColumnType($"varchar({BookSpecification.Isbn10Length})");

        builder.Property(x => x.Isbn13)
            .HasConversion(x => x.Value.Value, x => (Isbn13?) x)
            .HasColumnType($"varchar({BookSpecification.Isbn13Length})");

        builder.HasMany(x => x.Keywords).WithOne(x => x.Book);
        builder.HasMany(x => x.Quotes).WithOne(x => x.Book);
        builder.HasMany(x => x.Reviews).WithOne(x => x.Book);

        builder.HasOne(x => x.Author);
        builder.HasOne(x => x.Category);
        builder.HasOne(x => x.Publisher);
    }
}