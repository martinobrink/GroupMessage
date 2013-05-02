using GroupMessage.Server.Model;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using System.Collections.Generic;

namespace GroupMessage.Server.Test.Module
{
    [TestFixture]
    public class MessageModuleTest : NancyIntegrationTestBase<User>
    {
        [Test]
        public void PUT_ShouldSendMessageToAllUsersInDatabase()
        {
            // ARRANGE
            Db.EntityCollection.Insert(new User {Name = "Name1", SurName = "Surname1", Email = "email1@mail.dk"});
            Db.EntityCollection.Insert(new User {Name = "Name2", SurName = "Surname2", Email = "email2@mail.dk"});

			var msg = new Message {MessageId="1234", Text="MyTestText"};

            // ACT
            BrowserResponse response = Browser.Put("/groupmessage/message/1234", with =>
            {
                with.HttpRequest();
				with.Body("{'Text': 'MyTestText'}");
            });

            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

		string AsString (BrowserResponseBodyWrapper body)
		{
			return System.Text.Encoding.UTF8.GetString(System.Linq.Enumerable.ToArray<byte>((IEnumerable <byte>)body));
		}
    }
}