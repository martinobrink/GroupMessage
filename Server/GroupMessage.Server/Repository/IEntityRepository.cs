using GroupMessage.Server.Model;

namespace GroupMessage.Server.Repository
{
    public interface IEntityRepository<T> where T : IEntity
    {
        void Create(T entity);

        void Delete(string id);

        T GetById(string id);

        void Update(T entity);
    }
}
