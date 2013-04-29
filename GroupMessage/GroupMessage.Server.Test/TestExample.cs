using System;
using GroupMessage.Server.Data;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using Mongo2Go;
using MongoDB.Driver;
using NUnit.Framework;
using MongoDB.Driver.Linq;

namespace GroupMessage.Server.Test
{
    [TestFixture]
    public class TestExample
    {
        /// <summary>
        /// Test showing usage of UserRepository with an injected database used only for testing.
        /// Not done yet, must be further integrated with Nancy using a CustomBootstrapper.
        /// </summary>
        [Test]
        public void SomeTest()
        {
            var dbRunner = MongoDbRunner.Start();
            var client = new MongoClient(dbRunner.ConnectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("IntegrationTest");
            var userRepository = new UserRepository(new MongoDbWrapper<User>(database));
            userRepository.Create(new User
                {
                    Name = "Name1",
                    SurName = "Surname1",
                    Email = "Email1"
                });
            userRepository.Create(new User
                {
                    Name = "Name2",
                    SurName = "Surname2",
                    Email = "Email2"
                });


            foreach (var user in userRepository.Users.AsQueryable())
            {
                Console.WriteLine("{0} {1}, {2}", user.Name, user.SurName, user.Email);
            }
        }
    }
}
