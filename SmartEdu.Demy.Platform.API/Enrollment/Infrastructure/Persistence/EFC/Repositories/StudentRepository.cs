using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Infrastructure.Persistence.EFC.Repositories;

public class StudentRepository(AppDbContext context)
    : BaseRepository<Student>(context), IStudentRepository
{
    public async Task<bool> ExistsByDniAsync(string dni)
    {
        return await context.Set<Student>()
            .AnyAsync(s => s.Dni.Value == dni);
    }

    public async Task<Student?> FindByDniAsync(string dni)
    {
        return await context.Set<Student>()
            .FirstOrDefaultAsync(s => s.Dni.Value == dni);
    }
}