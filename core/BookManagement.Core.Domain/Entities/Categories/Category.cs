using BookManagement.Core.Domain.Entities.Categories.Validations;
using BookManagement.Core.Shared.Entities;

namespace BookManagement.Core.Domain.Entities.Categories;

public class Category : BaseEntity<CategoryValidation>
{
    private List<Category> _childCategories;

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Category(string name, string description, Category parentCategory)
    {
        Name = name;
        Description = description;
        SetParent(parentCategory);
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public virtual Category ParentCategory { get; private set; }
    public virtual IReadOnlyCollection<Category> ChildCategories => _childCategories?.AsReadOnly();

    public void Edit(string name, string description)
    {
        Name = name;
        Description = description;
        SetUpdatedDate(DateTime.Now);
    }

    public void AddChildCategory(Category category)
    {
        _childCategories ??= new List<Category>();

        var categoryExists = _childCategories.Any(x => x.Id == category.Id);
        if (!categoryExists)
            _childCategories.Add(category);
    }

    public void SetParent(Category parentCategory)
    {
        ParentCategory = parentCategory;
    }

    public IEnumerable<string> GetParentHierarchyNames()
    {
        var parents = GetParentHierarchyNames(this);

        return parents;
    }

    public IEnumerable<string> GetParentHierarchyNames(Category category)
    {
        ICollection<string> parentCategories = new List<string>();
        while (true)
        {
            if (category.ParentCategory is null)
                return parentCategories.Reverse();

            parentCategories.Add(category.ParentCategory.Name);
            category = category.ParentCategory;
        }
    }
}