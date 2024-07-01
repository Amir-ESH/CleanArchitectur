using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Domain.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Domain.Entities;

public class User : BaseAuditableEntity<Guid>
{
    public User()
    {
        UserRoles = new List<UserRole>();
    }

    [DisplayName("First Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string FirstName { get; set; } = null!;

    [DisplayName("Last Name")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string LastName { get; set; } = null!;

    [DisplayName("Email Address")]
    [StringLength(40, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "{0} Is not valid of data Type.")]
    public string Email { get; set; } = null!;

    [DisplayName("Password")]
    [StringLength(300, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string Password { get; set; } = null!;

    [DisplayName("Private Key")]
    [StringLength(64, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string PrivateKey { get; set; } = null!;

    [DisplayName("Public Key")]
    [StringLength(64, ErrorMessage = "{0} can't be a more then {1} characters.")]
    public string PublicKey { get; set; } = null!;

    [DisplayName("Is Active?")]
    public bool IsActive { get; set; }

    #region Navigational properties

    public ICollection<UserRole> UserRoles { get; set; }

    #endregion
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.IsActive).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
