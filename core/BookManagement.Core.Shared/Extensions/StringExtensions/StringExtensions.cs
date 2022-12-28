namespace BookManagement.Core.Shared.Extensions.StringExtensions;

public static class StringExtensions
{
    public static bool HasValue(this string value)
    {
        return !string.IsNullOrEmpty(value);
    }

    public static DateTime ToDateTime(this string value)
    {
        DateTime.TryParse(value, out var result);
        return result;
    }
}