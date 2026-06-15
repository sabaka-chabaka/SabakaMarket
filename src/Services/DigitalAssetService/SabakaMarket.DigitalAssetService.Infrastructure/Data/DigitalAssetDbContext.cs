using Microsoft.EntityFrameworkCore;
using SabakaMarket.DigitalAssetService.Domain.Entities;

namespace SabakaMarket.DigitalAssetService.Infrastructure.Data;

public class DigitalAssetDbContext(DbContextOptions<DigitalAssetDbContext> options) : DbContext(options)
{
    public DbSet<DigitalAsset> DigitalAssets => Set<DigitalAsset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DigitalAssetDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}