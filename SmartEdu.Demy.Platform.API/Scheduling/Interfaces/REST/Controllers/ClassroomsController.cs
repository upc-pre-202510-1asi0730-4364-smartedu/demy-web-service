using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Classroom Endpoints.")]
public class ClassroomsController(
    IClassroomCommandService classroomCommandService,
    IClassroomQueryService classroomQueryService)
: ControllerBase
{
    [HttpGet("{classroomId:int}")]
    [SwaggerOperation("Get Classroom by Id", "Get a classroom by its unique identifier.", OperationId = "GetClassroomById")]
    [SwaggerResponse(200, "The classroom was found and returned.", typeof(ClassroomResource))]
    [SwaggerResponse(404, "The classroom was not found.")]
    public async Task<IActionResult> GetClassroomById(int classroomId)
    {
        var getClassroomByIdQuery = new GetClassroomByIdQuery(classroomId);
        var classroom = await classroomQueryService.Handle(getClassroomByIdQuery);
        if (classroom is null) return NotFound();
        var classroomResource = ClassroomResourceFromEntityAssembler.ToResourceFromEntity(classroom);
        return Ok(classroomResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Classroom", "Create a new classroom.", OperationId = "CreateClassroom")]
    [SwaggerResponse(201, "The classroom was created.", typeof(ClassroomResource))]
    [SwaggerResponse(400, "The classroom was not created.")]
    public async Task<IActionResult> CreateClassroom(CreateClassroomResource resource)
    {
        var createClassroomCommand = CreateClassroomCommandFromResourceAssembler.ToCommandFromResource(resource);
        var classroom = await classroomCommandService.Handle(createClassroomCommand);
        if (classroom is null) return BadRequest();
        var classroomResource = ClassroomResourceFromEntityAssembler.ToResourceFromEntity(classroom);
        return CreatedAtAction(nameof(GetClassroomById), new { classroomId = classroom.Id }, classroomResource);
    }

    [HttpPut("{classroomId:int}")]
    [SwaggerOperation("Update Classroom", "Update an existing classroom.", OperationId = "UpdateClassroom")]
    [SwaggerResponse(200, "The classroom was updated.", typeof(ClassroomResource))]
    [SwaggerResponse(404, "The classroom was not found.")]
    [SwaggerResponse(400, "The classroom was not updated.")]
    public async Task<IActionResult> UpdateClassroom(int classroomId, UpdateClassroomResource resource)
    {
        var updateClassroomCommand = UpdateClassroomCommandFromResourceAssembler.ToCommandFromResource(classroomId, resource);
        var classroom = await classroomCommandService.Handle(updateClassroomCommand);
        if (classroom is null) return NotFound();
        var classroomResource = ClassroomResourceFromEntityAssembler.ToResourceFromEntity(classroom);
        return Ok(classroomResource);
    }
    
    [HttpDelete("{classroomId:int}")]
    [SwaggerOperation("Delete Classroom", "Delete an existing classroom.", OperationId = "DeleteClassroom")]
    [SwaggerResponse(200, "The classroom was deleted.")]
    [SwaggerResponse(404, "The classroom was not found.")]
    public async Task<IActionResult> DeleteClassroom(int classroomId)
    {
        var deleteClassroomCommand = new DeleteClassroomCommand(classroomId);
        var result = await classroomCommandService.Handle(deleteClassroomCommand);
        if (!result) return NotFound();
        return Ok();
    }

    [HttpGet]
    [SwaggerOperation("Get All Classrooms", "Get all classrooms.", OperationId = "GetAllClassrooms")]
    [SwaggerResponse(200, "The classrooms were found and returned.", typeof(IEnumerable<ClassroomResource>))]
    public async Task<IActionResult> GetAllClassrooms()
    {
        var getAllClassroomsQuery = new GetAllClassroomsQuery();
        var classrooms = await classroomQueryService.Handle(getAllClassroomsQuery);
        var classroomResources = classrooms.Select(ClassroomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(classroomResources);
    }

}