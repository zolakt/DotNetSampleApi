using System;
using System.Collections.Generic;
using SampleApp.DAL.Memory.DbModels;

namespace SampleApp.DAL.Memory.DataContext.Initializer
{
    public class DatabaseUsersInitializer : IDatabaseUsersInitializer
    {
        public ICollection<User> Initialize()
        {
            return new List<User>() {
                new User() {
                    Id = new Guid("103C8287-30CB-4630-B3F2-978286F72BD7"),
                    FirstName = "John",
                    LastName = "Doe",
                    Country = "UK",
                    City = "London",
                    Street = "Baker street",
                    HouseNumber = "221b"
                },
                new User() {
                    Id = new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"),
                    FirstName = "Jane",
                    LastName = "Doe",
                    Country = "Croatia",
                    City = "Kutina",
                    Street = "Kolodvorska",
                    HouseNumber = "41/4"
                }
            };
        }
    }
}