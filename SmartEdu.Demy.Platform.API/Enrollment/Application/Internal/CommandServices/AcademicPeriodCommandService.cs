using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.CommandServices;

public class AcademicPeriodCommandService(
    IAcademicPeriodRepository academicPeriodRepository,
    IUnitOfWork unitOfWork)
    : IAcademicPeriodCommandService
{
    public async Task<AcademicPeriod?> Handle(CreateAcademicPeriodCommand command)
    {
        var academicPeriod = new AcademicPeriod(command);
        try
        {
            await academicPeriodRepository.AddAsync(academicPeriod);
            await unitOfWork.CompleteAsync();
            return academicPeriod;
        }
        catch (Exception e)
        {
            // Log the exception e
            return null;
        }
    }

    public async Task<AcademicPeriod?> Handle(UpdateAcademicPeriodCommand command)
    {
        var academicPeriod = await academicPeriodRepository.FindByIdAsync(command.Id);
        if (academicPeriod == null) return null;

        try
        {
            academicPeriod.UpdateInformation(
                command.PeriodName,
                command.StartDate, 
                command.EndDate,
                command.IsActive
            );
            academicPeriodRepository.Update(academicPeriod);
            await unitOfWork.CompleteAsync();
            return academicPeriod;
        }
        catch (Exception e)
        {
            // Log the exception e
            return null;
        }
    }

    public async Task<bool> Handle(DeleteAcademicPeriodCommand command)
    {
        var academicPeriod = await academicPeriodRepository.FindByIdAsync(command.AcademicPeriodId);
        if (academicPeriod == null) return false;

        try
        {
            academicPeriodRepository.Remove(academicPeriod);
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
