using GroupMessage.Server.Model;

namespace GroupMessage.Server.Communication
{
    public class PushMessageSender : IMessageSender
    {
        public MessageSenderType SenderType
        {
            get { return MessageSenderType.PushNotification; }
        }

        public SendStatus Send(User user, string text)
        {
            return new SendStatus{Success = true, NumberOfTries = 1, ErrorMessage = null};
        }
    }
}