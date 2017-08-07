using System.Net;
using System.Net.Http;
using SampleApp.Common.Service;
using SampleApp.WebAPI.Helper;

namespace SampleApp.WebAPI.ResponseFormat
{
    public class HttpResponseBuilder : IHttpResponseBuilder
    {
        public HttpResponseMessage BuildResponse<T>(HttpRequestMessage requestMessage,
            ServiceResponseBase<T> baseResponse)
        {
            var statusCode = HttpStatusCode.OK;

            if (baseResponse.Exception != null)
            {
                statusCode = baseResponse.Exception.ConvertToHttpStatusCode();

                var message = new HttpResponseMessage(statusCode) {
                    Content = new StringContent(baseResponse.Exception.Message)
                };

                return message;
            }

            return requestMessage.CreateResponse(statusCode, baseResponse.Result);
        }
    }
}