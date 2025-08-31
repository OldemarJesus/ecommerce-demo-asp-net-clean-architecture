namespace EccomerceDemo.Presentation.Product.Create;

public record class ProductResponse(
    Guid ProductId,
    Guid CreatedByUserId,
    string Name,
    string Description,
    decimal Price,
    string Currency
);