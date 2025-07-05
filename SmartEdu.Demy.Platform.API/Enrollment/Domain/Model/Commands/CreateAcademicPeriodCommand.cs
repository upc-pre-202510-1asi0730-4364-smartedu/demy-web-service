namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

public record CreateAcademicPeriodCommand(string PeriodName, DateTime StartDate, DateTime EndDate, bool IsActive);