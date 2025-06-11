using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Repositories;


namespace SmartEdu.Demy.Platform.API.Attendance.Application.Internal.QueryServices;

public class ClassSessionQueryService(IClassSessionRepository classSessionRepository): IClassSessionQueryService

{
    public async Task<ClassSession> Handle(GetClassSessionByIdQuery query)
    {
        return await classSessionRepository.FindByIdAsync(query.Id);
    }
}