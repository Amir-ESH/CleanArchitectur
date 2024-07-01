using CleanArchitecture.Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Domain.Entities;

public class Role : BaseAuditableEntity<int>
{
    public Role()
    {
        UserRoles = new List<UserRole>();
    }

    [DisplayName("Last Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string Name { get; set; } = null!;

    [DisplayName("Is Active?")]
    public bool IsActive { get; set; }

    #region Navigational properties

    public ICollection<UserRole> UserRoles { get; set; }

    #endregion
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(e => e.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.IsActive).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
