using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Entities.Banks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Domain.Entities.Customers;

public class BankAccount : BaseAuditableEntity<Guid>
{
    [StringLength(255)]
    [Unicode(false)]
    public string AccountNumber { get; set; } = default!;

    public Guid BankId { get; set; }

    public Guid CustomerId { get; set; }

    #region Navigational properties

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = default!;

    [ForeignKey(nameof(BankId))]
    public virtual Bank Bank { get; set; } = default!;

    #endregion
}

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
