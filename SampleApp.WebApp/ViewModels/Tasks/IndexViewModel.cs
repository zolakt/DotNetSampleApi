using SampleApp.Service.Task.DTO;
using System.Collections.Generic;

namespace SampleApp.WebApp.ViewModels.Tasks
{
    public class IndexViewModel
    {
        public IEnumerable<TaskDTO> Tasks { get; set; }
    }
}