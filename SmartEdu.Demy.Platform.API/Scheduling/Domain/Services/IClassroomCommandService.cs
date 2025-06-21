using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

public interface IClassroomCommandService
{
    Task<Classroom?> Handle(CreateClassroomCommand command);
    
    Task<Classroom?> Handle(UpdateClassroomCommand command);
    
    Task<bool> Handle(DeleteClassroomCommand command);
}