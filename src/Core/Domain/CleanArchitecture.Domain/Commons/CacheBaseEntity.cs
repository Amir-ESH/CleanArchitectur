using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Commons;

public interface ICacheEntity
{
    byte[] RowVersion { get; set; }
}

public abstract class CacheBaseEntity : ICacheEntity
{
    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}

public interface ICacheEntity<TKey> : ICacheEntity
{
    TKey Id { get; set; }
}

public abstract class CacheBaseEntity<TKey> : ICacheEntity<TKey>
{
    public TKey Id { get; set; } = default!;

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}
