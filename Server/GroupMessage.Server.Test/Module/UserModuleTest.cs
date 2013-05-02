using GroupMessage.Server.Model;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace GroupMessage.Server.Test.Module
{
    [TestFixture]
    public class UserModuleTest : NancyIntegrationTestBase<User>
    {
        [Test]
        public void GET_ShouldReturnStatusOKAndAllUsers()
        {
            // ARRANGE
            Db.EntityCollection.Insert(new User {Name = "Name1", LastName = "Lastname1", Email = "email1@mail.dk"});
            Db.EntityCollection.Insert(new User {Name = "Name2", LastName = "Lastname2", Email = "email2@mail.dk"});

            // ACT
            var response = Browser.Get("/groupmessage/user", with =>
            {
                with.HttpRequest();
                with.Header("accept", "application/json");
            });

            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body.AsString(), Is.StringContaining("Name1"));
            Assert.That(response.Body.AsString(), Is.StringContaining("Name2"));
        }
    }
}