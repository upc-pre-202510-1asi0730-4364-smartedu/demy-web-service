using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;

/// <summary>
/// Partial class of <see cref="UserAccount"/> adding support for automatic tracking of creation and update timestamps.
/// Implements <see cref="IEntityWithCreatedUpdatedDate"/> interface.
/// </summary>
public partial class UserAccount : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    /// Gets or sets the timestamp of when the user account was created.
    /// Automatically managed by Entity Framework.
    /// </summary>
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    /// <summary>
    /// Gets or sets the timestamp of the last update to the user account.
    /// Automatically managed by Entity Framework.
    /// </summary>
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}