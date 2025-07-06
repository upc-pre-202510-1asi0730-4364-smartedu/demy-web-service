namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;

public record CreateEnrollmentResource(int StudentId, int AcademicPeriodId, string WeeklyScheduleName, decimal Amount, string Currency, string EnrollmentStatus, string PaymentStatus);