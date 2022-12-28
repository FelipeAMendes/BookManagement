namespace BookManagement.Core.Shared.Extensions.FluentValidation;

public class KeyEqualityComparerValidator<T> : IEqualityComparer<T>
{
    private readonly Func<T, object> _keyExtractor;

    public KeyEqualityComparerValidator(Func<T, object> keyExtractor)
    {
        _keyExtractor = keyExtractor;
    }

    public bool Equals(T x, T y)
    {
        return _keyExtractor(x).Equals(_keyExtractor(y));
    }

    public int GetHashCode(T obj)
    {
        var func = _keyExtractor(obj);
        if (func == null)
            return -1;

        return func.GetHashCode();
    }
}