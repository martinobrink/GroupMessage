using System;
using GroupMessage.Server.Communication;
using NUnit.Framework;

namespace GroupMessage.Server.Test
{
    //[TestFixture]
    public class SendTwilioMessage
    {
        public SendTwilioMessage ()
        {
        }

        //[Test]
        public void Send()
        {
            var sender = new TwilioMessageSender();

            GroupMessage.Server.Model.User user = new GroupMessage.Server.Model.User{ PhoneNumber = "+45 61 33 54 40" };
            sender.Send(user, "ohøj");
        }

    }
}

