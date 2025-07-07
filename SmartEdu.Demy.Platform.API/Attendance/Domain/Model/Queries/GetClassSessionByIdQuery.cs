namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;

/// <summary>
/// Query used to retrieve a specific class session by its unique identifier.
/// </summary>
/// <param name="Id">The unique identifier of the class session.</param>
public record GetClassSessionByIdQuery(int Id);
