using GroupMessage.Server.Model;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using System.Collections.Generic;
using System.Linq;

namespace GroupMessage.Server.Test.Module
{
    [TestFixture]
    public class UserModuleTest : NancyIntegrationTestBase<User>
    {
        [Test]
        public void GET_ShouldReturnStatusOKAndAllUsers()
        {
            // ARRANGE
            Db.EntityCollection.Insert(new User {Name = "Name1", LastName = "Lastname1", PhoneNumber = "11111111", Email = "email1@mail.dk"});
            Db.EntityCollection.Insert(new User {Name = "Name2", LastName = "Lastname2", PhoneNumber = "11111111", Email = "email2@mail.dk"});

            // ACT
            var response = Browser.Get("/groupmessage/user", with =>
            {
                with.HttpRequest();
                with.Header("accept", "application/json");
            });

            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var users = response.Body.DeserializeJson<List<User>>();
            Assert.That(users.Count, Is.EqualTo(2));
            Assert.That(users.Count(user => user.Name == "Name1"), Is.EqualTo(1));
            Assert.That(users.Count(user => user.Name == "Name2"), Is.EqualTo(1));
        }
    }
}