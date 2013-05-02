using System;
using GroupMessage.Server.Data;
using GroupMessage.Server.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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

        public User GetByPhoneNumber(string phoneNumber)
        {
            var entityQuery = Query<User>.EQ(user => user.PhoneNumber, phoneNumber);
            return this.MongoDb.EntityCollection.FindOne(entityQuery);
        }

        public void DeleteByPhoneNumber(string phoneNumber)
        {
            var entityQuery = Query<User>.EQ(user => user.PhoneNumber, phoneNumber);
            this.MongoDb.EntityCollection.Remove(entityQuery);
        }
    }
}
