using System;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.Common.Domain.Exceptions;
using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Service;
using SampleApp.Common.Service.Exceptions;
using SampleApp.Domain.User;
using SampleApp.Service.User.Mapper;
using SampleApp.Service.User.Messaging;

namespace SampleApp.Service.User
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDtoMapper _userMapper;
        private readonly IValidator<Domain.User.User> _userValidator;

        public UserService(IUnitOfWork uow, IUserRepository userRepository, IUserDtoMapper userMapper,
            IValidator<Domain.User.User> userValidator) : base(uow)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }
            if (userMapper == null)
            {
                throw new ArgumentNullException("userMapper");
            }

            if (userValidator == null)
            {
                throw new ArgumentNullException("userValidator");
            }

            _userRepository = userRepository;
            _userMapper = userMapper;
            _userValidator = userValidator;
        }


        public GetUserResponse GetUser(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var task = _userRepository.FindBy(request.Id);

                if (task == null)
                {
                    response.Exception = GetStandardUserNotFoundException();
                }
                else
                {
                    response.Result = _userMapper.ConvertToDTO(task);
                }
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public GetUsersResponse GetUsers(GetUsersRequest request)
        {
            var response = new GetUsersResponse();

            try
            {
                var users = _userRepository.Find(request.QueryParams);
                response.Result = _userMapper.ConvertToDTO(users);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public InsertUserResponse InsertUser(InsertUserRequest request)
        {
            var response = new InsertUserResponse();

            try
            {
                var newUser = _userMapper.ConvertToDomainObject(request.UserProperties);

                ThrowExceptionIfUserIsInvalid(newUser);

                _userRepository.Insert(newUser);
                _uow.Commit();

                response.Result = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                return response;
            }
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            var response = new UpdateUserResponse();

            try
            {
                var existingUser = _userRepository.FindBy(request.Id);

                if (existingUser != null)
                {
                    _userMapper.PopulateDomainObject(existingUser, request.UserProperties);

                    ThrowExceptionIfUserIsInvalid(existingUser);

                    _userRepository.Update(existingUser);
                    _uow.Commit();

                    response.Result = true;

                    return response;
                }

                response.Exception = GetStandardUserNotFoundException();
                return response;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                return response;
            }
        }

        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            var response = new DeleteUserResponse();

            try
            {
                var task = _userRepository.FindBy(request.Id);

                if (task != null)
                {
                    _userRepository.Delete(task);
                    _uow.Commit();

                    response.Result = true;

                    return response;
                }

                response.Exception = GetStandardUserNotFoundException();
                return response;
            }
            catch (Exception ex)
            {
                response.Exception = ex;
                return response;
            }
        }


        private ResourceNotFoundException GetStandardUserNotFoundException()
        {
            return new ResourceNotFoundException("The requested task was not found.");
        }

        private void ThrowExceptionIfUserIsInvalid(Domain.User.User newUser)
        {
            var brokenRules = new BusinessRuleCollection(_userValidator.GetBrokenRules(newUser));

            if (brokenRules.HasRules())
            {
                throw new ValidationException(brokenRules.GetRulesSummary());
            }
        }
    }
}