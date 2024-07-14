using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Domain.CacheEntities.Products;

public class Product : CacheBaseAuditableEntity<long>
{
    //public long Id { get; set; }

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

    ///// <summary>
    ///// Created Date Time
    ///// </summary>
    //public DateTimeOffset CreatedAt { get; set; }

    ///// <summary>
    ///// Entity Created By Who?
    ///// </summary>
    //[DisplayName("Created By?")]
    //[StringLength(50, ErrorMessage = "{0} can't be a more then {1} characters.")]
    //[Unicode(false)]
    //public string? CreatedBy { get; set; }

    //public DateTimeOffset? LastModifiedAt { get; set; }

    ///// <summary>
    ///// Entity Changed By Who?
    ///// </summary>
    //[DisplayName("Last Modified By?")]
    //[StringLength(50, ErrorMessage = "{0} can't be a more then {1} characters.")]
    //[Unicode(false)]
    //public string? LastModifiedBy { get; set; }

    //[DisplayName("Is Deleted?")]
    //public bool IsDeleted { get; set; }

    //[Timestamp]
    //public byte[] RowVersion { get; set; } = null!;
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasNoKey();

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
