using SampleApp.Service.User.DTO;
using SampleApp.WebApp.ViewModels.Shared;

namespace SampleApp.WebApp.ViewModels.Users
{
    public class EditViewModel
    {
        public SaveMode SaveMode { get; set; }

        public UserDTO User { get; set; }

        public string JsonValidation { get; set; }
    }
}