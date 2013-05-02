using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nancy.Json;

namespace GroupMessage.Server.Model
{
    public abstract class EntityBase : IEntity
    {
        [BsonId]
        [ScriptIgnore]
        public ObjectId Id { get; set; }
    }
}