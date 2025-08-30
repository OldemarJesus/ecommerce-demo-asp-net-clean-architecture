using EccomerceDemo.Domain.Entities.Order;
using EccomerceDemo.Domain.Entities.Order.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace EccomerceDemo.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value)
            );

        builder.Property(o => o.CreatedAt)
            .IsRequired();

        builder.OwnsOne(o => o.TotalPrice, tp =>
        {
            tp.Property(p => p.AmountInCents).HasColumnName("TotalPriceInCents").IsRequired();
            tp.Property(p => p.Currency).HasColumnName("TotalPriceCurrency").IsRequired();
        });

        builder.HasOne(o => o.CreatedByUser)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.CreatedBy);

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);
    }
}
