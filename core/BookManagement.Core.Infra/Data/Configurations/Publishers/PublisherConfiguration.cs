using BookManagement.Core.Domain.Entities.Publishers;
using BookManagement.Core.Domain.Entities.Publishers.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Core.Infra.Data.Configurations.Publishers;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(PublisherSpecification.NameColumnSize);
        builder.Property(x => x.Description).HasMaxLength(PublisherSpecification.DescriptionColumnSize);
    }
}