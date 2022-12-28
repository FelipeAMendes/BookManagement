using BookManagement.Core.Domain.Entities.Categories;

namespace BookManagement.Core.Domain.UnitTest.Entities.Categories;

[Collection(nameof(CategoryTestsCollection))]
public class CategoryTests
{
    private readonly CategoryTestsFixture _fixture;

    public CategoryTests(CategoryTestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Valid category")]
    [Trait("Category", "Category Entity")]
    public void CategoryIsValid_ValidateMethodIsCalled_ReturnsSuccess()
    {
        //Arrange
        var category = _fixture.GetValidCategory();

        //Act
        var resultValidation = category.Validate();

        //Assert
        Assert.True(resultValidation.IsValid);
        Assert.Empty(resultValidation.Errors);
    }

    [Fact(DisplayName = "Valid category with parent")]
    [Trait("Category", "Category Entity")]
    public void CategoryWithParentIsValid_ParentIsChecked_ReturnsSuccess()
    {
        //Arrange
        var category = _fixture.GetValidCategoryWithParent();

        //Act
        var parentCategory = category.ParentCategory;

        //Assert
        Assert.NotNull(parentCategory);
        Assert.NotEmpty(parentCategory.Name);
    }

    [Fact(DisplayName = "Invalid category")]
    [Trait("Category", "Category Entity")]
    public void CategoryIsNotValid_ValidateMethodIsCalled_ReturnsFalse()
    {
        //Arrange
        var category = _fixture.GetInvalidCategory();

        //Act
        var resultValidation = category.Validate();

        //Assert
        Assert.False(resultValidation.IsValid);
        Assert.Equal(2, resultValidation.Errors.Count);
    }

    [Fact(DisplayName = "Dont add repeated child")]
    [Trait("Category", "Category Entity")]
    public void CategoryIsValid_AddChildCategoryMethodIsCalled_DontAddRepeated()
    {
        //Arrange
        var category = _fixture.GetValidCategory();
        var childCategory = new Category("ChildCategory", "Description");

        //Act
        category.AddChildCategory(childCategory);
        category.AddChildCategory(childCategory);

        //Assert
        Assert.Equal(1, category.ChildCategories.Count);
    }

    [Fact(DisplayName = "Correctly parent hierarchy names")]
    [Trait("Category", "Category Entity")]
    public void CategoryIsValid_SetParentMethodIsCalled_ReturnsCorrectlyParentHierarchyNames()
    {
        //Arrange
        var category = _fixture.GetValidCategory();
        var parentCategory = new Category("ParentCategory", "Description");
        var grandParentCategory = new Category("GrandParentCategory", "Description");

        //Act
        category.SetParent(parentCategory);
        parentCategory.SetParent(grandParentCategory);

        //Assert
        var exprectedResult = new List<string> {"GrandParentCategory", "ParentCategory"};
        Assert.Equal(exprectedResult, category.GetParentHierarchyNames());
    }
}