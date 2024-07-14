using System.ComponentModel.DataAnnotations.Schema;
using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities.Roles;

namespace CleanArchitecture.Domain.Entities.Users;

public class UserRole : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public int RoleId { get; set; }

    #region Navigational properties

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = default!;

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = default!;

    #endregion
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {

    }
}
