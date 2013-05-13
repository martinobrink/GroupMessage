using FakeItEasy;
using GroupMessage.Server.Communication;
using GroupMessage.Server.Model;
using NUnit.Framework;
using Nancy;
using System.Collections.Generic;

namespace GroupMessage.Server.Test.Module
{
    [TestFixture]
    public class MessageModuleTest : NancyIntegrationTestBase<User>
    {
        private readonly User _user1 = new User {Name = "Name1", LastName = "LastName1", PhoneNumber = "11111111", Email = "email1@mail.dk"};
        private readonly User _user2 = new User {Name = "Name2", LastName = "LastName2", PhoneNumber = "22222222", Email = "email2@mail.dk"};

        protected override void OnSetup()
        {
            Db.EntityCollection.Insert(_user1);
            Db.EntityCollection.Insert(_user2);
        }

        [Test]
        public void PUT_ShouldSendMessageToAllUsersInDatabase()
        {
            // ARRANGE
            var twilioMessageSenderMock = A.Fake<IMessageSender>();
            A.CallTo(() => twilioMessageSenderMock.SenderType).Returns(MessageSenderType.Twilio);
            A.CallTo(() => twilioMessageSenderMock.Send(A<User>.Ignored, A<string>.Ignored)).Returns(new SendStatus{Success = true});
            A.CallTo(() => MessageSenderFactoryFake.GetMessageSenders()).Returns(new List<IMessageSender>(new[] {twilioMessageSenderMock}));
            var message = new Message {MessageId = "1234", Text = "SomeMessage"};

            // ACT
            Nancy.Testing.BrowserResponse response = Browser.Put("/groupmessage/message/"+message.MessageId, message.AsJson());
            
            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            AssertSenderMockWasCalled(twilioMessageSenderMock, _user1.Name, message.Text);
            AssertSenderMockWasCalled(twilioMessageSenderMock, _user2.Name, message.Text);
        }

        [Test]
		public void PUT_IdMismatch_ShouldReturnBadRequest()
		{
			// ARRANGE
			var message = new Message { MessageId = "1234", Text = "SomeMessage" };
		    const string anotherId = "5678";

			// ACT
			Nancy.Testing.BrowserResponse response = Browser.Put("/groupmessage/message/"+anotherId, message.AsJson());
			
			// ASSERT
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

        private static void AssertSenderMockWasCalled(IMessageSender messageSenderMock, string userName, string messageText)
        {
            A.CallTo(() => messageSenderMock.Send(
                A<User>.That.Matches(user => user.Name == userName),
                A<string>.That.Matches(text => text == messageText)))
             .MustHaveHappened();
        }
    }
}