using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabakaMarket.DigitalAssetService.Domain.Entities;

namespace SabakaMarket.DigitalAssetService.Infrastructure.Data.Configurations;

public class DigitalAssetConfiguration : IEntityTypeConfiguration<DigitalAsset>
{
    public void Configure(EntityTypeBuilder<DigitalAsset> builder)
    {
        builder.ToTable("digital_assets");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(a => a.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.OrderId)
            .IsRequired(false);

        builder.HasIndex(a => a.ProductId);
        builder.HasIndex(a => a.SellerId);
        builder.HasIndex(a => a.OrderId);
    }
}