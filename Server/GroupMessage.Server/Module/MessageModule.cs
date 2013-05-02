using System.Linq;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;
using System.IO;
using MongoDB.Driver.Builders;

namespace GroupMessage.Server.Module
{
    public class MessageModule : ModuleBase
    {
        private readonly IMessageSender _sender;
        private readonly UserRepository _userRepository;


        public MessageModule (IMessageSender messageSender, UserRepository userRepository, MessageStatusRepository _messageStatusRepository) : base("groupmessage")
        {
            _sender = messageSender;
            _userRepository = userRepository;

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

                var users = _userRepository.Users.AsQueryable().ToList();
                foreach (var user in users) 
                {
                    _messageStatusRepository.Create(new MessageStatus{Message=message, User=user});
                }


                var statuses = _messageStatusRepository.Statuses.AsQueryable<MessageStatus>().Where(s => s.Message.Id == message.Id && s.Status.NumberOfTries == 0);
                foreach (var status in statuses) 
                {
                    var sendStatus = _sender.Send(status.User, status.Message.Text);
                    status.Status.NumberOfTries++;
                    status.Status.Success = sendStatus.Success;
                    status.Status.ErrorMessage = sendStatus.ErrorMessage;
                    _messageStatusRepository.Update(status);
                }

                return "";
            };
        }
    }
}
