using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;
using DayOfWeek = SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.ValueObjects.DayOfWeek;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.CommandServices;

/// <summary>
/// Weekly schedule command service implementation
/// </summary>
public class WeeklyScheduleCommandService(
    IWeeklyScheduleRepository weeklyScheduleRepository,
    IScheduleRepository scheduleRepository,
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
        catch 
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
        catch 
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
        catch 
        {
            // Log error
            return null;
        }
    }
    
    public async Task<bool> Handle(DeleteWeeklyScheduleCommand command)
    {
        var weeklySchedule = await weeklyScheduleRepository.FindByIdAsync(command.WeeklyScheduleId);
        if (weeklySchedule == null) return false;
    
        try
        {
            weeklyScheduleRepository.Remove(weeklySchedule);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch 
        {
            // Log error
            return false;
        }
    }
    
    public async Task<Schedule?> Handle(UpdateScheduleCommand command)
    {
        var schedule = await scheduleRepository.FindByIdAsync(command.ScheduleId);
        if (schedule == null)
        {
            throw new ArgumentException($"Schedule with id {command.ScheduleId} not found");
        }

        schedule.UpdateSchedule(
            command.StartTime,
            command.EndTime,
            Enum.Parse<DayOfWeek>(command.DayOfWeek, true),
            command.ClassroomId
        );
        
        scheduleRepository.Update(schedule);
        await unitOfWork.CompleteAsync();
        return schedule;
    }

}