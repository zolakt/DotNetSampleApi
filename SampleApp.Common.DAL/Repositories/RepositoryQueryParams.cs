using System.Runtime.Serialization;

namespace SampleApp.Common.DAL.Repositories
{
    [DataContract]
    public abstract class RepositoryQueryParams<TFilterParams, TIncludeParams, TSortParams>
    {
        [DataMember]
        public TFilterParams Filter { get; set; }

        [DataMember]
        public TIncludeParams Include { get; set; }

        [DataMember]
        public TSortParams Sort { get; set; }

        [DataMember]
        public PaginationOptions Pagination { get; set; }
    }

    [DataContract]
    public abstract class RepositoryQueryParams<TFilterParams, TIncludeParams>
    {
        [DataMember]
        public TFilterParams Filter { get; set; }

        [DataMember]
        public TIncludeParams Include { get; set; }

        [DataMember]
        public PaginationOptions Pagination { get; set; }
    }

    [DataContract]
    public abstract class RepositoryQueryParams<TFilterParams>
    {
        [DataMember]
        public TFilterParams Filter { get; set; }

        [DataMember]
        public PaginationOptions Pagination { get; set; }
    }
}