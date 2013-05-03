using GroupMessage.Server.Model;
using MongoDB.Driver;

namespace GroupMessage.Server.Repository
{
    public interface IUserRepository : IEntityRepository<User>
    {
        MongoCollection<User> Users { get; }
        User GetByPhoneNumber(string phoneNumber);
        void DeleteByPhoneNumber(string phoneNumber);
    }
}