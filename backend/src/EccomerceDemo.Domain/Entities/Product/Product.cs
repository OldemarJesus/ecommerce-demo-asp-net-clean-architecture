using EccomerceDemo.Domain.Entities.Product.ValueObjects;
using EccomerceDemo.Domain.Entities.User.ValueObjects;
using EccomerceDemo.Domain.Primitives;
using EccomerceDemo.Domain.Primitives.ValueObjects;

namespace EccomerceDemo.Domain.Entities.Product;

public sealed class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Price PriceInCents { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public UserId CreatedBy { get; private set; }
    public User.User CreatedByUser { get; }

    private Product() : base(default!) { } // For EF Core

    private Product(ProductId id, string name, string description, Price priceInCents, UserId createdBy) : base(id)
    {
        Name = name;
        Description = description;
        PriceInCents = priceInCents;
        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public static Product Create(string name, string description, Price price, UserId createdBy)
    {
        return new Product(
            ProductId.CreateUnique(),
            name,
            description,
            price,
            createdBy);
    }
}
