namespace GroupMessage.Server.Model
{
    public class SendStatus : EntityBase
    {
        public int NumberOfTries {get; set;}
        public bool Success {get; set;}
        public string ErrorMessage {get; set;}
    }
}

