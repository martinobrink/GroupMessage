using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GroupMessage.Server.Model
{
    public enum DeviceOs
    {
        Android = 0,
        iOS = 1,
        WindowsPhone = 2,
        Windows8 = 3
    }

    public class User : EntityBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [BsonRepresentation(BsonType.String)]
        public DeviceOs DeviceOs { get; set; }
        public string DeviceToken { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}