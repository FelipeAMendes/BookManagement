using BookManagement.Core.Domain.Entities.Categories;
using BookManagement.Core.Domain.Entities.Categories.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Core.Infra.Data.Configurations.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(CategorySpecification.NameColumnSize);
        builder.Property(x => x.Description).HasMaxLength(CategorySpecification.DescriptionColumnSize);

        builder.HasMany(x => x.ChildCategories).WithOne(x => x.ParentCategory);
    }
}