using EccomerceDemo.Domain.Entities.Order;
using EccomerceDemo.Domain.Entities.OrderItem;
using EccomerceDemo.Domain.Entities.Product;
using EccomerceDemo.Domain.Entities.User;

using Microsoft.EntityFrameworkCore;

namespace EccomerceDemo.Infrastructure.Persistence;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcommerceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
