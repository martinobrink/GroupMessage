using System.IO;
using System.Linq;
using Twilio;

namespace GroupMessage.Server.Communication
{
    public class TwilioMessageSender : IMessageSender
    {
        private readonly string _senderNumber;
        private readonly TwilioRestClient _client;

        public TwilioMessageSender()
        {
            var twilioConfigLines = File.ReadLines("Twilio.txt").ToList();
            var accountSID = twilioConfigLines[0].Split('=')[1];
            var authToken = twilioConfigLines[1].Split('=')[1];
            _senderNumber = twilioConfigLines[2].Split('=')[1];

            _client = new TwilioRestClient(accountSID, authToken);
        }

        public void Send(Model.User user, string text)
        {
            _client.SendSmsMessage(_senderNumber, user.PhoneNumber, text);
        }
    }
}

