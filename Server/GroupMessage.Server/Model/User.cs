namespace GroupMessage.Server.Model
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //[BsonRepresentation(BsonType.String)]
        //public DeviceOs DeviceOs { get; set; }
        //public String DeviceToken { get; set; }
    }
}