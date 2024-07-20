using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntity).Assembly;

        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        modelBuilder.AddPluralizingTableNameConvention();
        modelBuilder.AddRestrictDeleteBehaviorConvention();
    }
}
