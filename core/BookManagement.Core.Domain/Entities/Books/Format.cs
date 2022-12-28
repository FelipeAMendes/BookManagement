using System.ComponentModel;

namespace BookManagement.Core.Domain.Entities.Books;

public enum Format
{
    [Description("Paperback")] Paperback = 1,
    [Description("Hardcover")] Hardcover,
    [Description("Kindle Edition")] KindleEdition,
    [Description("Large Print")] LargePrint,
    [Description("Loose Leaf")] LooseLeaf
}