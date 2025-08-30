using EccomerceDemo.Domain.Primitives;

namespace EccomerceDemo.Domain.Entities.Order.ValueObjects;

public class OrderId : ValueObject
{
    public Guid Value { get; private set; }

    private OrderId(Guid value)
    {
        Value = value;
    }

    public static OrderId CreateUnique()
    {
        return new OrderId(Guid.NewGuid());
    }

    public static OrderId Create(Guid value)
    {
        return new OrderId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
