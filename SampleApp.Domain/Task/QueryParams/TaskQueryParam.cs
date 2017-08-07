using System;
using System.Runtime.Serialization;
using SampleApp.Common.DAL.Repositories;

namespace SampleApp.Domain.Task.QueryParams
{
    public class TaskQueryParam : RepositoryQueryParams<TaskQueryParam.FilterOptions, TaskQueryParam.IncludeOptions>
    {
        [DataContract]
        public class IncludeOptions
        {
            [DataMember]
            public bool User { get; set; }
        }

        [DataContract]
        public class FilterOptions
        {
            [DataMember]
            public Guid? UserId { get; set; }
        }
    }
}