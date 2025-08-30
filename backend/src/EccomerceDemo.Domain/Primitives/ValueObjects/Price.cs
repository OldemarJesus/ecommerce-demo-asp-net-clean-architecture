namespace EccomerceDemo.Domain.Primitives.ValueObjects;

public class Price : ValueObject
{
    public long AmountInCents { get; private set; }
    public string Currency { get; private set; }

    private Price(long amountInCents, string currency)
    {
        AmountInCents = amountInCents;
        Currency = currency;
    }

    public static Price Create(long amountInCents, string currency)
    {
        return new Price(amountInCents, currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return AmountInCents;
        yield return Currency;
    }
}
