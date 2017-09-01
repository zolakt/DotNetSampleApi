namespace SampleApp.DAL.EF.Migrations
{
    using DbModels;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SampleApp.DAL.EF.DataContext.EfDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SampleApp.DAL.EF.DataContext.EfDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            var user1 = new User()
            {
                Id = new Guid("103C8287-30CB-4630-B3F2-978286F72BD7"),
                FirstName = "John",
                LastName = "Doe",
                Country = "UK",
                City = "London",
                Street = "Baker street",
                HouseNumber = "221b"
            };

            var user2 = new User()
            {
                Id = new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"),
                FirstName = "Jane",
                LastName = "Doe",
                Country = "Croatia",
                City = "Kutina",
                Street = "Kolodvorska",
                HouseNumber = "41/4"
            };

            var task1 = new Task()
            {
                Id = new Guid("fc387e24-ee3c-497b-b9a1-63b98bb05f82"),
                Name = "Task 1",
                Time = new DateTime(2017, 2, 20, 22, 00, 00),
                User = user1,
                UserId = user1?.Id ?? Guid.Empty
            };

            var task2 = new Task()
            {
                Id = new Guid("4b4d4728-1846-4e0d-9d0f-c9272edf35ea"),
                Time = new DateTime(2017, 2, 20, 21, 00, 00),
                Name = "Task 2",
                User = user1,
                UserId = user1?.Id ?? Guid.Empty
            };

            var task3 = new Task()
            {
                Id = new Guid("d5f49d70-b3c9-41b1-9455-307d9082bbc4"),
                Name = "Task 3",
                Time = new DateTime(2017, 2, 22, 10, 30, 00),
                User = user2,
                UserId = user2?.Id ?? Guid.Empty
            };

            context.Users.AddOrUpdate(u => u.Id,
                user1,
                user2);

            context.Tasks.AddOrUpdate(t => t.Id,
                task1,
                task2,
                task3);

            context.SaveChanges();
        }
    }
}
