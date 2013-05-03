using System;
using GroupMessage.Server.Communication;
using GroupMessage.Server.Model;

namespace GroupMessage.Server
{
    /// <summary>
    /// Represents the status for all of the sending transactions for one Message 
    /// </summary>
    public class MessageStatus : EntityBase
    {
        public User User { get; set; }
        public Message Message { get; set; }
        public SendStatus Status { get; set; }
        public MessageSenderType Type { get; set; }

        public MessageStatus() 
        {
            Status = new SendStatus{NumberOfTries=0, Success=false, ErrorMessage=null};
        }
    }
}

