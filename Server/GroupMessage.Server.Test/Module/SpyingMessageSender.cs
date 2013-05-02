using System;
using System.Collections.Generic;
using GroupMessage.Server.Model;

namespace GroupMessage.Server.Test
{
    public class SpyingMessageSender: IMessageSender
    {
        public int NumberOfCalls {
            get { return Users.Count; }
        }

        public List<User> Users {
            get;
            private set;
        }

        public List<String> Texts {
            get;
            private set;
        }

        public SpyingMessageSender ()
        {
            Users = new List<User>();
            Texts = new List<String>();
        }

        void IMessageSender.Send (GroupMessage.Server.Model.User user, string text)
        {
            Users.Add(user);
            Texts.Add(text);
        }
    }
}

