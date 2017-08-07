using System;

namespace SampleApp.Common.Service.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message)
            : base(message) {}

        public ResourceNotFoundException()
            : base(Resources.Services.ResourceNotFoundException) {}
    }
}