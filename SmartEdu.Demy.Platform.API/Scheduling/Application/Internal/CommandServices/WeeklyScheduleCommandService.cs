using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.CommandServices;

/// <summary>
/// Weekly schedule command service implementation
/// </summary>
public class WeeklyScheduleCommandService(
    IWeeklyScheduleRepository weeklyScheduleRepository,
    IUnitOfWork unitOfWork) 
    : IWeeklyScheduleCommandService
{
    /// <inheritdoc />
    public async Task<WeeklySchedule?> Handle(CreateWeeklyScheduleCommand command)
    {
        var weeklySchedule = new WeeklySchedule(command);
        try
        {
            await weeklyScheduleRepository.AddAsync(weeklySchedule);
            await unitOfWork.CompleteAsync();
            return weeklySchedule;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<WeeklySchedule?> Handle(AddScheduleToWeeklyCommand command)
    {
        try
        {
            var weeklySchedule = await weeklyScheduleRepository.FindByIdAsync(command.WeeklyScheduleId);
            if (weeklySchedule == null) 
                throw new InvalidOperationException($"Weekly schedule with ID {command.WeeklyScheduleId} not found");
            
            weeklySchedule.AddScheduleFromCommand(command);
            weeklyScheduleRepository.Update(weeklySchedule);
            await unitOfWork.CompleteAsync();
            return weeklySchedule;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error adding schedule: {e.Message}", e);
        }
    }

    /// <inheritdoc />
    public async Task<WeeklySchedule?> Handle(RemoveScheduleFromWeeklyCommand command)
    {
        try
        {
            var weeklySchedule = await weeklyScheduleRepository.FindByIdAsync(command.WeeklyScheduleId);
            if (weeklySchedule == null) return null;
            
            weeklySchedule.RemoveSchedule(command);
            weeklyScheduleRepository.Update(weeklySchedule);
            await unitOfWork.CompleteAsync();
            return weeklySchedule;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<WeeklySchedule?> Handle(UpdateWeeklyScheduleNameCommand command)
    {
        try
        {
            var weeklySchedule = await weeklyScheduleRepository.FindByIdAsync(command.WeeklyScheduleId);
            if (weeklySchedule == null) return null;
            
            weeklySchedule.UpdateName(command);
            weeklyScheduleRepository.Update(weeklySchedule);
            await unitOfWork.CompleteAsync();
            return weeklySchedule;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }
}