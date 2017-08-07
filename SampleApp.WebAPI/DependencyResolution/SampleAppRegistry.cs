using System.Collections.Generic;
using SampleApp.Common.Caching;
using SampleApp.Common.Configuration;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.UnitOfWork;
using SampleApp.Common.Domain;
using SampleApp.Common.Domain.Validation;
using SampleApp.Common.Email;
using SampleApp.Common.Logging;
using SampleApp.Common.OutputWriter;
using SampleApp.Common.Service;
using SampleApp.DAL.Memory.DataContext;
using SampleApp.Domain.Address;
using SampleApp.Domain.Address.Specifications;
using SampleApp.Domain.Task;
using SampleApp.Domain.Task.Specifications;
using SampleApp.Domain.User;
using SampleApp.Domain.User.Specifications;
using SampleApp.Domain.User.Validator;
using SampleApp.Service.Configuration;
using SampleApp.Service.Task;
using SampleApp.Service.User;
using SampleApp.WebAPI.Configuration;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SampleApp.WebAPI.DependencyResolution
{
    public class SampleAppRegistry : Registry
    {
        public SampleAppRegistry()
        {
            Scan(scan => {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<ICacheStorage>();
                scan.AssemblyContainingType<IDataContext>();
                scan.AssemblyContainingType<IAggregateRoot>();
                scan.AssemblyContainingType<ServiceBase>();
                scan.AssemblyContainingType<InMemoryDataContext>();
                scan.AssemblyContainingType<User>();
                scan.AssemblyContainingType<ITaskService>();
                scan.WithDefaultConventions();
            });

            SetupCommonComponents();

            SetupServices();

            SetUpValidators();
        }

        private void SetupCommonComponents()
        {
            For<IAppConfig>().Use<WebApiAppConfig>();
            For<IAppConfig<IServiceConfigOptions>>().Use<WebApiAppConfig>();
            For<ICacheStorage>().Use<SystemRuntimeCacheStorage>();
            For<IDataContextFactory<IDomainContext>>().Use<InMemoryDataContextFactory>().Singleton();
            For<IUnitOfWork>().Use<InMemoryUnitOfWork>().Singleton();
            For<IOutputWriter>().Use<ConsoleOutputWritter>();
            For<ILoggingService>().Use<ConsoleLoggingService>();
            For<IEmailService>().Use<FakeEmailService>();
        }

        private void SetupServices()
        {
            For<ITaskService>().Use<EnrichedTaskService>()
                .Ctor<ITaskService>().Is<TaskService>();

            For<IUserService>().Use<EnrichedUserService>()
                .Ctor<IUserService>().Is<UserService>();
        }

        private void SetUpValidators()
        {
            var addressValidator = new SpecificationValidator<Address>(new List<ISpecification<Address>> {
                new AddressCountryRequired()
            });

            var userValidator = new UserValidator(new List<ISpecification<User>> {
                new UserNameRequired()
            }, addressValidator);

            var taskValidator = new SpecificationValidator<Task>(new List<ISpecification<Task>> {
                new TaskNameRequired(),
                new TaskTimeRequired(),
                new TaskUserRequired()
            });

            For<IValidator<Address>>().Use(addressValidator);
            For<IValidator<User>>().Use(userValidator);
            For<IValidator<Task>>().Use(taskValidator);
        }
    }
}