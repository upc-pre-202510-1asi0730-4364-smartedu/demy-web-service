namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record CreateAcademicPeriodResource(string PeriodName, DateTime StartDate, DateTime EndDate, bool IsActive);