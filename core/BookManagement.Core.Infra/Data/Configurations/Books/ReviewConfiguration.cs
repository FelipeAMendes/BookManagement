using BookManagement.Core.Domain.Entities.Books;
using BookManagement.Core.Domain.Entities.Books.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Core.Infra.Data.Configurations.Books;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(x => x.Description).HasMaxLength(ReviewSpecification.DescriptionColumnSize);
        builder.Property(x => x.AuthorName).HasMaxLength(ReviewSpecification.AuthorNameColumnSize);
        builder.Property(x => x.AuthorNameInfo).HasMaxLength(ReviewSpecification.AuthorNameInfoColumnSize);
        builder.Property(x => x.ReviewType);
    }
}