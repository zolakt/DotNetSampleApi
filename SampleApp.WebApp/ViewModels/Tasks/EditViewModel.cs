using SampleApp.Service.Task.DTO;
using SampleApp.WebApp.ViewModels.Shared;
using System.Web.Mvc;

namespace SampleApp.WebApp.ViewModels.Tasks
{
    public class EditViewModel
    {
        public SaveMode SaveMode { get; set; }

        public TaskDTO Task { get; set; }

        public SelectList Users { get; set; }

        public string JsonValidation { get; set; }
    }
}