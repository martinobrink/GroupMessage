using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GroupMessage.Server.Model
{
    public abstract class EntityBase : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}