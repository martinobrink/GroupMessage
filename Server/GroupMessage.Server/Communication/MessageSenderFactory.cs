using System.Collections.Generic;

namespace GroupMessage.Server.Communication
{
    public class MessageSenderFactory : IMessageSenderFactory
    {
        public List<IMessageSender> GetMessageSenders()
        {
            return new List<IMessageSender>(new IMessageSender[] { new TwilioMessageSender(), new PushMessageSender()});
        }
    }
}