using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

public interface IClassroomQueryService
{
    Task<IEnumerable<Classroom>> Handle(GetAllClassroomsQuery query);
    
    Task<Classroom?> Handle(GetClassroomByIdQuery query);
    
}