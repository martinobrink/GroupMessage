using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using GroupMessage.Server.Service;
using Nancy;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;
using System.Linq;

namespace GroupMessage.Server.Module
{
    public class MessageModule : ModuleBase
    {
        private readonly MessageService _messageService;
        private readonly MessageStatusRepository _messageStatusRepository;

        public MessageModule(MessageService messageService, MessageStatusRepository messageStatusRepository) : base("groupmessage")
        {
            _messageService = messageService;
            _messageStatusRepository = messageStatusRepository;

            Put ["/message/{idInUrl}"] = parameters =>
            {
                var message = this.Bind<Message> (); 

                if (parameters["idInUrl"] != message.MessageId) {
                    return new Response().Create(HttpStatusCode.BadRequest, "MessageId in body must match messageId in URL.");
                }

                if (MessageIdHasBeenUsedPreviously(message))
                {
                    return new Response().Create(HttpStatusCode.BadRequest, "MessageId has already been used.");
                }

                _messageService.Send(message);

                return HttpStatusCode.OK;
            };
        }

        private bool MessageIdHasBeenUsedPreviously(Message message)
        {
            var messageStatuses = _messageStatusRepository.Statuses.AsQueryable().ToList();
            return messageStatuses.Any(status => status.Message.MessageId == message.MessageId);
        }
    }
}
