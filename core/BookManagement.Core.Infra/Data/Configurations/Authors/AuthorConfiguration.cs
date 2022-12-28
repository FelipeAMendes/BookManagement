using BookManagement.Core.Domain.Entities.Authors;
using BookManagement.Core.Domain.Entities.Authors.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Core.Infra.Data.Configurations.Authors;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(AuthorSpecification.NameColumnSize);
        builder.Property(x => x.Description).HasMaxLength(AuthorSpecification.DescriptionColumnSize);
    }
}