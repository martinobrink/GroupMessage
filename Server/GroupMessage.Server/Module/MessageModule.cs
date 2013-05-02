using System;
using System.Text;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;

namespace GroupMessage.Server.Module
{
    public class MessageModule : ModuleBase
    {
        public MessageModule() : base("groupmessage")
        {
			Put["/message/{messageId}"] = parameters =>
                {
					return "";
                };
        }
    }
}
