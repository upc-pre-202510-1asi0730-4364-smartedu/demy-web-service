using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

public interface IAcademicPeriodCommandService
{
    Task<AcademicPeriod?> Handle(CreateAcademicPeriodCommand command);
    Task<AcademicPeriod?> Handle(UpdateAcademicPeriodCommand commnand);
    Task<bool> Handle(DeleteAcademicPeriodCommand command);
}