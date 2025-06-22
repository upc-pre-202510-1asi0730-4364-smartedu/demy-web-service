using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.CommandServices;

public class EnrollmentCommandService(
    IEnrollmentRepository enrollmentRepository,
    IUnitOfWork unitOfWork)
    : IEnrollmentCommandService
{
    public async Task<Domain.Model.Aggregates.Enrollment?> Handle(CreateEnrollmentCommand command)
    {
        var enrollment = new Domain.Model.Aggregates.Enrollment(command);
        try
        {
            await enrollmentRepository.AddAsync(enrollment);
            await unitOfWork.CompleteAsync();
            return enrollment;
        }
        catch (Exception e)
        {
            // Log the exception e
            return null;
        }
    }

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
            // Log the exception e
            return null;
        }
    }

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
            // Log the exception e
            return false;
        }
    }
}
