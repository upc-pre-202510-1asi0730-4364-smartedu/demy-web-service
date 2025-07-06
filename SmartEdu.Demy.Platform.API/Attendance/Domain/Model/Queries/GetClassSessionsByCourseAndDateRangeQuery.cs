namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;

public record GetClassSessionsByCourseAndDateRangeQuery( long CourseId, string Dni,
    DateOnly StartDate,
    DateOnly EndDate);
