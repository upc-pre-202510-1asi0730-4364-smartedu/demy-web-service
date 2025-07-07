using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

public interface IWeeklyScheduleCommandService
{
    Task<WeeklySchedule?> Handle(CreateWeeklyScheduleCommand command);
    
    
    Task<WeeklySchedule?> Handle(AddScheduleToWeeklyCommand command);
    
    
    Task<WeeklySchedule?> Handle(RemoveScheduleFromWeeklyCommand command);
    
  
    Task<WeeklySchedule?> Handle(UpdateWeeklyScheduleNameCommand command);
    
    Task<bool> Handle(DeleteWeeklyScheduleCommand command);
    
    Task<Schedule?> Handle(UpdateScheduleCommand command);
    
    
}