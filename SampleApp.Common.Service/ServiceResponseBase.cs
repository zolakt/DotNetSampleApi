using System;

namespace SampleApp.Common.Service
{
    public abstract class ServiceResponseBase<T>
    {
        public ServiceResponseBase()
        {
            Exception = null;
        }

        public T Result { get; set; }

        public Exception Exception { get; set; }
    }
}