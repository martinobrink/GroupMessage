using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using GroupMessage.Server.Model;
using Newtonsoft.Json;

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
            string jsonData = @"{ ""registration_ids"": ["""+user.DeviceToken+"\"], \"data\":  { \"Message\": \""+text+"\", \"Group\": \"All\" } }";
            var httpClient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://android.googleapis.com/gcm/send");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("key", string.Format("={0}", _googleApiKey));
            requestMessage.Content = new StringContent(jsonData);
            requestMessage.Content.Headers.ContentType.MediaType = "application/json";
            
            var responseMessage = httpClient.SendAsync(requestMessage).Result;
            var result = responseMessage.Content.ReadAsStringAsync().Result;
            var pushNotificationResult = JsonConvert.DeserializeObject<GooglePushNotificationResult>(result);
            
            return new SendStatus
                {
                    Success = pushNotificationResult.success, 
                    ErrorMessage = pushNotificationResult.results[0].error
                };
        }
    }
}