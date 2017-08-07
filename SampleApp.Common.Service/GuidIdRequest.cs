using System;

namespace SampleApp.Common.Service
{
    public abstract class GuidIdRequest : ServiceRequestBase
    {
        public GuidIdRequest(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            Id = id;
        }

        public Guid Id { get; }
    }
}