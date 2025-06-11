using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Entities;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.CommandServices;

public class CourseCommandService(
    ICourseRepository courseRepository, 
    IUnitOfWork unitOfWork) 
    : ICourseCommandService
{
    
    public async Task<Course?> Handle(CreateCourseCommand command)
    {
        var course = new Course(command.Name, command.Code, command.Description);
        try
        {
            await courseRepository.AddAsync(course);
            await unitOfWork.CompleteAsync();
            return course;
        } 
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    public async Task<Course?> Handle(UpdateCourseCommand command)
    {
        var course = await courseRepository.FindByIdAsync(command.Id);
        if (course == null) return null;
        
        try
        {
            course.UpdateCourse(command.Name, command.Code, command.Description);
            courseRepository.Update(course);
            await unitOfWork.CompleteAsync();
            return course;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }

    public async Task<bool> Handle(DeleteCourseCommand command)
    {
        var course = await courseRepository.FindByIdAsync(command.Id);
        if (course == null) return false;
        
        try
        {
            courseRepository.Remove(course);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            // Log error
            return false;
        }
    }
}