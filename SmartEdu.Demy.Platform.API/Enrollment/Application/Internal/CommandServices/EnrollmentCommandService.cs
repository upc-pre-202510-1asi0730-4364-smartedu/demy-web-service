using SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.OutboundServices.ACL;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.ValueObjects;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.CommandServices;

/// <summary>
/// Service that handles commands to create, update, and delete enrollments.
/// </summary>
/// <remarks>Paul Sulca</remarks>
public class EnrollmentCommandService(
    IEnrollmentRepository enrollmentRepository, 
    ExternalSchedulingService externalSchedulingService,
    IUnitOfWork unitOfWork)
    : IEnrollmentCommandService
{
    /// <summary>
    /// Handles creating a new enrollment.
    /// </summary>
    /// <param name="command">Command with enrollment data</param>
    /// <returns>The created Enrollment, or null if an error occurs</returns>
    public async Task<Domain.Model.Aggregates.Enrollment?> Handle(CreateEnrollmentCommand command)
    {
        var studentId = command.StudentId; 
        var academicPeriodId = command.PeriodId;
        var weeklyScheduleId = await externalSchedulingService.FetchWeeklyScheduleIdByName(command.WeeklyScheduleName);
        if (weeklyScheduleId <= 0)
        {
            throw new Exception($"WeeklySchedule with name '{command.WeeklyScheduleName}' is invalid.");
        }
        var amount = command.Amount; 
        var currency = command.Currency;
        var enrollmentStatus = Enum.Parse<EEnrollmentStatus>(command.EnrollmentStatus, ignoreCase: true);
        var paymentStatus = Enum.Parse<EPaymentStatus>(command.PaymentStatus, ignoreCase: true);

        if (amount <= 0)
        {
            throw new Exception($"Amount of {amount} is invalid.");
        }
        
        var enrollment = new Domain.Model.Aggregates.Enrollment(
            studentId,
            academicPeriodId,
            weeklyScheduleId,
            amount,
            currency,
            enrollmentStatus,
            paymentStatus
        );
        try
        {
            await enrollmentRepository.AddAsync(enrollment);
            await unitOfWork.CompleteAsync();
            return enrollment;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to add enrollment", e);
        }
    }

    /// <summary>
    /// Handles updating an existing enrollment.
    /// </summary>
    /// <param name="command">Command with updated enrollment data</param>
    /// <returns>The updated Enrollment, or null if not found or on error</returns>
    public async Task<Domain.Model.Aggregates.Enrollment?> Handle(UpdateEnrollmentCommand command)
    {
        var enrollment = await enrollmentRepository.FindByIdAsync(command.EnrollmentId);
        if (enrollment == null) return null;

        try
        {
            enrollment.UpdateInformation(
                command.Amount,
                command.Currency,
                command.EnrollmentStatus,
                command.PaymentStatus
            );
            enrollmentRepository.Update(enrollment);
            await unitOfWork.CompleteAsync();
            return enrollment;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to update enrollment", e);
        }
    }

    /// <summary>
    /// Handles deleting an enrollment.
    /// </summary>
    /// <param name="command">Command specifying the enrollment to delete</param>
    /// <returns>True if deleted successfully; false otherwise</returns>
    public async Task<bool> Handle(DeleteEnrollmentCommand command)
    {
        var enrollment = await enrollmentRepository.FindByIdAsync(command.EnrollmentId);
        if (enrollment == null) return false;

        try
        {
            enrollmentRepository.Remove(enrollment);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to delete enrollment", e);
        }
    }
}
