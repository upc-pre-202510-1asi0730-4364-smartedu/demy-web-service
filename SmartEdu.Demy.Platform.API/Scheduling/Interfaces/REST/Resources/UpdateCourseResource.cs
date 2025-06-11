namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;

public record UpdateCourseResource(
    string Name, 
    string Code, 
    string Description);