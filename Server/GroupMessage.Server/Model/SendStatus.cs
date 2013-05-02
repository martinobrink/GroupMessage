using System;

namespace GroupMessage.Server
{
    public class SendStatus
    {
        public int NumberOfTries {get; set;}
        public bool Success {get; set;}
        public string ErrorMessage {get; set;}
    }
}

