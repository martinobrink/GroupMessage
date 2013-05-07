using FakeItEasy;
using GroupMessage.Server.Communication;
using GroupMessage.Server.Model;
using NUnit.Framework;
using System.Linq;
using Nancy;
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
            Db.EntityCollection.Insert(new User {Name = "Name1", LastName = "Surname1", Email = "email1@mail.dk"});
			Db.EntityCollection.Insert(new User {Name = "Name2", LastName = "Surname2", Email = "email2@mail.dk"});
            var twilioMessageSenderMock = A.Fake<IMessageSender>();
            A.CallTo(() => twilioMessageSenderMock.SenderType).Returns(MessageSenderType.Twilio);
            A.CallTo(() => twilioMessageSenderMock.Send(A<User>.Ignored, A<string>.Ignored)).Returns(new SendStatus{Success = true});
            A.CallTo(() => MessageSenderFactoryFake.GetMessageSenders()).Returns(new List<IMessageSender>(new[] {twilioMessageSenderMock}));

            // ACT
            //todo create Message object and use .ToJson() instead of hardcoded string below
            Nancy.Testing.BrowserResponse response = Browser.Put("/groupmessage/message/1234", "{'MessageId':'1234', 'Text': 'MyTestText'}");
            
            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            A.CallTo(() => twilioMessageSenderMock.Send(
                A<User>.That.Matches(user => user.Name == "Name1"),
                A<string>.That.Matches(text => text == "MyTestText")))
             .MustHaveHappened();
            A.CallTo(() => twilioMessageSenderMock.Send(
                A<User>.That.Matches(user => user.Name == "Name2"),
                A<string>.That.Matches(text => text == "MyTestText")))
             .MustHaveHappened();
        }

		[Test]
		public void PUT_IdMisMatch_ShouldReturnBadRequest()
		{
			// ARRANGE
			Db.EntityCollection.Insert(new User {Name = "Name1", LastName = "Surname1", Email = "email1@mail.dk"});
			Db.EntityCollection.Insert(new User {Name = "Name2", LastName = "Surname2", Email = "email2@mail.dk"});

			// ACT
			Nancy.Testing.BrowserResponse response = Browser.Put("/groupmessage/message/8989", "{'MessageId':'4567','Text': 'MyTestText'}");
			
			// ASSERT
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}
    }
}