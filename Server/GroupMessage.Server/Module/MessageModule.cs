using System.Linq;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using GroupMessage.Server.Service;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;
using System.IO;
using MongoDB.Driver.Builders;

namespace GroupMessage.Server.Module
{
    public class MessageModule : ModuleBase
    {
        public MessageModule (MessageService _messageService) : base("groupmessage")
        {
            Put ["/message/{idInUrl}"] = parameters =>
            {
                // TODO: Handle duplicate submissions of message with same id

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

                _messageService.initialSend(message);

                return "";
            };
        }
    }
}
