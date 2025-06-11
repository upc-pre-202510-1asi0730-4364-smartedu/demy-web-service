namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;

public class Classroom
{
    public int Id { get; private set; }
    public string Code { get; private set; }
    public int Capacity { get; private set; }
    public string Campus { get; private set; }

    public Classroom()
    {
        Code = string.Empty;
        Capacity = 0;
        Campus = string.Empty;
    }
    
    public Classroom(string code, int capacity, string campus)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Capacity = capacity > 0 ? capacity : throw new ArgumentException("Capacity must be greater than 0", nameof(capacity));
        Campus = campus ?? throw new ArgumentNullException(nameof(campus));
    }
    
    public void UpdateClassroom(string code, int capacity, string campus)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Capacity = capacity > 0 ? capacity : throw new ArgumentException("Capacity must be greater than 0", nameof(capacity));
        Campus = campus ?? throw new ArgumentNullException(nameof(campus));
    }
}