using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    /// <summary>
    /// Obtiene una lista de horarios asignados a un profesor específico.
    /// </summary>
    /// <param name="teacherId">Identificador único del profesor.</param>
    /// <returns>Lista de entidades Schedule asociadas al profesor especificado.</returns>
    Task<IEnumerable<Schedule>> FindByTeacherIdAsync(long teacherId);
}