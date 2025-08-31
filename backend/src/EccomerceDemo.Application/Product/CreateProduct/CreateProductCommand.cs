using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Domain.Entities.User.ValueObjects;
using EccomerceDemo.Domain.Primitives.ValueObjects;

namespace EccomerceDemo.Application.Product.CreateProduct;

public record class CreateProductCommand(
    string Name,
    string Description,
    Price PriceInCents,
    UserId UserId
) : ICommand<CreateProductResponse>;