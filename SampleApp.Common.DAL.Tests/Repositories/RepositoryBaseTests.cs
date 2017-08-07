using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleApp.Common.DAL.DataContext;
using SampleApp.Common.DAL.Tests.Fakes;
using SampleApp.Common.DAL.UnitOfWork;
// ReSharper disable ExpressionIsAlwaysNull

namespace SampleApp.Common.DAL.Tests.Repositories
{
    [TestClass]
    public class RepositoryBaseTests
    {
        [TestMethod]
        public void PersistInsertionTest()
        {
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.AddEntity(It.IsAny<FakeDatabaseUser>()));

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            repo.PersistInsertion(domainUser);

            mockContext.Verify(w => w.AddEntity(It.IsAny<FakeDatabaseUser>()), Times.Once);
        }

        [TestMethod]
        public void PersistInsertionInvalidTest()
        {
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.AddEntity(It.IsAny<FakeDatabaseUser>()));

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            repo.PersistInsertion(null);

            mockContext.Verify(w => w.AddEntity(It.IsAny<FakeDatabaseUser>()), Times.Never);
        }

        [TestMethod]
        public void PersistUpdateTest()
        {
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.UpdateEntity(It.IsAny<FakeDatabaseUser>()));

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            repo.PersistUpdate(domainUser);

            mockContext.Verify(w => w.UpdateEntity(It.IsAny<FakeDatabaseUser>()), Times.Once);
        }

        [TestMethod]
        public void PersistUpdateInvalidTest()
        {
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.UpdateEntity(It.IsAny<FakeDatabaseUser>()));

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            repo.PersistUpdate(null);

            mockContext.Verify(w => w.UpdateEntity(It.IsAny<FakeDatabaseUser>()), Times.Never);
        }

        [TestMethod]
        public void PersistDeletionTest()
        {
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.DeleteEntity(It.IsAny<FakeDatabaseUser>()));

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            repo.PersistDeletion(domainUser);

            mockContext.Verify(w => w.DeleteEntity(It.IsAny<FakeDatabaseUser>()), Times.Once);
        }

        [TestMethod]
        public void PersistDeletionInvalidTest()
        {
            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.DeleteEntity(It.IsAny<FakeDatabaseUser>()));

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            repo.PersistDeletion(null);

            mockContext.Verify(w => w.DeleteEntity(It.IsAny<FakeDatabaseUser>()), Times.Never);
        }


        [TestMethod]
        public void FindByIdTest()
        {
            var expectedGuid = Guid.NewGuid();
            var expectedName = "test";

            var databaseUser = new FakeDatabaseUser {
                Id = expectedGuid,
                Name = expectedName
            };

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.GetEntity<FakeDatabaseUser>(expectedGuid)).Returns(databaseUser);

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = repo.FindBy(expectedGuid);

            Assert.AreEqual(databaseUser.Id, domainUser.Id);
            Assert.AreEqual(databaseUser.Name, domainUser.Name);
        }

        [TestMethod]
        public void FindByInvalidIdTest()
        {
            var expectedGuid = Guid.NewGuid();

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.GetEntity<FakeDatabaseUser>(expectedGuid)).Returns((FakeDatabaseUser) null);

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = repo.FindBy(expectedGuid);

            Assert.IsNull(domainUser);
        }

        [TestMethod]
        public void FindAllTest()
        {
            var expectedGuid1 = Guid.NewGuid();
            var expectedName1 = "test";

            var expectedGuid2 = Guid.NewGuid();
            var expectedName2 = "test2";

            var databaseUsers = new List<FakeDatabaseUser> {
                new FakeDatabaseUser {
                    Id = expectedGuid1,
                    Name = expectedName1
                },
                new FakeDatabaseUser {
                    Id = expectedGuid2,
                    Name = expectedName2
                }
            };

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.GetAllEntities<FakeDatabaseUser>()).Returns(databaseUsers);

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUsers = repo.FindAll();

            Assert.AreEqual(databaseUsers.Count, domainUsers.Count());

            var first = domainUsers.First();
            var last = domainUsers.Last();

            Assert.IsNotNull(first);
            Assert.IsNotNull(last);
            Assert.AreEqual(databaseUsers[0].Id, first.Id);
            Assert.AreEqual(databaseUsers[0].Name, first.Name);
            Assert.AreEqual(databaseUsers[1].Id, last.Id);
            Assert.AreEqual(databaseUsers[1].Name, last.Name);
        }

        [TestMethod]
        public void FindAllEmptyTest()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var databaseUsers = new List<FakeDatabaseUser>();

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(x => x.GetAllEntities<FakeDatabaseUser>()).Returns(databaseUsers);

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUsers = repo.FindAll();

            Assert.AreEqual(0, domainUsers.Count());
        }

        [TestMethod]
        public void UpdateTest()
        {
            var mockContext = new Mock<IDataContext>();

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            mockUow.Setup(x => x.RegisterUpdate(domainUser, repo));

            repo.Update(domainUser);

            mockUow.Verify(w => w.RegisterUpdate(domainUser, repo), Times.Once);
        }

        [TestMethod]
        public void UpdateInvalidTest()
        {
            var mockContext = new Mock<IDataContext>();

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = (FakeDomainUser) null;

            mockUow.Setup(x => x.RegisterUpdate(domainUser, repo));

            repo.Update(domainUser);

            mockUow.Verify(w => w.RegisterUpdate(domainUser, repo), Times.Never);
        }

        [TestMethod]
        public void InsertTest()
        {
            var mockContext = new Mock<IDataContext>();

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            mockUow.Setup(x => x.RegisterInsertion(domainUser, repo));

            repo.Insert(domainUser);

            mockUow.Verify(w => w.RegisterInsertion(domainUser, repo), Times.Once);
        }

        [TestMethod]
        public void InsertInvalidTest()
        {
            var mockContext = new Mock<IDataContext>();

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = (FakeDomainUser) null;

            mockUow.Setup(x => x.RegisterInsertion(domainUser, repo));

            repo.Insert(domainUser);

            mockUow.Verify(w => w.RegisterInsertion(domainUser, repo), Times.Never);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var mockContext = new Mock<IDataContext>();

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = new FakeDomainUser {
                Id = Guid.NewGuid(),
                Name = "test"
            };

            mockUow.Setup(x => x.RegisterDeletion(domainUser, repo));

            repo.Delete(domainUser);

            mockUow.Verify(w => w.RegisterDeletion(domainUser, repo), Times.Once);
        }

        [TestMethod]
        public void DeleteInvalidTest()
        {
            var mockContext = new Mock<IDataContext>();

            var mockContextFactory = new Mock<IDataContextFactory<IDataContext>>();
            mockContextFactory.Setup(x => x.Create()).Returns(mockContext.Object);

            var mockUow = new Mock<IUnitOfWork>();

            var repo = new FakeRepository(mockUow.Object, mockContextFactory.Object);

            var domainUser = (FakeDomainUser) null;

            mockUow.Setup(x => x.RegisterDeletion(domainUser, repo));

            repo.Delete(domainUser);

            mockUow.Verify(w => w.RegisterInsertion(domainUser, repo), Times.Never);
        }
    }
}