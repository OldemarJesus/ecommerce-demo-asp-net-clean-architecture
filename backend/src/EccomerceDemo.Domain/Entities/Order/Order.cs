using EccomerceDemo.Domain.Entities.Order.ValueObjects;
using EccomerceDemo.Domain.Entities.User.ValueObjects;
using EccomerceDemo.Domain.Primitives;
using EccomerceDemo.Domain.Primitives.ValueObjects;
namespace EccomerceDemo.Domain.Entities.Order;

public class Order : AggregateRoot<OrderId>
{
    private readonly HashSet<OrderItem.OrderItem> _orderItems = new();
    public Price TotalPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public UserId CreatedBy { get; private set; }
    public User.User CreatedByUser { get; }
    public IReadOnlyCollection<OrderItem.OrderItem> OrderItems => _orderItems.ToList().AsReadOnly();

    private Order() : base(default!) { } // For EF Core
    private Order(OrderId id, Price totalPrice, UserId createdBy) : base(id)
    {
        TotalPrice = totalPrice;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public static Order Create(Price totalPrice, UserId createdBy)
    {
        return new Order(OrderId.CreateUnique(), totalPrice, createdBy);
    }

    public void AddOrderItem(OrderItem.OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }

    public void RemoveOrderItem(OrderItem.OrderItem orderItem)
    {
        _orderItems.Remove(orderItem);
    }
}
