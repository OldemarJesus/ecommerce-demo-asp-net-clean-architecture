using EccomerceDemo.Domain.Abstractions;
using EccomerceDemo.Domain.Exceptions.Errors;
using EccomerceDemo.Domain.Primitives;

namespace EccomerceDemo.Domain.Entities;

public class Item : Entity
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public Price Price { get; private set; } = null!;
    public string Sku { get; private set; } = null!;
    public long StockQuantity { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private Item() : base() { }

    public static Result<Item> TryCreate(
        Guid id,
        string name,
        string description,
        Price price,
        string sku,
        long stockQuantity,
        bool isAvailable,
        DateTime createdAt)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Item>(DomainError.Item.InvalidParameter);

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Item>(DomainError.Item.InvalidParameter);

        if (price is null || price.AmountInCents < 0 || string.IsNullOrWhiteSpace(price.Currency))
            return Result.Failure<Item>(DomainError.Item.InvalidParameter);

        if (string.IsNullOrWhiteSpace(sku))
            return Result.Failure<Item>(DomainError.Item.InvalidParameter);

        if (stockQuantity < 0)
            return Result.Failure<Item>(DomainError.Item.InvalidParameter);

        if (createdAt == default)
            return Result.Failure<Item>(DomainError.Item.InvalidParameter);

        return new Item
        {
            Id = id,
            Name = name,
            Description = description,
            Price = price,
            Sku = sku,
            StockQuantity = stockQuantity,
            IsAvailable = isAvailable,
            CreatedAt = createdAt
        };
    }

    public static Result<Item> TryCreate(
        string name,
        string description,
        Price price,
        string sku,
        long stockQuantity,
        bool isAvailable,
        DateTime createdAt)
    {
        return TryCreate(
            Guid.NewGuid(),
            name,
            description,
            price,
            sku,
            stockQuantity,
            isAvailable,
            createdAt);
    }
}
