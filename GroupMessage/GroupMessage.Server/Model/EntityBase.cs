using MongoDB.Bson;

namespace GroupMessage.Server.Model
{
    //todo should contain entity id
    public class EntityBase
    {
        public ObjectId Id { get; set; }
    }
}