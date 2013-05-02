using System;
using System.Text;
using System.Linq;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using Nancy.ModelBinding;
using Nancy.Responses;
using MongoDB.Driver.Linq;
using System.IO;

namespace GroupMessage.Server.Module
{
    public class MessageModule : ModuleBase
    {
        private readonly IMessageSender _sender;
        private readonly UserRepository _userRepository;


        public MessageModule (IMessageSender messageSender, UserRepository userRepository) : base("groupmessage")
        {
            _sender = messageSender;
            _userRepository = userRepository;

            Put ["/message/{idInUrl}"] = parameters =>
            {
                var message = this.Bind<Message> (); 

                if (parameters ["idInUrl"] != message.MessageId) {
                    return new Nancy.Response {
                        ContentType = "text/plain",
                        StatusCode = Nancy.HttpStatusCode.BadRequest,
                        Contents = s =>  {
                            var writer = new StreamWriter (s);
                            writer.Write ("messageId in body must match messageId in URL");
                            writer.Flush ();
                        }
                    };
                }

                var users = _userRepository.Users.AsQueryable().ToList();
                foreach (var user in users) 
                {
                    _sender.Send(user, message.Text);
                }

                return "";
            };
        }
    }
}
