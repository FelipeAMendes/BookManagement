using System.ComponentModel;

namespace BookManagement.Core.Shared.Extensions.EnumExtensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum element)
    {
        var type = element.GetType();
        var propertyMember = element.ToString();
        var memberInfo = type.GetMember(propertyMember);

        if (memberInfo is not {Length: > 0})
            return propertyMember;

        var attributes = memberInfo.First().GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes is {Length: > 0}
            ? ((DescriptionAttribute) attributes.First()).Description
            : propertyMember;
    }
}