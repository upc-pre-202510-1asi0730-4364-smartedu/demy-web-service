using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects;
using DayOfWeek = SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects.DayOfWeek;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;

/// <summary>
/// WeeklySchedule es un agregado root que representa un horario semanal.
/// Es responsable de mantener la consistencia de los horarios y asegurar que no haya conflictos.
/// </summary>
public partial class WeeklySchedule
{
    private readonly List<Schedule> _schedules;
    
    public int Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyList<Schedule> Schedules => _schedules.AsReadOnly();

    // Parameterless constructor for EF Core
    protected WeeklySchedule()
    {
        Name = string.Empty;
        _schedules = new List<Schedule>();
    }
    
    public WeeklySchedule(CreateWeeklyScheduleCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
            
        Name = command.Name;
        _schedules = new List<Schedule>();
        
        ValidateWeeklySchedule();
    }
    
    public void UpdateName(UpdateWeeklyScheduleNameCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        if (command.WeeklyScheduleId != Id)
            throw new InvalidOperationException("Command WeeklyScheduleId does not match this schedule");

        Name = command.NewName;
        ValidateWeeklySchedule();
    }
    
    private void AddSchedule(Schedule schedule)
    {
        if (schedule == null)
            throw new ArgumentNullException(nameof(schedule));
            
        // Check for conflicts with existing schedules
        if (HasConflictWith(schedule))
            throw new InvalidOperationException($"Schedule conflicts with existing schedule in the same classroom and time slot");
            
        _schedules.Add(schedule);
        
        // Validate aggregate invariants after adding schedule
        ValidateWeeklySchedule();
    }
    
    private void AddSchedule(TimeRange timeRange, DayOfWeek dayOfWeek, int courseId, int classroomId)
    {
        var schedule = new Schedule(timeRange, dayOfWeek, courseId, classroomId);
        AddSchedule(schedule);
    }
    
    public void AddScheduleFromCommand(AddScheduleToWeeklyCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        if (command.WeeklyScheduleId != Id)
            throw new InvalidOperationException("Command WeeklyScheduleId does not match this schedule");

        if (!Enum.TryParse<DayOfWeek>(command.DayOfWeek, true, out var dayOfWeek))
            throw new ArgumentException($"Invalid day of week: {command.DayOfWeek}", nameof(command.DayOfWeek));

        var timeRange = new TimeRange(command.StartTime, command.EndTime);

        AddSchedule(timeRange, dayOfWeek, command.CourseId, command.ClassroomId);
    }
    
    public void RemoveSchedule(RemoveScheduleFromWeeklyCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        if (command.WeeklyScheduleId != Id)
            throw new InvalidOperationException("Command WeeklyScheduleId does not match this schedule");

        var schedule = _schedules.FirstOrDefault(s => s.Id == command.ScheduleId);
        if (schedule == null)
            throw new InvalidOperationException($"Schedule with ID {command.ScheduleId} not found");
            
        _schedules.Remove(schedule);
    }
    
    public IEnumerable<Schedule> GetSchedulesForTeacherId(int teacherId)
    {
        // TODO: Implement when teacher functionality is added
        // This will need to be implemented when we add the teacher relationship
        // For now, return empty list
        return Enumerable.Empty<Schedule>();
    }
    
    public bool HasConflicts()
    {
        for (int i = 0; i < _schedules.Count; i++)
        {
            for (int j = i + 1; j < _schedules.Count; j++)
            {
                if (_schedules[i].ConflictsWith(_schedules[j]))
                    return true;
            }
        }
        return false;
    }
    
    public IEnumerable<(Schedule Schedule1, Schedule Schedule2)> GetConflicts()
    {
        var conflicts = new List<(Schedule, Schedule)>();
        
        for (int i = 0; i < _schedules.Count; i++)
        {
            for (int j = i + 1; j < _schedules.Count; j++)
            {
                if (_schedules[i].ConflictsWith(_schedules[j]))
                    conflicts.Add((_schedules[i], _schedules[j]));
            }
        }
        
        return conflicts;
    }
    
    public decimal GetTotalWeeklyHours()
    {
        var totalMinutes = _schedules.Sum(s => s.DurationInMinutes);
        return totalMinutes / 60.0m;
    }
    
    private bool HasConflictWith(Schedule newSchedule)
    {
        return _schedules.Any(existingSchedule => existingSchedule.ConflictsWith(newSchedule));
    }
    
    private void ValidateWeeklySchedule()
    {
        if (string.IsNullOrEmpty(Name))
            throw new ArgumentException("Weekly schedule name cannot be empty", nameof(Name));
            
        // Validate that there are no conflicts in the schedule
        if (HasConflicts())
            throw new InvalidOperationException("Weekly schedule contains conflicting schedules");
            
        // Validate that total weekly hours don't exceed maximum (e.g., 40 hours)
        if (GetTotalWeeklyHours() > 40)
            throw new InvalidOperationException("Weekly schedule exceeds maximum allowed hours (40)");
    }
}