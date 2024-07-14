using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Domain.Entities.Customers;

public class FamilyRelationship : BaseEntity<Guid>
{
    public Guid CustomerId { get; set; } // Customer

    public Guid PersonId { get; set; } // Customer Child or Father or Mother or etc

    [Range(1, 10, ErrorMessage = "")]
    public RelationshipType Relationship { get; set; } // Relationship like customer is person Father
}

public class FamilyRelationshipConfiguration : IEntityTypeConfiguration<FamilyRelationship>
{
    public void Configure(EntityTypeBuilder<FamilyRelationship> builder)
    {
        builder.Property(e => e.Relationship).HasDefaultValue(RelationshipType.Father);
    }
}
