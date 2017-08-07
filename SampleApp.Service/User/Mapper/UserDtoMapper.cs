using System.Collections.Generic;
using SampleApp.Domain.Address;
using SampleApp.Service.User.DTO;

namespace SampleApp.Service.User.Mapper
{
    public class UserDtoMapper : IUserDtoMapper
    {
        public UserDTO ConvertToDTO(Domain.User.User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Country = user.Address?.Country,
                City = user.Address?.City,
                Street = user.Address?.Street,
                HouseNumber = user.Address?.HouseNumber
            };
        }

        public IEnumerable<UserDTO> ConvertToDTO(IEnumerable<Domain.User.User> users)
        {
            foreach (var user in users)
            {
                yield return ConvertToDTO(user);
            }
        }


        public Domain.User.User ConvertToDomainObject(UserEditDTO dto)
        {
            return PopulateDomainObject(new Domain.User.User(), dto);
        }

        public Domain.User.User PopulateDomainObject(Domain.User.User entity, UserEditDTO dto)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;

            entity.Address = new Address() {
                Country = dto.Country,
                City = dto.City,
                Street = dto.Street,
                HouseNumber = dto.HouseNumber
            };

            return entity;
        }
    }
}