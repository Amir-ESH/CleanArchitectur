using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Application.Common.DTO;

public class BaseDto<TType>
{
    /// <summary>
    /// Entity ID
    /// </summary>
    // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 
    public TType? Id { get; set; }
#pragma warning restore CS8618

    /// <summary>
    /// Created Date Time
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public DateTimeOffset? Created { get; set; }

    /// <summary>
    /// Entity Created By Who?
    /// </summary>
    [DisplayName("Created By?")]
    [StringLength(50, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Unicode(false)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string? CreatedBy { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public DateTimeOffset? LastModified { get; set; }

    /// <summary>
    /// Entity Changed By Who?
    /// </summary>
    [DisplayName("Last Modified By?")]
    [StringLength(50, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Unicode(false)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastModifiedBy { get; set; }

    [DisplayName("Is Deleted?")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool? IsDeleted { get; set; } = false;

    /// <summary>
    /// This Is Entity Row Version Time Stamp
    /// </summary>
    [Timestamp]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public byte[] RowVersion { get; set; } = null!;
}
