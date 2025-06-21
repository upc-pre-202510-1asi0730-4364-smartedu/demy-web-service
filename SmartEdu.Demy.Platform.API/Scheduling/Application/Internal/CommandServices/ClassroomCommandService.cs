using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Commands;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Model.Aggregates;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Repositories;
using SmartEdu.Demy.Platform.API.Scheduling.Domain.Services;
using SmartEdu.Demy.Platform.API.Shared.Domain.Repositories;

namespace SmartEdu.Demy.Platform.API.Scheduling.Application.Internal.CommandServices;

public class ClassroomCommandService(
    IClassroomRepository classroomRepository, 
    IUnitOfWork unitOfWork) 
    : IClassroomCommandService
{
    public async Task<Classroom?> Handle(CreateClassroomCommand command)
    {
        var classroom = new Classroom(command);
        try
        {
            await classroomRepository.AddAsync(classroom);
            await unitOfWork.CompleteAsync();
            return classroom;
        } 
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }
    
    public async Task<Classroom?> Handle(UpdateClassroomCommand command)
    {
        var classroom = await classroomRepository.FindByIdAsync(command.Id);
        if (classroom == null) return null;
        
        try
        {
            classroom.UpdateClassroom(command.Code, command.Capacity, command.Campus);
            classroomRepository.Update(classroom);
            await unitOfWork.CompleteAsync();
            return classroom;
        }
        catch (Exception e)
        {
            // Log error
            return null;
        }
    }
    
    public async Task<bool> Handle(DeleteClassroomCommand command)
    {
        var classroom = await classroomRepository.FindByIdAsync(command.Id);
        if (classroom == null) return false;
        
        try
        {
            classroomRepository.Remove(classroom);
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