using GroupMessage.Server.Model;
using MongoDB.Driver;

namespace GroupMessage.Server.Repository
{
    public interface IUserRepository : IEntityRepository<User>
    {
        MongoCollection<User> Users { get; }
        /// <summary>
        /// <returns>The user matching the supplied phoneNumber, null if no such user exists</returns>
        User GetByPhoneNumber(string phoneNumber);
        void DeleteByPhoneNumber(string phoneNumber);
    }
}