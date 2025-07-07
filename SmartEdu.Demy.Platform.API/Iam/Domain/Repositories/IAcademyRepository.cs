using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;


/// <summary>
/// Repository interface for managing <see cref="Academy"/> aggregate operations.
/// </summary>
public interface IAcademyRepository : IBaseRepository<Academy>
{
    /// <summary>
    /// Checks if an academy exists with the specified RUC.
    /// </summary>
    /// <param name="ruc">The RUC (Registro Único de Contribuyente) to check.</param>
    /// <returns>True if an academy exists with the given RUC; otherwise, false.</returns>
    Task<bool> ExistsByRucAsync(string ruc);
    
    
    /// <summary>
    /// Checks if an academy exists that is associated with the specified user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to check for associated academy.</param>
    /// <returns>True if an academy exists for the user; otherwise, false.</returns>
    Task<bool> ExistsByUserIdAsync(long userId);
    
    /// <summary>
    /// Checks if an academy exists with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the academy to check.</param>
    /// <returns>True if the academy exists; otherwise, false.</returns>
    
    Task<bool> ExistsByIdAsync(long id);

}