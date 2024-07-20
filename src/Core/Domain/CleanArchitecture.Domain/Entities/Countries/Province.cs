using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Domain.Entities.Countries;

public class Province : BaseAuditableEntity<long>
{
    [StringLength(170)]
    [Unicode(false)]
    public string Name { get; set; } = default!;

    public int CountryId { get; set; }

    [Column(TypeName = "decimal(11,8)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(11,8)")]
    public decimal? Longitude { get; set; }

    #region Navigational properties

    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = default!;

    public virtual ICollection<City> Cities { get; set; } = default!;

    #endregion
}

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
