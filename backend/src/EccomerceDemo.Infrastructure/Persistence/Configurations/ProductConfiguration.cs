using EccomerceDemo.Domain.Entities.Product;
using EccomerceDemo.Domain.Entities.Product.ValueObjects;
using EccomerceDemo.Domain.Entities.User;

using Microsoft.EntityFrameworkCore;

namespace EccomerceDemo.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value)
            );

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.OwnsOne(p => p.PriceInCents, pp =>
        {
            pp.Property(p => p.AmountInCents).HasColumnName("PriceInCents").IsRequired();
            pp.Property(p => p.Currency).HasColumnName("PriceCurrency").IsRequired();
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasOne(p => p.CreatedByUser)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.CreatedBy);
    }
}
