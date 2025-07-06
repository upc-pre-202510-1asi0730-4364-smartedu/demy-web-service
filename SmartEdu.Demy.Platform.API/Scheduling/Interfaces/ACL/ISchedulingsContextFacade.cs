namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.ACL;

public interface ISchedulingsContextFacade
{
    Task<int> FetchWeeklyScheduleIdByName(string name);
}