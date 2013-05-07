using GroupMessage.Server.Model;
using GroupMessage.Server.Service;
using Nancy;
using Nancy.ModelBinding;

namespace GroupMessage.Server.Module
{
    public class MessageModule : ModuleBase
    {
        public MessageModule(MessageService messageService) : base("groupmessage")
        {
            Put ["/message/{idInUrl}"] = parameters =>
            {
                // TODO: Handle duplicate submissions of message with same id

                var message = this.Bind<Message> (); 

                if (parameters ["idInUrl"] != message.MessageId) {
                    return new Response().Create(HttpStatusCode.BadRequest, "MessageId in body must match messageId in URL");
                }

                messageService.initialSend(message);

                return "";
            };
        }
    }
}
