using System.Collections.Generic;
using FakeItEasy;
using GroupMessage.Server.Communication;
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
        protected IMessageSenderFactory MessageSenderFactoryFake;

        protected virtual void OnSetup()
        {
        }

        protected virtual void OnConfigureBootstrapper(Nancy.Testing.ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator configurator)
        {
        }

        [SetUp]
        public void Setup()
        {
            var mongoClient = new MongoClient();
            var mongoServer = mongoClient.GetServer();
            mongoServer.DropDatabase("IntegrationTest");
            var database = mongoServer.GetDatabase("IntegrationTest");
            MessageSenderFactoryFake = A.Fake<IMessageSenderFactory>();
            A.CallTo(() => MessageSenderFactoryFake.GetMessageSenders()).Returns(new List<IMessageSender>());
            
            var bootstrapper = new ConfigurableBootstrapper(with =>
                {
                    with.Dependency<MongoDatabase>(database);
                    with.Dependency<IMongoDbWrapper<User>>(new MongoDbWrapper<User>(database));
                    with.Dependency<IMongoDbWrapper<MessageStatus>>(new MongoDbWrapper<MessageStatus>(database));
                    with.Dependency<IMessageSenderFactory>(MessageSenderFactoryFake);

                    with.Module<UserModule>();
					with.Module<MessageModule>();
                    with.Module<TwilioModule>();

                    OnConfigureBootstrapper(with);
			});


            Browser = new Browser(bootstrapper);
            Db = new MongoDbWrapper<TEntity>(database);

            OnSetup();
        }
    }
}
