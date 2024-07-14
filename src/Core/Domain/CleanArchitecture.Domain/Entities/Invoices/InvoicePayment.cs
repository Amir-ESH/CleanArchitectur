using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities.Invoices;

public class InvoicePayment : BaseAuditableEntity<Guid>
{
    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; }
}

public class InvoicePaymentConfiguration : IEntityTypeConfiguration<InvoicePayment>
{
    public void Configure(EntityTypeBuilder<InvoicePayment> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
