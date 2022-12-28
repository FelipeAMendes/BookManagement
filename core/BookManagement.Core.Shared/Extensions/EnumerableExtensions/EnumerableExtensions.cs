namespace BookManagement.Core.Shared.Extensions.EnumerableExtensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> ToGenericList<T>(this T value)
    {
        return new List<T> {value};
    }
}