using System;
using Twilio;

namespace GroupMessage.Server
{
    public class TwilioMessageSender : IMessageSender
    {
        string SenderNumber;

        TwilioRestClient Client;

        public TwilioMessageSender()
        {
            var accountSID = "AC0c600b58e62248e7f62b052a475609d7";
            SenderNumber = "+1 469-275-4652";
            var authToken = "0507d05d7a50317000d51ca2f1e8699e";

            Client = new TwilioRestClient(accountSID, authToken);
        }

        public void Send(GroupMessage.Server.Model.User user, string text)
        {
            Client.SendSmsMessage(SenderNumber, user.PhoneNumber, text);
        }
    }
}

