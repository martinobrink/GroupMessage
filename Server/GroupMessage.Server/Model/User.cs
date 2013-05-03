using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;

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
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [BsonRepresentation(BsonType.String)]
        public DeviceOs DeviceOs { get; set; }
        public string DeviceOsVersion { get; set; }
        public string DeviceToken { get; set; }
        public DateTime LastUpdate { get; set; }

        public User Normalized() 
        {
            NormalizePhoneNumber();
            return this;
        }

        private void NormalizePhoneNumber()
        {
            PhoneNumber = Regex.Replace (PhoneNumber, @"\s+", "");
            PhoneNumber = PhoneNumber.Substring (PhoneNumber.Length - 8, 8);
        }
    }
}