using System;
using GroupMessage.Server.Communication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GroupMessage.Server.Model
{
    /// <summary>
    /// Represents the status for all of the sending transactions for one Message 
    /// </summary>
    public class MessageStatus : EntityBase
    {
        public User User { get; set; }
        public Message Message { get; set; }
        [BsonRepresentation(BsonType.String)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageSenderType Type { get; set; }
        public SendStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
        
        public MessageStatus()
        {
            Status = new SendStatus
                {
                    NumberOfTries = 0,
                    Success = false,
                    ErrorMessage = null
                };
        }
    }
}

