using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Entities.Wallets;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities.Customers;

public class Customer : BaseAuditableEntity<Guid>
{
    [StringLength(255)]
    [Unicode(false)]
    public string Username { get; set; } = default!;

    [StringLength(300)]
    public string Password { get; set; } = default!;

    [StringLength(255)]
    [Unicode(false)]
    public string FirstName { get; set; } = default!;

    [StringLength(255)]
    [Unicode(false)]
    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public bool IsActive { get; set; }

    public Gender Gender { get; set; }

    public DateTimeOffset? DeathDate { get; set; }

    [DisplayName("Private Key")]
    [StringLength(64, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Unicode(false)]
    public string PrivateKey { get; set; } = null!;

    [DisplayName("Public Key")]
    [StringLength(64, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Unicode(false)]
    public string PublicKey { get; set; } = null!;

    #region Navigational properties

    public virtual Wallet Wallet { get; set; } = default!;
    
    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = default!;
    
    public virtual ICollection<CustomerAddress> Addresses { get; set; } = default!;
    
    public virtual ICollection<BankAccount> BankAccounts { get; set; } = default!;
    
    #endregion
}

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");
        builder.Property(e => e.CreatedBy).HasDefaultValue("Created By System");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.IsActive).HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
