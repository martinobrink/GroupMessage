using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GroupMessage.Server.Model;
using Twilio;

namespace GroupMessage.Server.Communication
{
    public class TwilioMessageSender : IMessageSender
    {
        private readonly string _senderNumber;
        private readonly TwilioRestClient _client;

        public MessageSenderType SenderType
        {
            get { return MessageSenderType.Twilio; }
        }

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

            Console.WriteLine ("Instantiating client with SID " + accountSID + " and token " + authToken);

            _client = new TwilioRestClient(accountSID, authToken);
        }

        public SendStatus Send(Model.User user, string text)
        {
            Console.WriteLine("About to send message " + text + " to user " + user.Name + " " + user.LastName);

            SMSMessage status = null;
            try {
                Console.WriteLine ("senderNumber=" + _senderNumber + "user.PhoneNumber" + user.PhoneNumber + "text=" + text);
               
                status = _client.SendSmsMessage(_senderNumber, "+45 "+user.PhoneNumber, text);
            } 
            catch (Exception e) 
            {
                Console.WriteLine (e.Message);
                Console.WriteLine (e.StackTrace);
                Console.WriteLine (e.GetBaseException().Message);
                return new SendStatus{Success=false, ErrorMessage="exception while sending: " + e.GetBaseException().Message};
            }


            if (status == null) 
            {
                Console.WriteLine("status returned from Twilio send was null");
                return new SendStatus{Success=false, ErrorMessage="status returned from Twilio send was null"};
            }

            if (status.Status != "sent" && status.Status != "queued") 
            {
                var errorMsg = "Unable to send sms to user " + user.Name + ", status is " + status.Status + ", exception is " + status.RestException.Message;
                Console.WriteLine (errorMsg);
                return new SendStatus{Success=false, ErrorMessage=status.RestException.Message};
            } 
            else 
            {
                return new SendStatus{Success=true};
            }
        }
    }
}

