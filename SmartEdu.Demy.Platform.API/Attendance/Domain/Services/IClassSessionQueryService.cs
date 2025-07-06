using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;


namespace SmartEdu.Demy.Platform.API.Attendance.Domain.Services;

public interface IClassSessionQueryService
{
  Task<ClassSession> Handle(GetClassSessionByIdQuery query);  
  Task<List<AttendanceRecord>> Handle(GetAttendanceRecordByDniCourseAndDateQuery query);
  
  Task<List<ClassSession>> Handle(GetClassSessionsByCourseAndDateRangeQuery query);

}