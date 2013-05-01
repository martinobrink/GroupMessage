using GroupMessage.Server.Model;
using MongoDB.Driver;

namespace GroupMessage.Server.Data
{
    public interface IMongoDbWrapper<T> where T : IEntity
    {
        MongoCollection<T> EntityCollection { get; }
        MongoDatabase Database { get; }
    }
}