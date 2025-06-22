using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;

public interface IEnrollmentRepository : IBaseRepository<Model.Aggregates.Enrollment>
{
    Task<IEnumerable<Model.Aggregates.Enrollment>> FindAllByStudentIdAsync(int studentId);

    Task<IEnumerable<Model.Aggregates.Enrollment>> FindAllByPeriodIdAsync(int periodId);

    Task<Model.Aggregates.Enrollment?> FindByStudentIdAndPeriodIdAsync(int studentId, int periodId);
}