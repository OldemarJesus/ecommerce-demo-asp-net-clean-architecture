using EccomerceDemo.Domain.Entities.Product.ValueObjects;

namespace EccomerceDemo.Application.Product.Interfaces;

public interface IProductRepository
{
    Task<Domain.Entities.Product.Product?> GetByIdAsync(ProductId productId);
    Task AddAsync(Domain.Entities.Product.Product product);
    Task UpdateAsync(Domain.Entities.Product.Product product);
    Task DeleteAsync(ProductId productId);
}
