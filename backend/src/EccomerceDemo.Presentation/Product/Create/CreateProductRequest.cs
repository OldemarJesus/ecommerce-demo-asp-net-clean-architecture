namespace EccomerceDemo.Presentation.Product.Create;

public record class CreateProductRequest(string Name, string Description, decimal Price, string Currency);