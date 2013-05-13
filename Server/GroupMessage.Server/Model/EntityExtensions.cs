using Newtonsoft.Json;

namespace GroupMessage.Server.Model
{
    public static class EntityExtensions
    {
        public static string AsJson<TEntity>(this TEntity entity) where TEntity : EntityBase
        {
            return JsonConvert.SerializeObject(entity);
        }
    }
}