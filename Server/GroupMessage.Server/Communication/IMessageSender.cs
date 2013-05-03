using System;
using GroupMessage.Server.Model;

namespace GroupMessage.Server.Communication
{
    public enum MessageSenderType
    {
        Twilio = 0,
        PushNotification = 1,
        Email = 2
    }

    public interface IMessageSender
    {
        SendStatus Send(User user, String text);
        MessageSenderType SenderType { get; }
    }
}