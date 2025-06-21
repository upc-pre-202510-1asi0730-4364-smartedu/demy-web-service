using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;

namespace SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;

public interface ICourseCommandService
{
    Task<Course?> Handle(CreateCourseCommand command);

    Task<Course?> Handle(UpdateCourseCommand command);

    Task<bool> Handle(DeleteCourseCommand command);
}