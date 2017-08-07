using System.Collections.Generic;
using SampleApp.Service.User.DTO;

namespace SampleApp.Service.User.Mapper
{
    public interface IUserDtoMapper
    {
        UserDTO ConvertToDTO(Domain.User.User user);
        IEnumerable<UserDTO> ConvertToDTO(IEnumerable<Domain.User.User> user);

        Domain.User.User ConvertToDomainObject(UserEditDTO dto);
        Domain.User.User PopulateDomainObject(Domain.User.User entity, UserEditDTO dto);
    }
}
