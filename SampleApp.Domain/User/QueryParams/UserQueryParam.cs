using System.Runtime.Serialization;
using SampleApp.Common.DAL.Repositories;

namespace SampleApp.Domain.User.QueryParams
{
    [DataContract]
    public class UserQueryParam : RepositoryQueryParams<UserQueryParam.FilterOptions>
    {
        [DataContract]
        public class FilterOptions
        {
            [DataMember]
            public string Country { get; set; }
        }
    }
}