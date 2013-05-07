using GroupMessage.Server.Communication;
using GroupMessage.Server.Repository;
using GroupMessage.Server.Model;
using MongoDB.Driver.Linq;
using System.Linq;

namespace GroupMessage.Server.Service
{
    public class MessageService
    {
        private readonly IMessageSenderFactory _messageSenderFactory;
        private readonly UserRepository _userRepository;

        private readonly MessageStatusRepository _messageStatusRepository;

        public MessageService (IMessageSenderFactory messageSenderFactory, UserRepository userRepository, MessageStatusRepository messageStatusRepository)
        {
            _messageSenderFactory = messageSenderFactory;
            _userRepository = userRepository;

            _messageStatusRepository = messageStatusRepository;
        }

        public void initialSend(Message message) {
            var users = _userRepository.Users.AsQueryable().ToList();
            var messageSenders = _messageSenderFactory.GetMessageSenders();

            foreach (var user in users)
            {
                foreach (var messageSender in messageSenders)
                {
                    _messageStatusRepository.Create(new MessageStatus
                        {
                            Type = messageSender.SenderType,
                            Message = message,
                            User = user
                        });
                }
            }

            var messageStatuses = _messageStatusRepository.Statuses.AsQueryable<MessageStatus>().Where(s => s.Message.Id == message.Id && s.Status.NumberOfTries == 0);
            foreach (var messageStatus in messageStatuses) 
            {
                var messageSender = messageSenders.SingleOrDefault(sender => sender.SenderType == messageStatus.Type);
                SendStatus sendStatus;
                if (messageSender != null)
                {
                    sendStatus = messageSender.Send(messageStatus.User, messageStatus.Message.Text);
                }
                else
                {
                    sendStatus = new SendStatus { Success = true, ErrorMessage = "Unable to find MessageSender of correct type. Will not resend." };
                }

                sendStatus.NumberOfTries = messageStatus.Status.NumberOfTries + 1;
                messageStatus.Status = sendStatus;
                _messageStatusRepository.Update(messageStatus);
            }
        }
    }
}

