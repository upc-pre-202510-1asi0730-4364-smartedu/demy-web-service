namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

public record CreateCourseResource(
    string Name, 
    string Code, 
    string Description);
