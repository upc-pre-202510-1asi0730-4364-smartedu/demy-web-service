namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

/// <summary>
/// Command to create a new Enrollment
/// </summary>
public record CreateEnrollmentCommand(
    int StudentId, 
    int PeriodId, 
    string WeeklyScheduleName, 
    decimal Amount, 
    string Currency, 
    string EnrollmentStatus, 
    string PaymentStatus);