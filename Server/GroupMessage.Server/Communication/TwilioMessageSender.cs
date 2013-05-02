using System;
using System.Collections.Generic;
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
            List<string> twilioConfigLines;
            try
            {
                twilioConfigLines = File.ReadLines("Twilio.txt").ToList();
            }
            catch (Exception exception)
            {
                var errorMessage = string.Format("Something failed during reading of file Twilio.txt! Did you forget to add a Twilio.txt to the same directory as the application? Exception: {0}", exception.Message);
                Console.WriteLine(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            var accountSID = twilioConfigLines[0].Split('=')[1];
            var authToken = twilioConfigLines[1].Split('=')[1];
            _senderNumber = twilioConfigLines[2].Split('=')[1];

            _client = new TwilioRestClient(accountSID, authToken);
        }

        public void Send(Model.User user, string text)
        {
            Console.WriteLine("About to send message " + text + " to user " + user.Name);
            
            var status = Client.SendSmsMessage(SenderNumber, user.PhoneNumber, text);
            
            if (status.Status != "sent") 
            {
                var errorMsg = "Unable to send sms to user " + user.Name + ", status is " + status.Status + ", exception is " + status.RestException.Message;
                Console.WriteLine (errorMsg);
                throw new Exception(errorMsg);
            }
        }
    }
}

