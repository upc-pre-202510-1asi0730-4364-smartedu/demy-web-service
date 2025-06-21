namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

    /// <summary>
    /// Command para crear un nuevo Enrollment
    /// </summary>
public record CreateEnrollmentCommand(int StudentId, int PeriodId, decimal Amount, string Currency, string EnrollmentStatus, string PaymentStatus);

