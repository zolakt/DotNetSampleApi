using System;
using System.Collections.Generic;
using System.Net;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Service.Exceptions;

namespace SampleApp.WebAPI.Helper
{
    public static class ExceptionDictionary
    {
        public static HttpStatusCode ConvertToHttpStatusCode(this Exception exception)
        {
            var dict = GetExceptionDictionary();

            if (dict.ContainsKey(exception.GetType()))
            {
                return dict[exception.GetType()];
            }

            return dict[typeof(Exception)];
        }

        private static Dictionary<Type, HttpStatusCode> GetExceptionDictionary()
        {
            return new Dictionary<Type, HttpStatusCode> {
                [typeof(ResourceNotFoundException)] = HttpStatusCode.NotFound,
                [typeof(ValidationException)] = HttpStatusCode.BadRequest,
                [typeof(Exception)] = HttpStatusCode.InternalServerError
            };
        }
    }
}