using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;


namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
/// <summary>
/// Defines the contract for querying class sessions and attendance records.
/// </summary>
public interface IClassSessionQueryService
{
  /// <summary>
  /// Retrieves a class session by its unique identifier.
  /// </summary>
  /// <param name="query">The query containing the ID of the class session.</param>
  /// <returns>A task that returns the <see cref="ClassSession"/> corresponding to the given ID.</returns>
  Task<ClassSession> Handle(GetClassSessionByIdQuery query);  
  Task<List<AttendanceRecord>> Handle(GetAttendanceRecordByDniCourseAndDateQuery query);
  /// <summary>
  /// Retrieves class sessions filtered by course and date range.
  /// </summary>
  /// <param name="query">The query parameters for filtering class sessions.</param>
  /// <returns>A task that returns a list of matching <see cref="ClassSession"/> instances.</returns>
  Task<List<ClassSession>> Handle(GetClassSessionsByCourseAndDateRangeQuery query);

}