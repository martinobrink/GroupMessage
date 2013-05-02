using GroupMessage.Server.Model;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using System.Collections.Generic;

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
            Assert.That(AsString(response.Body), Is.StringContaining("Name1"));
            Assert.That(AsString(response.Body), Is.StringContaining("Name2"));
        }

		string AsString (BrowserResponseBodyWrapper body)
		{
			return System.Text.Encoding.UTF8.GetString(System.Linq.Enumerable.ToArray<byte>((IEnumerable <byte>)body));
		}
    }
}