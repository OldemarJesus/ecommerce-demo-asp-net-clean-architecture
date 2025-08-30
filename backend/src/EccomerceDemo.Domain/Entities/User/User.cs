using EccomerceDemo.Domain.Entities.User.ValueObjects;
using EccomerceDemo.Domain.Primitives;

namespace EccomerceDemo.Domain.Entities.User;

public sealed class User : AggregateRoot<UserId>
{
    private readonly HashSet<Product.Product> _products = new();
    private readonly HashSet<Order.Order> _orders = new();

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyCollection<Product.Product> Products => _products.ToList().AsReadOnly();
    public IReadOnlyCollection<Order.Order> Orders => _orders.ToList().AsReadOnly();

    private User(UserId id, string fullName, string email, string password) : base(id)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
    }

    public static User Create(string fullName, string email, string password)
    {
        return new User(
            UserId.CreateUnique(),
            fullName,
            email,
            password);
    }
}
