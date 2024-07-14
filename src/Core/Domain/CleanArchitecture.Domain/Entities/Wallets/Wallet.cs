using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities.Customers;

namespace CleanArchitecture.Domain.Entities.Wallets;

public class Wallet : BaseAuditableEntity<Guid>
{
    [Range(-999_999_999_999.999, 999_999_999_999.999, ErrorMessage = "")]
    public decimal Balance { get; set; }

    public Guid CustomerId { get; set; }

    #region Navigational properties

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = default!;

    public ICollection<WalletTransaction> WalletTransactions { get; set; } = default!;

    #endregion
}

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.Property(e => e.Balance).HasDefaultValue(0.000M);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
