using System.Collections.Generic;

namespace GroupMessage.Server.Communication
{
    public interface IMessageSenderFactory
    {
        List<IMessageSender> GetMessageSenders();
    }
}