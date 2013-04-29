namespace GroupMessage.Server.Model
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        //[BsonRepresentation(BsonType.String)]
        //public DeviceOs DeviceOs { get; set; }
    }
}