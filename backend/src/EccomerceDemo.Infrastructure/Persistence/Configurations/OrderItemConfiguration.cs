using EccomerceDemo.Domain.Entities.OrderItem;
using EccomerceDemo.Domain.Entities.OrderItem.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace EccomerceDemo.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(oi => oi.Id);
        builder.Property(oi => oi.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => OrderItemId.Create(value)
            );

        builder.Property(oi => oi.Quantity)
            .IsRequired();

        builder.OwnsOne(oi => oi.PriceInCents, oip =>
        {
            oip.Property(p => p.AmountInCents).HasColumnName("PriceInCents").IsRequired();
            oip.Property(p => p.Currency).HasColumnName("PriceCurrency").IsRequired();
        });

        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
    }
}
