namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.ACL;

public interface ISchedulingContextFacade
{
    Task<int> FetchWeeklyScheduleIdByName(string name);
}