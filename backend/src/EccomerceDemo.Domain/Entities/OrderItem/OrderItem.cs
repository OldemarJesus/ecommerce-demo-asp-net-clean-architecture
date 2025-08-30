using EccomerceDemo.Domain.Entities.Order.ValueObjects;
using EccomerceDemo.Domain.Entities.OrderItem.ValueObjects;
using EccomerceDemo.Domain.Entities.Product.ValueObjects;
using EccomerceDemo.Domain.Primitives;
using EccomerceDemo.Domain.Primitives.ValueObjects;

namespace EccomerceDemo.Domain.Entities.OrderItem;

public class OrderItem : AggregateRoot<OrderItemId>
{
    public long Quantity { get; private set; }
    public Price TotalPriceInCents { get; }
    public Price PriceInCents { get; private set; }
    public ProductId ProductId { get; private set; }
    public OrderId OrderId { get; private set; }
    public Order.Order Order { get; }
    public Product.Product Product { get; }

    private OrderItem() : base(default!) { } // For EF Core

    private OrderItem(OrderItemId id, Price priceInCents, long quantity, ProductId productId, OrderId orderId) : base(id)
    {
        PriceInCents = priceInCents;
        Quantity = quantity;
        ProductId = productId;
        TotalPriceInCents = Price.Create(priceInCents.AmountInCents * quantity, priceInCents.Currency);
        OrderId = orderId;
    }

    public static OrderItem Create(long priceInCents, string currency, long quantity, ProductId productId, OrderId orderId)
    {
        return new OrderItem(OrderItemId.CreateUnique(), Price.Create(priceInCents, currency), quantity, productId, orderId);
    }
}
