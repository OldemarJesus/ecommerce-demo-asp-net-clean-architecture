using EccomerceDemo.Domain.Abstractions;
using EccomerceDemo.Domain.Exceptions.Errors;
using EccomerceDemo.Domain.Primitives;

namespace EccomerceDemo.Domain.Entities;

public class OrderItem : Entity
{
    public Guid ItemId { get; private set; }
    public string ItemName { get; private set; } = null!;
    public Price UnitPrice { get; private set; } = null!;
    public long Quantity { get; private set; }
    public Price TotalPrice { get; private set; } = null!;
    private OrderItem() : base() { }

    public static Result<OrderItem> TryCreate(
        Item item,
        long quantity)
    {
        if (item is null)
            return Result.Failure<OrderItem>(DomainError.Item.InvalidParameter);

        if (quantity < 0)
            return Result.Failure<OrderItem>(DomainError.Item.InvalidParameter);

        return new OrderItem
        {
            ItemId = item.Id,
            ItemName = item.Name,
            UnitPrice = item.Price,
            Quantity = quantity,
            TotalPrice = new Price(item.Price.AmountInCents * quantity, item.Price.Currency)
        };
    }

}
