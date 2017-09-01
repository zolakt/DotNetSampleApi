using System;
using System.Collections.Generic;

namespace SampleApp.Common.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        private IEnumerable<string> _errors;

        public ValidationException(string message) : base(message)
        {
            _errors = new[] { message };
        }

        public ValidationException(IEnumerable<string> messages) : base(string.Join(", ", messages))
        {
            _errors = messages;
        }

        public IEnumerable<string> Errors
        {
            get { return _errors; }
        }
    }
}