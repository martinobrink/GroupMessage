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
        private readonly List<IMessageSender> _messageSendersToReturn = new List<IMessageSender>();
        private IMessageSender _succeedingTwilioMessageSenderMock;

        protected override void OnSetup()
        {
            Db.EntityCollection.Insert(_user1);
            Db.EntityCollection.Insert(_user2);

            _succeedingTwilioMessageSenderMock = CreateMessageSenderMock(MessageSenderType.Twilio, new SendStatus { Success = true });
            _messageSendersToReturn.Clear();
        }

        [Test]
        public void PUT_ShouldSendMessageToAllUsersInDatabase()
        {
            // ARRANGE
            ConfigureMessageSendersToReturn(new[] {_succeedingTwilioMessageSenderMock});
            var message = new Message {MessageId = "1234", Text = "SomeMessage"};

            // ACT
            var response = Browser.Put("/groupmessage/message/"+message.MessageId, message.AsJson());
            
            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            AssertSenderMockWasCalled(_succeedingTwilioMessageSenderMock, _user1.Name, message.Text);
            AssertSenderMockWasCalled(_succeedingTwilioMessageSenderMock, _user2.Name, message.Text);
        }

        [Test]
		public void PUT_IdMismatch_ShouldReturnBadRequest()
		{
			// ARRANGE
			var message = new Message { MessageId = "1234", Text = "SomeMessage" };
		    const string anotherId = "5678";

			// ACT
			var response = Browser.Put("/groupmessage/message/"+anotherId, message.AsJson());
			
			// ASSERT
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

        [Test]
        public void PUT_AnIdAlreadyUsed_ShouldReturnBadRequest()
        {
            // ARRANGE
            ConfigureMessageSendersToReturn(new[] { _succeedingTwilioMessageSenderMock });
            var message = new Message { MessageId = "1234", Text = "SomeMessage" };
            Browser.Put("/groupmessage/message/" + message.MessageId, message.AsJson());//use id
            var otherMessageWithSameId = new Message { MessageId = "1234", Text = "SomeOtherMessage" };
            
            // ACT
            var response = Browser.Put("/groupmessage/message/"+otherMessageWithSameId.MessageId, otherMessageWithSameId.AsJson());

            // ASSERT
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        private static IMessageSender CreateMessageSenderMock(MessageSenderType senderType, SendStatus statusToReturn)
        {
            var messageSenderMock = A.Fake<IMessageSender>();
            A.CallTo(() => messageSenderMock.SenderType).Returns(senderType);
            A.CallTo(() => messageSenderMock.Send(A<User>.Ignored, A<string>.Ignored)).Returns(statusToReturn);
            return messageSenderMock;
        }

        private void ConfigureMessageSendersToReturn(IEnumerable<IMessageSender> messageSenders)
        {
            _messageSendersToReturn.AddRange(messageSenders);
            A.CallTo(() => MessageSenderFactoryFake.GetMessageSenders())
                .ReturnsLazily(() => _messageSendersToReturn);
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