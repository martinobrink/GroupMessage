using GroupMessage.Server.Data;
using GroupMessage.Server.Model;
using GroupMessage.Server.Module;
using MongoDB.Driver;
using NUnit.Framework;
using Nancy.Testing;

namespace GroupMessage.Server.Test
{
    public abstract class NancyIntegrationTestBase<TEntity> where TEntity : IEntity
    {
        protected MongoDbWrapper<TEntity> Db;
        protected Browser Browser;

        protected virtual void OnFixtureSetup()
        {
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var mongoClient = new MongoClient();
            var mongoServer = mongoClient.GetServer();
            mongoServer.DropDatabase("IntegrationTest");
            var database = mongoServer.GetDatabase("IntegrationTest");
            var bootstrapper = new ConfigurableBootstrapper(with =>
                {
                    with.Dependency<MongoDatabase>(database);
                    with.Dependency<IMongoDbWrapper<User>>(new MongoDbWrapper<User>(database));
                    
                    with.Module<UserModule>();
					with.Module<MessageModule>();
			});
            
            Browser = new Browser(bootstrapper);
            Db = new MongoDbWrapper<TEntity>(database);

            OnFixtureSetup();
        }
    }
}
