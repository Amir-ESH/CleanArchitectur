using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Entities.Banks;
using CleanArchitecture.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Domain.Entities.Countries;

public class Country : BaseAuditableEntity<int>
{
    [StringLength(70)] // Like: الجماهیریة العربیة اللیبیة الشعبیة الاشتراکیة العظمى
    [Unicode(false)]
    public string Name { get; set; } = default!;

    [Column(TypeName = "decimal(11,8)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(11,8)")]
    public decimal? Longitude { get; set; }

    #region Navigational properties

    public virtual ICollection<Bank> Banks { get; set; } = default!;

    public virtual ICollection<Province> Provinces { get; set; } = default!;

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = default!;

    #endregion
}

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
