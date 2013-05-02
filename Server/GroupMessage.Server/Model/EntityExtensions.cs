namespace GroupMessage.Server.Model
{
    public static class EntityExtensions
    {
        public static string AsJson<TEntity>(this TEntity entity) where TEntity : EntityBase
        {
            var serializer = new Nancy.Json.JavaScriptSerializer();
            return serializer.Serialize(entity);
        }
    }
}