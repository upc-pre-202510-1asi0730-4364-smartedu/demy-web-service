using SmartEdu.Demy.Platform.API.Iam.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Iam.Domain.Repositories;

public interface IAcademyRepository : IBaseRepository<Academy>
{
    Task<bool> ExistsByRucAsync(string ruc);
    Task<bool> ExistsByUserIdAsync(long userId);
    
    Task<bool> ExistsByIdAsync(long id);

}