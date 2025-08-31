using EccomerceDemo.Domain.Entities.Product.ValueObjects;
using EccomerceDemo.Domain.Entities.User.ValueObjects;
using EccomerceDemo.Domain.Primitives.ValueObjects;

namespace EccomerceDemo.Application.Product.CreateProduct;

public record class CreateProductResponse(
    ProductId ProductId,
    Price PriceInCents,
    UserId CreatedBy,
    string Name,
    string Description
);