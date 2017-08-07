using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.DAL.Repositories;
using SampleApp.Common.DAL.Tests.Fakes;
using SampleApp.Common.DAL.UnitOfWork;

namespace SampleApp.Common.DAL.Tests.UnitOfWork
{
    [TestClass]
    public class InMemoryUnitOfWorkTests
    {
        [TestMethod]
        public void CommitSingleRepositoryTest()
        {
            var mockRepo = new Mock<IUnitOfWorkRepository>();

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            var uow = new InMemoryUnitOfWork();

            uow.RegisterInsertion(domainUser, mockRepo.Object);

            domainUser.Name = "test";

            uow.RegisterUpdate(domainUser, mockRepo.Object);

            uow.RegisterDeletion(domainUser, mockRepo.Object);

            uow.Commit();

            mockRepo.Verify(w => w.PersistInsertion(domainUser), Times.Once);
            mockRepo.Verify(w => w.PersistUpdate(domainUser), Times.Once);
            mockRepo.Verify(w => w.PersistDeletion(domainUser), Times.Once);
        }

        [TestMethod]
        public void CommitMultipleRepositoriesTest()
        {
            var mockRepo1 = new Mock<IUnitOfWorkRepository>();

            var domainUser1 = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            var mockRepo2 = new Mock<IUnitOfWorkRepository>();

            var domainUser2 = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test2"
            };

            var uow = new InMemoryUnitOfWork();

            uow.RegisterInsertion(domainUser1, mockRepo1.Object);
            uow.RegisterInsertion(domainUser2, mockRepo2.Object);

            domainUser2.Name = "test22";

            uow.RegisterUpdate(domainUser2, mockRepo2.Object);

            uow.RegisterDeletion(domainUser1, mockRepo1.Object);

            uow.Commit();

            mockRepo1.Verify(w => w.PersistInsertion(domainUser1), Times.Once);
            mockRepo1.Verify(w => w.PersistUpdate(domainUser1), Times.Never);
            mockRepo1.Verify(w => w.PersistDeletion(domainUser1), Times.Once);

            mockRepo2.Verify(w => w.PersistInsertion(domainUser2), Times.Once);
            mockRepo2.Verify(w => w.PersistUpdate(domainUser2), Times.Once);
            mockRepo2.Verify(w => w.PersistDeletion(domainUser2), Times.Never);
        }

        [TestMethod]
        public void CommitEmptyTest()
        {
            var mockRepo = new Mock<IUnitOfWorkRepository>();

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            var uow = new InMemoryUnitOfWork();

            uow.Commit();

            mockRepo.Verify(w => w.PersistInsertion(domainUser), Times.Never);
            mockRepo.Verify(w => w.PersistUpdate(domainUser), Times.Never);
            mockRepo.Verify(w => w.PersistDeletion(domainUser), Times.Never);
        }
    }
}