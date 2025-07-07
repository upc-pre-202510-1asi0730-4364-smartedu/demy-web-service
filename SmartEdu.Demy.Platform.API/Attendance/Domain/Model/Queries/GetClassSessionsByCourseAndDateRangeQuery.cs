namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;

/// <summary>
/// Query to retrieve class sessions filtered by course ID, student DNI, and a date range.
/// </summary>
/// <param name="CourseId">The identifier of the course to filter sessions.</param>
/// <param name="Dni">The DNI of the student whose attendance is being queried.</param>
/// <param name="StartDate">The start date of the date range.</param>
/// <param name="EndDate">The end date of the date range.</param>
public record GetClassSessionsByCourseAndDateRangeQuery(int CourseId, string Dni,
    DateOnly StartDate,
    DateOnly EndDate);
