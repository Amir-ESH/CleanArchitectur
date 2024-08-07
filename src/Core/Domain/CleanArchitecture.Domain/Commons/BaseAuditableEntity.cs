﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Domain.Commons;

public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>
{
    /// <summary>
    /// Created Date Time
    /// </summary>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Entity Created By Who?
    /// </summary>
    [DisplayName("Created By?")]
    [StringLength(50, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    public DateTimeOffset? LastModified { get; set; }

    /// <summary>
    /// Entity Changed By Who?
    /// </summary>
    [DisplayName("Last Modified By?")]
    [StringLength(50, ErrorMessage = "{0} can't be a more then {1} characters.")]
    [Unicode(false)]
    public string? LastModifiedBy { get; set; }

    [DisplayName("Is Deleted?")]
    public bool IsDeleted { get; set; } = false;
}