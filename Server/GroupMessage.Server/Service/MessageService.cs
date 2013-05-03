using GroupMessage.Server.Communication;
using GroupMessage.Server.Repository;
using GroupMessage.Server.Model;
using MongoDB.Driver.Linq;
using System.Linq;

namespace GroupMessage.Server.Service
{
    public class MessageService
    {
        private readonly TwilioMessageSender _twilioMessageSender;
        private readonly PushMessageSender _pushMessageSender;
        private readonly UserRepository _userRepository;

        private readonly MessageStatusRepository _messageStatusRepository;

        public MessageService (TwilioMessageSender twilioMessageSender, PushMessageSender pushMessageSender, UserRepository userRepository, MessageStatusRepository messageStatusRepository)
        {
            _twilioMessageSender = twilioMessageSender;
            _pushMessageSender = pushMessageSender;
            _userRepository = userRepository;

            _messageStatusRepository = messageStatusRepository;
        }

        public void initialSend(Message message) {
            var users = _userRepository.Users.AsQueryable().ToList();
            foreach (var user in users) 
            {
                _messageStatusRepository.Create(new MessageStatus{Message=message, User=user});
            }
            
            
            var statuses = _messageStatusRepository.Statuses.AsQueryable<MessageStatus>().Where(s => s.Message.Id == message.Id && s.Status.NumberOfTries == 0);
            foreach (var status in statuses) 
            {
                var sendStatus = _twilioMessageSender.Send(status.User, status.Message.Text);
                status.Status.NumberOfTries++;
                status.Status.Success = sendStatus.Success;
                status.Status.ErrorMessage = sendStatus.ErrorMessage;
                _messageStatusRepository.Update(status);
            }
        }
    }
}

