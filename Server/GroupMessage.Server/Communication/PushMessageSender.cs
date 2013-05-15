using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GroupMessage.Server.Model;
using Newtonsoft.Json;
using RestSharp;

namespace GroupMessage.Server.Communication
{
    public class PushMessageSender : IMessageSender
    {
        private string _googleApiKey;
        private string _googleSenderId;
        private string _googlePackageName;

        public MessageSenderType SenderType
        {
            get { return MessageSenderType.PushNotification; }
        }

        public PushMessageSender()
        {
            List<string> googlePushConfigLines;
            try
            {
                googlePushConfigLines = File.ReadLines("GooglePushNotifications.txt").ToList();
            }
            catch (Exception exception)
            {
                var errorMessage = string.Format("Something failed during reading of file GooglePushNotifications.txt! Did you forget to add a GooglePushNotifications.txt to the same directory as the application? Exception: {0}", exception.Message);
                Console.WriteLine(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            _googleApiKey = googlePushConfigLines[0].Split('=')[1];
            _googleSenderId = googlePushConfigLines[1].Split('=')[1];
            _googlePackageName = googlePushConfigLines[2].Split('=')[1];
        }

        public SendStatus Send(User user, string text)
        {
            switch (user.DeviceOs)
            {
                case DeviceOs.Android:
                    {
                        return SendGooglePushNotification(user, text);
                    }
                case DeviceOs.iOS:
                    {
                        return new SendStatus { Success = false, ErrorMessage = "Devices connected via iOS not supported yet..." };
                    }
                case DeviceOs.WindowsPhone:
                    {
                        return new SendStatus { Success = false, ErrorMessage = "Devices connected via Windows Phone not supported yet..." };
                    }
                case DeviceOs.Windows8:
                    {
                        return new SendStatus { Success = false, ErrorMessage = "Devices connected via Windows 8 not supported yet..." };
                    }
                case DeviceOs.NotSet:
                    {
                        return new SendStatus {Success = false, ErrorMessage = "No device connected via app"};
                    }
            }

            return new SendStatus { Success = false, ErrorMessage = string.Format("Unknown value '{0}' of user.DeviceOs. Unable to send message via push notification.", user.DeviceOs.ToString()) };
        }

        private SendStatus SendGooglePushNotification(User user, string text)
        {
            var client = new RestClient("https://android.googleapis.com");
            var request = new RestRequest("gcm/send", Method.POST);
            var jsonData = @"{ ""registration_ids"": [""" + user.DeviceToken + "\"], \"data\":  { \"Message\": \"" + text + "\", \"Group\": \"All\" } }";
            request.AddParameter("application/json", jsonData, ParameterType.RequestBody);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", string.Format("key={0}", _googleApiKey));
            var response = client.Execute(request);
            var responseContent = response.Content;
            var pushNotificationResult = JsonConvert.DeserializeObject<GooglePushNotificationResult>(responseContent);
            
            return new SendStatus
                {
                    Success = pushNotificationResult.success,
                    ErrorMessage = pushNotificationResult.results[0].error
                };
        }
    }
}