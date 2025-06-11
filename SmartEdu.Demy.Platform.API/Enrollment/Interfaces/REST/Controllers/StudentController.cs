using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Student Endpoints.")]
public class StudentController(
    IStudentQueryService studentQueryService
) : ControllerBase
{
    [HttpGet("{studentId:long}")]
    [SwaggerOperation(
        Summary = "Get Student by ID",
        Description = "Retrieve a specific student using their ID.",
        OperationId = "GetStudentById"
    )]
    [SwaggerResponse(
        StatusCodes.Status200OK,
        "The student was found",
        typeof(StudentResource)
    )]
    [SwaggerResponse(
        StatusCodes.Status404NotFound,
        "The student was not found"
    )]
    public async Task<IActionResult> GetStudentById(long studentId)
    {
        var query = new GetStudentByIdQuery(studentId);
        var student = await studentQueryService.Handle(query);

        if (student == null)
            return NotFound();

        var resource = StudentResourceFromEntityAssembler.ToResourceFromEntity(student);
        return Ok(resource);
    }
}