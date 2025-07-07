using System.Net.Mime;
using System.Runtime.CompilerServices;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Shared.Domain.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Attendance.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Class Session")]
public class ClassSessionController(IClassSessionCommandService classSessionCommandService
, IClassSessionQueryService classSessionQueryService): ControllerBase
{
  [HttpPost]
  [SwaggerOperation(
    Summary = "Creates a class session",
    Description = "Creates a class session with a given CourseId and Date with a list of attendace records",
    OperationId = "CreateClassSession")]
  [SwaggerResponse(201, "The class session was created", typeof(ClassSessionResource))]
  [SwaggerResponse(400, "The class session was not crated")]
  public async Task<ActionResult> CreateClassSession([FromBody] CreateClassSessionResource resource)
  {
    var createClassSessionCommand = CreateClassSessionCommandFromResourceAssembler.ToCommandFromResource(resource);
    var result = await classSessionCommandService.Handle(createClassSessionCommand);
    if( result is null) return BadRequest();
    return CreatedAtAction(nameof(GetClassSessionById), new {id= result.Id}, ClassSessionResourceFromEntityAssembler.ToResourceFromEntity(result));
    
  }

  [HttpGet("{id}")]
  [SwaggerOperation(
    Summary = "Gets a class session by id",
    Description = "Gets a class session for a given class session identifier",
    OperationId = "GetClassSessionById")]
  [SwaggerResponse(200, "The class session was found", typeof(ClassSessionResource))]
  public async Task<ActionResult> GetClassSessionById(int id)
  {
    var getClassSessionByIdQuery = new GetClassSessionByIdQuery(id);
    var result = await classSessionQueryService.Handle(getClassSessionByIdQuery);
    if( result is null) return NotFound();
    var resource = ClassSessionResourceFromEntityAssembler.ToResourceFromEntity(result);
    return Ok(resource);
  }
  [HttpGet("report")]
  [SwaggerOperation(
    Summary = "Generates attendance report for a student in a course",
    Description = "Returns all attendance records for a given course, student DNI and date range",
    OperationId = "GetClassSessionAttendanceReport")]
  [SwaggerResponse(200, "The attendance report was generated", typeof(ClassSessionReportResource))]
  public async Task<ActionResult> GetClassSessionAttendanceReport(
    [FromQuery] int courseId,
    [FromQuery] string dni,
    [FromQuery] DateOnly startDate,
    [FromQuery] DateOnly endDate)
  {
    // Use the correct query that returns List<ClassSession>
    var query = new GetClassSessionsByCourseAndDateRangeQuery(courseId,dni ,startDate, endDate);

    var sessions = await classSessionQueryService.Handle(query); // ✅ Este devuelve List<ClassSession>

    if (sessions == null || sessions.Count == 0)
      return NotFound("No class sessions found.");

    // The assembler filters AttendanceRecords by DNI and uses the Date of each session
    var resource = ClassSessionReportFromEntityAssembler.ToResourceFromEntities(courseId, dni, sessions);

    return Ok(resource);
  }

  
  
  
}