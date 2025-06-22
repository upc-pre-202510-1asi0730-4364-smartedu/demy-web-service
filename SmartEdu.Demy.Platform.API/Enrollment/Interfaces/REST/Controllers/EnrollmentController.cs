using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Enrollment Endpoints.")]
public class EnrollmentController(
    IEnrollmentCommandService enrollmentCommandService,
    IEnrollmentQueryService enrollmentQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Enrollments", "Get all enrollments.", OperationId = "GetAllEnrollments")]
    [SwaggerResponse(200, "The enrollments were found and returned.", typeof(IEnumerable<EnrollmentResource>))]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllEnrollmentsQuery();
        var enrollments = await enrollmentQueryService.Handle(query);
        var resources = enrollments.Select(EnrollmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{enrollmentId:int}")]
    [SwaggerOperation("Get Enrollment by Id", "Get an enrollment by its unique identifier.", OperationId = "GetEnrollmentById")]
    [SwaggerResponse(200, "The enrollment was found and returned.", typeof(EnrollmentResource))]
    [SwaggerResponse(404, "The enrollment was not found.")]
    public async Task<IActionResult> GetById(int enrollmentId)
    {
        var query = new GetEnrollmentByIdQuery(enrollmentId);
        var enrollment = await enrollmentQueryService.Handle(query);
        if (enrollment is null) return NotFound();
        var resource = EnrollmentResourceFromEntityAssembler.ToResourceFromEntity(enrollment);
        return Ok(resource);
    }

    [HttpGet("student/{studentId:int}")]
    [SwaggerOperation("Get Enrollments by Student Id", "Get all enrollments for a given student id.", OperationId = "GetEnrollmentsByStudentId")]
    [SwaggerResponse(200, "The enrollments were found and returned.", typeof(IEnumerable<EnrollmentResource>))]
    public async Task<IActionResult> GetByStudentId(int studentId)
    {
        var query = new GetAllEnrollmentsByStudentIdQuery(studentId);
        var enrollments = await enrollmentQueryService.Handle(query);
        var resources = enrollments.Select(EnrollmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("student/dni/{dni}")]
    [SwaggerOperation("Get Enrollments by Student DNI", "Get all enrollments for a student using their DNI.", OperationId = "GetEnrollmentsByStudentDni")]
    [SwaggerResponse(200, "The enrollments were found and returned.", typeof(IEnumerable<EnrollmentResource>))]
    public async Task<IActionResult> GetByStudentDni(string dni)
    {
        var query = new GetAllEnrollmentsByStudentDniQuery(dni);
        var enrollments = await enrollmentQueryService.Handle(query);
        var resources = enrollments.Select(EnrollmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation("Create Enrollment", "Create a new enrollment.", OperationId = "CreateEnrollment")]
    [SwaggerResponse(201, "The enrollment was created.", typeof(EnrollmentResource))]
    [SwaggerResponse(400, "The enrollment was not created.")]
    public async Task<IActionResult> Create(CreateEnrollmentResource resource)
    {
        var command = CreateEnrollmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var enrollment = await enrollmentCommandService.Handle(command);
        if (enrollment is null) return BadRequest();
        var response = EnrollmentResourceFromEntityAssembler.ToResourceFromEntity(enrollment);
        return CreatedAtAction(nameof(GetById), new { enrollmentId = enrollment.Id }, response);
    }

    [HttpPut("{enrollmentId:int}")]
    [SwaggerOperation("Update Enrollment", "Update an existing enrollment.", OperationId = "UpdateEnrollment")]
    [SwaggerResponse(200, "The enrollment was updated.", typeof(EnrollmentResource))]
    [SwaggerResponse(404, "The enrollment was not found.")]
    [SwaggerResponse(400, "The enrollment was not updated.")]
    public async Task<IActionResult> Update(int enrollmentId, UpdateEnrollmentResource resource)
    {
        var command = UpdateEnrollmentCommandFromResourceAssembler.ToCommandFromResource(enrollmentId, resource);
        var enrollment = await enrollmentCommandService.Handle(command);
        if (enrollment is null) return NotFound();
        var response = EnrollmentResourceFromEntityAssembler.ToResourceFromEntity(enrollment);
        return Ok(response);
    }

    [HttpDelete("{enrollmentId:int}")]
    [SwaggerOperation("Delete Enrollment", "Delete an existing enrollment.", OperationId = "DeleteEnrollment")]
    [SwaggerResponse(200, "The enrollment was deleted.")]
    [SwaggerResponse(404, "The enrollment was not found.")]
    public async Task<IActionResult> Delete(int enrollmentId)
    {
        var command = new DeleteEnrollmentCommand(enrollmentId);
        var success = await enrollmentCommandService.Handle(command);
        if (!success) return NotFound();
        return Ok(new { message = "Enrollment successfully deleted" });
    }
}
