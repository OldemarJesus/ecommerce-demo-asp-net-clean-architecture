
using EccomerceDemo.Application.Product.Interfaces;
using EccomerceDemo.Domain.Entities.Product;
using EccomerceDemo.Domain.Entities.Product.ValueObjects;
using EccomerceDemo.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace EccomerceDemo.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly EcommerceDbContext _dbContext;

    public ProductRepository(EcommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProductId productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        if (product != null)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }

    public Task<Product?> GetByIdAsync(ProductId productId)
    {
        return _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public Task UpdateAsync(Product product)
    {
        _dbContext.Products.Update(product);
        return _dbContext.SaveChangesAsync();
    }
}
