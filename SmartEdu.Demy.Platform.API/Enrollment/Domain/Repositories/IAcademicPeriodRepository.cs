using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;

public interface IAcademicPeriodRepository : IBaseRepository<AcademicPeriod>
{
    Task<bool> ExistsByPeriodNameAsync(string periodName);

    Task<AcademicPeriod?> FindByPeriodNameAsync(string periodName);

    Task<bool> ExistsByPeriodNameAndIdIsNotAsync(string periodName, int id);
}
