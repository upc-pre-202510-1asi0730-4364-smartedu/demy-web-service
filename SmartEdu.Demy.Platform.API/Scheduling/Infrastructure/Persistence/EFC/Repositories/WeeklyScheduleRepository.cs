using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SmartEdu.Demy.Platform.API.Scheduling.Infrastructure.Persistence.EFC.Repositories;

public class WeeklyScheduleRepository(AppDbContext context) 
    : BaseRepository<WeeklySchedule>(context), IWeeklyScheduleRepository
{
    public async Task<IEnumerable<WeeklySchedule>> FindByNameAsync(string name)
    {
        return await Context.Set<WeeklySchedule>()
            .Where(ws => ws.Name.Contains(name))
            .Include(ws => ws.Schedules)
            .ToListAsync();
    }

    public async Task<IEnumerable<WeeklySchedule>> FindByCourseIdAsync(int courseId)
    {
        return await Context.Set<WeeklySchedule>()
            .Include(ws => ws.Schedules)
            .Where(ws => ws.Schedules.Any(s => s.CourseId == courseId))
            .ToListAsync();
    }

    public async Task<IEnumerable<WeeklySchedule>> FindByClassroomIdAsync(int classroomId)
    {
        return await Context.Set<WeeklySchedule>()
            .Include(ws => ws.Schedules)
            .Where(ws => ws.Schedules.Any(s => s.ClassroomId == classroomId))
            .ToListAsync();
    }
    
    public async Task<WeeklySchedule?> FindWeeklyScheduleByNameAsync(string name)
    {
        return Context.Set<WeeklySchedule>().FirstOrDefault(p => p.Name == name);
    }

    /// <summary>
    /// Override FindByIdAsync to include Schedules relationship
    /// </summary>
    /// <param name="id">The WeeklySchedule ID</param>
    /// <returns>WeeklySchedule entity with Schedules included</returns>
    public async Task<WeeklySchedule?> FindByIdAsync(int id)
    {
        return await Context.Set<WeeklySchedule>()
            .Include(ws => ws.Schedules)
            .FirstOrDefaultAsync(ws => ws.Id == id);
    }

    /// <summary>
    /// Override ListAsync to include Schedules relationship
    /// </summary>
    /// <returns>All WeeklySchedule entities with their Schedules included</returns>
    public async Task<IEnumerable<WeeklySchedule>> ListAsync()
    {
        return await Context.Set<WeeklySchedule>()
            .Include(ws => ws.Schedules)
            .ToListAsync();
    }
}