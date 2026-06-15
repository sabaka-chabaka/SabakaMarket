using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabakaMarket.PaymentService.Domain.Entities;

namespace SabakaMarket.PaymentService.Infrastructure.Data.Configurations;

public class PaymentTransactionConfiguration: IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    {
        builder.ToTable("payment_transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(t => t.OrderId).IsRequired();
        builder.Property(t => t.BuyerId).IsRequired();
        builder.Property(t => t.SellerId).IsRequired();
        
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.UpdatedAt).IsRequired(false);

        builder.HasIndex(t => t.OrderId).IsUnique();
        builder.HasIndex(t => t.BuyerId);
        builder.HasIndex(t => t.SellerId);
    }
}