using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace GroupMessage.Server.Model
{
    public abstract class EntityBase : IEntity
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId Id { get; set; }
    }
}