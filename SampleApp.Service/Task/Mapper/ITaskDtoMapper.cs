using System.Collections.Generic;
using SampleApp.Service.Task.DTO;

namespace SampleApp.Service.Task.Mapper
{
    public interface ITaskDtoMapper
    {
        TaskDTO ConvertToDTO(Domain.Task.Task task);
        IEnumerable<TaskDTO> ConvertToDTO(IEnumerable<Domain.Task.Task> tasks);

        Domain.Task.Task ConvertToDomainObject(TaskEditDTO dto);
        Domain.Task.Task PopulateDomainObject(Domain.Task.Task entity, TaskEditDTO dto);
    }
}
