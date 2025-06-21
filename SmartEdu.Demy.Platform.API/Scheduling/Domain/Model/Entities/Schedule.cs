using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects;
using DayOfWeek = SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects.DayOfWeek;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;

public class Schedule
{
    public int Id { get; private set; }
    public TimeRange TimeRange { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }
    
    // Foreign Keys
    public int CourseId { get; private set; }
    public int ClassroomId { get; private set; }
    
    // Navigation Properties
    public Course Course { get; private set; }
    public Classroom Classroom { get; private set; }
    
    // TODO: Add TeacherId when teacher functionality is implemented
    // public int TeacherId { get; private set; }
    // public Teacher Teacher { get; private set; }

    public Schedule()
    {
    }

    public Schedule(TimeRange timeRange, DayOfWeek dayOfWeek, int courseId, int classroomId)
    {
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        DayOfWeek = dayOfWeek;
        CourseId = courseId;
        ClassroomId = classroomId;
        
        ValidateSchedule();
    }

    public Schedule(string startTime, string endTime, DayOfWeek dayOfWeek, int courseId, int classroomId)
        : this(new TimeRange(startTime, endTime), dayOfWeek, courseId, classroomId)
    {
    }

    public void UpdateSchedule(TimeRange timeRange, DayOfWeek dayOfWeek, int courseId, int classroomId)
    {
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        DayOfWeek = dayOfWeek;
        CourseId = courseId;
        ClassroomId = classroomId;
        
        ValidateSchedule();
    }

    public void UpdateSchedule(string startTime, string endTime, DayOfWeek dayOfWeek, int courseId, int classroomId)
    {
        UpdateSchedule(new TimeRange(startTime, endTime), dayOfWeek, courseId, classroomId);
    }

    /// <summary>
    /// Checks if this schedule conflicts with another schedule (same day, overlapping time, same classroom)
    /// </summary>
    public bool ConflictsWith(Schedule other)
    {
        if (other == null) return false;
        
        return DayOfWeek == other.DayOfWeek && 
               ClassroomId == other.ClassroomId && 
               TimeRange.OverlapsWith(other.TimeRange);
    }

    /// <summary>
    /// Gets the duration of this schedule in minutes
    /// </summary>
    public int DurationInMinutes => TimeRange.DurationInMinutes;
    
    private void ValidateSchedule()
    {
        if (TimeRange == null)
            throw new ArgumentNullException(nameof(TimeRange));
        
        if (CourseId <= 0)
            throw new ArgumentException("CourseId must be greater than 0", nameof(CourseId));
        
        if (ClassroomId <= 0)
            throw new ArgumentException("ClassroomId must be greater than 0", nameof(ClassroomId));

        // Validate day of week is within valid range
        if (!Enum.IsDefined(typeof(DayOfWeek), DayOfWeek))
            throw new ArgumentException("Invalid day of week", nameof(DayOfWeek));
    }
}