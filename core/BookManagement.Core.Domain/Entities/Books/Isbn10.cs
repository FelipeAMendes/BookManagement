using BookManagement.Core.Shared.Validations;

namespace BookManagement.Core.Domain.Entities.Books;

public readonly struct Isbn10
{
    public Isbn10(string value)
    {
        if (!Isbn10Validator.IsValid(value))
            throw new ArgumentException($"ISBN-10 {value} is invalid", nameof(value));

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(Isbn10 value)
    {
        return value.Value;
    }

    public static implicit operator Isbn10(string value)
    {
        return new Isbn10(value);
    }

    public override string ToString()
    {
        return Value;
    }
}