using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

public interface IEnrollmentCommandService
{
    Task<Model.Aggregates.Enrollment?> Handle(CreateEnrollmentCommand command);
    Task<Model.Aggregates.Enrollment?> Handle(UpdateEnrollmentCommand command);
    Task<bool> Handle(DeleteEnrollmentCommand command);
}