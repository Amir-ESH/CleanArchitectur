using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Domain.Entities.Customers;

public class PhoneNumber : BaseAuditableEntity<Guid>
{
    [StringLength(20, ErrorMessage = "")]
    [Unicode(false)]
    [DataType(DataType.PhoneNumber, ErrorMessage = "")]
    public string Number { get; set; } = default!;

    [Range(1,3, ErrorMessage = "")]
    public PhoneType Type { get; set; } // "Mobile" or "Landline" or "Fax"

    public Guid? CustomerId { get; set; }

    public bool SendSms { get; set; }

    public bool IsDefault { get; set; }

    #region Navigational properties

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = default!;

    #endregion
}

public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
{
    public void Configure(EntityTypeBuilder<PhoneNumber> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.Property(e => e.SendSms).HasDefaultValue(false);
        builder.Property(e => e.IsDefault).HasDefaultValue(false);

        builder.Property(e => e.Type).HasDefaultValue(1);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
