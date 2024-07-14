using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities.Invoices;

public class Invoice : BaseAuditableEntity<Guid>
{
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Reduction { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal Tax { get; set; }

    [Column(TypeName = "decimal(3,2)")]
    public decimal VAT { get; set; }
}

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.Property(e => e.Amount).HasDefaultValue(0.0M);
        builder.Property(e => e.Reduction).HasDefaultValue(0.0M);
        builder.Property(e => e.Tax).HasDefaultValue(0.0M);
        builder.Property(e => e.VAT).HasDefaultValue(0.0M);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
