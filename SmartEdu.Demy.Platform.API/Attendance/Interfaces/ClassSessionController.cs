using System.Net.Mime;
using System.Runtime.CompilerServices;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Services;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Attendance.Interfaces.REST.Transform;
using SmartEdu.Demy.Platform.API.Attendance.Domain.Model.Queries;
using Microsoft.AspNetCore.Mvc;
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
    
}