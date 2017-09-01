using System.Runtime.Serialization;
using SampleApp.Common.DAL.Repositories;

namespace SampleApp.Domain.User.QueryParams
{
    [DataContract]
    public class UserQueryParam : RepositoryQueryParams<UserQueryParam.FilterOptions, UserQueryParam.IncludeOptions, UserQueryParam.SortOptions>
    {
        [DataContract]
        public class FilterOptions
        {
            [DataMember]
            public string Country { get; set; }
        }

        [DataContract]
        public class IncludeOptions
        {
        }

        [DataContract]
        public class SortOptions
        {
            [DataMember]
            public bool Latest { get; set; }
        }
    }
}