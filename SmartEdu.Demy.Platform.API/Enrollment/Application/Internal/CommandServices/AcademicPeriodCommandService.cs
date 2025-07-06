using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Enrollment.Application.Internal.CommandServices;

/// <summary>
/// Service that handles commands for creating, updating and deleting academic periods.
/// </summary>
/// <remarks>Paul Sulca</remarks>
public class AcademicPeriodCommandService(
    IAcademicPeriodRepository academicPeriodRepository,
    IUnitOfWork unitOfWork)
    : IAcademicPeriodCommandService
{
    /// <summary>
    /// Handles the creation of a new academic period.
    /// </summary>
    /// <param name="command">Command containing data to create the academic period</param>
    /// <returns>The created AcademicPeriod or null if an error occurred</returns>
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
            throw new Exception("Error creating academic period: " + e.Message);
        }
    }

    /// <summary>
    /// Handles the update of an existing academic period.
    /// </summary>
    /// <param name="command">Command containing updated data</param>
    /// <returns>The updated AcademicPeriod or null if not found or on error</returns>
    public async Task<AcademicPeriod?> Handle(UpdateAcademicPeriodCommand command)
    {
        var academicPeriod = await academicPeriodRepository.FindByIdAsync(command.Id);
        if (academicPeriod == null) throw new Exception("Academic period not found");

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
            throw new Exception($"WeeklySchedule with name '{command.Id}' is invalid.");
        }
    }

    /// <summary>
    /// Handles the deletion of an academic period.
    /// </summary>
    /// <param name="command">Command specifying the academic period to delete</param>
    /// <returns>True if deleted successfully; false otherwise</returns>
    public async Task<bool> Handle(DeleteAcademicPeriodCommand command)
    {
        var academicPeriod = await academicPeriodRepository.FindByIdAsync(command.AcademicPeriodId);
        if (academicPeriod == null)  throw new Exception("Academic period not found.");
        
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
