using BookManagement.Core.Shared.Validations;

namespace BookManagement.Core.Domain.Entities.Books;

public readonly struct Isbn13
{
    public Isbn13(string value)
    {
        if (!Isbn13Validator.IsValid(value))
            throw new ArgumentException($"ISBN-13 {value} is invalid", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(Isbn13 value)
    {
        return value.Value;
    }

    public static implicit operator Isbn13(string value)
    {
        return new Isbn13(value);
    }

    public override string ToString()
    {
        return Value;
    }
}