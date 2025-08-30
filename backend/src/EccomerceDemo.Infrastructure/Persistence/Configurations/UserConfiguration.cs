using EccomerceDemo.Domain.Entities.Product;
using EccomerceDemo.Domain.Entities.Product.ValueObjects;
using EccomerceDemo.Domain.Entities.User;
using EccomerceDemo.Domain.Entities.User.ValueObjects;

using Microsoft.EntityFrameworkCore;

namespace EccomerceDemo.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value)
            );

        builder.Property(u => u.FullName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Password)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.HasMany(u => u.Products)
            .WithOne(p => p.CreatedByUser)
            .HasForeignKey(p => p.CreatedBy);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.CreatedByUser)
            .HasForeignKey(o => o.CreatedBy);
    }
}
