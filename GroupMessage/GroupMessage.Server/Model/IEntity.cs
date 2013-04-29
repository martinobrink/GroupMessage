using MongoDB.Bson;

namespace GroupMessage.Server.Model
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}