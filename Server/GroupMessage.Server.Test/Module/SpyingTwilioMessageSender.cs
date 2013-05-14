using System;
using System.Collections.Generic;
using GroupMessage.Server.Communication;
using GroupMessage.Server.Model;

namespace GroupMessage.Server.Test.Module
{
    public class SpyingTwilioMessageSender: IMessageSender
    {
        public MessageSenderType SenderType
        {
            get { return MessageSenderType.Twilio; }
        }

        public int NumberOfCalls {
            get { return Users.Count; }
        }

        public List<User> Users {
            get;
            private set;
        }

        public List<String> Texts {
            get;
            private set;
        }

        public SpyingTwilioMessageSender()
        {
            Users = new List<User>();
            Texts = new List<String>();
        }

        SendStatus IMessageSender.Send(GroupMessage.Server.Model.User user, string text)
        {
            Users.Add(user);
            Texts.Add(text);
            return new SendStatus{Success=true};
        }
    }
}

