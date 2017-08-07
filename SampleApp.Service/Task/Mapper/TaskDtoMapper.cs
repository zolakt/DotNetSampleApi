using System.Collections.Generic;
using SampleApp.Service.Task.DTO;

namespace SampleApp.Service.Task.Mapper
{
    public class TaskDtoMapper : ITaskDtoMapper
    {
        public TaskDTO ConvertToDTO(Domain.Task.Task task)
        {
            return new TaskDTO {
                Id = task.Id,
                Name = task.Name,
                Time = task.Time,
                UserId = task.User.Id,
                UserFirstName = task.User?.FirstName,
                UserLastName = task.User?.LastName
            };
        }

        public IEnumerable<TaskDTO> ConvertToDTO(IEnumerable<Domain.Task.Task> tasks)
        {
            foreach (var task in tasks)
            {
                yield return ConvertToDTO(task);
            }
        }


        public Domain.Task.Task ConvertToDomainObject(TaskEditDTO dto)
        {
            return PopulateDomainObject(new Domain.Task.Task(), dto);
        }

        public Domain.Task.Task PopulateDomainObject(Domain.Task.Task entity, TaskEditDTO dto)
        {
            entity.Name = dto.Name;
            entity.Time = dto.Time;

            if (entity.User == null)
            {
                entity.User = new Domain.User.User();
            }

            entity.User.Id = dto.UserId;

            return entity;
        }
    }
}