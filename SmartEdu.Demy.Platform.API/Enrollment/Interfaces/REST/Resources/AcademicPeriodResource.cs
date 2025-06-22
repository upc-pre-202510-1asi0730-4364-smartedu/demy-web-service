namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record AcademicPeriodResource(int Id, string PeriodName, DateTime StartDate, DateTime EndDate, bool IsActive);