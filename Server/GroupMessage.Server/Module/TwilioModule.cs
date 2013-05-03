using System.Linq;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using GroupMessage.Server.Service;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;
using System.IO;
using MongoDB.Driver.Builders;
using System;
using Nancy;
using Nancy.Helpers;

namespace GroupMessage.Server.Module
{
    /// <summary>
    /// Receives requests from Twilio when group members responds to sms messages
    /// </summary>
    public class TwilioModule : ModuleBase
    {
        public TwilioModule(MessageService _messageService, UserRepository _userRepository) : base("twilio")
        {
            Post ["/sms"] = parameters =>
            {
                var bodyEntity = Request.Body.GetAsString ();
                var map = HttpUtility.ParseQueryString (bodyEntity);
                Console.WriteLine("Received from Twilio: " + bodyEntity);

                var smsBody = map["Body"];
                Console.WriteLine("Extracted message body: " + smsBody);

                var senderNumber = map["From"];

                var userQueryTemplate = new User{PhoneNumber=senderNumber}.Normalized(); // gets rid of the +45 in front of the Twilio sender number
                if (_userRepository.GetByPhoneNumber(userQueryTemplate.PhoneNumber) == null) {
                    var newUser = new User{PhoneNumber=senderNumber};
                    _userRepository.Create(newUser);
                }

                var status = map ["SmsStatus"];
                if (status == "received")
                {
                    var message = new Message{MessageId=Guid.NewGuid().ToString(), Text=smsBody };
                    _messageService.initialSend(message);
                }
                else
                {
                    Console.WriteLine("Ignoring unknown status '" + status + "'");
                }

                return new Response().SetContentType("application/xml").SetBody("<?xml version='1.0' encoding='UTF-8'?><Response></Response>"); // I don't know why, but Twilio requires a Response document in order to acknowledge the receive
            };
        }
    }
}
