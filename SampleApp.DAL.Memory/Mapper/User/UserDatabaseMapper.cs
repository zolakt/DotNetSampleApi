using SampleApp.Common.DAL.DataContext;
using SampleApp.Domain.Address;

namespace SampleApp.DAL.Memory.Mapper.User
{
    public class UserDatabaseMapper : IUserDatabaseMapper
    {
        public DbModels.User ConvertToDatabaseType(Domain.User.User domainType)
        {
            return new DbModels.User {
                Id = domainType.Id,
                FirstName = domainType.FirstName,
                LastName = domainType.LastName,
                Country = domainType.Address.Country,
                City = domainType.Address.City,
                Street = domainType.Address.Street,
                HouseNumber = domainType.Address.HouseNumber
            };
        }

        public Domain.User.User ConvertToDomainType(DbModels.User databaseType, IDataContext context)
        {
            return new Domain.User.User {
                Id = databaseType.Id,
                FirstName = databaseType.FirstName,
                LastName = databaseType.LastName,
                Address = new Address {
                    Country = databaseType.Country,
                    City = databaseType.City,
                    Street = databaseType.Street,
                    HouseNumber = databaseType.HouseNumber
                }
            };
        }
    }
}