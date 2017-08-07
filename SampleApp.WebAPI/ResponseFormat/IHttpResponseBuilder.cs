using System.Net.Http;
using SampleApp.Common.Service;

namespace SampleApp.WebAPI.ResponseFormat
{
    public interface IHttpResponseBuilder
    {
        HttpResponseMessage BuildResponse<T>(HttpRequestMessage requestMessage, ServiceResponseBase<T> baseResponse);
    }
}