using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Enrollment.Domain.Services;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Transform;

namespace SmartEdu.Demy.Platform.API.Enrollment.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Academic Period Endpoints.")]
public class AcademicPeriodController(
    IAcademicPeriodCommandService academicPeriodCommandService,
    IAcademicPeriodQueryService academicPeriodQueryService)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get All Academic Periods", "Get all academic periods.", OperationId = "GetAllAcademicPeriods")]
    [SwaggerResponse(200, "The academic periods were found and returned.", typeof(IEnumerable<AcademicPeriodResource>))]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllAcademicPeriodsQuery();
        var periods = await academicPeriodQueryService.Handle(query);
        var resources = periods.Select(AcademicPeriodResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{academicPeriodId:int}")]
    [SwaggerOperation("Get Academic Period by Id", "Get an academic period by its unique identifier.", OperationId = "GetAcademicPeriodById")]
    [SwaggerResponse(200, "The academic period was found and returned.", typeof(AcademicPeriodResource))]
    [SwaggerResponse(404, "The academic period was not found.")]
    public async Task<IActionResult> GetById(int academicPeriodId)
    {
        var query = new GetAcademicPeriodByIdQuery(academicPeriodId);
        var period = await academicPeriodQueryService.Handle(query);
        if (period is null) return NotFound();
        var resource = AcademicPeriodResourceFromEntityAssembler.ToResourceFromEntity(period);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation("Create Academic Period", "Create a new academic period.", OperationId = "CreateAcademicPeriod")]
    [SwaggerResponse(201, "The academic period was created.", typeof(AcademicPeriodResource))]
    [SwaggerResponse(400, "The academic period was not created.")]
    public async Task<IActionResult> Create(CreateAcademicPeriodResource resource)
    {
        var command = CreateAcademicPeriodCommandFromResourceAssembler.ToCommandFromResource(resource);
        var period = await academicPeriodCommandService.Handle(command);
        if (period is null) return BadRequest();
        var response = AcademicPeriodResourceFromEntityAssembler.ToResourceFromEntity(period);
        return CreatedAtAction(nameof(GetById), new { academicPeriodId = period.Id }, response);
    }

    [HttpPut("{academicPeriodId:int}")]
    [SwaggerOperation("Update Academic Period", "Update an existing academic period.", OperationId = "UpdateAcademicPeriod")]
    [SwaggerResponse(200, "The academic period was updated.", typeof(AcademicPeriodResource))]
    [SwaggerResponse(404, "The academic period was not found.")]
    [SwaggerResponse(400, "The academic period was not updated.")]
    public async Task<IActionResult> Update(int academicPeriodId, UpdateAcademicPeriodResource resource)
    {
        var command = UpdateAcademicPeriodCommandFromResourceAssembler.ToCommandFromResource(academicPeriodId, resource);
        var period = await academicPeriodCommandService.Handle(command);
        if (period is null) return NotFound();
        var response = AcademicPeriodResourceFromEntityAssembler.ToResourceFromEntity(period);
        return Ok(response);
    }

    [HttpDelete("{academicPeriodId:int}")]
    [SwaggerOperation("Delete Academic Period", "Delete an existing academic period.", OperationId = "DeleteAcademicPeriod")]
    [SwaggerResponse(200, "The academic period was deleted.")]
    [SwaggerResponse(404, "The academic period was not found.")]
    public async Task<IActionResult> Delete(int academicPeriodId)
    {
        var command = new DeleteAcademicPeriodCommand(academicPeriodId);
        var success = await academicPeriodCommandService.Handle(command);
        if (!success) return NotFound();
        return Ok();
    }
}
