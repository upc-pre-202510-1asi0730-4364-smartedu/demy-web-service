using System.Net.Mime;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Queries;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Resources;
using SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartEdu.Demy.Platform.API.Scheduling.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Weekly Schedule Endpoints.")]
public class WeeklySchedulesController(
    IWeeklyScheduleCommandService weeklyScheduleCommandService,
    IWeeklyScheduleQueryService weeklyScheduleQueryService,
    IScheduleQueryService scheduleQueryService)
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
        catch
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
    
    [HttpDelete("{weeklyScheduleId:int}")]
    [SwaggerOperation("Delete Weekly Schedule", "Delete a weekly schedule by its unique identifier.", OperationId = "DeleteWeeklySchedule")]
    [SwaggerResponse(200, "Weekly schedule deleted successfully")]
    [SwaggerResponse(404, "Weekly schedule not found")]
    public async Task<IActionResult> DeleteWeeklySchedule(int weeklyScheduleId)
    {
        var command = new DeleteWeeklyScheduleCommand(weeklyScheduleId);
        await weeklyScheduleCommandService.Handle(command);
        return Ok(new { message = "WeeklySchedule deleted successfully" });
    }

    [HttpGet("by-teacher/{teacherId:int}")]
    [SwaggerOperation("Get Schedules by Teacher Id", "Get all schedules for a given teacher ID.", OperationId = "GetSchedulesByTeacherId")]
    [SwaggerResponse(200, "Schedules found", typeof(IEnumerable<ScheduleResource>))]
    [SwaggerResponse(404, "No schedules found for teacher ID")]
    public async Task<IActionResult> GetSchedulesByTeacherId(int teacherId)
    {
        var query = new GetSchedulesByTeacherIdQuery(teacherId);
        var schedules = await scheduleQueryService.Handle(query);
        
        if (schedules == null || !schedules.Any())
        {
            return NotFound();
        }
        
        var scheduleResources = schedules.Select(ScheduleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(scheduleResources);
    }

    [HttpPut("schedules/{scheduleId:int}")]
    [SwaggerOperation("Update Schedule", "Updates the classroom, start time, end time and day fields of a Schedule by its ID.", OperationId = "UpdateSchedule")]
    [SwaggerResponse(200, "Schedule updated successfully", typeof(ScheduleResource))]
    [SwaggerResponse(404, "Schedule not found")]
    public async Task<IActionResult> UpdateSchedule(int scheduleId, UpdateScheduleResource resource)
    {
        var command = UpdateScheduleCommandFromResourceAssembler.ToCommandFromResource(scheduleId, resource);
        var updatedSchedule = await weeklyScheduleCommandService.Handle(command);
        
        if (updatedSchedule is null)
        {
            return NotFound();
        }
        
        var scheduleResource = ScheduleResourceFromEntityAssembler.ToResourceFromEntity(updatedSchedule);
        return Ok(scheduleResource);
    }
    
}