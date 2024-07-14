using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Domain.Entities.Wallets;

public class WalletTransaction : BaseAuditableEntity<Guid>
{
    [Range(-999_999_999_999.999, 999_999_999_999.999, ErrorMessage = "")]
    public decimal Transaction { get; set; }

    public Guid WalletId { get; set; }

    [StringLength(255), Unicode(false)]
    public string? Description { get; set; }

    #region Navigational properties

    [ForeignKey(nameof(WalletId))]
    public virtual Wallet Wallet { get; set; } = default!;

    #endregion
}

public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
