using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;

public interface IStudentRepository : IBaseRepository<Student>
{
    Task<bool> ExistsByDniAsync(string dni);

    Task<Student?> FindByDniAsync(string dni);
}