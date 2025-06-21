using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.QueryServices;

public class ClassroomQueryService(IClassroomRepository classroomRepository) : IClassroomQueryService
{
    public async Task<IEnumerable<Classroom>> Handle(GetAllClassroomsQuery query)
    {
        return await classroomRepository.ListAsync();
    }

    public async Task<Classroom?> Handle(GetClassroomByIdQuery query)
    {
        return await classroomRepository.FindByIdAsync(query.ClassroomId);
    }

}