using EccomerceDemo.Domain.Abstractions;
using EccomerceDemo.Domain.Exceptions.Errors;
using EccomerceDemo.Domain.Primitives;

namespace EccomerceDemo.Domain.Entities;

public class Order : Entity
{
    private readonly HashSet<OrderItem> _orderItems = new();
    public string OrderNumber { get; private set; } = null!;
    public Guid ClientId { get; private set; }
    public OrderState State { get; private set; }
    public Price TotalPrice { get; private set; } = null!;
    public Address ShippingAddress { get; private set; } = null!;
    public DateTime OrderDate { get; private set; }
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.ToList().AsReadOnly();
    private Order() : base() { }

    public static Result<Order> TryCreate(
        Guid id,
        string orderNumber,
        Guid clientId,
        OrderState state,
        Price totalPrice,
        Address shippingAddress,
        DateTime orderDate,
        IEnumerable<OrderItem> orderItems)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            return Result.Failure<Order>(DomainError.Order.InvalidParameter);

        if (clientId == Guid.Empty)
            return Result.Failure<Order>(DomainError.Order.InvalidParameter);

        if (totalPrice is null || totalPrice.AmountInCents < 0 || string.IsNullOrWhiteSpace(totalPrice.Currency))
            return Result.Failure<Order>(DomainError.Order.InvalidParameter);

        if (shippingAddress is null ||
            string.IsNullOrWhiteSpace(shippingAddress.Street) ||
            string.IsNullOrWhiteSpace(shippingAddress.City) ||
            string.IsNullOrWhiteSpace(shippingAddress.ZipCode))
            return Result.Failure<Order>(DomainError.Order.InvalidParameter);

        if (orderDate == default)
            return Result.Failure<Order>(DomainError.Order.InvalidParameter);

        if (orderItems is null || !orderItems.Any())
            return Result.Failure<Order>(DomainError.Order.InvalidParameter);

        var order = new Order
        {
            Id = id,
            OrderNumber = orderNumber,
            ClientId = clientId,
            State = state,
            TotalPrice = totalPrice,
            ShippingAddress = shippingAddress,
            OrderDate = orderDate
        };

        foreach (var item in orderItems)
        {
            order._orderItems.Add(item);
        }

        return order;
    }

    public static Result<Order> TryCreate(
        string orderNumber,
        Guid clientId,
        OrderState state,
        Price totalPrice,
        Address shippingAddress,
        DateTime orderDate,
        IEnumerable<OrderItem> orderItems)
    {
        return TryCreate(
            Guid.NewGuid(),
            orderNumber,
            clientId,
            state,
            totalPrice,
            shippingAddress,
            orderDate,
            orderItems);
    }
}
