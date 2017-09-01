using SampleApp.Service.User.DTO;
using System.Collections.Generic;

namespace SampleApp.WebApp.ViewModels.Users
{
    public class IndexViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }
    }
}