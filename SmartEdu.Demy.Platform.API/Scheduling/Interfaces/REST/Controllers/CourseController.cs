using System.Net.Mime;
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
[SwaggerTag("Available Course Endpoints.")]
public class CoursesController(
    ICourseCommandService courseCommandService,
    ICourseQueryService courseQueryService)
: ControllerBase
{
    [HttpGet("{courseId:int}")]
    [SwaggerOperation("Get Course by Id", "Get a course by its unique identifier.", OperationId = "GetCourseById")]
    [SwaggerResponse(200, "The course was found and returned.", typeof(CourseResource))]
    [SwaggerResponse(404, "The course was not found.")]
    public async Task<IActionResult> GetCourseById(int courseId)
    {
        var getCourseByIdQuery = new GetCourseByIdQuery(courseId);
        var course = await courseQueryService.Handle(getCourseByIdQuery);
        if (course is null) return NotFound();
        var courseResource = CourseResourceFromEntityAssembler.ToResourceFromEntity(course);
        return Ok(courseResource);
    }

    [HttpPost]
    [SwaggerOperation("Create Course", "Create a new course.", OperationId = "CreateCourse")]
    [SwaggerResponse(201, "The course was created.", typeof(CourseResource))]
    [SwaggerResponse(400, "The course was not created.")]
    public async Task<IActionResult> CreateCourse(CreateCourseResource resource)
    {
        var createCourseCommand = CreateCourseCommandFromResourceAssembler.ToCommandFromResource(resource);
        var course = await courseCommandService.Handle(createCourseCommand);
        if (course is null) return BadRequest();
        var courseResource = CourseResourceFromEntityAssembler.ToResourceFromEntity(course);
        return CreatedAtAction(nameof(GetCourseById), new { courseId = course.Id }, courseResource);
    }

    [HttpPut("{courseId:int}")]
    [SwaggerOperation("Update Course", "Update an existing course.", OperationId = "UpdateCourse")]
    [SwaggerResponse(200, "The course was updated.", typeof(CourseResource))]
    [SwaggerResponse(404, "The course was not found.")]
    [SwaggerResponse(400, "The course was not updated.")]
    public async Task<IActionResult> UpdateCourse(int courseId, UpdateCourseResource resource)
    {
        var updateCourseCommand = UpdateCourseCommandFromResourceAssembler.ToCommandFromResource(courseId, resource);
        var course = await courseCommandService.Handle(updateCourseCommand);
        if (course is null) return NotFound();
        var courseResource = CourseResourceFromEntityAssembler.ToResourceFromEntity(course);
        return Ok(courseResource);
    }

    [HttpDelete("{courseId:int}")]
    [SwaggerOperation("Delete Course", "Delete an existing course.", OperationId = "DeleteCourse")]
    [SwaggerResponse(200, "The course was deleted.")]
    [SwaggerResponse(404, "The course was not found.")]
    public async Task<IActionResult> DeleteCourse(int courseId)
    {
        var deleteCourseCommand = new DeleteCourseCommand(courseId);
        var result = await courseCommandService.Handle(deleteCourseCommand);
        if (!result) return NotFound();
        return Ok();
    }

    [HttpGet]
    [SwaggerOperation("Get All Courses", "Get all courses.", OperationId = "GetAllCourses")]
    [SwaggerResponse(200, "The courses were found and returned.", typeof(IEnumerable<CourseResource>))]
    public async Task<IActionResult> GetAllCourses()
    {
        var getAllCoursesQuery = new GetAllCoursesQuery();
        var courses = await courseQueryService.Handle(getAllCoursesQuery);
        var courseResources = courses.Select(CourseResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(courseResources);
    }
}