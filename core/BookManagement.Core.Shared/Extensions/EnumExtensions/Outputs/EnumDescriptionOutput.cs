using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Shared.Extensions.EnumExtensions.Outputs;

public class EnumDescriptionOutput : IQueryResult
{
    public EnumDescriptionOutput(int hashCode, string attribute, string description)
    {
        HashCode = hashCode;
        Attribute = attribute;
        Description = description;
    }

    public int HashCode { get; set; }
    public string Attribute { get; set; }
    public string Description { get; set; }
}