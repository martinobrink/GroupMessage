using System;
using GroupMessage.Server.Data;
using GroupMessage.Server.Model;
using MongoDB.Driver;

namespace GroupMessage.Server.Repository
{
    public class UserRepository : EntityRepositoryBase<User>
    {
        public MongoCollection<User> Users { get { return this.MongoDb.EntityCollection; } }

        public UserRepository() : this(new MongoDbWrapper<User>())
        {
        }

        public UserRepository(IMongoDbWrapper<User> mongoDb) : base(mongoDb)
        {
        }

        public override void Update(User entity)
        {
            //todo
            throw new NotImplementedException();
        }
    }
}
