using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GroupMessage.Server.Model
{
    public enum DeviceOs
    {
        NotSet = 0,
        Android = 1,
        iOS = 2,
        WindowsPhone = 3,
        Windows8 = 4
    }

    public class User : EntityBase
    {
        private string _phoneNumber;

        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = NormalizePhoneNumber(value); } 
        }
        public string Email { get; set; }
        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(StringEnumConverter))]
        public DeviceOs DeviceOs { get; set; }
        public string DeviceOsVersion { get; set; }
        public string DeviceToken { get; set; }
        public DateTime LastUpdate { get; set; }

        public static string NormalizePhoneNumber(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber))
            {
                return "";
            }
            var tempPhoneNumber = Regex.Replace(phoneNumber, @"\s+", "");
            return tempPhoneNumber.Substring(tempPhoneNumber.Length - 8, 8);
        }
    }
}