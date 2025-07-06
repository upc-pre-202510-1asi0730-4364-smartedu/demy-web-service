using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;

public interface IClassSessionRepository : IBaseRepository<ClassSession>
{
    /// <summary>
    ///     Verifica si existe una sesión de clase en una fecha para un curso.
    /// </summary>
    /// <param name="courseId">ID del curso</param>
    /// <param name="date">Fecha de la sesión</param>
    /// <returns>True si existe</returns>
    Task<ClassSession> FindByCourseAndDateAsync(long courseId, DateOnly date); 
}