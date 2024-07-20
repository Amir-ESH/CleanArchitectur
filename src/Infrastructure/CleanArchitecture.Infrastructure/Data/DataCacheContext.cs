using CleanArchitecture.Domain.CacheEntities.Products;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data;

public class DataCacheContext : DbContext
{
    public DataCacheContext(DbContextOptions<DataCacheContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
                    .HasNoKey();

        //var entitiesAssembly = typeof(ICacheEntity).Assembly;

        //modelBuilder.RegisterAllEntities<ICacheEntity>(entitiesAssembly);
        //modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        //modelBuilder.AddPluralizingTableNameConvention();
        //modelBuilder.AddRestrictDeleteBehaviorConvention();
    }
}
