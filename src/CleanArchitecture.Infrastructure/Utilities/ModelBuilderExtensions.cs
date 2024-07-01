using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pluralize.NET;

namespace CleanArchitecture.Infrastructure.Utilities;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Dynamically load all IEntityTypeConfiguration with Reflection
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var applyGenericMethod = typeof(ModelBuilder).GetMethods()
                                                     .First(m => m.Name == nameof(ModelBuilder.ApplyConfiguration));

        var types = assemblies.SelectMany(a => a.GetExportedTypes())
                              .Where(c => c is {IsClass: true, IsAbstract: false, IsPublic: true});

        foreach (var type in types)
        {
            foreach (var iFace in type.GetInterfaces())
            {
                if (iFace.IsConstructedGenericType &&
                    iFace.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                {
                    var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iFace.GenericTypeArguments[0]);
                    applyConcreteMethod.Invoke(modelBuilder, new[] {Activator.CreateInstance(type)});
                }
            }
        }
    }

    /// <summary>
    /// Set DeleteBehavior.Restrict by default for relations
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddRestrictDeleteBehaviorConvention(this ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                     .SelectMany(t => t.GetForeignKeys())
                                     .Where(fk => fk is {IsOwnership: false, DeleteBehavior: DeleteBehavior.Cascade});
        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }

    /// <summary>
    /// Dynamically register all Entities that inherit from specific BaseType
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterAllEntities<TBaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
                              .Where(c => c is {IsClass: true, IsAbstract: false, IsPublic: true} && typeof(TBaseType).IsAssignableFrom(c));
       
        foreach (var type in types)
            modelBuilder.Entity(type);
    }

    /// <summary>
    /// Pluralizing table name like Post to Posts or Person to People
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder)
    {
        var pluralizer = new Pluralizer();
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            entityType.SetTableName(pluralizer.Pluralize(tableName));
        }
    }
}
