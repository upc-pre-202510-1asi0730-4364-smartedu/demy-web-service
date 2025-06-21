using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Infrastructure.Persistence.EFC.Repositories;

public class ClassroomRepository(AppDbContext context) 
    : BaseRepository<Classroom>(context), IClassroomRepository
{
    /// <inheritdoc />
    public async Task<Classroom?> FindClassroomByCodeAsync(string code)
    {
        return await Context.Set<Classroom>().FirstOrDefaultAsync(c => c.Code == code);
    }
    
}