namespace EccomerceDemo.Domain.Primitives;

public enum OrderState
{
    Pending = 0,
    Processing = 1,
    Shipped = 2,
    Delivered = 3,
    Cancelled = 4
}
