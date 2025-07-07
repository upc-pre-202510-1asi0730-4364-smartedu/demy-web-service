using Microsoft.EntityFrameworkCore;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Infrastructure.Persistence.EFC.Repositories;

public class ScheduleRepository(AppDbContext context) 
    : BaseRepository<Schedule>(context), IScheduleRepository
{
    public async Task<IEnumerable<Schedule>> FindByTeacherIdAsync(long teacherId)
    {
        return await Context.Set<Schedule>()
            .Where(s => s.TeacherId == teacherId)
            .ToListAsync();
    }
}
