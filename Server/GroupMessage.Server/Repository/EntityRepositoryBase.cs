using GroupMessage.Server.Data;
using GroupMessage.Server.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace GroupMessage.Server.Repository
{
    public abstract class EntityRepositoryBase<T> : IEntityRepository<T> where T : IEntity
    {
        protected readonly IMongoDbWrapper<T> MongoDb;

        protected EntityRepositoryBase(IMongoDbWrapper<T> mongoDb )
        {
            MongoDb = mongoDb;
        }

        public virtual void Create(T entity)
        {
            //// Save the entity with safe mode (WriteConcern.Acknowledged)
            var result = this.MongoDb.EntityCollection.Save(
                entity,
                new MongoInsertOptions { WriteConcern = WriteConcern.Acknowledged });

            if (!result.Ok)
            {
                //// Something went wrong
            }
        }

        public virtual void Delete(string id)
        {
            var result = this.MongoDb.EntityCollection.Remove(
                Query<T>.EQ(entity => entity.Id, new ObjectId(id)),
                RemoveFlags.None,
                WriteConcern.Acknowledged);

            if (!result.Ok)
            {
                //// Something went wrong
            }
        }
        
        public virtual T GetById(string id)
        {
            var entityQuery = Query<T>.EQ(e => e.Id, new ObjectId(id));
            return this.MongoDb.EntityCollection.FindOne(entityQuery);
        }

        public abstract void Update(T entity);
    }
}
