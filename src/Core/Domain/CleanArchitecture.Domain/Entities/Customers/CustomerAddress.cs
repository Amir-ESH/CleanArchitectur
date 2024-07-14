using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Entities.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Domain.Entities.Customers;

public class CustomerAddress : BaseAuditableEntity<Guid>
{
    [StringLength(40, ErrorMessage = "")]
    [Length(3, 40, ErrorMessage = "")]
    [Unicode(false)]
    public string Title { get; set; } = default!;

    [StringLength(255, ErrorMessage = "")]
    [Length(10, 255, ErrorMessage = "")]
    [Unicode(false)]
    public string Address { get; set; } = default!;

    public long CityId { get; set; }

    public long ProvinceId { get; set; }

    public int CountryId { get; set; }

    [StringLength(20, ErrorMessage = "")]
    [Length(5, 20, ErrorMessage = "")]
    [Unicode(false)]
    public string ZipCode { get; set; } = default!;

    [Column(TypeName = "decimal(11,8)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(11,8)")]
    public decimal? Longitude { get; set; }

    public bool IsDefault { get; set; }

    public Guid CustomerId { get; set; }

    #region Navigational properties

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = default!;

    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = default!;

    [ForeignKey(nameof(ProvinceId))]
    public virtual Province Province { get; set; } = default!;

    [ForeignKey(nameof(CityId))]
    public virtual City City { get; set; } = default!;

    #endregion
}

public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.IsDefault).HasDefaultValue(false);

        builder.Property(e => e.Address).IsRequired();
        builder.Property(e => e.CityId).IsRequired();
        builder.Property(e => e.ProvinceId).IsRequired();
        builder.Property(e => e.CountryId).IsRequired();
        builder.Property(e => e.CustomerId).IsRequired();

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
