using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Domain.Entities.Products;

public class Product : BaseAuditableEntity<long>
{
    [StringLength(255), Unicode(false)]
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal Discount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Tax { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal VAT { get; set; }

    [Column(TypeName = "decimal(18,3)")]
    public decimal Quantity { get; set; }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.Property(e => e.Price).HasDefaultValue(0.0M);
        builder.Property(e => e.Discount).HasDefaultValue(0.0M);
        builder.Property(e => e.Tax).HasDefaultValue(0.0M);
        builder.Property(e => e.VAT).HasDefaultValue(0.0M);
        builder.Property(e => e.Quantity).HasDefaultValue(0.0M);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
