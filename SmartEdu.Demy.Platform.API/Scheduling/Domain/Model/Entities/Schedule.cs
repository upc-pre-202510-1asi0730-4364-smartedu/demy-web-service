using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;

public class Schedule
{
    public int Id { get; private set; }
    public string DayOfWeek { get; private set; }
    public TimeRange TimeRange { get; private set; }
    public Course Course { get; private set; }
    public Classroom Classroom { get; private set; }
    
    //public Teacher Teacher { get; private set; }

    public Schedule()
    {
        DayOfWeek = string.Empty;
        TimeRange = new TimeRange();
        Course = new Course();
        Classroom = new Classroom();
        //Teacher = new Teacher();
    }
    
    public Schedule(string dayOfWeek, TimeRange timeRange, Course course, Classroom classroom) //Teacher teacher
    {
        DayOfWeek = dayOfWeek ?? throw new ArgumentNullException(nameof(dayOfWeek));
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        Course = course ?? throw new ArgumentNullException(nameof(course));
        Classroom = classroom ?? throw new ArgumentNullException(nameof(classroom));
        //Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
    }
    
    
    public void UpdateSchedule(string dayOfWeek, TimeRange timeRange, Course course, Classroom classroom) //Teacher teacher
    {
        DayOfWeek = dayOfWeek ?? throw new ArgumentNullException(nameof(dayOfWeek));
        TimeRange = timeRange ?? throw new ArgumentNullException(nameof(timeRange));
        Course = course ?? throw new ArgumentNullException(nameof(course));
        Classroom = classroom ?? throw new ArgumentNullException(nameof(classroom));
        //Teacher = teacher ?? throw new ArgumentNullException(nameof(teacher));
    }
}