using System.Net.Mime;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Weekly Schedule Endpoints.")]
public class WeeklySchedulesController(
    IWeeklyScheduleCommandService weeklyScheduleCommandService,
    IWeeklyScheduleQueryService weeklyScheduleQueryService)
    : ControllerBase
{
    [HttpGet("{weeklyScheduleId:int}")]
    [SwaggerOperation("Get Weekly Schedule by Id", "Get a weekly schedule by its unique identifier.", OperationId = "GetWeeklyScheduleById")]
    [SwaggerResponse(200, "The weekly schedule was found and returned.", typeof(WeeklyScheduleResource))]
    [SwaggerResponse(404, "The weekly schedule was not found.")]
    public async Task<IActionResult> GetWeeklyScheduleById(int weeklyScheduleId)
    {
        var query = new GetWeeklyByIdQuery(weeklyScheduleId);
        var weeklySchedule = await weeklyScheduleQueryService.Handle(query);
        if (weeklySchedule is null) return NotFound();
        
        var resource = WeeklyScheduleResourceFromEntityAssembler.ToResourceFromEntity(weeklySchedule);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation("Create Weekly Schedule", "Create a new weekly schedule.", OperationId = "CreateWeeklySchedule")]
    [SwaggerResponse(201, "The weekly schedule was created.", typeof(WeeklyScheduleResource))]
    [SwaggerResponse(400, "The weekly schedule was not created.")]
    public async Task<IActionResult> CreateWeeklySchedule(CreateWeeklyScheduleResource resource)
    {
        var command = CreateWeeklyScheduleCommandFromResourceAssembler.ToCommandFromResource(resource);
        var weeklySchedule = await weeklyScheduleCommandService.Handle(command);
        if (weeklySchedule is null) return BadRequest();
        
        var weeklyScheduleResource = WeeklyScheduleResourceFromEntityAssembler.ToResourceFromEntity(weeklySchedule);
        return CreatedAtAction(nameof(GetWeeklyScheduleById), new { weeklyScheduleId = weeklySchedule.Id }, weeklyScheduleResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Weekly Schedules", "Get all weekly schedules.", OperationId = "GetAllWeeklySchedules")]
    [SwaggerResponse(200, "The weekly schedules were found and returned.", typeof(IEnumerable<WeeklyScheduleResource>))]
    public async Task<IActionResult> GetAllWeeklySchedules()
    {
        var query = new GetAllWeeklySchedulesQuery();
        var weeklySchedules = await weeklyScheduleQueryService.Handle(query);
        var resources = weeklySchedules.Select(WeeklyScheduleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost("{weeklyScheduleId:int}/schedules")]
    [SwaggerOperation("Add Schedule to Weekly Schedule", "Add a new schedule to an existing weekly schedule.", OperationId = "AddScheduleToWeeklySchedule")]
    [SwaggerResponse(200, "The schedule was added to the weekly schedule.", typeof(WeeklyScheduleResource))]
    [SwaggerResponse(400, "The schedule was not added.")]
    [SwaggerResponse(404, "The weekly schedule was not found.")]
    public async Task<IActionResult> AddScheduleToWeeklySchedule(int weeklyScheduleId, AddScheduleToWeeklyResource resource)
    {
        try
        {
            var command = AddScheduleToWeeklyCommandFromResourceAssembler.ToCommandFromResource(weeklyScheduleId, resource);
            var weeklySchedule = await weeklyScheduleCommandService.Handle(command);
            var weeklyScheduleResource = WeeklyScheduleResourceFromEntityAssembler.ToResourceFromEntity(weeklySchedule);
            return Ok(weeklyScheduleResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = "An unexpected error occurred while adding the schedule" });
        }
    }

    [HttpDelete("{weeklyScheduleId:int}/schedules/{scheduleId:int}")]
    [SwaggerOperation("Remove Schedule from Weekly Schedule", "Remove a schedule from a weekly schedule.", OperationId = "RemoveScheduleFromWeeklySchedule")]
    [SwaggerResponse(200, "The schedule was removed from the weekly schedule.", typeof(WeeklyScheduleResource))]
    [SwaggerResponse(400, "The schedule was not removed.")]
    [SwaggerResponse(404, "The weekly schedule or schedule was not found.")]
    public async Task<IActionResult> RemoveScheduleFromWeeklySchedule(int weeklyScheduleId, int scheduleId)
    {
        var command = new RemoveScheduleFromWeeklyCommand(weeklyScheduleId, scheduleId);
        var weeklySchedule = await weeklyScheduleCommandService.Handle(command);
        if (weeklySchedule is null) return BadRequest();
        
        var weeklyScheduleResource = WeeklyScheduleResourceFromEntityAssembler.ToResourceFromEntity(weeklySchedule);
        return Ok(weeklyScheduleResource);
    }

    [HttpPut("{weeklyScheduleId:int}/name")]
    [SwaggerOperation("Update Weekly Schedule Name", "Update the name of a weekly schedule.", OperationId = "UpdateWeeklyScheduleName")]
    [SwaggerResponse(200, "The weekly schedule name was updated.", typeof(WeeklyScheduleResource))]
    [SwaggerResponse(400, "The weekly schedule name was not updated.")]
    [SwaggerResponse(404, "The weekly schedule was not found.")]
    public async Task<IActionResult> UpdateWeeklyScheduleName(int weeklyScheduleId, UpdateWeeklyScheduleNameResource resource)
    {
        var command = UpdateWeeklyScheduleNameCommandFromResourceAssembler.ToCommandFromResource(weeklyScheduleId, resource);
        var weeklySchedule = await weeklyScheduleCommandService.Handle(command);
        if (weeklySchedule is null) return BadRequest();
        
        var weeklyScheduleResource = WeeklyScheduleResourceFromEntityAssembler.ToResourceFromEntity(weeklySchedule);
        return Ok(weeklyScheduleResource);
    }
}