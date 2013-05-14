using GroupMessage.Server.Model;

namespace GroupMessage.Server
{
    public class SendStatus : EntityBase
    {
        public int NumberOfTries {get; set;}
        public bool Success {get; set;}
        public string ErrorMessage {get; set;}
    }
}

