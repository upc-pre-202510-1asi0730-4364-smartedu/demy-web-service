namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

public record UpdateAcademicPeriodCommand(
    int     Id,
    string  PeriodName,
    DateTime StartDate,
    DateTime EndDate,
    bool    IsActive
);