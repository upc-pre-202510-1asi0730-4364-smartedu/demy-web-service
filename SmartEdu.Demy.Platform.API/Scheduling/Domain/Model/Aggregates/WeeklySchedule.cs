using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;

public partial class WeeklySchedule
{
    private readonly List<Schedule> _weekSchedule;
    
    public int Id { get; }
    public string Name { get; private set; }
    public IReadOnlyList<Schedule> WeekSchedule => _weekSchedule.AsReadOnly();

    public WeeklySchedule()
    {
        Name = string.Empty;
        _weekSchedule = new List<Schedule>();
    }
    
    public WeeklySchedule(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        _weekSchedule = new List<Schedule>();
    }

    public WeeklySchedule(CreateWeeklyScheduleCommand command)
    {
        Name = command.Name ?? throw new ArgumentNullException(nameof(command.Name));
        _weekSchedule = new List<Schedule>();
    }
    
    
    public void UpdateName(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
    
    
    public void AddSchedule(Schedule schedule)
    {
        if (schedule == null)
            throw new ArgumentNullException(nameof(schedule));
            
        _weekSchedule.Add(schedule);
    }
    
    
    public void RemoveSchedule(int scheduleId)
    {
        var schedule = _weekSchedule.FirstOrDefault(s => s.Id == scheduleId);
        if (schedule != null)
        {
            _weekSchedule.Remove(schedule);
        }
    }
    
    public IEnumerable<Schedule> GetSchedulesForDay(string dayOfWeek)
    {
        return _weekSchedule.Where(s => s.DayOfWeek.Equals(dayOfWeek, StringComparison.OrdinalIgnoreCase));
    }
    
}