namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record UpdateAcademicPeriodResource(string PeriodName, DateTime StartDate, DateTime EndDate, bool IsActive);