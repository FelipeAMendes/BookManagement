using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Core.Infra.Data.Configurations.Books;

public class KeywordConfiguration : IEntityTypeConfiguration<Keyword>
{
    public void Configure(EntityTypeBuilder<Keyword> builder)
    {
        builder.Property(x => x.Description).HasMaxLength(KeywordSpecification.DescriptionColumnSize);
    }
}