using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Infrastructure.Persistence.EFC.Repositories;

public class StudentRepository(AppDbContext context) 
    : BaseRepository<Student>(context), IStudentRepository
{
    // aca se podra añadir metodos especificos para student en el futuro.
    // solo por ahora hereda completamente de BaseRepository.
}