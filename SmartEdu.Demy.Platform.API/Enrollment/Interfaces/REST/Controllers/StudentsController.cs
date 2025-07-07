using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Controllers;

/// <summary>
/// REST controller that provides endpoints to manage students.
/// </summary>
/// <remarks>Paul Sulca</remarks>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Student Endpoints.")]
public class StudentsController(
    IStudentCommandService studentCommandService,
    IStudentQueryService studentQueryService)
    : ControllerBase
{
    /// <summary>
    /// Retrieves all students.
    /// </summary>
    /// <returns>A list of student resources.</returns>
    [HttpGet]
    [SwaggerOperation("Get All Students", "Get all students.", OperationId = "GetAllStudents")]
    [SwaggerResponse(200, "The students were found and returned.", typeof(IEnumerable<StudentResource>))]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllStudentsQuery();
        var students = await studentQueryService.Handle(query);
        var resources = students.Select(StudentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Retrieves a student by their ID.
    /// </summary>
    /// <param name="studentId">The student ID</param>
    /// <returns>The student resource if found, or 404 if not found.</returns>
    [HttpGet("{studentId:int}")]
    [SwaggerOperation("Get Student by Id", "Get a student by its unique identifier.", OperationId = "GetStudentById")]
    [SwaggerResponse(200, "The student was found and returned.", typeof(StudentResource))]
    [SwaggerResponse(404, "The student was not found.")]
    public async Task<IActionResult> GetById(int studentId)
    {
        var query = new GetStudentByIdQuery(studentId);
        var student = await studentQueryService.Handle(query);
        if (student is null) return NotFound();
        var resource = StudentResourceFromEntityAssembler.ToResourceFromEntity(student);
        return Ok(resource);
    }

    /// <summary>
    /// Retrieves a student by their DNI.
    /// </summary>
    /// <param name="dni">The student's DNI</param>
    /// <returns>The student resource if found, or 404 if not found.</returns>
    [HttpGet("dni/{dni}")]
    [SwaggerOperation("Get Student by DNI", "Get a student by DNI.", OperationId = "GetStudentByDni")]
    [SwaggerResponse(200, "The student was found and returned.", typeof(StudentResource))]
    [SwaggerResponse(404, "The student was not found.")]
    public async Task<IActionResult> GetByDni(string dni)
    {
        var query = new GetStudentByDniQuery(dni);
        var student = await studentQueryService.Handle(query);
        if (student is null) return NotFound();
        var resource = StudentResourceFromEntityAssembler.ToResourceFromEntity(student);
        return Ok(resource);
    }

    /// <summary>
    /// Creates a new student.
    /// </summary>
    /// <param name="resource">The student data</param>
    /// <returns>The created student resource, or 400 if creation failed.</returns>
    [HttpPost]
    [SwaggerOperation("Create Student", "Create a new student.", OperationId = "CreateStudent")]
    [SwaggerResponse(201, "The student was created.", typeof(StudentResource))]
    [SwaggerResponse(400, "The student was not created.")]
    public async Task<IActionResult> Create(CreateStudentResource resource)
    {
        var command = CreateStudentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var student = await studentCommandService.Handle(command);
        if (student is null) return BadRequest();
        var response = StudentResourceFromEntityAssembler.ToResourceFromEntity(student);
        return CreatedAtAction(nameof(GetById), new { studentId = student.Id }, response);
    }

    /// <summary>
    /// Updates an existing student.
    /// </summary>
    /// <param name="studentId">The student ID</param>
    /// <param name="resource">The updated student data</param>
    /// <returns>The updated student resource, or 404 if not found.</returns>
    [HttpPut("{studentId:int}")]
    [SwaggerOperation("Update Student", "Update an existing student.", OperationId = "UpdateStudent")]
    [SwaggerResponse(200, "The student was updated.", typeof(StudentResource))]
    [SwaggerResponse(404, "The student was not found.")]
    [SwaggerResponse(400, "The student was not updated.")]
    public async Task<IActionResult> Update(int studentId, UpdateStudentResource resource)
    {
        var command = UpdateStudentCommandFromResourceAssembler.ToCommandFromResource(studentId, resource);
        var student = await studentCommandService.Handle(command);
        if (student is null) return NotFound();
        var response = StudentResourceFromEntityAssembler.ToResourceFromEntity(student);
        return Ok(response);
    }

    /// <summary>
    /// Deletes a student by their ID.
    /// </summary>
    /// <param name="studentId">The student ID</param>
    /// <returns>200 if deleted, or 404 if not found.</returns>
    [HttpDelete("{studentId:int}")]
    [SwaggerOperation("Delete Student", "Delete an existing student.", OperationId = "DeleteStudent")]
    [SwaggerResponse(200, "The student was deleted.")]
    [SwaggerResponse(404, "The student was not found.")]
    public async Task<IActionResult> Delete(int studentId)
    {
        var command = new DeleteStudentCommand(studentId);
        var success = await studentCommandService.Handle(command);
        if (!success) return NotFound();
        return Ok(new { message = "Student successfully deleted" });
    }
}
