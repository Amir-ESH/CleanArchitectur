using CleanArchitecture.Domain.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.Domain.Entities.Roles;

public class Role : BaseAuditableEntity<int>
{
    [DisplayName("Last Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string Name { get; set; } = null!;

    [DisplayName("Is Active?")]
    public bool IsActive { get; set; }

    #region Navigational properties

    public ICollection<UserRole> UserRoles { get; set; } = default!;

    #endregion
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.IsActive).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
