using System;
using GroupMessage.Server.Data;
using GroupMessage.Server.Model;
using MongoDB.Driver;

namespace GroupMessage.Server.Repository
{
    public class MessageStatusRepository : EntityRepositoryBase<MessageStatus>
    {
        public MongoCollection<MessageStatus> Statuses { get { return this.MongoDb.EntityCollection; } }

        public MessageStatusRepository() : this(new MongoDbWrapper<MessageStatus>())
        {
        }

        public MessageStatusRepository(IMongoDbWrapper<MessageStatus> mongoDb) : base(mongoDb)
        {
        }

        public override void Update (MessageStatus entity)
        {
            var result = this.MongoDb.EntityCollection.Save(
                entity,
                new MongoInsertOptions { WriteConcern = WriteConcern.Acknowledged });
            
            if (!result.Ok)
            {
                //// Something went wrong
            }
        }
    }
}
