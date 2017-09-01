using SampleApp.Common.Domain.Validation;
using SampleApp.Domain.User;
using SampleApp.Domain.User.QueryParams;
using SampleApp.Service.User;
using SampleApp.Service.User.DTO;
using SampleApp.Service.User.Messaging;
using SampleApp.WebApp.JsonValidationMapper;
using SampleApp.WebApp.ViewModels.Shared;
using SampleApp.WebApp.ViewModels.Users;
using System;
using System.Web.Mvc;

namespace SampleApp.WebApp.Controllers
{
    public class UsersController : MvcController
    {
        private readonly IUserService _userService;
        private readonly IValidator<User> _userValidator;
        private readonly IJsonValidationMapper _jsValidationMapper;

        public UsersController(IUserService userService, IValidator<User> userValidator, 
            IJsonValidationMapper jsValidationMapper) : base()
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            if (userValidator == null)
            {
                throw new ArgumentNullException("userValidator");
            }
            if(jsValidationMapper == null)
            {
                throw new ArgumentNullException("jsValidationMapper");
            }

            _userService = userService;
            _userValidator = userValidator;
            _jsValidationMapper = jsValidationMapper;
        }

        [HttpGet]
        public ActionResult Index(UserQueryParam.FilterOptions filters)
        {
            var serviceRequest = new GetUsersRequest
            {
                QueryParams = new UserQueryParam
                {
                    Filter = filters
                }
            };

            var serviceResponse = _userService.GetUsers(serviceRequest);

            HandleServiceException(serviceResponse);

            return View(new IndexViewModel
            {
                Users = serviceResponse.Result
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return GetEditView(new UserDTO(), SaveMode.Create);
        }

        [HttpPost]
        public ActionResult Create(UserDTO user)
        {
            var serviceRequest = new InsertUserRequest
            {
                UserProperties = user
            };

            var serviceResponse = _userService.InsertUser(serviceRequest);

            HandleServiceException(serviceResponse);

            return GetEditView(user, SaveMode.Create);
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            var serviceRequest = new GetUserRequest(id);
            var serviceResponse = _userService.GetUser(serviceRequest);

            HandleServiceException(serviceResponse);

            return GetEditView(serviceResponse.Result, SaveMode.Update);
        }

        [HttpPost]
        public ActionResult Update(UserDTO user)
        {
            var serviceRequest = new UpdateUserRequest(user.Id)
            {
                UserProperties = user
            };

            var serviceResponse = _userService.UpdateUser(serviceRequest);

            HandleServiceException(serviceResponse);

            return GetEditView(user, SaveMode.Update);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var serviceRequest = new DeleteUserRequest(id);
            var serviceResponse = _userService.DeleteUser(serviceRequest);

            HandleServiceException(serviceResponse);

            return RedirectToAction("Index");
        }


        private ViewResult GetEditView(UserDTO user, SaveMode mode)
        {
            return View("Edit", new EditViewModel
            {
                SaveMode = mode,
                User = user,
                JsonValidation = _jsValidationMapper.Format(_userValidator.GetRulesDetails(), "User")
            });
        }
    }
}