using System;
using System.Net.Http;
using System.Web.Http;
using SampleApp.Domain.User.QueryParams;
using SampleApp.Service.User;
using SampleApp.Service.User.DTO;
using SampleApp.Service.User.Messaging;
using SampleApp.WebAPI.ResponseFormat;

namespace SampleApp.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IHttpResponseBuilder _responseBuilder;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IHttpResponseBuilder responseBuilder)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (responseBuilder == null)
            {
                throw new ArgumentNullException("responseBuilder");
            }

            _userService = userService;
            _responseBuilder = responseBuilder;
        }


        public HttpResponseMessage Get(Guid id)
        {
            var request = new GetUserRequest(id);
            var response = _userService.GetUser(request);

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Get([FromUri] UserQueryParam options)
        {
            var request = new GetUsersRequest {
                QueryParams = options
            };

            var response = _userService.GetUsers(request);

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Post([FromBody] UserEditDTO user)
        {
            var request = new InsertUserRequest {
                UserProperties = user
            };

            var response = new InsertUserResponse();

            try
            {
                response = _userService.InsertUser(request);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Put([FromBody] UserDTO user)
        {
            var request = new UpdateUserRequest(user.Id) {
                UserProperties = user
            };

            var response = new UpdateUserResponse();

            try
            {
                response = _userService.UpdateUser(request);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return _responseBuilder.BuildResponse(Request, response);
        }

        public HttpResponseMessage Delete(Guid id)
        {
            var request = new DeleteUserRequest(id);

            var response = new DeleteUserResponse();

            try
            {
                response = _userService.DeleteUser(request);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return _responseBuilder.BuildResponse(Request, response);
        }
    }
}