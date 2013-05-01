using MongoDB.Driver;
using GroupMessage.Server.Model;

namespace GroupMessage.Server.Data
{
    public class MongoDbWrapper<T> : IMongoDbWrapper<T> where T : IEntity
    {
        public MongoDatabase Database { get; private set; }
        public MongoCollection<T> EntityCollection {
            get
            {
                return Database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
            }
        }

        public MongoDbWrapper()
        {
            const string connectionString = "mongodb://localhost";
            const string databaseName = "groupmessage";
            var mongoClient = new MongoClient(connectionString);
            var mongoServer = mongoClient.GetServer();

            Database = mongoServer.GetDatabase(databaseName);
        }

        public MongoDbWrapper(MongoDatabase database)
        {
            Database = database;
        }
    }
}
