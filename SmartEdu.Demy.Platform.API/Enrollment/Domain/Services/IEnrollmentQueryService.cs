using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;

namespace SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;

public interface IEnrollmentQueryService
{
    Task<IEnumerable<Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsQuery query);
    Task<Model.Aggregates.Enrollment?> Handle(GetEnrollmentByIdQuery query);
    Task<IEnumerable<Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsByStudentIdQuery query);
    Task<IEnumerable<Model.Aggregates.Enrollment>> Handle(GetAllEnrollmentsByStudentDniQuery query);
}

