namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;

public class Course
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    public Course()
    {
        Name = string.Empty;
        Code = string.Empty;
        Description = string.Empty;
    }
    
    public Course(string name, string code, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? string.Empty;
    }
    
    public void UpdateCourse(string name, string code, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Description = description ?? string.Empty;
    }
}