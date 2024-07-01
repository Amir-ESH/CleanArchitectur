using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Commons;

public interface IEntity
{
    byte[] RowVersion { get; set; }
}

public abstract class BaseEntity : IEntity
{
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
}

public abstract class BaseEntity<TKey> : IEntity<TKey>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey Id { get; set; } = default!;

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
